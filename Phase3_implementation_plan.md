# Phase 3 — Live Dashboard, Sales Returns & Reports, Print System, Financial Polish, Barcode Generator

## Background

Phase 1 built the core architecture (Auth, DB pool, Inventory/Sales basics). Phase 2 delivered all remaining pages (33 routes, zero build errors): Financial System (CoA, Vouchers, Bills, Trial Balance, Statements), missing ERP pages (Stores, Transfer, Dispose, Stock Status, Quotations), HR module (Employees, Payroll, Vacations), and Admin pages (Users, Regions, Agents).

**What remains:**
1. The dashboard shows static `"—"` values — needs live data from the DB
2. Sales Returns and Sales Reports are stub pages
3. No print/PDF capability — the legacy system used Crystal Reports for invoices, vouchers, statements, etc.
4. Legacy financial features not yet ported: **Voucher Reversal** and **Voucher Approval**
5. Minor sidebar/routing gaps
6. **No barcode generation** — the legacy system used `ZXing` + `BarcodeLib.Barcode` DLLs to generate QR codes and linear barcodes for products. Products enter the system through Items Registry (purchase/registration), and barcodes need to be generated for labelling before items enter the physical vault/stock.

> [!IMPORTANT]
> This plan is written to be self-contained. All SQL column names use the **exact casing from the live MariaDB schema**. The Transactions table has two distinct debit/credit column pairs:
> - `TotalIn` / `TotalOut` — used by Pay/Receipt Bills and Invoice-generated entries
> - `TotalValueIn` / `TotalValueOut` — used by Journal Vouchers
>
> All queries that compute balances must SUM both pairs.

---

## Part A — Live Dashboard

### Problem

The dashboard page ([`page.js`](file:///home/abdallahelamin/Documents/Programming%20&%20Projects/Mahadi_Work/Oasis-ERP_System(JS_Recreation)/new-codebase/src/app/(dashboard)/dashboard/page.js)) is a Server Component that renders static `"—"` values in the four stat cards. The module status badges say "Groundwork" for Finance and HR, which are now fully built.

### Solution

#### [NEW] `src/app/api/dashboard/stats/route.js`

API that queries four aggregate counts in a single request:

```sql
-- 1. Total active clients
SELECT COUNT(*) AS totalClients FROM Clients;

-- 2. Invoices this month
SELECT COUNT(DISTINCT InvNo) AS invoicesThisMonth
FROM Invoices
WHERE YEAR(TransDate) = YEAR(CURDATE()) AND MONTH(TransDate) = MONTH(CURDATE());

-- 3. Distinct stock items with positive quantity
SELECT COUNT(DISTINCT item) AS stockItems
FROM Stock
GROUP BY item
HAVING SUM(COALESCE(QntIn,0)) - SUM(COALESCE(QntOut,0)) > 0;
-- Return the count of groups, not the groups themselves

-- 4. Active employees
SELECT COUNT(*) AS activeEmployees FROM Employees WHERE IsActive = 1;
```

Return: `{ totalClients, invoicesThisMonth, stockItems, activeEmployees }`

Auth check required (same as all other APIs).

#### [MODIFY] `src/app/(dashboard)/dashboard/page.js`

**Convert from Server Component to Client Component** (`"use client"`) so it can fetch live data.

Changes:
- Add `useEffect` to fetch `/api/dashboard/stats` on mount
- Replace static `"—"` values with fetched numbers
- Add a loading shimmer state while data loads
- Update module status badges: Finance and HR → `"Active"`, not `"Groundwork"`
- Add a 5th stat card: **Cash Balance** — fetch from `/api/finance/statements/balance?acc1=Assets&acc2=Current Assets&acc3=Cash&acc4=Cash on Hand` (reuse existing endpoint)
- Add a **Recent Activity** section below Quick Actions showing the last 10 transactions:
  ```sql
  SELECT MoveNo, TransType, Acc4, TotalIn, TotalOut, TotalValueIn, TotalValueOut, employee, TransDate
  FROM Transactions
  ORDER BY TransDate DESC, SNo DESC
  LIMIT 10
  ```

#### [NEW] `src/app/api/dashboard/activity/route.js`

Returns last 10 transactions for the recent activity feed.

---

## Part B — Sales Returns (Full Implementation)

### Problem

Currently a stub page. Legacy `frmReturndInvoice.vb` allows looking up an invoice by `InvNo`, displaying its line items, and "returning" them — which restores stock and (optionally) creates reversing journal entries.

### Solution

#### [NEW] `src/app/api/sales/returns/route.js`

**GET** — Look up an invoice by InvNo:

```sql
SELECT StoreName, item, BatchNo, pack, price, Rpric, Qnt, Disc, VAT,
       NetAmount, CustID, CustName, AmountInWords
FROM Invoices
WHERE InvNo = ? AND YEAR(TransDate) = ?
```

Query params: `invNo`, `year` (default current year)

**POST** — Process the return:

Body: `{ invNo, year, custId, custName, netAmount, lines: [{ storeName, item, batchNo, pack, wPrice, rPrice, qnt }] }`

Logic (in a single DB transaction):

```
1. For each returned line:
   INSERT INTO Stock (StoreName, item, BatchNo, pack, WPrice, RPrice, QntIn,
     details, employee, TransType)
   VALUES (?, ?, ?, ?, ?, ?, ?, 'Recovered Invoice# {invNo}', ?, 'Returned Invoice')

2. Create reversing journal entries (matching legacy logic):
   a. Get next MoveNo for the year
   b. Debit: Acc1="Purchase & Sales", Acc2="Sales", Acc3="Sales", Acc4="Sales"
      → TotalIn = netAmount (reversing the original credit)
   c. Credit: Acc1="Assets", Acc2="Current Assets", Acc3="Clients", Acc4=custName
      → TotalOut = netAmount (reducing the receivable)
   d. Ref = "Return Invoice# {invNo}"
```

> [!NOTE]
> The legacy code has the financial journal entries commented out (`' ''''Financial Part`). Our implementation will include them since the Phase 2 financial system is fully active. This makes the return invoice produce proper accounting trail.

#### [MODIFY] `src/app/(dashboard)/sales/returns/page.js`

Replace stub with full Client Component:

- **Lookup section**: Input field for Invoice Number + Year selector + "Load" button
- **Invoice details display**: Shows CustID, CustName, Discount, VAT, Net Amount, Amount in Words (read-only)
- **Items grid**: Table showing all line items from the invoice. Each row has:
  - StoreName, Item, BatchNo, Pack, W.Price, R.Price, Qty
  - Checkbox or editable Qty field to select which items / quantities to return
- **Actions bar**:
  - "Return Selected" button → POST to `/api/sales/returns`
  - "Clear" button → resets form
- **Success state**: After return, show a confirmation message with the MoveNo of the reversing journal entry

---

## Part C — Sales Reports

### Problem

Currently a stub page. Legacy `frmSalesReports.vb` supports two report types:
1. **Sales by Item**: Groups `Invoices` by `item`, sums `price * Qnt` for each item in a date range
2. **Sales by Month**: Groups by `YEAR(TransDate), MONTH(TransDate)`, sums total sales per month

Both used Crystal Reports. We replace with in-page HTML tables + `@media print` CSS for browser printing.

### Solution

#### [NEW] `src/app/api/sales/reports/route.js`

**GET** — Sales reports with query param `type`:

When `type=by-item`:
```sql
SELECT item, SUM(Rpric * Qnt) AS totalSales, SUM(Qnt) AS totalQty, COUNT(DISTINCT InvNo) AS invoiceCount
FROM Invoices
WHERE TransDate >= ? AND TransDate <= ?
GROUP BY item
ORDER BY totalSales DESC
```

When `type=by-month`:
```sql
SELECT YEAR(TransDate) AS yr, MONTH(TransDate) AS mo,
       SUM(Rpric * Qnt) AS totalSales, COUNT(DISTINCT InvNo) AS invoiceCount
FROM Invoices
WHERE YEAR(TransDate) = ?
GROUP BY YEAR(TransDate), MONTH(TransDate)
ORDER BY MONTH(TransDate) ASC
```

Additional query params: `from`, `to` (for by-item), `year` (for by-month).

When `type=by-client`:
```sql
SELECT CustName, SUM(Rpric * Qnt) AS totalSales, SUM(Qnt) AS totalQty, COUNT(DISTINCT InvNo) AS invoiceCount
FROM Invoices
WHERE TransDate >= ? AND TransDate <= ?
GROUP BY CustName
ORDER BY totalSales DESC
```

#### [MODIFY] `src/app/(dashboard)/sales/reports/page.js`

Replace stub. Client Component with three tabs:

**Tab 1 — Sales by Item:**
- Date range picker (From / To)
- "Generate" button
- Results table: Item | Total Qty | Invoice Count | Total Sales
- Grand total row
- "Print" button → `window.print()` — styled with `@media print`

**Tab 2 — Sales by Month:**
- Year selector dropdown
- "Generate" button
- Results table: Month | Invoice Count | Total Sales
- Yearly total row

**Tab 3 — Sales by Client:**
- Date range picker
- Results table: Client | Total Qty | Invoice Count | Total Sales

Each tab includes a bar chart visualization using simple CSS bar widths (no external charting library needed — keep it lightweight):
```html
<div style="width: {percentage}%; background: var(--accent-primary); height: 8px; border-radius: 4px" />
```

---

## Part D — Print-Ready HTML Layouts

### Problem

The legacy system used Crystal Reports (`.rpt` files) for all printed documents: Invoices, Quotations, Pay Vouchers, Receipt Vouchers, Account Statements, Trial Balance, etc. We need a web-native replacement.

### Approach

Use dedicated `/print` route pages with `@media print` CSS. Each print page:
- Is a standalone page that fetches data server-side (or client-side, then calls `window.print()`)
- Has a clean, professional layout optimized for A4 paper
- Includes company header, document title, data table, and signatures section
- Uses `@media print` rules to hide nav, sidebar, and print button; force white background; set proper margins

### Print CSS Foundation

#### [NEW] `src/app/globals.css` (append)

```css
/* ─── Print Utilities ─── */
@media print {
  body { background: white !important; color: black !important; }
  .erp-sidebar, .erp-topbar, .no-print { display: none !important; }
  .print-only { display: block !important; }
  .erp-main { margin-left: 0 !important; padding: 0 !important; }
  .card { box-shadow: none !important; border: 1px solid #ddd !important; }
  
  @page {
    size: A4;
    margin: 15mm 10mm;
  }
}
.print-only { display: none; }

.print-header {
  text-align: center;
  margin-bottom: 1rem;
  padding-bottom: 0.75rem;
  border-bottom: 2px solid #333;
}
.print-header h1 { font-size: 1.2rem; margin: 0; }
.print-header p { font-size: 0.8rem; color: #666; margin: 0.25rem 0 0; }
.print-meta { display: flex; justify-content: space-between; font-size: 0.8rem; margin-bottom: 1rem; }
.print-signatures {
  display: flex; justify-content: space-between;
  margin-top: 3rem; padding-top: 1rem;
}
.print-signatures div {
  text-align: center; min-width: 150px;
  border-top: 1px solid #333; padding-top: 0.5rem;
  font-size: 0.8rem;
}
```

### Print Pages

#### [NEW] `src/app/(dashboard)/sales/invoices/[invNo]/print/page.js`

**Invoice Print Page**

Server Component or Client Component that:
1. Fetches invoice data by `invNo` from: `GET /api/sales/invoices/[invNo]`
2. Renders:
   - **Header**: "Oasis ERP — Tax Invoice" + Invoice No. + Date
   - **Client info**: CustID, CustName
   - **Items table**: Item | BatchNo | Pack | W.Price | R.Price | Qty | Total
   - **Totals section**: Subtotal, Discount %, VAT %, Net Amount
   - **Amount in words**: `AmountInWords` field
   - **Signatures**: Prepared By | Approved By | Received By
3. Auto-prints or shows "Print" button that calls `window.print()`

Need supporting API:

#### [NEW] `src/app/api/sales/invoices/[invNo]/route.js`

```sql
SELECT * FROM Invoices WHERE InvNo = ? AND YEAR(TransDate) = ?
```

Returns all line items for that invoice number.

#### [NEW] `src/app/(dashboard)/sales/quotations/[invNo]/print/page.js`

Identical layout to invoice print but title = "Price Quotation" and fetches from Quotations table.

#### [NEW] `src/app/api/sales/quotations/[invNo]/route.js`

Same as invoice detail but queries `Quotations` table.

#### [NEW] `src/app/(dashboard)/finance/vouchers/[moveNo]/print/page.js`

**Journal Voucher Print Page**

1. Fetch voucher lines: `SELECT * FROM Transactions WHERE MoveNo = ? AND TransType = 'Journal Voucher' AND YEAR(TransDate) = ?`
2. Render:
   - Header: "Journal Voucher" + Move No. + Date
   - Lines table: Acc1 | Acc2 | Acc3 | Acc4 | Description | Debit | Credit
   - Totals row: Total Debit | Total Credit
   - Signatures: Prepared By | Reviewed By | Approved By

#### [NEW] `src/app/(dashboard)/finance/bills/[moveNo]/print/page.js`

**Pay/Receipt Voucher Print Page**

1. Fetch bill lines: `SELECT * FROM Transactions WHERE MoveNo = ? AND TransType IN ('Pay Voucher', 'Receipt Voucher')`
2. Render:
   - Header: "Payment Voucher" or "Receipt Voucher" (based on TransType) + Bill No. (`PaperNo`) + Date
   - Source/Payee: `Source` field
   - Lines table: Account (Acc3 › Acc4) | Amount
   - Total amount
   - Amount in words: `Writting` field
   - Payment method: Cash or Bank (with cheque details if applicable)
   - Signatures: Prepared By | Approved By | Received By

---

## Part E — Financial Polish (Voucher Reversal + Approval)

### Problem

Two legacy features from the Financial System are not yet ported:
1. **Voucher Reversal** (`frmVoucherReverse.vb`): Look up a voucher by MoveNo, reverse it by creating a new voucher with TotalIn/TotalOut swapped, and mark the original as `Reversed=1`
2. **Voucher Approval** (`frmApprovingVouchers.vb`): Approve pending vouchers by setting `Approved=1`

The `Transactions` table already has an `Approved` column (tinyint, default 0). It does **not** have a `Reversed` column — this is a Phase 3 schema addition.

### Schema Change

> [!WARNING]
> This is the only schema change in Phase 3. It adds a nullable column to the existing `Transactions` table.

```sql
ALTER TABLE Transactions ADD COLUMN Reversed TINYINT(1) NOT NULL DEFAULT 0;
```

Run this migration manually or via an API-triggered migration script.

### E1. Voucher Reversal

#### [NEW] `src/app/api/finance/vouchers/reverse/route.js`

**POST** — Reverse a voucher:

Body: `{ moveNo, year }`

Logic (in a transaction):
```
1. Fetch all lines: SELECT * FROM Transactions WHERE MoveNo = ? AND YEAR(TransDate) = ?
2. If no rows found → 404
3. If already reversed (Reversed = 1 on any row) → 409 Conflict
4. Get next MoveNo for the year
5. For each original line:
   INSERT INTO Transactions with:
   - Same Acc1-Acc4
   - TotalIn = original.TotalOut (swap)
   - TotalOut = original.TotalIn (swap)
   - TotalValueIn = original.TotalValueOut (swap)
   - TotalValueOut = original.TotalValueIn (swap)
   - Ref = "Reversing voucher # {originalMoveNo}"
   - TransType = "Journal Voucher"
6. UPDATE Transactions SET Reversed = 1 WHERE MoveNo = {originalMoveNo} AND YEAR(TransDate) = {year}
7. Return { newMoveNo }
```

#### [NEW] `src/app/(dashboard)/finance/vouchers/reverse/page.js`

Client Component:
- **Lookup section**: Year selector + MoveNo input + "Load" button
- **Voucher display**: Table showing the voucher's lines (Acc1-4, Description, Debit, Credit)
- **Status indicator**: Shows "Active" or "Reversed" based on `Reversed` flag
- **"Reverse Voucher" button**: Confirmation dialog → POST → shows new MoveNo
- "Print" link to the new reversal voucher

### E2. Voucher Approval

#### [NEW] `src/app/api/finance/vouchers/approve/route.js`

**GET** — List unapproved vouchers:
```sql
SELECT DISTINCT MoveNo, MIN(TransDate) AS TransDate, MIN(employee) AS employee
FROM Transactions
WHERE Approved = 0
GROUP BY MoveNo
ORDER BY MoveNo DESC
```

**POST** — Approve a voucher:
Body: `{ moveNo }`
```sql
UPDATE Transactions SET Approved = 1 WHERE MoveNo = ?
```

#### [NEW] `src/app/(dashboard)/finance/vouchers/approve/page.js`

Client Component:
- **Left panel**: List of pending voucher MoveNos (clickable)
- **Right panel**: When a MoveNo is clicked, fetch and display its lines
  - Table: Acc1 | Acc2 | Acc3 | Acc4 | Description | Debit | Credit
  - Running totals at bottom
- **Action buttons**: "Approve" (sets Approved=1), "Delete" (removes from `Transactions` — admin only)

---

## Part F — Sidebar & Navigation Updates

### [MODIFY] [`Sidebar.jsx`](file:///home/abdallahelamin/Documents/Programming%20&%20Projects/Mahadi_Work/Oasis-ERP_System(JS_Recreation)/new-codebase/src/components/layout/Sidebar.jsx)

Add missing nav items to the Finance section:

```diff
 {
   label: "Finance",
   items: [
     // ... existing items ...
+    {
+      href: "/finance/vouchers/reverse",
+      label: "Reverse Voucher",
+      icon: "M4 4v5h.582m15.356 2A8.001 8.001 0 004.582 9m0 0H9m11 11v-5h-.581m0 0a8.003 8.003 0 01-15.357-2m15.357 2H15",
+    },
+    {
+      href: "/finance/vouchers/approve",
+      label: "Approve Vouchers",
+      icon: "M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z",
+    },
   ],
 },
```

Also add print links to invoice/quotation list pages:
- In the invoices table, each row gets a small "Print" icon/link → `/sales/invoices/{invNo}/print`
- Same for quotations

---

## File Summary

### Part A — Dashboard
| Action | File |
|--------|------|
| NEW | `src/app/api/dashboard/stats/route.js` |
| NEW | `src/app/api/dashboard/activity/route.js` |
| MODIFY | `src/app/(dashboard)/dashboard/page.js` — live data + recent activity |

---

### Part B — Sales Returns
| Action | File |
|--------|------|
| NEW | `src/app/api/sales/returns/route.js` |
| MODIFY | `src/app/(dashboard)/sales/returns/page.js` — full implementation |

---

### Part C — Sales Reports
| Action | File |
|--------|------|
| NEW | `src/app/api/sales/reports/route.js` |
| MODIFY | `src/app/(dashboard)/sales/reports/page.js` — 3-tab report page |

---

### Part D — Print System
| Action | File |
|--------|------|
| MODIFY | `src/app/globals.css` — append print CSS utilities |
| NEW | `src/app/api/sales/invoices/[invNo]/route.js` |
| NEW | `src/app/(dashboard)/sales/invoices/[invNo]/print/page.js` |
| NEW | `src/app/api/sales/quotations/[invNo]/route.js` |
| NEW | `src/app/(dashboard)/sales/quotations/[invNo]/print/page.js` |
| NEW | `src/app/(dashboard)/finance/vouchers/[moveNo]/print/page.js` |
| NEW | `src/app/(dashboard)/finance/bills/[moveNo]/print/page.js` |

---

### Part E — Financial Polish
| Action | File |
|--------|------|
| NEW | `src/app/api/finance/vouchers/reverse/route.js` |
| NEW | `src/app/(dashboard)/finance/vouchers/reverse/page.js` |
| NEW | `src/app/api/finance/vouchers/approve/route.js` |
| NEW | `src/app/(dashboard)/finance/vouchers/approve/page.js` |

---

### Part F — Navigation
| Action | File |
|--------|------|
| MODIFY | `src/components/layout/Sidebar.jsx` — add Reverse + Approve links |
| MODIFY | `src/app/(dashboard)/sales/invoices/page.js` — add Print column |
| MODIFY | `src/app/(dashboard)/sales/quotations/page.js` — add Print column |

---

## Schema Migration

```sql
-- Run once before implementing Part E
ALTER TABLE Transactions ADD COLUMN IF NOT EXISTS Reversed TINYINT(1) NOT NULL DEFAULT 0;
```

---

## Verification Plan

### Automated Tests
```bash
# 1. Build check — zero errors
npm run build

# 2. DB schema verification
mariadb -u root -pabdallah -D oasis_erp -e "DESCRIBE Transactions;" | grep Reversed
```

### Manual Verification

1. **Dashboard**: Login → dashboard stat cards show real numbers (not "—")
2. **Sales Returns**: Enter an existing InvNo → items load → return them → verify Stock table gets `QntIn` rows with `TransType = 'Returned Invoice'`
3. **Sales Reports**: Generate "Sales by Item" with a date range that has data → table renders
4. **Print Invoice**: Navigate to an invoice → click Print → browser print dialog shows clean A4 layout
5. **Print Voucher**: Same for a journal voucher
6. **Voucher Reversal**: Load an existing MoveNo → click Reverse → verify new reversed MoveNo is created + original marked `Reversed=1`
7. **Voucher Approval**: Navigate to Approve Vouchers → pending list shows → approve one → `Approved` flag updated

---

## Implementation Order

Execute in this exact sequence to minimize broken states:

1. **Schema migration** — `ALTER TABLE Transactions ADD COLUMN Reversed` (~1 minute)
2. **Part A** (Dashboard live data) — ~30 minutes
3. **Part D** (Print CSS + print pages) — ~60 minutes — standalone, no dependencies
4. **Part B** (Sales Returns) — ~45 minutes
5. **Part C** (Sales Reports) — ~45 minutes
6. **Part E** (Voucher Reverse + Approve) — ~45 minutes
7. **Part F** (Sidebar + print links) — ~15 minutes
8. **Verification** — ~20 minutes

---

---

## Part G — Barcode Generator

### Context & Legacy Behaviour

The legacy system used two libraries:
- **`ZXing`** (from `zxing.dll` in `bin/Debug/`) — for QR code generation
- **`BarcodeLib.Barcode.CrystalReports.dll`** — for linear barcodes (Code 128, EAN)

The legacy implementation was a standalone test form (`Form1.vb`) — not integrated into the product flow. **Our implementation goes further**: barcodes are generated for registered products (from `ItemsRegistry`) immediately after they enter the system, before being received into the vault/stock.

**Product flow where barcodes fit:**
```
[Supplier → Purchase Order]
     ↓
[ItemsRegistry — product registered in the system]
     ↓  ← BARCODE GENERATED HERE (Phase 3)
[Print labels before items enter the vault]
     ↓
[Stock/Vault — items received via "Add to Stock" form]
     ↓
[Sales Invoice — items sold out of stock]
```

### Schema Migration

Add a `Barcode` column to `ItemsRegistry` to store the barcode value persistently:

```sql
ALTER TABLE ItemsRegistry
  ADD COLUMN Barcode VARCHAR(100) NULL,
  ADD COLUMN BarcodeType VARCHAR(20) NULL DEFAULT 'CODE128';
```

- `Barcode` — the encoded string value (e.g., `"OAS-0042"` or the item's SNo padded to 12 digits)
- `BarcodeType` — `'CODE128'`, `'EAN13'`, or `'QR'` — determines which format is rendered

Auto-generate on item creation: when a new item is `INSERT`ed into `ItemsRegistry`, the API generates the barcode value as `OAS-{SNo}` (e.g., `OAS-0001`) using the new `insertId`.

> [!NOTE]
> `CODE128` is the best default for pharmaceutical/warehouse use: it encodes any ASCII string, is compact, and is readable by virtually all handheld scanners. EAN-13 is available for items that need retail POS compatibility. QR is available for richer data (item name, pack, price embedded in one scan).

### G1. Schema Migration Script

#### [NEW] `src/lib/migrations/add-barcode-to-items.sql`

```sql
-- Run once before Phase 3 deployment
ALTER TABLE ItemsRegistry
  ADD COLUMN IF NOT EXISTS Barcode VARCHAR(100) NULL,
  ADD COLUMN IF NOT EXISTS BarcodeType VARCHAR(20) NOT NULL DEFAULT 'CODE128';

-- Back-fill barcode values for existing items
UPDATE ItemsRegistry
SET Barcode = CONCAT('OAS-', LPAD(SNo, 4, '0'))
WHERE Barcode IS NULL;
```

### G2. Items Registry API — Barcode Integration

#### [MODIFY] `src/app/api/inventory/items/route.js`

In the **POST** handler (create item), after the `INSERT`, back-fill the barcode:

```js
const result = await pool.query(
  `INSERT INTO ItemsRegistry (item, GenericName, pack, WPrice, RPrice, CompanyName) VALUES (?,?,?,?,?,?)`,
  [item, genericName, pack, wPrice, rPrice, companyName]
);
const newSNo = Number(result.insertId);
const barcodeValue = `OAS-${String(newSNo).padStart(4, '0')}`;
await pool.query(
  `UPDATE ItemsRegistry SET Barcode = ?, BarcodeType = 'CODE128' WHERE SNo = ?`,
  [barcodeValue, newSNo]
);
return NextResponse.json({ SNo: newSNo, barcode: barcodeValue }, { status: 201 });
```

In the **GET** handler (list items), include `Barcode` and `BarcodeType` in the SELECT.

#### [NEW] `src/app/api/inventory/items/[sno]/barcode/route.js`

A dedicated endpoint for per-item barcode operations:

**GET** — Return the barcode data for a single item:
```sql
SELECT SNo, item, GenericName, pack, WPrice, RPrice, CompanyName, Barcode, BarcodeType
FROM ItemsRegistry
WHERE SNo = ?
```
Return: `{ sno, item, genericName, pack, wPrice, rPrice, barcode, barcodeType }`

**PUT** — Override/update the barcode value or type:
```js
// Body: { barcode, barcodeType }
// Validation: barcode must not be empty; barcodeType must be one of CODE128, EAN13, QR
UPDATE ItemsRegistry SET Barcode = ?, BarcodeType = ? WHERE SNo = ?
```
This allows manually assigning an existing barcode (e.g., if the manufacturer's barcode should be used instead of the auto-generated one).

**POST** — Regenerate the barcode (reset to auto-generated value):
```js
// Regenerates: OAS-{SNo padded to 4 digits}
UPDATE ItemsRegistry SET Barcode = CONCAT('OAS-', LPAD(SNo, 4, '0')), BarcodeType = 'CODE128' WHERE SNo = ?
```

#### [NEW] `src/app/api/inventory/items/barcodes/route.js`

Bulk barcode endpoint — returns barcode data for multiple items at once (used by the label sheet printer):

**GET** with query param `snos` (comma-separated list of SNo values):
```sql
SELECT SNo, item, GenericName, pack, WPrice, RPrice, CompanyName, Barcode, BarcodeType
FROM ItemsRegistry
WHERE SNo IN (?)
ORDER BY SNo ASC
```
Return: array of items with barcode data.

### G3. Barcode Generator Page

#### [NEW] `src/app/(dashboard)/inventory/barcodes/page.js`

This is the main barcode management page. Client Component (`"use client"`).

**Library to install:**
```bash
npm install jsbarcode qrcode
```
- **`JsBarcode`** — renders Code 128 and EAN-13 as inline SVG (no canvas/image round-trip)
- **`qrcode`** — renders QR codes as SVG or data URLs

Both libraries are pure client-side: zero server load, instant rendering, zero CORS issues.

**Page layout — three panels:**

```
┌─────────────────────────────────────────────────────────────┐
│  Search & Select Items           │  Barcode Preview         │
│  ┌──────────────────────────┐   │  ┌────────────────────┐  │
│  │ Search: [____________]   │   │  │  |||||||||||||||    │  │
│  │                          │   │  │  OAS-0042           │  │
│  │ ☑ Amoxicillin 500mg     │   │  │  Amoxicillin 500mg  │  │
│  │ ☑ Paracetamol 250mg     │   │  │  Pack: 10's         │  │
│  │ ☐ Ibuprofen 400mg       │   │  │  RPrice: 25.00      │  │
│  │ ...                      │   │  └────────────────────┘  │
│  └──────────────────────────┘   │                          │
│                                  │  Type: [CODE128 ▾]      │
│  [Select All] [Clear]            │  [Regenerate] [Copy]     │
│                                  │                          │
├──────────────────────────────────┴──────────────────────────┤
│  Selected for batch printing: 2 items                        │
│  Labels per item: [1▾] [2] [3] [4] [5]                      │
│  [🖨 Print Label Sheet]  [⬇ Export SVG]                      │
└─────────────────────────────────────────────────────────────┘
```

**Left Panel — Item Selector:**

- Search input (filters by `item`, `GenericName`, `CompanyName`)
- Checkbox list of all items from `GET /api/inventory/items` (showing item name, pack, company)
- "Select All" / "Clear Selection" buttons
- Item count badge: "X items selected"

**Right Panel — Live Barcode Preview:**

When a single item is selected (or the last-checked item in multi-select):
- The barcode SVG renders in real-time using `JsBarcode` into a `<svg ref={svgRef}>` element
- Below the barcode: item name, pack, RPrice
- **Barcode Type Selector** dropdown: `CODE128`, `EAN13`, `QR`
  - CODE128: renders linear barcode via `JsBarcode`
  - EAN13: renders EAN-13 via `JsBarcode` (pads/truncates barcode value to 12 digits)
  - QR: renders QR code via `qrcode` library, embedding `{item}|{batch}|{rPrice}` as the QR data
- **Regenerate button**: calls `POST /api/inventory/items/{sno}/barcode` → resets to `OAS-{SNo}`, refreshes preview
- **Custom value button**: opens an inline input to manually type a barcode string → calls `PUT /api/inventory/items/{sno}/barcode`
- **Copy SVG button**: copies the rendered SVG markup to clipboard

**Bottom Bar — Batch Label Sheet:**

- Shows count of selected items
- **Labels per item** quick-select: 1, 2, 3, 4, 5, or custom number input
- **Print Label Sheet** button → opens a new browser window with the print layout (see G4 below)
- **Export SVG** button → generates an SVG file containing all selected barcodes in a grid, triggers download as `barcodes.svg`

**Implementation detail for barcode rendering:**

```js
// In a useEffect whenever selectedItem or barcodeType changes:
import JsBarcode from 'jsbarcode';
import QRCode from 'qrcode';

// For CODE128 / EAN13:
useEffect(() => {
  if (svgRef.current && selectedItem?.barcode && barcodeType !== 'QR') {
    JsBarcode(svgRef.current, selectedItem.barcode, {
      format: barcodeType,  // 'CODE128' or 'EAN13'
      width: 2,
      height: 60,
      displayValue: true,
      fontSize: 12,
      margin: 10,
    });
  }
}, [selectedItem, barcodeType]);

// For QR:
useEffect(() => {
  if (canvasRef.current && barcodeType === 'QR') {
    const qrData = `${selectedItem.item}|${selectedItem.pack}|${selectedItem.rPrice}`;
    QRCode.toCanvas(canvasRef.current, qrData, { width: 160, margin: 2 });
  }
}, [selectedItem, barcodeType]);
```

Render both `<svg ref={svgRef}>` and `<canvas ref={canvasRef}>` in the DOM; show/hide based on `barcodeType`.

### G4. Print Label Sheet

#### [NEW] `src/app/(dashboard)/inventory/barcodes/print/page.js`

A dedicated print-only page. Receives selected item SNos and label count as URL search params:
```
/inventory/barcodes/print?snos=1,5,12&count=3
```

This page:
1. Fetches item + barcode data from `GET /api/inventory/items/barcodes?snos=1,5,12`
2. Renders a **label sheet grid** — each label cell contains:
   ```
   ┌──────────────────────┐
   │  [BARCODE SVG]       │
   │  OAS-0001            │
   │  Amoxicillin 500mg   │
   │  Pack: 10's          │
   │  RPrice: 25.00 SDG   │
   └──────────────────────┘
   ```
3. If `count=3`, the same label repeats 3 times per item (useful for labelling individual boxes)
4. Uses `@media print` CSS to format as a proper A4 sheet with 3-column label grid:

```css
/* Label sheet: 3 columns × ~10 rows on A4 */
.label-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 4px;
}
.label-cell {
  border: 1px dashed #ccc;
  padding: 6px 4px;
  text-align: center;
  font-size: 9px;
  break-inside: avoid;
}
.label-cell svg { max-width: 100%; height: auto; }

@media print {
  body { margin: 5mm; }
  .no-print { display: none; }
  .label-grid { grid-template-columns: repeat(3, 1fr); }
  .label-cell { border-color: #999; }
}
```

5. **Auto-calls** `window.print()` on load after a 500ms delay (allows SVGs to finish rendering)
6. Includes a "Print" button in the top bar (`.no-print` class hides it in print mode)

### G5. Items Registry Page — Barcode Column

#### [MODIFY] `src/app/(dashboard)/inventory/items/page.js`

Add barcode visibility to the existing items table:

- Add a **Barcode** column to the table showing the `Barcode` value as text (e.g., `OAS-0042`)
- Add a **"🏷 Print"** icon-button in the Actions column for each row:
  - Clicking opens the barcode preview for that item (navigates to `/inventory/barcodes?sno={sno}`)
  - Or opens an inline popover showing the barcode SVG rendered client-side
- Add a **"Barcode Generator"** nav button in the page title bar linking to `/inventory/barcodes`

### G6. Add to Stock Page — Barcode Scanner Input

#### [MODIFY] `src/app/(dashboard)/inventory/stock/page.js`

The Add to Stock page is where items physically enter the vault. Add a **barcode scanner integration** for faster item lookup:

- Add a **"Scan Barcode"** input field at the top of the form (a plain `<input>` with autofocus)
- When a value is typed/scanned and Enter is pressed:
  1. Query `GET /api/inventory/items?barcode={value}` (add `barcode` query param to the existing items API)
  2. If found, **auto-fill** the Item Name, Generic Name, Pack, W.Price, R.Price fields
  3. If not found, show an error: "Barcode not found in registry"
- The scanner input is always visible and auto-focused, making it ergonomic for warehouse staff with a USB/Bluetooth barcode scanner

This requires a small addition to the items API:
```js
// In GET handler of /api/inventory/items/route.js:
const barcode = searchParams.get('barcode');
if (barcode) {
  const rows = await pool.query(
    'SELECT * FROM ItemsRegistry WHERE Barcode = ? LIMIT 1',
    [barcode.trim()]
  );
  return NextResponse.json(rows[0] || null);
}
```

### G7. Sidebar Navigation Update

#### [MODIFY] `src/components/layout/Sidebar.jsx`

Add the Barcode Generator to the ERP/Inventory section:

```diff
 {
   label: "ERP / Inventory",
   items: [
     // ... existing items ...
+    {
+      href: "/inventory/barcodes",
+      label: "Barcode Generator",
+      icon: "M12 4v1m6 11h2m-6 0h-2v4m0-11v3m0 0h.01M12 12h4.01M16 20h4M4 12h4m12 0h.01M5 8h2a1 1 0 001-1V5a1 1 0 00-1-1H5a1 1 0 00-1 1v2a1 1 0 001 1zm12 0h2a1 1 0 001-1V5a1 1 0 00-1-1h-2a1 1 0 00-1 1v2a1 1 0 001 1zM5 20h2a1 1 0 001-1v-2a1 1 0 00-1-1H5a1 1 0 00-1 1v2a1 1 0 001 1z",
+    },
   ],
 },
```

---

## Part G — File Summary

| Action | File |
|--------|------|
| NEW | `src/lib/migrations/add-barcode-to-items.sql` |
| MODIFY | `src/app/api/inventory/items/route.js` — auto-generate barcode on create; add `?barcode=` lookup; include Barcode in GET response |
| NEW | `src/app/api/inventory/items/[sno]/barcode/route.js` — GET/PUT/POST per-item barcode |
| NEW | `src/app/api/inventory/items/barcodes/route.js` — bulk barcode fetch for label sheet |
| NEW | `src/app/(dashboard)/inventory/barcodes/page.js` — full barcode generator UI |
| NEW | `src/app/(dashboard)/inventory/barcodes/print/page.js` — label sheet print layout |
| MODIFY | `src/app/(dashboard)/inventory/items/page.js` — add Barcode column + Print icon |
| MODIFY | `src/app/(dashboard)/inventory/stock/page.js` — add barcode scanner input |
| MODIFY | `src/components/layout/Sidebar.jsx` — add Barcode Generator nav item |

**New dependency:**
```bash
npm install jsbarcode qrcode
```
- `jsbarcode` — ~50KB, renders CODE128/EAN13 into `<svg>` directly, zero server needed
- `qrcode` — ~75KB, renders QR codes into `<canvas>` or as SVG string, pure client-side

---

## Part G — Verification

1. **Auto-generation**: Create a new item in Items Registry → confirm `Barcode` column is set to `OAS-{SNo}` in the DB
2. **Barcode page**: Navigate to `/inventory/barcodes` → select an item → barcode SVG renders correctly
3. **Type switching**: Switch between CODE128, EAN13, and QR — each renders a different visual format
4. **Label sheet**: Select 3 items with 2 labels each → click Print → browser print dialog shows 6 labels in a 3-column grid on A4
5. **Scanner lookup**: On the Add to Stock page, type a valid barcode value (e.g., `OAS-0001`) in the scan input and press Enter → item fields auto-fill
6. **Custom barcode**: Use the custom value input on the barcode page to set a manual barcode string → verify the DB is updated via PUT
7. **Back-fill migration**: After running the SQL migration, verify all existing items have `Barcode` values

---

## Open Questions

> [!IMPORTANT]
> **Company header for printed documents**: The print pages include a company header ("Oasis ERP — Tax Invoice"). Should this be customizable (e.g., stored in a settings table), or is "Oasis ERP" the final company name for all printed documents?

> [!IMPORTANT]
> **Partial returns**: The legacy return invoice form returns **all items** of an invoice at full quantity. Should the new implementation support **partial returns** (returning only some items or reduced quantities)?

> [!IMPORTANT]
> **Approval workflow**: In the legacy system, vouchers are created in a `TempVouchers` staging table, then an approver moves them to `Transactions`. The current DB has no `TempVouchers` table. The plan above uses the simpler approach of writing directly to `Transactions` with `Approved=0`, then toggling the flag. Is this acceptable, or should we create a `TempVouchers` staging table?

> [!IMPORTANT]
> **Barcode format for existing items from manufacturer**: Some pharmaceutical products already have manufacturer EAN-13 barcodes printed on their packaging. Should warehouse staff be able to scan those existing barcodes to register the item (i.e., the system stores the manufacturer's barcode instead of generating `OAS-{SNo}`), and fall back to auto-generate only when no manufacturer barcode is provided?
