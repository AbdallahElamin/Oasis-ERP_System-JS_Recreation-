// GET  /api/clients — list all clients (with optional search)
// POST /api/clients — create a new client
import { NextResponse } from "next/server";
import prisma from "@/lib/prisma";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const q = searchParams.get("q") || "";
  const page = parseInt(searchParams.get("page") || "1", 10);
  const limit = parseInt(searchParams.get("limit") || "50", 10);
  const skip = (page - 1) * limit;

  const where = q
    ? {
        OR: [
          { name: { contains: q } },
          { mobile: { contains: q } },
          { licNo: { contains: q } },
        ],
      }
    : {};

  const [clients, total] = await Promise.all([
    prisma.client.findMany({
      where,
      orderBy: { name: "asc" },
      skip,
      take: limit,
    }),
    prisma.client.count({ where }),
  ]);

  return NextResponse.json({ clients, total, page, limit });
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const {
    name, licNo, taxNo, mobile, clientClass,
    state, region, area, city, town, district, street, buildingNo,
    salesMan, medicalRepresentative,
    pharmacyOwner, pharmacyOwnerMob, pharmacyDoctor, pharmacyDoctorMob,
  } = body;

  if (!name?.trim()) {
    return NextResponse.json({ error: "Client name is required" }, { status: 400 });
  }

  // Use a transaction: create client + open account in Chart of Accounts
  const client = await prisma.$transaction(async (tx) => {
    const newClient = await tx.client.create({
      data: {
        name: name.trim(),
        licNo: licNo?.trim() || null,
        taxNo: taxNo?.trim() || null,
        mobile: mobile?.trim() || null,
        clientClass: clientClass?.trim() || null,
        state: state?.trim() || null,
        region: region?.trim() || null,
        area: area?.trim() || null,
        city: city?.trim() || null,
        town: town?.trim() || null,
        district: district?.trim() || null,
        street: street?.trim() || null,
        buildingNo: buildingNo?.trim() || null,
        salesMan: salesMan?.trim() || null,
        medicalRepresentative: medicalRepresentative?.trim() || null,
        pharmacyOwner: pharmacyOwner?.trim() || null,
        pharmacyOwnerMob: pharmacyOwnerMob?.trim() || null,
        pharmacyDoctor: pharmacyDoctor?.trim() || null,
        pharmacyDoctorMob: pharmacyDoctorMob?.trim() || null,
        userName: session.user.name,
      },
    });

    // Open financial account in Chart of Accounts (mirrors original frmClientsAdd.vb)
    await tx.account.create({
      data: {
        acc1: "Assets",
        acc2: "Current Assets",
        acc3: "Clients",
        acc4: name.trim(),
      },
    });

    return newClient;
  });

  return NextResponse.json(client, { status: 201 });
}
