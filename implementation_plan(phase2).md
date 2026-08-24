# Phase 2 — Financial System, Remaining ERP/Sales Pages, and Admin Module

## Background

Phase 1 established the ERP/Inventory and Sales modules (Item Registry, Add to Stock, Clients, Invoices). The database layer was migrated from Prisma to a raw `mariadb` pool (`src/lib/db.js`). Authentication works via `next-auth` with bcrypt + JWT.

This plan covers three areas:
1. **Bug fixes** — immediate issues blocking navigation
2. **Gap filling** — sidebar links that currently 404 (stores, transfer, dispose, stock status, quotations, returns, sales reports, admin pages)
3. **Financial System** — the full Chart of Accounts, Journal Vouchers, Pay/Receipt Bills, Trial Balance, and Account Statements module

> [!IMPORTANT]
> This plan is written to be self-contained. Any model implementing it should be able to follow the steps without needing additional context. All SQL column names use the **exact casing from the live MariaDB schema** (e.g., `StoreName` not `storeName`, `Acc1` not `acc1`).

---

## Part A — Immediate Bug Fixes

### A1. `StoreName` table name mismatch

The actual MariaDB table is **`StoreName`** (singular), but the API route at `src/app/api/inventory/stores/route.js` queries `StoreNames` (plural). The column is also `StoreName` (not `storeName`).

#### [MODIFY] [route.js](file:///home/abdallahelamin/Documents/Programming%20&%20Projects/Mahadi_Work/Oasis-ERP_System(JS_Recreation)/new-codebase/src/app/api/inventory/stores/route.js)

```diff
 // GET handler
-const stores = await pool.query("SELECT * FROM StoreNames ORDER BY storeName ASC");
+const stores = await pool.query("SELECT * FROM StoreName ORDER BY StoreName ASC");

 // POST handler
-const result = await pool.query("INSERT INTO StoreNames (storeName) VALUES (?)", [storeName.trim()]);
+const result = await pool.query("INSERT INTO StoreName (StoreName) VALUES (?)", [storeName.trim()]);
```

### A2. Stock API — column casing

The `Stock` table uses PascalCase columns: `StoreName`, `BatchNo`, `QntIn`, `QntOut`, `WPrice`, `RPrice`, `ExpireDate`, `CompanyName`, `TransType`, `TransDate`. The current API at `src/app/api/inventory/stock/route.js` uses camelCase in SQL. Fix all column references to match the live schema.

Key column mappings:
| API currently uses | Actual DB column |
|---|---|
| `storeName` | `StoreName` |
| `batchNo` | `BatchNo` |
| `qntIn` | `QntIn` |
| `qntOut` | `QntOut` |
| `wPrice` | `WPrice` |
| `rPrice` | `RPrice` |
| `expireDate` | `ExpireDate` |
| `transType` | `TransType` |

Similarly fix `ItemsRegistry` columns: `GenericName` (not `genericName`), `WPrice`, `RPrice`, `CompanyName`.

### A3. Invoices API — column casing

The `Invoices` table uses: `InvNo`, `CustID`, `CustName`, `StoreName`, `BatchNo`, `Rpric` (not `rPrice`), `Qnt`, `Disc`, `VAT`, `NetAmount`, `TotalSDG`, `AmountInWords`, `TransDate`. Fix `src/app/api/sales/invoices/route.js` accordingly.

### A4. Transactions API — column casing

The `Transactions` table uses: `MoveNo` (not `moveNo`), `CustID`, `CustName`, `Ref` (not `ref`), `Acc1`-`Acc4`, `TotalIn`, `TotalOut`, `TotalValueIn`, `TotalValueOut`, `TransType`, `PaymentType`, `Source`, `Writting`, `TransDate`, `PaperNo`, `Package`, `CheqDate`, `Approved`.

The invoice creation route inserts into Transactions with lowercase column names — fix to PascalCase.

### A5. Clients API — column casing

The `Clients` table uses: `SNo` (PK, not `id`), `LicNo`, `TaxNo`, `ClientClass`, `BuildingNo`, `SalesMan`, `MedicalRepresentative`, `PharmacyOwner`, `PharmacyOwnerMob`, `PharmacyDoctor`, `PharmacyDoctorMob`, `UserName`, `CreatedAt`. The `Accs` table (not `Accounts`) uses `Acc1`-`Acc4`.

Fix `src/app/api/clients/route.js` and `src/app/api/clients/[id]/route.js`:
- Use `SNo` instead of `id` for the primary key
- Use correct PascalCase column names
- Change `INSERT INTO Accounts` → `INSERT INTO Accs`

---

## Part B — Missing ERP/Sales/Admin Pages

Each of these is a sidebar link that currently 404s. Create the page + API route for each.

### B1. Stores Management Page

#### [NEW] `src/app/(dashboard)/inventory/stores/page.js`

Client Component (`"use client"`). Features:
- List all stores from `GET /api/inventory/stores`
- "Add Store" button opens an inline form or modal
- POST new store name to `POST /api/inventory/stores`
- Simple table: ID, Store Name, Actions

### B2. Stock Transfer Page

Mirrors legacy `frmItemMove.vb` logic. Transfer items between stores.

#### [NEW] `src/app/api/inventory/transfer/route.js`

POST handler:
1. Validate: `fromStore`, `toStore`, `item`, `batchNo`, `quantity` required
2. In a transaction:
   - INSERT into `Stock` with `QntOut` = quantity, `StoreName` = fromStore, `TransType` = 'Transfer Out'
   - INSERT into `Stock` with `QntIn` = quantity, `StoreName` = toStore, `TransType` = 'Transfer In'
3. Return success

#### [NEW] `src/app/(dashboard)/inventory/transfer/page.js`

Client Component. Form with:
- Dropdown: From Store (fetched from `/api/inventory/stores`)
- Dropdown: To Store
- Autocomplete: Item (fetched from `/api/inventory/items`)
- Input: Batch No, Quantity
- Submit → POST `/api/inventory/transfer`

### B3. Dispose Items Page

Write off expired or damaged stock.

#### [NEW] `src/app/api/inventory/dispose/route.js`

POST handler:
1. Validate: `storeName`, `item`, `batchNo`, `quantity`, `reason`
2. INSERT into `Stock` with `QntOut` = quantity, `TransType` = 'Disposal', `details` = reason
3. Return success

#### [NEW] `src/app/(dashboard)/inventory/dispose/page.js`

Client Component. Form: Store dropdown, Item autocomplete, Batch No, Quantity, Reason textarea. Submit → POST.

### B4. Stock Status Page

Read-only report of current stock levels across all stores.

#### [NEW] `src/app/(dashboard)/inventory/status/page.js`

Client Component. Fetches `GET /api/inventory/stock` (the existing grouped stock endpoint). Displays:
- Filter bar: store dropdown, item search
- Table: Store, Item, Batch, Pack, W.Price, R.Price, Available Qty
- Color-code rows where Available Qty ≤ 0 (out of stock) in red

### B5. Quotations Page

The `Quotations` table has the **exact same schema** as `Invoices` (same columns: `SNo`, `InvNo`, `CustID`, `CustName`, `StoreName`, `item`, `BatchNo`, `pack`, `price`, `Rpric`, `Qnt`, `Disc`, `VAT`, `NetAmount`, `TotalSDG`, `AmountInWords`, `prescription`, `employee`, `TransDate`).

#### [NEW] `src/app/api/sales/quotations/route.js`

Mirror the invoices API but query `Quotations` table instead of `Invoices`. Same GET (list) and POST (create) logic.

#### [NEW] `src/app/(dashboard)/sales/quotations/page.js`

List page — same layout as invoices list but for quotations.

#### [NEW] `src/app/(dashboard)/sales/quotations/new/page.js`

Create form — same as invoice creation form but POSTs to `/api/sales/quotations`. Key difference: quotations do **NOT** create stock deductions or financial journal entries (they are price proposals only).

### B6. Sales Returns Page (Stub)

#### [NEW] `src/app/(dashboard)/sales/returns/page.js`

Stub page with "Coming Soon" message. The return invoice logic is complex (reverse stock + reverse journal entries) and will be implemented in a later phase.

### B7. Sales Reports Page (Stub)

#### [NEW] `src/app/(dashboard)/sales/reports/page.js`

Stub page. Reports will be implemented alongside the print-ready PDF system in a later phase.

### B8. Admin — User Management

#### [NEW] `src/app/api/admin/users/route.js`

- GET: `SELECT id, FullName, role, IsActive, CreatedAt FROM Users ORDER BY id ASC`
- POST: Create user. Hash password with `bcrypt.hashSync(password, 10)`. INSERT into `Users (FullName, Pass, role, IsActive)`.

#### [NEW] `src/app/api/admin/users/[id]/route.js`

- PUT: Update user fields. If `password` is provided, hash it before UPDATE.
- DELETE: Soft-delete by setting `IsActive = 0`.

#### [NEW] `src/app/(dashboard)/admin/users/page.js`

Client Component. Table of users with columns: ID, Full Name, Role, Active, Created. Actions: Edit (inline or modal), Deactivate toggle. "Add User" button.

### B9. Admin — Regions Page

#### [NEW] `src/app/(dashboard)/admin/regions/page.js`

Client Component. Uses existing `/api/admin/regions` endpoint. Three-column cascading view:
- Column 1: States list
- Column 2: Regions for selected state
- Column 3: Areas for selected region
- Add button for each level

### B10. Admin — Sales Agents Page

#### [NEW] `src/app/(dashboard)/admin/sales-agents/page.js`

Client Component. Uses existing `/api/admin/agents` endpoint. Two tabs/sections:
- Distributors table (from `AgentDistributors`)
- Representatives table (from `AgentRepresentatives`)

---

## Part C — Financial System Module (Full Implementation)

This is the core of Phase 2. The financial system revolves around the `Transactions` table and the `Accs` (Chart of Accounts) table.

### Architecture Overview

```
Accs table (Chart of Accounts)
├── Acc1 (Level 1: e.g., "Assets", "Liabilities", "Purchase & Sales")
│   ├── Acc2 (Level 2: e.g., "Current Assets", "Cash & Banks")
│   │   ├── Acc3 (Level 3: e.g., "Cash", "Bank Accounts", "Clients")
│   │   │   └── Acc4 (Level 4: e.g., "Cash on Hand", "Bank ABC", "Client XYZ")

Transactions table (Journal Entries)
├── MoveNo: sequential voucher number per year
├── Acc1-Acc4: which account this line hits
├── TotalIn: credit amount (money coming in to this account)
├── TotalOut: debit amount (money going out of this account)
├── TotalValueIn / TotalValueOut: used for voucher entries
├── TransType: "Pay Voucher", "Receipt Voucher", "Journal Voucher", etc.
├── PaymentType: "C" (cash) or "B" (bank)
├── Source: who/what the payment is from/to
├── Writting: amount in words
├── CheqDate: cheque date (if bank payment)
├── Approved: 0/1
```

### C1. Chart of Accounts (CoA) Tree API

#### [NEW] `src/app/api/finance/accounts/route.js`

**GET** — Returns the full CoA as a nested tree structure:

```js
// Query all accounts
const rows = await pool.query("SELECT DISTINCT Acc1, Acc2, Acc3, Acc4 FROM Accs WHERE Acc1 IS NOT NULL ORDER BY Acc1, Acc2, Acc3, Acc4");

// Build tree: { label, children: [{ label, children: [...] }] }
// Level 0: Acc1 values
// Level 1: Acc2 values under each Acc1
// Level 2: Acc3 values under each Acc1+Acc2
// Level 3: Acc4 values (leaf nodes — these are the actual accounts)
```

Return JSON: `[{ label: "Assets", children: [{ label: "Current Assets", children: [...] }] }]`

**POST** — Add a new account:

```js
// Body: { acc1, acc2, acc3, acc4 }
// INSERT INTO Accs (Acc1, Acc2, Acc3, Acc4) VALUES (?, ?, ?, ?)
// Validate: all 4 levels must be provided for a leaf account
// Check for duplicates before inserting
```

**DELETE** — Remove an account (only if no Transactions reference it):

```js
// Body: { acc1, acc2, acc3, acc4 }
// Check: SELECT COUNT(*) FROM Transactions WHERE Acc1=? AND Acc2=? AND Acc3=? AND Acc4=?
// If count > 0, return 409 Conflict
// Else: DELETE FROM Accs WHERE Acc1=? AND Acc2=? AND Acc3=? AND Acc4=?
```

#### [MODIFY] `src/app/(dashboard)/finance/accounts/page.js`

Replace stub with a full Client Component:

- **Left panel**: Interactive tree view of the CoA (collapsible nodes)
  - Level 0 nodes: Acc1 values (e.g., "Assets", "Liabilities")
  - Level 1 nodes: Acc2 values
  - Level 2 nodes: Acc3 values  
  - Level 3 nodes (leaves): Acc4 values — clicking shows balance
- **Right panel**: "Add Account" form
  - 4 text inputs: Level 1, Level 2, Level 3, Level 4
  - When a tree node is clicked at level 0-2, auto-fill the parent levels
  - Save button → POST `/api/finance/accounts`
- **Balance display**: When a leaf node (Acc4) is clicked, fetch and show:
  ```sql
  SELECT COALESCE(SUM(TotalIn) - SUM(TotalOut), 0) AS balance
  FROM Transactions
  WHERE Acc1 = ? AND Acc2 = ? AND Acc3 = ? AND Acc4 = ?
  ```

### C2. Journal Vouchers

This is the core voucher creation form from the legacy `frmMakeVoucher.vb`.

#### [NEW] `src/app/api/finance/vouchers/route.js`

**GET** — List vouchers (grouped by MoveNo):

```sql
SELECT MoveNo, MIN(TransDate) as TransDate, MIN(employee) as employee,
       SUM(TotalValueIn) as totalCredit, SUM(TotalValueOut) as totalDebit,
       COUNT(*) as lineCount
FROM Transactions
WHERE TransType = 'Journal Voucher'
  AND YEAR(TransDate) = ?  -- query param: year (default current year)
GROUP BY MoveNo
ORDER BY MoveNo DESC
```

**POST** — Create a new journal voucher:

Body: `{ date, lines: [{ acc1, acc2, acc3, acc4, description, debit, credit }] }`

Validation:
1. `lines` array must not be empty
2. Total debits must **exactly equal** total credits (balanced voucher)
3. Each line must have all 4 account levels

Logic (in a transaction):
```js
const conn = await pool.getConnection();
await conn.beginTransaction();

// Get next MoveNo for current year
const [{ lastNo }] = await conn.query(
  "SELECT COALESCE(MAX(MoveNo), 0) AS lastNo FROM Transactions WHERE YEAR(TransDate) = YEAR(CURDATE())"
);
const moveNo = lastNo + 1;

for (const line of lines) {
  await conn.query(
    `INSERT INTO Transactions (MoveNo, Acc1, Acc2, Acc3, Acc4, Ref, TotalValueIn, TotalValueOut, TransType, employee, TransDate)
     VALUES (?, ?, ?, ?, ?, ?, ?, ?, 'Journal Voucher', ?, ?)`,
    [moveNo, line.acc1, line.acc2, line.acc3, line.acc4, line.description,
     line.credit || 0, line.debit || 0, session.user.name, date]
  );
}

await conn.commit();
```

#### [MODIFY] `src/app/(dashboard)/finance/vouchers/page.js`

Replace stub. Two views:

**List View** (default):
- Year selector dropdown
- Table: Move No, Date, Employee, Total Debit, Total Credit, Line Count
- Click row → view voucher detail (expand in-place or navigate)

**Create View** (button "New Voucher" or route `/finance/vouchers/new`):

#### [NEW] `src/app/(dashboard)/finance/vouchers/new/page.js`

Client Component. Layout mirrors legacy `frmMakeVoucher`:

- **Left side**: CoA tree (fetched from `/api/finance/accounts`) — clicking a leaf (level 3) auto-fills Acc1-Acc4
- **Right side**: Voucher builder form
  - Date picker (defaults to today)
  - "Add Line" section:
    - Acc1, Acc2, Acc3, Acc4 fields (auto-filled from tree, or manual)
    - Type dropdown: "Debit" or "Credit"
    - Amount input
    - Description input
    - "Add" button → adds row to the grid below
  - **Voucher Grid** (table):
    - Columns: Acc1 | Acc2 | Acc3 | Acc4 | Description | Debit | Credit | Remove
    - Each row shows the line. "Remove" button deletes the row.
  - **Totals bar** below the grid:
    - Total Debit: `Σ debit`
    - Total Credit: `Σ credit`
    - Balance: `credit - debit` (must be 0.00 to save)
  - **Save button**: disabled unless balance === 0. POST to `/api/finance/vouchers`.

### C3. Payment Voucher (Pay Bill)

From legacy `frmMakePayBill.vb`. Records outgoing payments (cash or bank cheque).

#### [NEW] `src/app/api/finance/bills/pay/route.js`

**POST** — Create a payment voucher:

Body: `{ date, source, description, paymentType ("cash"|"bank"), chequeNo, bankName, chequeDate, amountInWords, lines: [{ acc1, acc2, acc3, acc4, amount }] }`

Logic (in a transaction):
```js
// Get next MoveNo
// Get next SNo2 for "Pay Voucher" + paymentType

// For each line (credit side — the expense accounts):
INSERT INTO Transactions (MoveNo, TransType, PaymentType, SNo2, Source, Ref,
  Acc1, Acc2, Acc3, Acc4, ChNo, Writting, TotalOut, employee, TransDate)

// Debit side (the cash/bank account):
// If cash: Acc1="Current Assets", Acc2="Cash & Banks", Acc3="Cash", Acc4="Cash on Hand"
// If bank: Acc1="Current Assets", Acc2="Cash & Banks", Acc3="Bank Accounts", Acc4=bankName
INSERT INTO Transactions (MoveNo, TransType, PaymentType, SNo2, Source, Ref,
  Acc1, Acc2, Acc3, Acc4, ChNo, CheqDate, Writting, TotalIn, employee, TransDate)
```

#### [NEW] `src/app/api/finance/bills/receipt/route.js`

**POST** — Mirror of pay bill but reversed:
- Lines use `TotalIn` (the revenue/asset accounts)
- Counter-entry uses `TotalOut` on the cash/bank account

### C4. Bills Archive

#### [MODIFY] `src/app/(dashboard)/finance/bills/page.js`

Replace stub. Client Component with:
- Tab bar: "Pay Vouchers" | "Receipt Vouchers" | "All"
- Date range filter (from/to)
- Fetch from a new GET endpoint

#### [NEW] `src/app/api/finance/bills/route.js`

**GET** — List bills:
```sql
SELECT MoveNo, SNo2, TransType, PaymentType, Source, Writting,
       SUM(TotalIn) as totalIn, SUM(TotalOut) as totalOut,
       MIN(TransDate) as transDate, MIN(employee) as employee
FROM Transactions
WHERE TransType IN ('Pay Voucher', 'Receipt Voucher')
  AND TransDate > ? AND TransDate < ?
GROUP BY MoveNo, SNo2, TransType, PaymentType, Source, Writting
ORDER BY MoveNo DESC
```

Query params: `from`, `to`, `type` (optional: "pay" or "receipt")

### C5. Trial Balance

#### [MODIFY] `src/app/(dashboard)/finance/trial-balance/page.js`

Replace stub. Client Component:
- Date range picker (From / To)
- "Show" button → fetches data
- Results table: Acc1 | Acc2 | Acc3 | Acc4 | Debit | Credit
- Totals row at bottom

#### [NEW] `src/app/api/finance/trial-balance/route.js`

**GET** — Trial balance report:
```sql
SELECT Acc1, Acc2, Acc3, Acc4,
       SUM(TotalIn) - SUM(TotalOut) AS balance
FROM Transactions
WHERE TransDate > ? AND TransDate < ?
GROUP BY Acc1, Acc2, Acc3, Acc4
ORDER BY Acc1, Acc2, Acc3, Acc4
```

Query params: `from` (date), `to` (date)

Return the rows. On the client side:
- If `balance > 0` → show in Credit column
- If `balance < 0` → show absolute value in Debit column
- Calculate total Debit and total Credit sums

### C6. Account Statement

#### [MODIFY] `src/app/(dashboard)/finance/statements/page.js`

Replace stub. Client Component with two panels:

**Left panel**: CoA tree (same tree component as vouchers page — extract to `src/components/finance/AccountTree.js`)

**Right panel**:
- Date range picker
- "Show" button (enabled when a tree node is selected)
- Statement table:
  - First row: "Opening Balance" (sum of all transactions before the `from` date for the selected account level)
  - Subsequent rows: individual transactions within the date range
  - Columns: Move No | Date | Description | Trans Type | Debit | Credit | Running Balance

#### [NEW] `src/app/api/finance/statements/route.js`

**GET** — Account statement:

Query params: `from`, `to`, `acc1`, `acc2` (optional), `acc3` (optional), `acc4` (optional)

Logic (mirrors legacy `frmStatement.vb`):
```sql
-- Opening balance (all transactions before 'from' date at the selected level)
SELECT COALESCE(SUM(TotalOut) - SUM(TotalIn), 0) AS openingBalance
FROM Transactions
WHERE Acc1 = ? [AND Acc2 = ?] [AND Acc3 = ?] [AND Acc4 = ?]
  AND TransDate < ?

-- Period transactions
SELECT MoveNo, Ref, TransType, Acc1, Acc2, Acc3, Acc4,
       TotalIn, TotalOut, TransDate
FROM Transactions
WHERE Acc1 = ? [AND Acc2 = ?] [AND Acc3 = ?] [AND Acc4 = ?]
  AND TransDate >= ? AND TransDate <= ?
ORDER BY TransDate ASC, MoveNo ASC
```

The WHERE clause depth depends on which tree level was selected (level 0 = Acc1 only, level 3 = all four).

---

## Part D — Shared Components to Extract

### D1. Account Tree Component

#### [NEW] `src/components/finance/AccountTree.js`

Client Component. Reusable across CoA page, Voucher creation, and Statement page.

Props:
- `onSelect(node)` — callback when a node is clicked. `node` = `{ acc1, acc2, acc3, acc4, level }`
- `selectable` — which levels can be selected (default: all)

Fetches tree data from `/api/finance/accounts` on mount. Renders a collapsible tree with expand/collapse arrows. Selected node is highlighted.

### D2. Date Range Picker Component

#### [NEW] `src/components/common/DateRangePicker.js`

Client Component. Two `<input type="date">` fields (From / To) with sensible defaults (start of year → today).

Props:
- `from`, `to` — controlled values
- `onChange({ from, to })` — callback

---

## Part E — Database Schema Adjustments

> [!WARNING]
> The `Transactions` table columns `TotalIn`/`TotalOut` vs `TotalValueIn`/`TotalValueOut` serve different purposes in the legacy system:
> - `TotalIn`/`TotalOut`: Used by Pay/Receipt Bills (the cash/bank counter-entry)
> - `TotalValueIn`/`TotalValueOut`: Used by Journal Vouchers
> - Invoice-generated entries use `TotalIn`/`TotalOut`
>
> The new code must respect this distinction.

No new tables need to be created. All required tables already exist in the database. The only schema work is ensuring column name casing matches in all SQL queries.

---

## File Summary

### Bug Fixes (Part A)
| Action | File |
|--------|------|
| MODIFY | `src/app/api/inventory/stores/route.js` — fix table name `StoreName` |
| MODIFY | `src/app/api/inventory/stock/route.js` — fix column casing |
| MODIFY | `src/app/api/inventory/items/route.js` — fix column casing |
| MODIFY | `src/app/api/sales/invoices/route.js` — fix column casing |
| MODIFY | `src/app/api/clients/route.js` — fix PK to `SNo`, table `Accs`, column casing |
| MODIFY | `src/app/api/clients/[id]/route.js` — fix PK to `SNo` |

### New Pages (Part B)
| Action | File |
|--------|------|
| NEW | `src/app/(dashboard)/inventory/stores/page.js` |
| NEW | `src/app/api/inventory/transfer/route.js` |
| NEW | `src/app/(dashboard)/inventory/transfer/page.js` |
| NEW | `src/app/api/inventory/dispose/route.js` |
| NEW | `src/app/(dashboard)/inventory/dispose/page.js` |
| NEW | `src/app/(dashboard)/inventory/status/page.js` |
| NEW | `src/app/api/sales/quotations/route.js` |
| NEW | `src/app/(dashboard)/sales/quotations/page.js` |
| NEW | `src/app/(dashboard)/sales/quotations/new/page.js` |
| NEW | `src/app/(dashboard)/sales/returns/page.js` — stub |
| NEW | `src/app/(dashboard)/sales/reports/page.js` — stub |
| NEW | `src/app/api/admin/users/route.js` |
| NEW | `src/app/api/admin/users/[id]/route.js` |
| NEW | `src/app/(dashboard)/admin/users/page.js` |
| NEW | `src/app/(dashboard)/admin/regions/page.js` |
| NEW | `src/app/(dashboard)/admin/sales-agents/page.js` |

### Financial System (Part C)
| Action | File |
|--------|------|
| NEW | `src/app/api/finance/accounts/route.js` |
| MODIFY | `src/app/(dashboard)/finance/accounts/page.js` — full CoA tree |
| NEW | `src/app/api/finance/vouchers/route.js` |
| MODIFY | `src/app/(dashboard)/finance/vouchers/page.js` — voucher list |
| NEW | `src/app/(dashboard)/finance/vouchers/new/page.js` — voucher builder |
| NEW | `src/app/api/finance/bills/route.js` — bills list |
| NEW | `src/app/api/finance/bills/pay/route.js` — pay voucher |
| NEW | `src/app/api/finance/bills/receipt/route.js` — receipt voucher |
| MODIFY | `src/app/(dashboard)/finance/bills/page.js` — bills archive + create |
| NEW | `src/app/api/finance/trial-balance/route.js` |
| MODIFY | `src/app/(dashboard)/finance/trial-balance/page.js` — report |
| NEW | `src/app/api/finance/statements/route.js` |
| MODIFY | `src/app/(dashboard)/finance/statements/page.js` — account statement |

### Shared Components (Part D)
| Action | File |
|--------|------|
| NEW | `src/components/finance/AccountTree.js` |
| NEW | `src/components/common/DateRangePicker.js` |

---

## Verification Plan

### Automated Tests
```bash
# Verify the dev server starts without errors
npm run dev

# Quick DB connectivity test
node -e "const m=require('mariadb');const p=m.createPool({host:'127.0.0.1',user:'root',password:'abdallah',database:'oasis_erp'});p.query('SELECT 1').then(()=>{console.log('OK');p.end()})"
```

### Manual Verification
1. **Login** with ID `1`, password `admin123` — should succeed
2. **Navigate every sidebar link** — no 404s
3. **Stores page**: verify the store list loads (table `StoreName` is queried correctly)
4. **Stock page**: verify items + stores dropdowns load correctly
5. **Chart of Accounts**: verify the tree loads from `Accs` table, clicking a leaf shows its balance
6. **Journal Voucher creation**: add 2+ lines (1 debit, 1 credit), verify balance must be 0, save, verify row appears in `Transactions`
7. **Trial Balance**: pick date range, verify grouped balances display
8. **Account Statement**: select an account from tree, pick date range, verify opening balance + transactions display

---

## Implementation Order

Execute in this exact sequence to minimize broken states:

1. **Part A** (bug fixes) — ~30 minutes
2. **Part D** (shared components) — ~20 minutes
3. **Part C1** (CoA API + page) — ~45 minutes
4. **Part C2** (Journal Vouchers API + pages) — ~60 minutes
5. **Part C5** (Trial Balance API + page) — ~30 minutes
6. **Part C6** (Account Statement API + page) — ~45 minutes
7. **Part C3 + C4** (Pay/Receipt Bills + Archive) — ~60 minutes
8. **Part B** (remaining pages) — ~90 minutes
9. **Verification** — ~30 minutes
