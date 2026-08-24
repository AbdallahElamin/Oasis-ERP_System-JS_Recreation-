import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

/**
 * POST /api/finance/bills/receipt
 *
 * Create a Receipt Voucher (incoming payment).
 * Body: {
 *   date, source, description, amountInWords,
 *   paymentType: "cash" | "bank",
 *   chequeNo, bankName, chequeDate,
 *   lines: [{ acc1, acc2, acc3, acc4, amount }]
 * }
 *
 * Journal entries:
 *   - Debit side (each income line):   TotalIn = amount  (money coming in)
 *   - Credit side (cash/bank account): TotalOut = total  (account credited)
 */
export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const {
    date,
    source,
    description,
    amountInWords,
    paymentType,
    chequeNo,
    bankName,
    chequeDate,
    lines,
  } = body;

  if (!source?.trim())
    return NextResponse.json({ error: "Source is required" }, { status: 400 });
  if (!lines?.length)
    return NextResponse.json({ error: "At least one income line required" }, { status: 400 });
  if (paymentType === "bank" && !bankName?.trim())
    return NextResponse.json({ error: "Bank name required for bank receipt" }, { status: 400 });

  const totalAmount = lines.reduce((s, l) => s + (parseFloat(l.amount) || 0), 0);
  if (totalAmount <= 0)
    return NextResponse.json({ error: "Total amount must be greater than 0" }, { status: 400 });

  const transDate = date ? new Date(date) : new Date();
  const pmtType = paymentType === "bank" ? "B" : "C";

  const conn = await pool.getConnection();
  try {
    await conn.beginTransaction();

    const yr = transDate.getFullYear();
    const [{ lastMove }] = await conn.query(
      "SELECT COALESCE(MAX(MoveNo), 0) AS lastMove FROM Transactions WHERE YEAR(TransDate) = ?",
      [yr]
    );
    const moveNo = Number(lastMove) + 1;

    const [{ lastPaper }] = await conn.query(
      "SELECT COALESCE(MAX(PaperNo), 0) AS lastPaper FROM Transactions WHERE TransType = 'Receipt Voucher' AND PaymentType = ?",
      [pmtType]
    );
    const paperNo = Number(lastPaper) + 1;

    // Debit side — each income/asset line
    for (const line of lines) {
      await conn.query(
        `INSERT INTO Transactions
           (MoveNo, TransType, PaymentType, PaperNo, Source, Ref,
            Acc1, Acc2, Acc3, Acc4, Package, Writting, TotalIn, employee, TransDate)
         VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`,
        [
          moveNo, "Receipt Voucher", pmtType, paperNo,
          source.trim(), description?.trim() || null,
          line.acc1, line.acc2, line.acc3, line.acc4,
          chequeNo?.trim() || null,
          amountInWords?.trim() || null,
          parseFloat(line.amount) || 0,
          session.user.name, transDate,
        ]
      );
    }

    // Credit side — cash or bank account
    const acc3 = pmtType === "C" ? "Cash" : "Bank Accounts";
    const acc4 = pmtType === "C" ? "Cash on Hand" : bankName.trim();
    const chqDate = pmtType === "B" && chequeDate ? new Date(chequeDate) : null;

    await conn.query(
      `INSERT INTO Transactions
         (MoveNo, TransType, PaymentType, PaperNo, Source, Ref,
          Acc1, Acc2, Acc3, Acc4, Package, CheqDate, Writting, TotalOut, employee, TransDate)
       VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`,
      [
        moveNo, "Receipt Voucher", pmtType, paperNo,
        source.trim(), description?.trim() || null,
        "Current Assets", "Cash & Banks", acc3, acc4,
        chequeNo?.trim() || null,
        chqDate,
        amountInWords?.trim() || null,
        totalAmount,
        session.user.name, transDate,
      ]
    );

    await conn.commit();
    return NextResponse.json({ moveNo, paperNo }, { status: 201 });
  } catch (err) {
    await conn.rollback();
    console.error("Receipt voucher error:", err);
    return NextResponse.json({ error: "Failed to save receipt voucher" }, { status: 500 });
  } finally {
    conn.release();
  }
}
