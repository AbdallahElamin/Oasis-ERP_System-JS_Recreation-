import { NextResponse } from "next/server";
import prisma from "@/lib/prisma";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });
  const { searchParams } = new URL(request.url);
  const type = searchParams.get("type");
  if (type === "distributor") {
    const agents = await prisma.agentDistributor.findMany({ orderBy: { name: "asc" } });
    return NextResponse.json(agents);
  }
  if (type === "representative") {
    const reps = await prisma.agentRepresentative.findMany({ orderBy: { name: "asc" } });
    return NextResponse.json(reps);
  }
  const [distributors, reps] = await Promise.all([
    prisma.agentDistributor.findMany(),
    prisma.agentRepresentative.findMany(),
  ]);
  return NextResponse.json({ distributors, representatives: reps });
}
