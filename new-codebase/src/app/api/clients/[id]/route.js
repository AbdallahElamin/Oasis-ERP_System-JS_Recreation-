// GET /api/clients/[id]
// PUT /api/clients/[id]
import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request, { params }) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const id = parseInt(params.id, 10);
  if (isNaN(id)) return NextResponse.json({ error: "Invalid ID" }, { status: 400 });

  const rows = await pool.query("SELECT * FROM Clients WHERE id = ? LIMIT 1", [id]);
  if (!rows.length) return NextResponse.json({ error: "Client not found" }, { status: 404 });

  return NextResponse.json(rows[0]);
}

export async function PUT(request, { params }) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const id = parseInt(params.id, 10);
  const body = await request.json();

  const fields = Object.keys(body).map(k => `${k} = ?`).join(", ");
  const values = [...Object.values(body), id];

  await pool.query(`UPDATE Clients SET ${fields} WHERE id = ?`, values);
  const rows = await pool.query("SELECT * FROM Clients WHERE id = ? LIMIT 1", [id]);
  return NextResponse.json(rows[0]);
}
