import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET() {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  // Run all four count queries in parallel
  const [clientRows, invoiceRows, stockRows, empRows] = await Promise.all([
    pool.query("SELECT COUNT(*) AS cnt FROM Clients"),
    pool.query(
      "SELECT COUNT(DISTINCT InvNo) AS cnt FROM Invoices WHERE YEAR(TransDate) = YEAR(CURDATE()) AND MONTH(TransDate) = MONTH(CURDATE())"
    ),
    pool.query(
      "SELECT COUNT(*) AS cnt FROM (SELECT item FROM Stock GROUP BY item HAVING SUM(COALESCE(QntIn,0)) - SUM(COALESCE(QntOut,0)) > 0) t"
    ),
    pool.query("SELECT COUNT(*) AS cnt FROM Employees WHERE IsActive = 1"),
  ]);

  return NextResponse.json({
    totalClients: Number(clientRows[0].cnt),
    invoicesThisMonth: Number(invoiceRows[0].cnt),
    stockItems: Number(stockRows[0].cnt),
    activeEmployees: Number(empRows[0].cnt),
  });
}
