import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const q = searchParams.get("q") || "";

  let sql = `
    SELECT e.*, d.name AS DepartmentName, j.title AS JobTitle, g.level AS GradeLevel
    FROM Employees e
    LEFT JOIN Departments d ON e.DepartmentId = d.id
    LEFT JOIN JobDescriptions j ON e.JobDescriptionId = j.id
    LEFT JOIN GradeLevels g ON e.GradeLevelId = g.id
    WHERE 1=1
  `;
  const params = [];
  if (q) { sql += " AND (e.FullName LIKE ? OR e.NationalId LIKE ? OR e.mobile LIKE ?)"; params.push(`%${q}%`, `%${q}%`, `%${q}%`); }
  sql += " ORDER BY e.EmpNo DESC";

  const rows = await pool.query(sql, params);
  return NextResponse.json(rows);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { fullName, nationalId, mobile, email, dateOfBirth, dateOfJoining, departmentId, jobDescriptionId, gradeLevelId, basicSalary, contractType } = body;

  if (!fullName?.trim()) return NextResponse.json({ error: "Full name is required" }, { status: 400 });

  const result = await pool.query(
    `INSERT INTO Employees (FullName, NationalId, mobile, email, DateOfBirth, DateOfJoining,
       DepartmentId, JobDescriptionId, GradeLevelId, BasicSalary, ContractType, IsActive)
     VALUES (?,?,?,?,?,?,?,?,?,?,?,1)`,
    [
      fullName.trim(),
      nationalId?.trim() || null,
      mobile?.trim() || null,
      email?.trim() || null,
      dateOfBirth ? new Date(dateOfBirth) : null,
      dateOfJoining ? new Date(dateOfJoining) : null,
      departmentId ? parseInt(departmentId) : null,
      jobDescriptionId ? parseInt(jobDescriptionId) : null,
      gradeLevelId ? parseInt(gradeLevelId) : null,
      parseFloat(basicSalary) || 0,
      contractType?.trim() || null,
    ]
  );
  return NextResponse.json({ EmpNo: Number(result.insertId), fullName: fullName.trim() }, { status: 201 });
}
