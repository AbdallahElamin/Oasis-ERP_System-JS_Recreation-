import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

// GET — list pending (unapproved) vouchers
export async function GET() {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const rows = await pool.query(
    `SELECT DISTINCT MoveNo,
            MIN(TransDate) AS TransDate,
            MIN(employee)  AS employee,
            MIN(TransType) AS TransType,
            SUM(TotalValueIn)  AS totalCredit,
            SUM(TotalValueOut) AS totalDebit
     FROM Transactions
     WHERE Approved = 0
     GROUP BY MoveNo
     ORDER BY MoveNo DESC`
  );
  return NextResponse.json(rows);
}

// POST — approve a voucher
export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { moveNo } = await request.json();
  if (!moveNo) return NextResponse.json({ error: "moveNo required" }, { status: 400 });

  await pool.query("UPDATE Transactions SET Approved = 1 WHERE MoveNo = ?", [moveNo]);
  return NextResponse.json({ success: true });
}

// DELETE — delete all lines of a pending voucher
export async function DELETE(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  if (session.user?.role !== "admin") return NextResponse.json({ error: "Forbidden" }, { status: 403 });

  const { moveNo } = await request.json();
  if (!moveNo) return NextResponse.json({ error: "moveNo required" }, { status: 400 });

  await pool.query("DELETE FROM Transactions WHERE MoveNo = ? AND Approved = 0", [moveNo]);
  return NextResponse.json({ success: true });
}
