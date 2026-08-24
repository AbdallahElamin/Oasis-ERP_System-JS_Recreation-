// GET /api/inventory/stores
// POST /api/inventory/stores
import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET() {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const stores = await pool.query("SELECT * FROM StoreNames ORDER BY storeName ASC");
  return NextResponse.json(stores);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const { storeName } = await request.json();
  if (!storeName?.trim()) return NextResponse.json({ error: "Store name required" }, { status: 400 });
  const result = await pool.query("INSERT INTO StoreNames (storeName) VALUES (?)", [storeName.trim()]);
  return NextResponse.json({ id: Number(result.insertId), storeName: storeName.trim() }, { status: 201 });
}
