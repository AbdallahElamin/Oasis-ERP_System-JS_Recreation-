import { PrismaClient } from "@prisma/client";
import { PrismaMariaDb } from "@prisma/adapter-mariadb";
import mariadb from "mariadb";

function createPrismaClient() {
  const pool = mariadb.createPool({
    host: process.env.DB_HOST || "localhost",
    port: parseInt(process.env.DB_PORT || "3306", 10),
    user: process.env.DB_USER || "root",
    password: process.env.DB_PASSWORD || "",
    database: process.env.DB_NAME || "oasis_erp",
    connectionLimit: 5,
  });

  const adapter = new PrismaMariaDb(pool);
  return new PrismaClient({ adapter });
}

// Prevent multiple Prisma client instances in development (hot reload issue)
const globalForPrisma = globalThis;

const prisma = globalForPrisma.prisma ?? createPrismaClient();

if (process.env.NODE_ENV !== "production") {
  globalForPrisma.prisma = prisma;
}

export default prisma;
