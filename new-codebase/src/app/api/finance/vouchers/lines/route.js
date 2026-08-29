import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const moveNo = parseInt(searchParams.get("moveNo"), 10);
  const year = parseInt(searchParams.get("year") || String(new Date().getFullYear()), 10);

  if (!moveNo) return NextResponse.json({ error: "moveNo required" }, { status: 400 });

  const rows = await pool.query(
    `SELECT SNo, MoveNo, Acc1, Acc2, Acc3, Acc4, Ref, TotalValueIn, TotalValueOut,
            TotalIn, TotalOut, TransType, PaymentType, Source, Writting,
            employee, TransDate, PaperNo, CheqDate, Approved, Reversed
     FROM Transactions
     WHERE MoveNo = ? AND YEAR(TransDate) = ?
     ORDER BY SNo ASC`,
    [moveNo, year]
  );

  return NextResponse.json(rows);
}
