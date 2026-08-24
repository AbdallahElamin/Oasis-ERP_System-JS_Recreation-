import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { storeName, item, batchNo, pack, quantity, reason } = body;

  if (!storeName || !item || !quantity) {
    return NextResponse.json({ error: "storeName, item, and quantity are required" }, { status: 400 });
  }
  const qty = parseFloat(quantity);
  if (qty <= 0) return NextResponse.json({ error: "Quantity must be positive" }, { status: 400 });

  const conn = await pool.getConnection();
  try {
    await conn.beginTransaction();

    const [avail] = await conn.query(
      `SELECT SUM(COALESCE(QntIn, 0)) - SUM(COALESCE(QntOut, 0)) AS available
       FROM Stock WHERE StoreName = ? AND item = ?`,
      [storeName, item]
    );
    if (Number(avail.available) < qty) {
      throw new Error(`Insufficient stock. Available: ${avail.available}`);
    }

    await conn.query(
      `INSERT INTO Stock (StoreName, item, BatchNo, pack, QntOut, employee, TransType, TransDate, details)
       VALUES (?,?,?,?,?,?,'Disposal',?,?)`,
      [storeName, item, batchNo || null, pack || null, qty, session.user.name, new Date(), reason?.trim() || "Disposed"]
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
