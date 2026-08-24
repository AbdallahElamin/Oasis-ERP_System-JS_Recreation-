// GET  /api/sales/invoices — list invoices grouped by InvNo
// POST /api/sales/invoices — create invoice with full transaction
import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const q = searchParams.get("q") || "";
  const year = parseInt(searchParams.get("year") || String(new Date().getFullYear()), 10);

  let sql = `
    SELECT invNo, custId, custName, disc, vat, transDate, employee,
           SUM(netAmount) as totalNet, SUM(totalSdg) as totalSdg, COUNT(*) as itemCount
    FROM Invoices
    WHERE YEAR(transDate) = ?
  `;
  const params = [year];

  if (q) {
    sql += " AND (custName LIKE ? OR item LIKE ?)";
    params.push(`%${q}%`, `%${q}%`);
  }

  sql += " GROUP BY invNo, custId, custName, disc, vat, transDate, employee ORDER BY invNo DESC";

  const rows = await pool.query(sql, params);
  return NextResponse.json(rows);
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const body = await request.json();
  const { custId, custName, items, discPerc, vatPerc, netAmount, amountInWords } = body;

  if (!custId || !custName || !items?.length) {
    return NextResponse.json({ error: "Customer and at least one item are required" }, { status: 400 });
  }

  const conn = await pool.getConnection();
  try {
    await conn.beginTransaction();

    // Get next InvNo for current year
    const year = new Date().getFullYear();
    const lastRows = await conn.query(
      "SELECT MAX(invNo) as lastNo FROM Invoices WHERE YEAR(transDate) = ?", [year]
    );
    const invNo = (Number(lastRows[0]?.lastNo) || 0) + 1;

    // Insert one row per item
    for (const item of items) {
      await conn.query(
        `INSERT INTO Invoices
          (invNo, custId, custName, storeName, item, batchNo, pack, price, rPrice,
           qnt, disc, vat, netAmount, totalSdg, amountInWords, prescription, employee)
         VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`,
        [invNo, parseInt(custId, 10), custName, item.storeName, item.item,
         item.batchNo || null, item.pack || null,
         parseFloat(item.wPrice) || 0, parseFloat(item.rPrice) || 0,
         parseFloat(item.qnt) || 0, parseFloat(discPerc) || 0, parseFloat(vatPerc) || 0,
         parseFloat(netAmount) || 0, parseFloat(item.total) || 0,
         amountInWords || null, item.description || "Sales", session.user.name]
      );

      // Deduct from stock
      await conn.query(
        `INSERT INTO Stock (storeName, item, batchNo, pack, wPrice, rPrice, qntOut, details, employee, transType)
         VALUES (?,?,?,?,?,?,?,?,?,'Invoice')`,
        [item.storeName, item.item, item.batchNo || null, item.pack || null,
         parseFloat(item.wPrice) || 0, parseFloat(item.rPrice) || 0,
         parseFloat(item.qnt) || 0, `Invoice# ${invNo}`, session.user.name]
      );
    }

    // Get next MoveNo for financial journal entries
    const lastTxRows = await conn.query("SELECT MAX(moveNo) as lastNo FROM Transactions");
    const moveNo = (Number(lastTxRows[0]?.lastNo) || 0) + 1;

    // Debit: Assets > Current Assets > Clients > custName
    await conn.query(
      `INSERT INTO Transactions (moveNo, custId, custName, ref, acc1, acc2, acc3, acc4, totalOut, employee)
       VALUES (?,?,?,?,?,?,?,?,?,?)`,
      [moveNo, parseInt(custId, 10), custName, `Invoice# ${invNo}`,
       "Assets", "Current Assets", "Clients", custName,
       parseFloat(netAmount) || 0, session.user.name]
    );

    // Credit: Purchase & Sales > Sales
    await conn.query(
      `INSERT INTO Transactions (moveNo, custId, custName, ref, acc1, acc2, acc3, acc4, totalIn, employee)
       VALUES (?,?,?,?,?,?,?,?,?,?)`,
      [moveNo, parseInt(custId, 10), custName, `Invoice# ${invNo}`,
       "Purchase & Sales", "Sales", "Sales", "Sales",
       parseFloat(netAmount) || 0, session.user.name]
    );

    await conn.commit();
    return NextResponse.json({ invNo }, { status: 201 });
  } catch (err) {
    await conn.rollback();
    console.error("Invoice creation error:", err);
    return NextResponse.json({ error: "Failed to save invoice" }, { status: 500 });
  } finally {
    conn.release();
  }
}
