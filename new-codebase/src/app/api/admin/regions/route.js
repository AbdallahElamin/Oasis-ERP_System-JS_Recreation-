import { NextResponse } from "next/server";
import prisma from "@/lib/prisma";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const stateFilter = searchParams.get("state");
  const regionFilter = searchParams.get("region");
  const distinct = searchParams.get("distinct");

  if (distinct === "state") {
    const rows = await prisma.region.findMany({ select: { state: true }, where: { state: { not: null } }, distinct: ["state"], orderBy: { state: "asc" } });
    return NextResponse.json(rows.map((r) => r.state));
  }
  if (distinct === "region" && stateFilter) {
    const rows = await prisma.region.findMany({ select: { region: true }, where: { state: stateFilter, region: { not: null } }, distinct: ["region"], orderBy: { region: "asc" } });
    return NextResponse.json(rows.map((r) => r.region));
  }
  if (distinct === "area" && regionFilter) {
    const rows = await prisma.region.findMany({ select: { area: true }, where: { region: regionFilter, area: { not: null } }, distinct: ["area"], orderBy: { area: "asc" } });
    return NextResponse.json(rows.map((r) => r.area));
  }

  const all = await prisma.region.findMany({ orderBy: { state: "asc" } });
  return NextResponse.json(all);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const { state, region, area } = await request.json();
  const rec = await prisma.region.create({ data: { state, region, area } });
  return NextResponse.json(rec, { status: 201 });
}
