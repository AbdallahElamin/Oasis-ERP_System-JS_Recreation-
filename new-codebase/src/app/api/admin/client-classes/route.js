import { NextResponse } from "next/server";
import prisma from "@/lib/prisma";
import { auth } from "@/lib/auth";

export async function GET() {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const classes = await prisma.clientClass.findMany({ orderBy: { name: "asc" } });
  return NextResponse.json(classes);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const { name } = await request.json();
  const rec = await prisma.clientClass.create({ data: { name } });
  return NextResponse.json(rec, { status: 201 });
}
