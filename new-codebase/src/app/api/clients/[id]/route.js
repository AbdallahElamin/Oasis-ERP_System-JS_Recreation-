import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request, { params }) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const id = parseInt(params.id, 10);
  if (isNaN(id)) return NextResponse.json({ error: "Invalid ID" }, { status: 400 });

  const rows = await pool.query("SELECT * FROM Clients WHERE SNo = ? LIMIT 1", [id]);
  if (!rows.length) return NextResponse.json({ error: "Client not found" }, { status: 404 });

  return NextResponse.json(rows[0]);
}

export async function PUT(request, { params }) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const id = parseInt(params.id, 10);
  if (isNaN(id)) return NextResponse.json({ error: "Invalid ID" }, { status: 400 });

  const body = await request.json();

  // Map camelCase body fields to DB column names
  const colMap = {
    name: "name",
    licNo: "LicNo",
    taxNo: "TaxNo",
    mobile: "mobile",
    clientClass: "ClientClass",
    state: "state",
    region: "region",
    area: "area",
    city: "city",
    town: "town",
    district: "district",
    street: "street",
    buildingNo: "BuildingNo",
    salesMan: "SalesMan",
    medicalRepresentative: "MedicalRepresentative",
    pharmacyOwner: "PharmacyOwner",
    pharmacyOwnerMob: "PharmacyOwnerMob",
    pharmacyDoctor: "PharmacyDoctor",
    pharmacyDoctorMob: "PharmacyDoctorMob",
  };

  const setClauses = [];
  const values = [];
  for (const [key, col] of Object.entries(colMap)) {
    if (key in body) {
      setClauses.push(`${col} = ?`);
      values.push(body[key]);
    }
  }

  if (setClauses.length === 0) {
    return NextResponse.json({ error: "No fields to update" }, { status: 400 });
  }

  values.push(id);
  await pool.query(`UPDATE Clients SET ${setClauses.join(", ")} WHERE SNo = ?`, values);

  const rows = await pool.query("SELECT * FROM Clients WHERE SNo = ? LIMIT 1", [id]);
  return NextResponse.json(rows[0]);
}

export async function DELETE(request, { params }) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const id = parseInt(params.id, 10);
  if (isNaN(id)) return NextResponse.json({ error: "Invalid ID" }, { status: 400 });

  await pool.query("DELETE FROM Clients WHERE SNo = ?", [id]);
  return NextResponse.json({ success: true });
}
