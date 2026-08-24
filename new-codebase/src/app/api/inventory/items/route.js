// GET /api/inventory/items  — list all items
// POST /api/inventory/items — create a new item
import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const q = searchParams.get("q") || "";

  let rows;
  if (q) {
    rows = await pool.query(
      "SELECT * FROM ItemsRegistry WHERE item LIKE ? OR genericName LIKE ? OR companyName LIKE ? ORDER BY item ASC",
      [`%${q}%`, `%${q}%`, `%${q}%`]
    );
  } else {
    rows = await pool.query("SELECT * FROM ItemsRegistry ORDER BY item ASC");
  }

  return NextResponse.json(rows);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { item, genericName, pack, wPrice, rPrice, companyName } = body;

  if (!item?.trim()) {
    return NextResponse.json({ error: "Item name is required" }, { status: 400 });
  }

  const existing = await pool.query("SELECT id FROM ItemsRegistry WHERE item = ? LIMIT 1", [item.trim()]);
  if (existing.length > 0) {
    return NextResponse.json({ error: "Item already exists" }, { status: 409 });
  }

  const result = await pool.query(
    "INSERT INTO ItemsRegistry (item, genericName, pack, wPrice, rPrice, companyName) VALUES (?, ?, ?, ?, ?, ?)",
    [item.trim(), genericName?.trim() || null, pack?.trim() || null, parseFloat(wPrice) || 0, parseFloat(rPrice) || 0, companyName?.trim() || null]
  );

  return NextResponse.json({ id: Number(result.insertId), item: item.trim() }, { status: 201 });
}
