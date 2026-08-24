import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const q = searchParams.get("q") || "";
  const page = parseInt(searchParams.get("page") || "1", 10);
  const limit = parseInt(searchParams.get("limit") || "50", 10);
  const offset = (page - 1) * limit;

  let where = "1=1";
  const params = [];
  if (q) {
    where = "(name LIKE ? OR mobile LIKE ? OR LicNo LIKE ?)";
    params.push(`%${q}%`, `%${q}%`, `%${q}%`);
  }

  const [clients, countRows] = await Promise.all([
    pool.query(
      `SELECT * FROM Clients WHERE ${where} ORDER BY name ASC LIMIT ? OFFSET ?`,
      [...params, limit, offset]
    ),
    pool.query(`SELECT COUNT(*) AS total FROM Clients WHERE ${where}`, params),
  ]);

  return NextResponse.json({
    clients,
    total: Number(countRows[0].total),
    page,
    limit,
  });
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const {
    name,
    licNo,
    taxNo,
    mobile,
    clientClass,
    state,
    region,
    area,
    city,
    town,
    district,
    street,
    buildingNo,
    salesMan,
    medicalRepresentative,
    pharmacyOwner,
    pharmacyOwnerMob,
    pharmacyDoctor,
    pharmacyDoctorMob,
  } = body;

  if (!name?.trim()) {
    return NextResponse.json({ error: "Client name is required" }, { status: 400 });
  }

  const conn = await pool.getConnection();
  try {
    await conn.beginTransaction();

    const result = await conn.query(
      `INSERT INTO Clients
        (name, LicNo, TaxNo, mobile, ClientClass, state, region, area, city, town, district,
         street, BuildingNo, SalesMan, MedicalRepresentative,
         PharmacyOwner, PharmacyOwnerMob, PharmacyDoctor, PharmacyDoctorMob, UserName)
       VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`,
      [
        name.trim(),
        licNo?.trim() || null,
        taxNo?.trim() || null,
        mobile?.trim() || null,
        clientClass?.trim() || null,
        state?.trim() || null,
        region?.trim() || null,
        area?.trim() || null,
        city?.trim() || null,
        town?.trim() || null,
        district?.trim() || null,
        street?.trim() || null,
        buildingNo?.trim() || null,
        salesMan?.trim() || null,
        medicalRepresentative?.trim() || null,
        pharmacyOwner?.trim() || null,
        pharmacyOwnerMob?.trim() || null,
        pharmacyDoctor?.trim() || null,
        pharmacyDoctorMob?.trim() || null,
        session.user.name,
      ]
    );

    const clientSNo = Number(result.insertId);

    // Open financial account in Chart of Accounts (Accs table)
    await conn.query(
      "INSERT INTO Accs (Acc1, Acc2, Acc3, Acc4) VALUES ('Assets', 'Current Assets', 'Clients', ?)",
      [name.trim()]
    );

    await conn.commit();
    return NextResponse.json({ id: clientSNo, name: name.trim() }, { status: 201 });
  } catch (err) {
    await conn.rollback();
    console.error("Client creation error:", err);
    return NextResponse.json({ error: "Failed to save client" }, { status: 500 });
  } finally {
    conn.release();
  }
}
