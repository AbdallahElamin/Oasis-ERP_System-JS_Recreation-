import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";
import bcrypt from "bcryptjs";

export async function PUT(request, { params }) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const id = parseInt(params.id, 10);
  const body = await request.json();
  const { fullName, password, role, isActive } = body;

  const sets = [];
  const values = [];

  if (fullName !== undefined) { sets.push("FullName = ?"); values.push(fullName.trim()); }
  if (role !== undefined) { sets.push("role = ?"); values.push(role); }
  if (isActive !== undefined) { sets.push("IsActive = ?"); values.push(isActive ? 1 : 0); }
  if (password) { sets.push("Pass = ?"); values.push(bcrypt.hashSync(password, 10)); }

  if (!sets.length) return NextResponse.json({ error: "Nothing to update" }, { status: 400 });

  values.push(id);
  await pool.query(`UPDATE Users SET ${sets.join(", ")} WHERE id = ?`, values);
  const rows = await pool.query("SELECT id, FullName, role, IsActive, CreatedAt FROM Users WHERE id = ?", [id]);
  return NextResponse.json(rows[0]);
}

export async function DELETE(request, { params }) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const id = parseInt(params.id, 10);
  await pool.query("UPDATE Users SET IsActive = 0 WHERE id = ?", [id]);
  return NextResponse.json({ success: true });
}
