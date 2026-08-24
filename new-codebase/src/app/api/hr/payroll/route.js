import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const year = parseInt(searchParams.get("year") || String(new Date().getFullYear()), 10);
  const month = parseInt(searchParams.get("month") || String(new Date().getMonth() + 1), 10);

  const rows = await pool.query(
    `SELECT ps.*, e.FullName
     FROM PaySheets ps
     JOIN Employees e ON ps.EmpNo = e.EmpNo
     WHERE ps.year = ? AND ps.month = ?
     ORDER BY e.FullName ASC`,
    [year, month]
  );
  return NextResponse.json(rows);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { empNo, month, year, basicSalary, allowances, deductions, netPay } = body;

  if (!empNo || !month || !year) {
    return NextResponse.json({ error: "EmpNo, month, and year are required" }, { status: 400 });
  }

  // Upsert — delete existing for same employee/month/year and re-insert
  const conn = await pool.getConnection();
  try {
    await conn.beginTransaction();
    await conn.query("DELETE FROM PaySheets WHERE EmpNo = ? AND month = ? AND year = ?", [empNo, month, year]);
    const result = await conn.query(
      "INSERT INTO PaySheets (EmpNo, month, year, BasicSalary, Allowances, Deductions, NetPay) VALUES (?,?,?,?,?,?,?)",
      [empNo, month, year, parseFloat(basicSalary) || 0, parseFloat(allowances) || 0, parseFloat(deductions) || 0, parseFloat(netPay) || 0]
    );
    await conn.commit();
    return NextResponse.json({ id: Number(result.insertId) }, { status: 201 });
  } catch (err) {
    await conn.rollback();
    return NextResponse.json({ error: "Failed to save payroll" }, { status: 500 });
  } finally {
    conn.release();
  }
}
