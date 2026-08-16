import { defineConfig } from "prisma/config";
import { PrismaMariaDb } from "@prisma/adapter-mariadb";
import mariadb from "mariadb";

export default defineConfig({
  earlyAccess: true,
  schema: "prisma/schema.prisma",
  migrate: {
    adapter: async () => {
      const pool = mariadb.createPool({
        host: process.env.DB_HOST || "localhost",
        port: parseInt(process.env.DB_PORT || "3306", 10),
        user: process.env.DB_USER || "root",
        password: process.env.DB_PASSWORD || "",
        database: process.env.DB_NAME || "oasis_erp",
        connectionLimit: 5,
      });
      return new PrismaMariaDb(pool);
    },
  },
});
