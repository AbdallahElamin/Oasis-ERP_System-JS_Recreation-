// GET  /api/inventory/stock — list stock (grouped summary)
// POST /api/inventory/stock — add stock entries
import { NextResponse } from "next/server";
import prisma from "@/lib/prisma";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const storeName = searchParams.get("store");
  const item = searchParams.get("item");

  // Build a grouped stock summary — sum of QntIn minus sum of QntOut per store/item/batch
  const where = {};
  if (storeName) where.storeName = storeName;
  if (item) where.item = { contains: item };

  // Raw grouped query via Prisma's groupBy
  const grouped = await prisma.stock.groupBy({
    by: ["storeName", "item", "batchNo", "pack", "wPrice", "rPrice"],
    where,
    _sum: { qntIn: true, qntOut: true },
    orderBy: [{ storeName: "asc" }, { item: "asc" }],
  });

  const result = grouped.map((row) => ({
    storeName: row.storeName,
    item: row.item,
    batchNo: row.batchNo,
    pack: row.pack,
    wPrice: row.wPrice,
    rPrice: row.rPrice,
    availableQnt: (row._sum.qntIn ?? 0) - (row._sum.qntOut ?? 0),
  }));

  return NextResponse.json(result);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { entries } = body; // Array of stock entries

  if (!entries || !Array.isArray(entries) || entries.length === 0) {
    return NextResponse.json({ error: "No stock entries provided" }, { status: 400 });
  }

  // Use a transaction to insert all entries + update item prices
  const result = await prisma.$transaction(async (tx) => {
    const created = [];
    for (const entry of entries) {
      const { storeName, item, pack, batchNo, qntIn, wPrice, rPrice, expireDate, details } = entry;

      // Insert stock record
      const stock = await tx.stock.create({
        data: {
          storeName,
          item,
          pack: pack || null,
          batchNo: batchNo || null,
          qntIn: parseFloat(qntIn) || 0,
          wPrice: parseFloat(wPrice) || 0,
          rPrice: parseFloat(rPrice) || 0,
          expireDate: expireDate ? new Date(expireDate) : null,
          details: details || null,
          employee: session.user.name,
          transType: "Addition",
        },
      });

      // Update price in ItemsRegistry
      await tx.itemRegistry.updateMany({
        where: { item },
        data: { wPrice: parseFloat(wPrice) || 0, rPrice: parseFloat(rPrice) || 0 },
      });

      created.push(stock);
    }
    return created;
  });

  return NextResponse.json(result, { status: 201 });
}
