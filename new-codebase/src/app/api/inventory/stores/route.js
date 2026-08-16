import { NextResponse } from "next/server";
import prisma from "@/lib/prisma";
import { auth } from "@/lib/auth";

export async function GET() {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const stores = await prisma.storeName.findMany({ orderBy: { storeName: "asc" } });
  return NextResponse.json(stores);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const { storeName } = await request.json();
  if (!storeName?.trim()) return NextResponse.json({ error: "Store name required" }, { status: 400 });
  const store = await prisma.storeName.create({ data: { storeName: storeName.trim() } });
  return NextResponse.json(store, { status: 201 });
}
