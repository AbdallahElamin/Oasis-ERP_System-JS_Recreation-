// GET /api/inventory/items  — list all items
// POST /api/inventory/items — create a new item
import { NextResponse } from "next/server";
import prisma from "@/lib/prisma";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const q = searchParams.get("q") || "";

  const items = await prisma.itemRegistry.findMany({
    where: q
      ? {
          OR: [
            { item: { contains: q } },
            { genericName: { contains: q } },
            { companyName: { contains: q } },
          ],
        }
      : {},
    orderBy: { item: "asc" },
  });

  return NextResponse.json(items);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { item, genericName, pack, wPrice, rPrice, companyName } = body;

  if (!item?.trim()) {
    return NextResponse.json({ error: "Item name is required" }, { status: 400 });
  }

  const existing = await prisma.itemRegistry.findUnique({ where: { item } });
  if (existing) {
    return NextResponse.json({ error: "Item already exists" }, { status: 409 });
  }

  const newItem = await prisma.itemRegistry.create({
    data: {
      item: item.trim(),
      genericName: genericName?.trim() || null,
      pack: pack?.trim() || null,
      wPrice: parseFloat(wPrice) || 0,
      rPrice: parseFloat(rPrice) || 0,
      companyName: companyName?.trim() || null,
    },
  });

  return NextResponse.json(newItem, { status: 201 });
}
