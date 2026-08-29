import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

export async function GET(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { searchParams } = new URL(request.url);
  const invNo = parseInt(searchParams.get("invNo"), 10);
  const year = parseInt(searchParams.get("year") || String(new Date().getFullYear()), 10);

  if (!invNo) return NextResponse.json({ error: "invNo required" }, { status: 400 });

  const rows = await pool.query(
    `SELECT SNo, InvNo, CustID, CustName, StoreName, item, BatchNo, pack,
            price, Rpric, Qnt, Disc, VAT, NetAmount, TotalSDG, AmountInWords, employee, TransDate
     FROM Invoices
     WHERE InvNo = ? AND YEAR(TransDate) = ?`,
    [invNo, year]
  );

  if (!rows.length) return NextResponse.json({ error: "Invoice not found" }, { status: 404 });

  // Group header from first row, lines from all rows
  const header = {
    invNo: rows[0].InvNo,
    custId: rows[0].CustID,
    custName: rows[0].CustName,
    disc: rows[0].Disc,
    vat: rows[0].VAT,
    netAmount: rows[0].NetAmount,
    amountInWords: rows[0].AmountInWords,
    transDate: rows[0].TransDate,
    employee: rows[0].employee,
  };
  const lines = rows.map((r) => ({
    sno: r.SNo,
    storeName: r.StoreName,
    item: r.item,
    batchNo: r.BatchNo,
    pack: r.pack,
    price: r.price,
    rpric: r.Rpric,
    qnt: r.Qnt,
  }));

  return NextResponse.json({ header, lines });
}

export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { invNo, year, custId, custName, netAmount, lines } = await request.json();

  if (!invNo || !lines?.length) {
    return NextResponse.json({ error: "invNo and lines are required" }, { status: 400 });
  }

  const conn = await pool.getConnection();
  try {
    await conn.beginTransaction();

    // 1. Restore stock for each returned line
    for (const line of lines) {
      await conn.query(
        `INSERT INTO Stock (StoreName, item, BatchNo, pack, WPrice, RPrice, QntIn, details, employee, TransType)
         VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, 'Returned Invoice')`,
        [
          line.storeName,
          line.item,
          line.batchNo || "",
          line.pack || "",
          parseFloat(line.price) || 0,
          parseFloat(line.rpric) || 0,
          parseFloat(line.qnt) || 0,
          `Recovered Invoice#${invNo}`,
          session.user?.name || "system",
        ]
      );
    }

    // 2. Get next MoveNo for the year
    const [{ lastNo }] = await conn.query(
      "SELECT COALESCE(MAX(MoveNo), 0) AS lastNo FROM Transactions WHERE YEAR(TransDate) = ?",
      [year || new Date().getFullYear()]
    );
    const moveNo = Number(lastNo) + 1;
    const now = new Date().toISOString().slice(0, 19).replace("T", " ");

    // 3. Reversing journal entries
    // Debit: Purchase & Sales > Sales (reverse the original revenue credit)
    await conn.query(
      `INSERT INTO Transactions (MoveNo, CustID, CustName, Ref, Acc1, Acc2, Acc3, Acc4,
         TotalIn, TransType, employee, TransDate)
       VALUES (?,?,?,?,?,?,?,?,?,?,?,?)`,
      [moveNo, custId || null, custName || "", `Return Invoice#${invNo}`,
       "Purchase & Sales", "Sales", "Sales", "Sales",
       parseFloat(netAmount) || 0, "Journal Voucher", session.user?.name || "system", now]
    );
    // Credit: Assets > Current Assets > Clients (reduce receivable)
    await conn.query(
      `INSERT INTO Transactions (MoveNo, CustID, CustName, Ref, Acc1, Acc2, Acc3, Acc4,
         TotalOut, TransType, employee, TransDate)
       VALUES (?,?,?,?,?,?,?,?,?,?,?,?)`,
      [moveNo, custId || null, custName || "", `Return Invoice#${invNo}`,
       "Assets", "Current Assets", "Clients", custName || "Client",
       parseFloat(netAmount) || 0, "Journal Voucher", session.user?.name || "system", now]
    );

    await conn.commit();
    return NextResponse.json({ success: true, moveNo }, { status: 201 });
  } catch (err) {
    await conn.rollback();
    console.error("Return invoice error:", err);
    return NextResponse.json({ error: "Failed to process return" }, { status: 500 });
  } finally {
    conn.release();
  }
}
