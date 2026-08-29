import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

// GET — bulk barcode data by comma-separated SNo list
export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const snos = (searchParams.get("snos") || "").split(",").map(Number).filter(Boolean);

  if (!snos.length) return NextResponse.json([]);

  const placeholders = snos.map(() => "?").join(",");
  const rows = await pool.query(
    `SELECT SNo, item, GenericName, pack, WPrice, RPrice, CompanyName, Barcode, BarcodeType
     FROM ItemsRegistry WHERE SNo IN (${placeholders}) ORDER BY SNo ASC`,
    snos
  );
  return NextResponse.json(rows);
}
