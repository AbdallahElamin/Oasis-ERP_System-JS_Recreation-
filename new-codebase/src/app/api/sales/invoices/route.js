// GET  /api/sales/invoices — list invoices (grouped by InvNo)
// POST /api/sales/invoices — create a new invoice
import { NextResponse } from "next/server";
import prisma from "@/lib/prisma";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const q = searchParams.get("q") || "";
  const year = parseInt(searchParams.get("year") || String(new Date().getFullYear()), 10);

  const startOfYear = new Date(`${year}-01-01T00:00:00.000Z`);
  const endOfYear = new Date(`${year + 1}-01-01T00:00:00.000Z`);

  const where = {
    transDate: { gte: startOfYear, lt: endOfYear },
    ...(q ? { OR: [{ custName: { contains: q } }, { item: { contains: q } }] } : {}),
  };

  // Group by invNo to get one record per invoice
  const invoices = await prisma.invoice.groupBy({
    by: ["invNo", "custId", "custName", "disc", "vat", "transDate", "employee"],
    where,
    _sum: { netAmount: true, totalSdg: true },
    _count: { invNo: true },
    orderBy: { invNo: "desc" },
  });

  return NextResponse.json(invoices);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { custId, custName, items, discPerc, vatPerc, netAmount, amountInWords } = body;

  if (!custId || !custName || !items?.length) {
    return NextResponse.json({ error: "Customer and at least one item are required" }, { status: 400 });
  }

  const result = await prisma.$transaction(async (tx) => {
    // Get next InvNo for the current year
    const year = new Date().getFullYear();
    const startOfYear = new Date(`${year}-01-01T00:00:00.000Z`);
    const endOfYear = new Date(`${year + 1}-01-01T00:00:00.000Z`);

    const lastInvoice = await tx.invoice.findFirst({
      where: { transDate: { gte: startOfYear, lt: endOfYear } },
      orderBy: { invNo: "desc" },
      select: { invNo: true },
    });

    const invNo = (lastInvoice?.invNo ?? 0) + 1;

    // Insert one row per item into Invoices table
    const invoiceRows = [];
    for (const item of items) {
      const row = await tx.invoice.create({
        data: {
          invNo,
          custId: parseInt(custId, 10),
          custName,
          storeName: item.storeName,
          item: item.item,
          batchNo: item.batchNo || null,
          pack: item.pack || null,
          price: parseFloat(item.wPrice) || 0,
          rPrice: parseFloat(item.rPrice) || 0,
          qnt: parseFloat(item.qnt) || 0,
          disc: parseFloat(discPerc) || 0,
          vat: parseFloat(vatPerc) || 0,
          netAmount: parseFloat(netAmount) || 0,
          totalSdg: parseFloat(item.total) || 0,
          amountInWords: amountInWords || null,
          prescription: item.description || "Sales",
          employee: session.user.name,
        },
      });
      invoiceRows.push(row);

      // Deduct from stock
      await tx.stock.create({
        data: {
          storeName: item.storeName,
          item: item.item,
          batchNo: item.batchNo || null,
          pack: item.pack || null,
          wPrice: parseFloat(item.wPrice) || 0,
          rPrice: parseFloat(item.rPrice) || 0,
          qntOut: parseFloat(item.qnt) || 0,
          details: `Invoice# ${invNo}`,
          employee: session.user.name,
          transType: "Invoice",
        },
      });
    }

    // Get next MoveNo for financial transaction
    const lastTx = await tx.transaction.findFirst({
      orderBy: { moveNo: "desc" },
      select: { moveNo: true },
    });
    const moveNo = (lastTx?.moveNo ?? 0) + 1;

    // Debit: Assets > Current Assets > Clients > custName
    await tx.transaction.create({
      data: {
        moveNo,
        custId: parseInt(custId, 10),
        custName,
        ref: `Invoice# ${invNo}`,
        acc1: "Assets", acc2: "Current Assets", acc3: "Clients", acc4: custName,
        totalOut: parseFloat(netAmount) || 0,
        employee: session.user.name,
      },
    });

    // Credit: Purchase & Sales > Sales
    await tx.transaction.create({
      data: {
        moveNo,
        custId: parseInt(custId, 10),
        custName,
        ref: `Invoice# ${invNo}`,
        acc1: "Purchase & Sales", acc2: "Sales", acc3: "Sales", acc4: "Sales",
        totalIn: parseFloat(netAmount) || 0,
        employee: session.user.name,
      },
    });

    return { invNo, rows: invoiceRows };
  });

  return NextResponse.json(result, { status: 201 });
}
