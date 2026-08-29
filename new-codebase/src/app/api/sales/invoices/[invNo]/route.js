import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request, { params }) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const invNo = parseInt(params.invNo, 10);
  const { searchParams } = new URL(request.url);
  const year = parseInt(searchParams.get("year") || String(new Date().getFullYear()), 10);

  const rows = await pool.query(
    `SELECT * FROM Invoices WHERE InvNo = ? AND YEAR(TransDate) = ? ORDER BY SNo ASC`,
    [invNo, year]
  );
  if (!rows.length) return NextResponse.json({ error: "Invoice not found" }, { status: 404 });
  return NextResponse.json(rows);
}
