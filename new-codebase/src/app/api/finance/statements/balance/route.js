import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

/** GET /api/finance/statements/balance?acc1=&acc2=&acc3=&acc4= */
export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const acc1 = searchParams.get("acc1");
  const acc2 = searchParams.get("acc2");
  const acc3 = searchParams.get("acc3");
  const acc4 = searchParams.get("acc4");

  if (!acc1) return NextResponse.json({ error: "acc1 required" }, { status: 400 });

  let sql = "SELECT COALESCE(SUM(TotalIn) - SUM(TotalOut), 0) AS balance FROM Transactions WHERE Acc1 = ?";
  const params = [acc1];

  if (acc2) { sql += " AND Acc2 = ?"; params.push(acc2); }
  if (acc3) { sql += " AND Acc3 = ?"; params.push(acc3); }
  if (acc4) { sql += " AND Acc4 = ?"; params.push(acc4); }

  const rows = await pool.query(sql, params);
  return NextResponse.json({ balance: Number(rows[0].balance) });
}
