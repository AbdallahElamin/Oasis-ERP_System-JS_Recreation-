/**
 * db.js — shared MariaDB connection pool
 *
 * Replaces the Prisma adapter approach (which has pool-init issues with
 * @prisma/adapter-mariadb v7) with a direct mariadb pool. All database
 * access in this project now goes through this singleton pool.
 *
 * Usage:
 *   import pool from "@/lib/db";
 *   const rows = await pool.query("SELECT * FROM Users WHERE id = ?", [id]);
 *   // or:
 *   const conn = await pool.getConnection();
 *   try { await conn.query(...) } finally { conn.release(); }
 */

import mariadb from "mariadb";

const globalForDb = globalThis;

function createPool() {
  return mariadb.createPool({
    host: process.env.DB_HOST || "localhost",
    port: parseInt(process.env.DB_PORT || "3306", 10),
    user: process.env.DB_USER || "root",
    password: process.env.DB_PASSWORD || "",
    database: process.env.DB_NAME || "oasis_erp",
    connectionLimit: 10,
    connectTimeout: 10000,
    // mariadb-specific: enables proper bigint handling
    bigIntAsNumber: true,
  });
}

const pool = globalForDb._mariadbPool ?? createPool();

if (process.env.NODE_ENV !== "production") {
  globalForDb._mariadbPool = pool;
}

export default pool;
