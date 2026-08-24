import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";
import bcrypt from "bcryptjs";

export async function GET() {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const rows = await pool.query(
    "SELECT id, FullName, role, IsActive, CreatedAt FROM Users ORDER BY id ASC"
  );
  return NextResponse.json(rows);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { fullName, password, role } = await request.json();

  if (!fullName?.trim() || !password) {
    return NextResponse.json({ error: "Full name and password required" }, { status: 400 });
  }

  const hashed = bcrypt.hashSync(password, 10);

  const result = await pool.query(
    "INSERT INTO Users (FullName, Pass, role, IsActive) VALUES (?, ?, ?, 1)",
    [fullName.trim(), hashed, role || "User"]
  );

  return NextResponse.json({ id: Number(result.insertId), fullName: fullName.trim() }, { status: 201 });
}
