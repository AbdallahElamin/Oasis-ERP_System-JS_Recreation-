import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET() {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const rows = await pool.query("SELECT * FROM GradeLevels ORDER BY level ASC");
  return NextResponse.json(rows);
}
