import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

const VALID_TYPES = ["CODE128", "EAN13", "QR"];

// GET — return barcode data for a single item
export async function GET(request, { params }) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const sno = parseInt(params.sno, 10);
  const rows = await pool.query(
    "SELECT SNo, item, GenericName, pack, WPrice, RPrice, CompanyName, Barcode, BarcodeType FROM ItemsRegistry WHERE SNo = ?",
    [sno]
  );
  if (!rows.length) return NextResponse.json({ error: "Item not found" }, { status: 404 });
  return NextResponse.json(rows[0]);
}

// PUT — override barcode value/type manually
export async function PUT(request, { params }) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const sno = parseInt(params.sno, 10);
  const { barcode, barcodeType } = await request.json();

  if (!barcode?.trim()) return NextResponse.json({ error: "Barcode value required" }, { status: 400 });
  const type = (barcodeType || "CODE128").toUpperCase();
  if (!VALID_TYPES.includes(type)) return NextResponse.json({ error: `barcodeType must be one of: ${VALID_TYPES.join(", ")}` }, { status: 400 });

  await pool.query("UPDATE ItemsRegistry SET Barcode = ?, BarcodeType = ? WHERE SNo = ?", [barcode.trim(), type, sno]);
  const rows = await pool.query("SELECT SNo, item, Barcode, BarcodeType FROM ItemsRegistry WHERE SNo = ?", [sno]);
  return NextResponse.json(rows[0]);
}

// POST — regenerate auto barcode (reset to OAS-{SNo})
export async function POST(request, { params }) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const sno = parseInt(params.sno, 10);
  const barcodeValue = `OAS-${String(sno).padStart(4, "0")}`;
  await pool.query("UPDATE ItemsRegistry SET Barcode = ?, BarcodeType = 'CODE128' WHERE SNo = ?", [barcodeValue, sno]);
  return NextResponse.json({ barcode: barcodeValue, barcodeType: "CODE128" });
}
