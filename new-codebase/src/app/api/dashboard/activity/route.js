import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET() {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const rows = await pool.query(
    `SELECT SNo, MoveNo, TransType, Acc4, TotalIn, TotalOut, TotalValueIn, TotalValueOut,
            employee, TransDate, Ref
     FROM Transactions
     ORDER BY TransDate DESC, SNo DESC
     LIMIT 10`
  );

  return NextResponse.json(rows);
}
