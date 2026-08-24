import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

/**
 * GET /api/finance/statements
 * Query params: from, to, acc1, acc2?, acc3?, acc4?
 *
 * Returns:
 *   { openingBalance: number, rows: [...transaction rows] }
 */
export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const acc1 = searchParams.get("acc1");
  const acc2 = searchParams.get("acc2") || null;
  const acc3 = searchParams.get("acc3") || null;
  const acc4 = searchParams.get("acc4") || null;

  if (!acc1) return NextResponse.json({ error: "acc1 required" }, { status: 400 });

  const year = new Date().getFullYear();
  const from = searchParams.get("from") || `${year}-01-01`;
  const to = searchParams.get("to") || `${year}-12-31`;

  const fromDate = `${from} 00:00:00`;
  const toDate = `${to} 23:59:59`;

  // Build dynamic WHERE clause based on which account levels provided
  let accWhere = "Acc1 = ?";
  const accParams = [acc1];
  if (acc2) { accWhere += " AND Acc2 = ?"; accParams.push(acc2); }
  if (acc3) { accWhere += " AND Acc3 = ?"; accParams.push(acc3); }
  if (acc4) { accWhere += " AND Acc4 = ?"; accParams.push(acc4); }

  // Opening balance — sum of all transactions BEFORE the from date
  const obRows = await pool.query(
    `SELECT COALESCE(
       (SUM(TotalOut) + SUM(TotalValueOut)) - (SUM(TotalIn) + SUM(TotalValueIn)),
       0
     ) AS openingBalance
     FROM Transactions
     WHERE ${accWhere} AND TransDate < ?`,
    [...accParams, fromDate]
  );
  const openingBalance = Number(obRows[0].openingBalance);

  // Period transactions
  const txRows = await pool.query(
    `SELECT SNo, MoveNo, Ref, TransType, PaymentType, Acc1, Acc2, Acc3, Acc4,
            TotalIn + TotalValueIn   AS credit,
            TotalOut + TotalValueOut AS debit,
            TransDate, employee
     FROM Transactions
     WHERE ${accWhere}
       AND TransDate >= ? AND TransDate <= ?
     ORDER BY TransDate ASC, SNo ASC`,
    [...accParams, fromDate, toDate]
  );

  return NextResponse.json({ openingBalance, rows: txRows });
}
