import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

/** GET /api/finance/trial-balance?from=YYYY-MM-DD&to=YYYY-MM-DD */
export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const year = new Date().getFullYear();
  const from = searchParams.get("from") || `${year}-01-01`;
  const to = searchParams.get("to") || `${year}-12-31`;

  const rows = await pool.query(
    `SELECT Acc1, Acc2, Acc3, Acc4,
            SUM(TotalIn)  + SUM(TotalValueIn)  AS totalIn,
            SUM(TotalOut) + SUM(TotalValueOut) AS totalOut,
            (SUM(TotalIn) + SUM(TotalValueIn)) - (SUM(TotalOut) + SUM(TotalValueOut)) AS balance
     FROM Transactions
     WHERE TransDate >= ? AND TransDate <= ?
     GROUP BY Acc1, Acc2, Acc3, Acc4
     ORDER BY Acc1, Acc2, Acc3, Acc4`,
    [`${from} 00:00:00`, `${to} 23:59:59`]
  );

  return NextResponse.json(rows);
}
