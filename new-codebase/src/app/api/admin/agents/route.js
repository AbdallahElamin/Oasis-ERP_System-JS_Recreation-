// GET /api/admin/agents — sales agents and medical representatives
import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const type = searchParams.get("type");

  if (type === "distributor") {
    const rows = await pool.query("SELECT * FROM AgentDistributors ORDER BY name ASC");
    return NextResponse.json(rows);
  }
  if (type === "representative") {
    const rows = await pool.query("SELECT * FROM AgentRepresentatives ORDER BY name ASC");
    return NextResponse.json(rows);
  }

  const [distributors, reps] = await Promise.all([
    pool.query("SELECT * FROM AgentDistributors ORDER BY name ASC"),
    pool.query("SELECT * FROM AgentRepresentatives ORDER BY name ASC"),
  ]);
  return NextResponse.json({ distributors, representatives: reps });
}
