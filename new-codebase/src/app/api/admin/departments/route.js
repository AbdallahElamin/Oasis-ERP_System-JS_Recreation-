import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET() {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const rows = await pool.query("SELECT * FROM Departments ORDER BY name ASC");
  return NextResponse.json(rows);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const { name } = await request.json();
  const result = await pool.query("INSERT INTO Departments (name) VALUES (?)", [name.trim()]);
  return NextResponse.json({ id: Number(result.insertId), name: name.trim() }, { status: 201 });
}
