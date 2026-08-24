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
    SELECT StoreName, item, BatchNo, pack,
           SUM(COALESCE(QntIn, 0)) - SUM(COALESCE(QntOut, 0)) AS availableQnt,
           MAX(WPrice) AS WPrice, MAX(RPrice) AS RPrice, MAX(ExpireDate) AS ExpireDate
    FROM Stock
    WHERE 1=1
  `;
  const params = [];

  if (storeName) { sql += " AND StoreName = ?"; params.push(storeName); }
  if (itemQ) { sql += " AND item LIKE ?"; params.push(`%${itemQ}%`); }

  sql += " GROUP BY StoreName, item, BatchNo, pack ORDER BY StoreName ASC, item ASC";

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
        `INSERT INTO Stock (StoreName, item, pack, BatchNo, QntIn, WPrice, RPrice, ExpireDate, details, employee, TransType)
         VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 'Addition')`,
        [
          storeName, item, pack || null, batchNo || null,
          parseFloat(qntIn) || 0,
          parseFloat(wPrice) || 0,
          parseFloat(rPrice) || 0,
          expireDate ? new Date(expireDate) : null,
          details || null,
          session.user.name,
        ]
      );
      created.push({ id: Number(result.insertId), item });

      // Update price in ItemsRegistry
      await conn.query(
        "UPDATE ItemsRegistry SET WPrice = ?, RPrice = ? WHERE item = ?",
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
