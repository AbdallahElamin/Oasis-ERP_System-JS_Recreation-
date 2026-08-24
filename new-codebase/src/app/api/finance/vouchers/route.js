import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

// ── GET — list journal vouchers grouped by MoveNo ─────────────────
export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const year = parseInt(searchParams.get("year") || String(new Date().getFullYear()), 10);

  const rows = await pool.query(
    `SELECT MoveNo,
            MIN(TransDate) AS TransDate,
            MIN(employee)  AS employee,
            SUM(TotalValueIn)  AS totalCredit,
            SUM(TotalValueOut) AS totalDebit,
            COUNT(*)           AS lineCount
     FROM Transactions
     WHERE TransType = 'Journal Voucher'
       AND YEAR(TransDate) = ?
     GROUP BY MoveNo
     ORDER BY MoveNo DESC`,
    [year]
  );

  return NextResponse.json(rows);
}

// ── POST — create a balanced journal voucher ──────────────────────
export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { date, lines } = body;

  if (!lines?.length) {
    return NextResponse.json({ error: "At least one line required" }, { status: 400 });
  }

  // Validate balance: total debit must equal total credit
  const totalDebit = lines.reduce((s, l) => s + (parseFloat(l.debit) || 0), 0);
  const totalCredit = lines.reduce((s, l) => s + (parseFloat(l.credit) || 0), 0);
  if (Math.abs(totalDebit - totalCredit) > 0.001) {
    return NextResponse.json(
      { error: `Voucher is not balanced. Debit: ${totalDebit.toFixed(2)}, Credit: ${totalCredit.toFixed(2)}` },
      { status: 400 }
    );
  }

  for (const line of lines) {
    if (!line.acc1?.trim() || !line.acc2?.trim() || !line.acc3?.trim() || !line.acc4?.trim()) {
      return NextResponse.json({ error: "All 4 account levels required for each line" }, { status: 400 });
    }
  }

  const transDate = date ? new Date(date) : new Date();

  const conn = await pool.getConnection();
  try {
    await conn.beginTransaction();

    // Get next MoveNo for the year
    const yr = transDate.getFullYear();
    const [{ lastNo }] = await conn.query(
      "SELECT COALESCE(MAX(MoveNo), 0) AS lastNo FROM Transactions WHERE YEAR(TransDate) = ?",
      [yr]
    );
    const moveNo = Number(lastNo) + 1;

    for (const line of lines) {
      await conn.query(
        `INSERT INTO Transactions
           (MoveNo, Acc1, Acc2, Acc3, Acc4, Ref, TotalValueIn, TotalValueOut, TransType, employee, TransDate)
         VALUES (?, ?, ?, ?, ?, ?, ?, ?, 'Journal Voucher', ?, ?)`,
        [
          moveNo,
          line.acc1.trim(),
          line.acc2.trim(),
          line.acc3.trim(),
          line.acc4.trim(),
          line.description?.trim() || null,
          parseFloat(line.credit) || 0,
          parseFloat(line.debit) || 0,
          session.user.name,
          transDate,
        ]
      );
    }

    await conn.commit();
    return NextResponse.json({ moveNo }, { status: 201 });
  } catch (err) {
    await conn.rollback();
    console.error("Voucher creation error:", err);
    return NextResponse.json({ error: "Failed to save voucher" }, { status: 500 });
  } finally {
    conn.release();
  }
}
