import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

/**
 * POST /api/finance/bills/pay
 *
 * Create a Pay Voucher (outgoing payment).
 * Body: {
 *   date, source, description, amountInWords,
 *   paymentType: "cash" | "bank",
 *   chequeNo, bankName, chequeDate,
 *   lines: [{ acc1, acc2, acc3, acc4, amount }]
 * }
 *
 * Journal entries created:
 *   - Credit side (each expense line): TotalOut = amount
 *   - Debit side (cash/bank account):  TotalIn  = total amount
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
    return NextResponse.json({ error: "At least one expense line required" }, { status: 400 });
  if (paymentType === "bank" && !bankName?.trim())
    return NextResponse.json({ error: "Bank name required for bank payment" }, { status: 400 });

  const totalAmount = lines.reduce((s, l) => s + (parseFloat(l.amount) || 0), 0);
  if (totalAmount <= 0)
    return NextResponse.json({ error: "Total amount must be greater than 0" }, { status: 400 });

  const transDate = date ? new Date(date) : new Date();
  const pmtType = paymentType === "bank" ? "B" : "C";

  const conn = await pool.getConnection();
  try {
    await conn.beginTransaction();

    // Next MoveNo for the year
    const yr = transDate.getFullYear();
    const [{ lastMove }] = await conn.query(
      "SELECT COALESCE(MAX(MoveNo), 0) AS lastMove FROM Transactions WHERE YEAR(TransDate) = ?",
      [yr]
    );
    const moveNo = Number(lastMove) + 1;

    // Next PaperNo (bill serial) for Pay Vouchers of this payment type
    const [{ lastPaper }] = await conn.query(
      "SELECT COALESCE(MAX(PaperNo), 0) AS lastPaper FROM Transactions WHERE TransType = 'Pay Voucher' AND PaymentType = ?",
      [pmtType]
    );
    const paperNo = Number(lastPaper) + 1;

    // Credit side — each expense line
    for (const line of lines) {
      await conn.query(
        `INSERT INTO Transactions
           (MoveNo, TransType, PaymentType, PaperNo, Source, Ref,
            Acc1, Acc2, Acc3, Acc4, Package, Writting, TotalOut, employee, TransDate)
         VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`,
        [
          moveNo, "Pay Voucher", pmtType, paperNo,
          source.trim(), description?.trim() || null,
          line.acc1, line.acc2, line.acc3, line.acc4,
          chequeNo?.trim() || null,
          amountInWords?.trim() || null,
          parseFloat(line.amount) || 0,
          session.user.name, transDate,
        ]
      );
    }

    // Debit side — cash or bank account
    const acc3 = pmtType === "C" ? "Cash" : "Bank Accounts";
    const acc4 = pmtType === "C" ? "Cash on Hand" : bankName.trim();
    const chqDate = pmtType === "B" && chequeDate ? new Date(chequeDate) : null;

    await conn.query(
      `INSERT INTO Transactions
         (MoveNo, TransType, PaymentType, PaperNo, Source, Ref,
          Acc1, Acc2, Acc3, Acc4, Package, CheqDate, Writting, TotalIn, employee, TransDate)
       VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`,
      [
        moveNo, "Pay Voucher", pmtType, paperNo,
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
    console.error("Pay voucher error:", err);
    return NextResponse.json({ error: "Failed to save pay voucher" }, { status: 500 });
  } finally {
    conn.release();
  }
}
