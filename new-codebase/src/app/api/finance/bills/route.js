import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

// ── GET — list all bills (pay + receipt vouchers) ─────────────────
export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const type = searchParams.get("type"); // "pay" | "receipt" | null
  const from = searchParams.get("from");
  const to = searchParams.get("to");

  const year = new Date().getFullYear();
  const fromDate = from ? `${from} 00:00:00` : `${year}-01-01 00:00:00`;
  const toDate = to ? `${to} 23:59:59` : `${year}-12-31 23:59:59`;

  let transTypeFilter = "TransType IN ('Pay Voucher', 'Receipt Voucher')";
  if (type === "pay") transTypeFilter = "TransType = 'Pay Voucher'";
  if (type === "receipt") transTypeFilter = "TransType = 'Receipt Voucher'";

  const rows = await pool.query(
    `SELECT MoveNo, PaperNo AS SNo2, TransType, PaymentType, Source,
            Writting, SUM(TotalIn) AS totalIn, SUM(TotalOut) AS totalOut,
            MIN(TransDate) AS TransDate, MIN(employee) AS employee
     FROM Transactions
     WHERE ${transTypeFilter}
       AND TransDate >= ? AND TransDate <= ?
     GROUP BY MoveNo, PaperNo, TransType, PaymentType, Source, Writting
     ORDER BY MoveNo DESC`,
    [fromDate, toDate]
  );

  return NextResponse.json(rows);
}
