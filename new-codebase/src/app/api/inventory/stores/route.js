import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET() {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const stores = await pool.query("SELECT * FROM StoreName ORDER BY StoreName ASC");
  return NextResponse.json(stores);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { storeName } = await request.json();
  if (!storeName?.trim()) return NextResponse.json({ error: "Store name required" }, { status: 400 });

  const existing = await pool.query("SELECT id FROM StoreName WHERE StoreName = ? LIMIT 1", [storeName.trim()]);
  if (existing.length > 0) return NextResponse.json({ error: "Store already exists" }, { status: 409 });

  const result = await pool.query("INSERT INTO StoreName (StoreName) VALUES (?)", [storeName.trim()]);
  return NextResponse.json({ id: Number(result.insertId), StoreName: storeName.trim() }, { status: 201 });
}

export async function DELETE(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { id } = await request.json();
  await pool.query("DELETE FROM StoreName WHERE id = ?", [id]);
  return NextResponse.json({ success: true });
}
