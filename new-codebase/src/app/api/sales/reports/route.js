import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const type = searchParams.get("type") || "by-item";
  const from = searchParams.get("from");
  const to = searchParams.get("to");
  const year = parseInt(searchParams.get("year") || String(new Date().getFullYear()), 10);

  if (type === "by-item") {
    if (!from || !to) return NextResponse.json({ error: "from and to required" }, { status: 400 });
    const rows = await pool.query(
      `SELECT item,
              SUM(Rpric * Qnt) AS totalSales,
              SUM(Qnt)         AS totalQty,
              COUNT(DISTINCT InvNo) AS invoiceCount
       FROM Invoices
       WHERE TransDate >= ? AND TransDate <= ?
       GROUP BY item
       ORDER BY totalSales DESC`,
      [`${from} 00:00:00`, `${to} 23:59:59`]
    );
    return NextResponse.json(rows);
  }

  if (type === "by-month") {
    const rows = await pool.query(
      `SELECT YEAR(TransDate) AS yr, MONTH(TransDate) AS mo,
              SUM(Rpric * Qnt) AS totalSales,
              COUNT(DISTINCT InvNo) AS invoiceCount
       FROM Invoices
       WHERE YEAR(TransDate) = ?
       GROUP BY YEAR(TransDate), MONTH(TransDate)
       ORDER BY MONTH(TransDate) ASC`,
      [year]
    );
    return NextResponse.json(rows);
  }

  if (type === "by-client") {
    if (!from || !to) return NextResponse.json({ error: "from and to required" }, { status: 400 });
    const rows = await pool.query(
      `SELECT CustName,
              SUM(Rpric * Qnt) AS totalSales,
              SUM(Qnt)         AS totalQty,
              COUNT(DISTINCT InvNo) AS invoiceCount
       FROM Invoices
       WHERE TransDate >= ? AND TransDate <= ?
       GROUP BY CustName
       ORDER BY totalSales DESC`,
      [`${from} 00:00:00`, `${to} 23:59:59`]
    );
    return NextResponse.json(rows);
  }

  return NextResponse.json({ error: "Invalid type. Use by-item, by-month, or by-client" }, { status: 400 });
}
