// GET /api/admin/regions — cascading state/region/area lookups
import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const stateFilter = searchParams.get("state");
  const regionFilter = searchParams.get("region");
  const distinct = searchParams.get("distinct");

  if (distinct === "state") {
    const rows = await pool.query("SELECT DISTINCT state FROM Regions WHERE state IS NOT NULL ORDER BY state ASC");
    return NextResponse.json(rows.map(r => r.state));
  }
  if (distinct === "region" && stateFilter) {
    const rows = await pool.query("SELECT DISTINCT region FROM Regions WHERE state = ? AND region IS NOT NULL ORDER BY region ASC", [stateFilter]);
    return NextResponse.json(rows.map(r => r.region));
  }
  if (distinct === "area" && regionFilter) {
    const rows = await pool.query("SELECT DISTINCT area FROM Regions WHERE region = ? AND area IS NOT NULL ORDER BY area ASC", [regionFilter]);
    return NextResponse.json(rows.map(r => r.area));
  }

  const all = await pool.query("SELECT * FROM Regions ORDER BY state ASC");
  return NextResponse.json(all);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const { state, region, area } = await request.json();
  const result = await pool.query("INSERT INTO Regions (state, region, area) VALUES (?,?,?)", [state, region, area]);
  return NextResponse.json({ id: Number(result.insertId) }, { status: 201 });
}
