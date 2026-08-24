// GET /api/inventory/stock — grouped stock summary
// POST /api/inventory/stock — add stock entries
import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const storeName = searchParams.get("store");
  const itemQ = searchParams.get("item");

  let sql = `
    SELECT storeName, item, batchNo, pack, wPrice, rPrice,
           SUM(COALESCE(qntIn, 0)) - SUM(COALESCE(qntOut, 0)) AS availableQnt
    FROM Stock
    WHERE 1=1
  `;
  const params = [];

  if (storeName) { sql += " AND storeName = ?"; params.push(storeName); }
  if (itemQ) { sql += " AND item LIKE ?"; params.push(`%${itemQ}%`); }

  sql += " GROUP BY storeName, item, batchNo, pack, wPrice, rPrice ORDER BY storeName ASC, item ASC";

  const rows = await pool.query(sql, params);
  return NextResponse.json(rows);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { entries } = body;

  if (!entries || !Array.isArray(entries) || entries.length === 0) {
    return NextResponse.json({ error: "No stock entries provided" }, { status: 400 });
  }

  const conn = await pool.getConnection();
  try {
    await conn.beginTransaction();
    const created = [];

    for (const entry of entries) {
      const { storeName, item, pack, batchNo, qntIn, wPrice, rPrice, expireDate, details } = entry;

      const result = await conn.query(
        `INSERT INTO Stock (storeName, item, pack, batchNo, qntIn, wPrice, rPrice, expireDate, details, employee, transType)
         VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 'Addition')`,
        [storeName, item, pack || null, batchNo || null, parseFloat(qntIn) || 0,
         parseFloat(wPrice) || 0, parseFloat(rPrice) || 0,
         expireDate ? new Date(expireDate) : null, details || null, session.user.name]
      );
      created.push({ id: Number(result.insertId), item });

      // Update price in ItemsRegistry
      await conn.query(
        "UPDATE ItemsRegistry SET wPrice = ?, rPrice = ? WHERE item = ?",
        [parseFloat(wPrice) || 0, parseFloat(rPrice) || 0, item]
      );
    }

    await conn.commit();
    return NextResponse.json(created, { status: 201 });
  } catch (err) {
    await conn.rollback();
    console.error("Stock insertion error:", err);
    return NextResponse.json({ error: "Failed to save stock" }, { status: 500 });
  } finally {
    conn.release();
  }
}
