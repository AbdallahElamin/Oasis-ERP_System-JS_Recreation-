import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { moveNo, year } = await request.json();
  if (!moveNo) return NextResponse.json({ error: "moveNo required" }, { status: 400 });

  const conn = await pool.getConnection();
  try {
    await conn.beginTransaction();

    // Fetch original lines
    const originals = await conn.query(
      "SELECT * FROM Transactions WHERE MoveNo = ? AND YEAR(TransDate) = ?",
      [moveNo, year || new Date().getFullYear()]
    );

    if (!originals.length) {
      await conn.rollback();
      return NextResponse.json({ error: "Voucher not found" }, { status: 404 });
    }

    // Check not already reversed
    if (originals.some((r) => r.Reversed)) {
      await conn.rollback();
      return NextResponse.json({ error: "Voucher is already reversed" }, { status: 409 });
    }

    // Get next MoveNo
    const [{ lastNo }] = await conn.query(
      "SELECT COALESCE(MAX(MoveNo), 0) AS lastNo FROM Transactions WHERE YEAR(TransDate) = YEAR(CURDATE())"
    );
    const newMoveNo = Number(lastNo) + 1;
    const now = new Date().toISOString().slice(0, 19).replace("T", " ");

    // Insert reversed lines (swap TotalIn/TotalOut and TotalValueIn/TotalValueOut)
    for (const line of originals) {
      await conn.query(
        `INSERT INTO Transactions
          (MoveNo, CustID, CustName, Acc1, Acc2, Acc3, Acc4, Ref,
           TotalIn, TotalOut, TotalValueIn, TotalValueOut,
           TransType, PaymentType, Source, Writting, employee, TransDate)
         VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`,
        [
          newMoveNo,
          line.CustID || null,
          line.CustName || null,
          line.Acc1 || null, line.Acc2 || null, line.Acc3 || null, line.Acc4 || null,
          `Reversing voucher #${moveNo}`,
          // Swap TotalIn ↔ TotalOut
          Number(line.TotalOut) || 0,
          Number(line.TotalIn) || 0,
          // Swap TotalValueIn ↔ TotalValueOut
          Number(line.TotalValueOut) || 0,
          Number(line.TotalValueIn) || 0,
          line.TransType || "Journal Voucher",
          line.PaymentType || null,
          line.Source || null,
          line.Writting || null,
          session.user?.name || "system",
          now,
        ]
      );
    }

    // Mark original as reversed
    await conn.query("UPDATE Transactions SET Reversed = 1 WHERE MoveNo = ? AND YEAR(TransDate) = ?",
      [moveNo, year || new Date().getFullYear()]);

    await conn.commit();
    return NextResponse.json({ success: true, newMoveNo }, { status: 201 });
  } catch (err) {
    await conn.rollback();
    console.error("Reversal error:", err);
    return NextResponse.json({ error: "Reversal failed" }, { status: 500 });
  } finally {
    conn.release();
  }
}
