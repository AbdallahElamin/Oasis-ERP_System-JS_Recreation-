import { NextResponse } from "next/server";
import { getToken } from "next-auth/jwt";

// Paths that don't require authentication
const PUBLIC_PATHS = ["/login", "/api/auth"];

export async function middleware(req) {
  const { pathname } = req.nextUrl;

  // Allow public paths
  const isPublic = PUBLIC_PATHS.some((path) => pathname.startsWith(path));
  if (isPublic) return NextResponse.next();

  // Check for a valid JWT session token
  const token = await getToken({
    req,
    secret: process.env.NEXTAUTH_SECRET,
  });

  if (!token) {
    const loginUrl = new URL("/login", req.url);
    loginUrl.searchParams.set("callbackUrl", pathname);
    return NextResponse.redirect(loginUrl);
  }

  return NextResponse.next();
}

export const config = {
  // Match all paths except static files and Next.js internals
  matcher: ["/((?!_next/static|_next/image|favicon.ico|public/).*)"],
};
