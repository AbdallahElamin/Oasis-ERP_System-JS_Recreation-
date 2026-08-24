import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const year = parseInt(searchParams.get("year") || String(new Date().getFullYear()), 10);

  const rows = await pool.query(
    `SELECT v.*, e.FullName
     FROM Vacations v
     JOIN Employees e ON v.EmpNo = e.EmpNo
     WHERE YEAR(v.StartDate) = ?
     ORDER BY v.StartDate DESC`,
    [year]
  );
  return NextResponse.json(rows);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { empNo, startDate, endDate, type, notes } = await request.json();
  if (!empNo || !startDate || !endDate || !type) {
    return NextResponse.json({ error: "EmpNo, startDate, endDate, and type are required" }, { status: 400 });
  }

  const result = await pool.query(
    "INSERT INTO Vacations (EmpNo, StartDate, EndDate, type, notes, status) VALUES (?,?,?,?,?,'Pending')",
    [empNo, new Date(startDate), new Date(endDate), type, notes?.trim() || null]
  );
  return NextResponse.json({ id: Number(result.insertId) }, { status: 201 });
}
