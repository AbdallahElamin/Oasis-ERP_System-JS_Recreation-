import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const q = searchParams.get("q") || "";
  const year = parseInt(searchParams.get("year") || String(new Date().getFullYear()), 10);

  let sql = `
    SELECT InvNo, CustID, CustName, Disc, VAT, TransDate, employee,
           SUM(NetAmount) AS totalNet, SUM(TotalSDG) AS totalSdg, COUNT(*) AS itemCount
    FROM Quotations
    WHERE YEAR(TransDate) = ?
  `;
  const params = [year];
  if (q) { sql += " AND (CustName LIKE ? OR item LIKE ?)"; params.push(`%${q}%`, `%${q}%`); }
  sql += " GROUP BY InvNo, CustID, CustName, Disc, VAT, TransDate, employee ORDER BY InvNo DESC";

  const rows = await pool.query(sql, params);
  return NextResponse.json(rows);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { custId, custName, items, discPerc, vatPerc, netAmount, amountInWords } = body;

  if (!custId || !custName || !items?.length) {
    return NextResponse.json({ error: "Customer and at least one item required" }, { status: 400 });
  }

  const conn = await pool.getConnection();
  try {
    await conn.beginTransaction();
    const year = new Date().getFullYear();
    const [{ lastNo }] = await conn.query(
      "SELECT COALESCE(MAX(InvNo), 0) AS lastNo FROM Quotations WHERE YEAR(TransDate) = ?", [year]
    );
    const invNo = Number(lastNo) + 1;

    for (const item of items) {
      await conn.query(
        `INSERT INTO Quotations
          (InvNo, CustID, CustName, StoreName, item, BatchNo, pack, price, Rpric,
           Qnt, Disc, VAT, NetAmount, TotalSDG, AmountInWords, prescription, employee)
         VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`,
        [
          invNo, parseInt(custId, 10), custName,
          item.storeName, item.item, item.batchNo || null, item.pack || null,
          parseFloat(item.wPrice) || 0, parseFloat(item.rPrice) || 0,
          parseFloat(item.qnt) || 0,
          parseFloat(discPerc) || 0, parseFloat(vatPerc) || 0,
          parseFloat(netAmount) || 0, parseFloat(item.total) || 0,
          amountInWords || null, item.description || "Quotation", session.user.name,
        ]
      );
    }
    await conn.commit();
    return NextResponse.json({ invNo }, { status: 201 });
  } catch (err) {
    await conn.rollback();
    return NextResponse.json({ error: "Failed to save quotation" }, { status: 500 });
  } finally {
    conn.release();
  }
}
