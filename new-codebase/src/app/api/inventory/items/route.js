import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const q = searchParams.get("q") || "";
  const barcode = searchParams.get("barcode");

  // Barcode scan lookup — returns single item or null
  if (barcode) {
    const rows = await pool.query(
      "SELECT * FROM ItemsRegistry WHERE Barcode = ? LIMIT 1",
      [barcode.trim()]
    );
    return NextResponse.json(rows[0] || null);
  }

  let rows;
  if (q) {
    rows = await pool.query(
      "SELECT SNo, item, GenericName, pack, WPrice, RPrice, CompanyName, Barcode, BarcodeType FROM ItemsRegistry WHERE item LIKE ? OR GenericName LIKE ? OR CompanyName LIKE ? ORDER BY item ASC",
      [`%${q}%`, `%${q}%`, `%${q}%`]
    );
  } else {
    rows = await pool.query("SELECT SNo, item, GenericName, pack, WPrice, RPrice, CompanyName, Barcode, BarcodeType FROM ItemsRegistry ORDER BY item ASC");
  }

  return NextResponse.json(rows);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { item, genericName, pack, wPrice, rPrice, companyName } = body;

  if (!item?.trim()) {
    return NextResponse.json({ error: "Item name is required" }, { status: 400 });
  }

  const existing = await pool.query(
    "SELECT SNo FROM ItemsRegistry WHERE item = ? LIMIT 1",
    [item.trim()]
  );
  if (existing.length > 0) {
    return NextResponse.json({ error: "Item already exists" }, { status: 409 });
  }

  const result = await pool.query(
    "INSERT INTO ItemsRegistry (item, GenericName, pack, WPrice, RPrice, CompanyName) VALUES (?, ?, ?, ?, ?, ?)",
    [
      item.trim(),
      genericName?.trim() || null,
      pack?.trim() || null,
      parseFloat(wPrice) || 0,
      parseFloat(rPrice) || 0,
      companyName?.trim() || null,
    ]
  );

  const newSNo = Number(result.insertId);
  const barcodeValue = `OAS-${String(newSNo).padStart(4, "0")}`;
  await pool.query(
    "UPDATE ItemsRegistry SET Barcode = ?, BarcodeType = 'CODE128' WHERE SNo = ?",
    [barcodeValue, newSNo]
  );

  return NextResponse.json({ id: newSNo, item: item.trim(), barcode: barcodeValue }, { status: 201 });
}

export async function PUT(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { sno, item, genericName, pack, wPrice, rPrice, companyName } = body;

  if (!sno) return NextResponse.json({ error: "SNo required" }, { status: 400 });

  await pool.query(
    "UPDATE ItemsRegistry SET item=?, GenericName=?, pack=?, WPrice=?, RPrice=?, CompanyName=? WHERE SNo=?",
    [
      item?.trim() || null,
      genericName?.trim() || null,
      pack?.trim() || null,
      parseFloat(wPrice) || 0,
      parseFloat(rPrice) || 0,
      companyName?.trim() || null,
      sno,
    ]
  );

  const rows = await pool.query("SELECT * FROM ItemsRegistry WHERE SNo = ?", [sno]);
  return NextResponse.json(rows[0]);
}

export async function DELETE(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { sno } = await request.json();
  if (!sno) return NextResponse.json({ error: "SNo required" }, { status: 400 });

  await pool.query("DELETE FROM ItemsRegistry WHERE SNo = ?", [sno]);
  return NextResponse.json({ success: true });
}
