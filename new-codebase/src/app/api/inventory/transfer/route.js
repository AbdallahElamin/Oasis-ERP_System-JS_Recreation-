import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { fromStore, toStore, item, batchNo, pack, quantity } = body;

  if (!fromStore || !toStore || !item || !quantity) {
    return NextResponse.json({ error: "fromStore, toStore, item, and quantity are required" }, { status: 400 });
  }
  if (fromStore === toStore) {
    return NextResponse.json({ error: "Source and destination stores must be different" }, { status: 400 });
  }
  const qty = parseFloat(quantity);
  if (qty <= 0) return NextResponse.json({ error: "Quantity must be positive" }, { status: 400 });

  const conn = await pool.getConnection();
  try {
    await conn.beginTransaction();

    const now = new Date();

    // Check available stock
    const [avail] = await conn.query(
      `SELECT SUM(COALESCE(QntIn, 0)) - SUM(COALESCE(QntOut, 0)) AS available
       FROM Stock WHERE StoreName = ? AND item = ? AND (BatchNo = ? OR ? IS NULL)`,
      [fromStore, item, batchNo || null, batchNo || null]
    );
    if (Number(avail.available) < qty) {
      throw new Error(`Insufficient stock. Available: ${avail.available}`);
    }

    // Transfer Out from source store
    await conn.query(
      `INSERT INTO Stock (StoreName, item, BatchNo, pack, QntOut, employee, TransType, TransDate, details)
       VALUES (?,?,?,?,?,?,'Transfer Out',?,?)`,
      [fromStore, item, batchNo || null, pack || null, qty, session.user.name, now, `To: ${toStore}`]
    );

    // Transfer In to destination store
    await conn.query(
      `INSERT INTO Stock (StoreName, item, BatchNo, pack, QntIn, employee, TransType, TransDate, details)
       VALUES (?,?,?,?,?,?,'Transfer In',?,?)`,
      [toStore, item, batchNo || null, pack || null, qty, session.user.name, now, `From: ${fromStore}`]
    );

    await conn.commit();
    return NextResponse.json({ success: true }, { status: 201 });
  } catch (err) {
    await conn.rollback();
    return NextResponse.json({ error: err.message }, { status: 400 });
  } finally {
    conn.release();
  }
}
