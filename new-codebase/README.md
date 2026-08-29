# Oasis ERP System — JavaScript Recreation

> A full-featured, multi-module Enterprise Resource Planning (ERP) web application. A ground-up JavaScript recreation of the original legacy VB.NET WinForms desktop application, migrated to a modern web stack while faithfully preserving all business logic, data structures, and workflows.

---

## Table of Contents

1. [Overview](#overview)
2. [Tech Stack & Dependencies](#tech-stack--dependencies)
3. [Architecture Overview](#architecture-overview)
4. [Prerequisites](#prerequisites)
5. [Setup & Installation](#setup--installation)
6. [Environment Variables](#environment-variables)
7. [Database Setup](#database-setup)
8. [Seeding the First User](#seeding-the-first-user)
9. [Running the Application](#running-the-application)
10. [Full Project Structure](#full-project-structure)
11. [Authentication & Security](#authentication--security)
12. [Module Reference](#module-reference)
13. [API Routes Reference](#api-routes-reference)
14. [Database Schema (Prisma)](#database-schema-prisma)
15. [Shared Utilities & Libraries](#shared-utilities--libraries)
16. [Design System & Styling](#design-system--styling)
17. [Print & Reporting](#print--reporting)
18. [Barcode & QR Code Generation](#barcode--qr-code-generation)
19. [Module Status Summary](#module-status-summary)
20. [Original System Reference](#original-system-reference)
21. [Development Notes & Known Gotchas](#development-notes--known-gotchas)

---

## Overview

The **Oasis ERP System** is a comprehensive business management platform originally built as a VB.NET WinForms desktop application (Visual Studio 2010). This project is a full JavaScript recreation that maps every module and business rule to a modern, browser-based web application.

**Key goals of this recreation:**

- Preserve 100% of original business logic (pricing, stock deduction, financial double-entry, payroll calculations).
- Replace Crystal Reports with browser-native `@media print` HTML/CSS layouts.
- Replace Microsoft SQL Server with MariaDB, accessed via a raw connection pool for performance and reliability.
- Replace Telerik RadControls with a fully custom CSS design system (dark-mode, responsive).
- Replace BarcodeLib with `jsbarcode` (CODE128, EAN13) and `qrcode` (QR codes) generated client-side.

---

## Tech Stack & Dependencies

### Production Dependencies

| Package | Version | Purpose |
|---|---|---|
| `next` | `16.3.1` | Full-stack React framework (App Router, API Routes, SSR/SSG) |
| `react` | `19.2.8` | UI library |
| `react-dom` | `19.2.8` | React DOM renderer |
| `next-auth` | `^5.0.0-beta.32` | Authentication (NextAuth.js v5, Credentials provider + JWT) |
| `@prisma/client` | `^7.9.1` | Prisma ORM client (used for schema/migration management) |
| `@prisma/adapter-mariadb` | `^7.9.1` | Prisma MariaDB adapter |
| `mariadb` | `^3.5.3` | Native MariaDB Node.js driver — **primary runtime DB access** |
| `bcryptjs` | `^3.0.3` | Password hashing and comparison (cost factor 10) |
| `jsbarcode` | `^3.12.3` | Client-side barcode generation (CODE128, EAN13) |
| `qrcode` | `^1.5.4` | Client-side QR code generation (Canvas-based) |

### Dev Dependencies

| Package | Version | Purpose |
|---|---|---|
| `prisma` | `^7.9.1` | Prisma CLI — used only for `db push` / schema management |
| `tailwindcss` | `^4` | Utility CSS (used selectively alongside the custom CSS system) |
| `@tailwindcss/postcss` | `^4` | PostCSS plugin for Tailwind v4 |
| `eslint` | `^9` | JavaScript linter |
| `eslint-config-next` | `16.3.1` | ESLint rules for Next.js |

### Framework Details

- **Next.js App Router** — All pages use the `/app` directory with React Server Components where appropriate and `"use client"` directives for interactive pages.
- **API Routes** — All backend logic is served from `src/app/api/**` using Next.js Route Handlers.
- **Database access** — A singleton `mariadb` connection pool (`src/lib/db.js`) is used for **all** runtime queries. Prisma is used **only** for schema management (`npx prisma db push`). This decision was made due to pool-initialization issues with `@prisma/adapter-mariadb` v7.

---

## Architecture Overview

```
Browser (React 19 / Next.js 16)
         │
         ▼
 Next.js App Router
  ├── /app/(dashboard)/**  ← All authenticated page routes
  ├── /app/login           ← Public login page
  └── /app/api/**          ← REST-style API Route Handlers
         │
         ▼
 src/middleware.js          ← JWT-based auth guard (all non-public routes)
         │
         ▼
 src/lib/db.js              ← MariaDB singleton connection pool (10 connections)
         │
         ▼
 MariaDB / MySQL Database   ← oasis_erp schema (managed by Prisma schema.prisma)
```

**Data flow pattern:**
1. The browser renders pages from `(dashboard)/**`.
2. Interactive pages fetch data from `/api/**` endpoints using `fetch()`.
3. API route handlers validate the session (via NextAuth.js), run raw SQL queries through the MariaDB pool, and return JSON.
4. Pages update their state and re-render.

---

## Prerequisites

Before you begin, ensure the following are installed and running:

1. **Node.js 18 or higher** — [Download](https://nodejs.org)
   - Verify: `node --version`
2. **MariaDB 10.6+ or MySQL 8+** — running locally or remotely
   - Verify: `mariadb --version` or `mysql --version`
3. **npm** (bundled with Node.js)
   - Verify: `npm --version`

> **Note:** The application uses MariaDB-specific features (e.g., `bigIntAsNumber` pool config). MySQL 8 is compatible but test thoroughly if deviating from MariaDB.

---

## Setup & Installation

### Step 1 — Install Dependencies

Navigate to the `new-codebase` directory and install all dependencies:

```bash
cd "new-codebase"
npm install
```

This installs all production and development dependencies including Next.js, Prisma, NextAuth, MariaDB driver, jsbarcode, qrcode, and Tailwind CSS.

### Step 2 — Configure Environment Variables

Copy the example environment file and edit it with your local values:

```bash
cp .env.example .env.local
```

See the [Environment Variables](#environment-variables) section for the full reference.

### Step 3 — Create the Database

```sql
-- Connect to your MariaDB/MySQL server and run:
CREATE DATABASE oasis_erp CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

Then push the Prisma schema to create all tables:

```bash
npx prisma db push
```

This reads `prisma/schema.prisma` and creates all 18 tables with the correct columns, types, and relations.

### Step 4 — Seed the First Admin User

```bash
node -e "
const { PrismaClient } = require('@prisma/client');
const bcrypt = require('bcryptjs');
const prisma = new PrismaClient();
bcrypt.hash('admin123', 10).then(hash => {
  return prisma.user.create({ data: { fullName: 'Admin', pass: hash, role: 'admin' } });
}).then(user => {
  console.log('User created! ID:', user.id);
  process.exit(0);
}).catch(e => { console.error(e); process.exit(1); });
"
```

Default credentials: **User ID** = `1` (or the printed ID), **Password** = `admin123`.

> After the first user is created, additional users can be managed through **Administration → User Management** in the UI.

### Step 5 — Start the Development Server

```bash
npm run dev
```

Open [http://localhost:3000](http://localhost:3000) in your browser. You will be redirected to the login page automatically.

### Available npm Scripts

| Script | Command | Description |
|---|---|---|
| `dev` | `next dev` | Start development server with hot-reloading |
| `build` | `next build` | Build production-optimized bundle |
| `start` | `next start` | Start the production server (requires `build` first) |
| `lint` | `eslint` | Run ESLint on the codebase |

---

## Environment Variables

All variables are defined in `.env.local` (copied from `.env.example`).

```env
# ─── MariaDB Connection (used by src/lib/db.js at runtime) ───────────────────
DB_HOST="localhost"          # Database server hostname
DB_PORT="3306"               # Database port (default: 3306)
DB_USER="root"               # Database username
DB_PASSWORD=""               # Database password (empty string = no password)
DB_NAME="oasis_erp"          # Database name

# ─── Prisma DATABASE_URL (used ONLY by Prisma CLI for db push/migrate) ───────
# Format: mysql://USER:PASSWORD@HOST:PORT/DATABASE
DATABASE_URL="mysql://root:@localhost:3306/oasis_erp"

# ─── NextAuth.js ──────────────────────────────────────────────────────────────
# NEXTAUTH_SECRET: Signs/verifies JWT tokens. MUST be changed for production.
# Generate with: openssl rand -base64 32
NEXTAUTH_SECRET="your-super-secret-key-change-this"

# NEXTAUTH_URL: Canonical URL of your deployment.
NEXTAUTH_URL="http://localhost:3000"
```

> **Security Warning:** Never commit `.env.local` to version control. The `.gitignore` already excludes it. Always use a randomly generated `NEXTAUTH_SECRET` in production (minimum 32 characters).

---

## Database Setup

### Database: MariaDB (MySQL-compatible)

The application uses **MariaDB** as its primary database. The Prisma schema (`prisma/schema.prisma`) defines the full data model which is applied to the database using `npx prisma db push`.

### Connection Pool Configuration (`src/lib/db.js`)

The runtime connection is managed by a singleton `mariadb` pool with these settings:

| Setting | Value | Description |
|---|---|---|
| `connectionLimit` | `10` | Maximum concurrent connections |
| `connectTimeout` | `10000 ms` | Connection attempt timeout |
| `bigIntAsNumber` | `true` | Returns BIGINT/INT columns as JS numbers (prevents JSON serialization errors) |
| Pool singleton | `globalThis._mariadbPool` | Survives Next.js HMR in development without exhausting connections |

### Schema Management

After editing `prisma/schema.prisma`, sync changes to the database:

```bash
npx prisma db push
```

> **Note:** `db push` applies schema changes directly without creating migration history files. For production environments, consider using `prisma migrate dev` to maintain a proper migration history.

---

## Full Project Structure

```
new-codebase/
│
├── prisma/
│   └── schema.prisma              # Full Prisma data model — all 18 tables defined here
│
├── src/
│   ├── middleware.js               # Edge middleware — JWT auth guard for all routes
│   │
│   ├── app/
│   │   ├── layout.js              # Root HTML layout — imports globals.css
│   │   ├── page.js                # Root page — redirects to /dashboard
│   │   ├── globals.css            # Complete custom CSS design system (~15KB):
│   │   │                          #   CSS variables, dark theme, cards, buttons,
│   │   │                          #   tables, badges, forms, animations, print rules
│   │   │
│   │   ├── login/
│   │   │   └── page.js            # Public login page (not auth-guarded)
│   │   │
│   │   ├── (dashboard)/           # Auth-required route group
│   │   │   ├── layout.js          # Dashboard shell: Sidebar + Header + main area
│   │   │   │
│   │   │   ├── dashboard/
│   │   │   │   ├── page.js        # Home: live KPIs, quick actions, activity feed
│   │   │   │   └── api/           # Local API: stats + activity endpoints
│   │   │   │
│   │   │   ├── inventory/
│   │   │   │   ├── items/         # Items Registry — product catalog CRUD
│   │   │   │   │   ├── page.js
│   │   │   │   │   └── api/
│   │   │   │   ├── stock/         # Add to Stock — batch ingest into store
│   │   │   │   ├── stores/        # Store name management
│   │   │   │   ├── transfer/      # Inter-store stock transfer
│   │   │   │   │   ├── page.js
│   │   │   │   │   └── api/
│   │   │   │   ├── dispose/       # Stock disposal / write-off
│   │   │   │   │   ├── page.js
│   │   │   │   │   └── api/
│   │   │   │   ├── status/        # Stock status report (available qty per item/store)
│   │   │   │   │   ├── page.js
│   │   │   │   │   └── api/
│   │   │   │   └── barcodes/
│   │   │   │       └── page.js    # Barcode/QR generator (CODE128, EAN13, QR)
│   │   │   │
│   │   │   ├── clients/
│   │   │   │   ├── page.js        # Client list + inline registration form
│   │   │   │   ├── new/           # Dedicated new client page
│   │   │   │   └── [id]/          # Client detail / edit
│   │   │   │
│   │   │   ├── sales/
│   │   │   │   ├── invoices/
│   │   │   │   │   ├── page.js    # Invoice list with search and filters
│   │   │   │   │   ├── new/
│   │   │   │   │   │   └── page.js  # Create invoice (multi-line, stock deduction)
│   │   │   │   │   └── [invNo]/
│   │   │   │   │       └── print/ # Printable invoice layout
│   │   │   │   ├── quotations/
│   │   │   │   │   ├── page.js    # Quotation list
│   │   │   │   │   ├── new/
│   │   │   │   │   │   └── page.js  # Create quotation (no stock deduction)
│   │   │   │   │   └── [invNo]/   # View/print specific quotation
│   │   │   │   ├── returns/       # Sales return / invoice reversal
│   │   │   │   │   ├── page.js
│   │   │   │   │   └── api/
│   │   │   │   └── reports/       # Sales reports with date-range filtering
│   │   │   │       ├── page.js
│   │   │   │       └── api/
│   │   │   │
│   │   │   ├── finance/
│   │   │   │   ├── accounts/      # Chart of Accounts (4-level hierarchy)
│   │   │   │   │   ├── page.js
│   │   │   │   │   └── api/
│   │   │   │   ├── vouchers/      # Journal Vouchers — create, list, view
│   │   │   │   │   ├── page.js    # Voucher list
│   │   │   │   │   ├── new/       # New voucher entry
│   │   │   │   │   ├── [moveNo]/  # View specific voucher
│   │   │   │   │   ├── approve/   # Bulk approve pending vouchers
│   │   │   │   │   └── reverse/   # Reverse (void) a posted voucher
│   │   │   │   ├── bills/         # Supplier bills archive
│   │   │   │   │   ├── page.js
│   │   │   │   │   ├── [moveNo]/
│   │   │   │   │   └── api/
│   │   │   │   ├── trial-balance/ # Trial balance report (date-range)
│   │   │   │   │   ├── page.js
│   │   │   │   │   └── api/
│   │   │   │   ├── statements/    # Per-account transaction history
│   │   │   │   │   ├── page.js
│   │   │   │   │   └── api/
│   │   │   │   ├── balance-sheet/ # Balance sheet (groundwork)
│   │   │   │   ├── budget/        # Budget management (groundwork)
│   │   │   │   ├── cheques/       # Cheque tracking (groundwork)
│   │   │   │   ├── pay/           # Pay vouchers (groundwork)
│   │   │   │   └── receipt/       # Receipt vouchers (groundwork)
│   │   │   │
│   │   │   ├── hr/
│   │   │   │   ├── employees/     # Employee registry (list, new, edit)
│   │   │   │   │   ├── page.js
│   │   │   │   │   ├── new/
│   │   │   │   │   └── [id]/
│   │   │   │   ├── payroll/       # Monthly payroll processing & approval
│   │   │   │   │   ├── page.js
│   │   │   │   │   └── api/
│   │   │   │   ├── vacations/     # Vacation requests & approval workflow
│   │   │   │   ├── departments/   # Department management
│   │   │   │   ├── jobs/          # Job description management
│   │   │   │   ├── contracts/     # Employee contracts (groundwork)
│   │   │   │   └── appraisals/    # Performance appraisals (groundwork)
│   │   │   │
│   │   │   └── admin/
│   │   │       ├── users/         # System user management
│   │   │       ├── regions/       # Geographic hierarchy (State→Region→Area)
│   │   │       ├── sales-agents/  # Sales agent / distributor registry
│   │   │       └── med-reps/      # Medical representative registry
│   │   │
│   │   └── api/                   # All backend API Route Handlers
│   │       ├── auth/[...nextauth]/ # NextAuth.js (sign-in, sign-out, session, CSRF)
│   │       ├── dashboard/
│   │       │   ├── stats/         # KPI aggregation endpoint
│   │       │   └── activity/      # Recent transaction feed endpoint
│   │       ├── clients/           # Client CRUD
│   │       ├── inventory/
│   │       │   ├── items/         # Items CRUD
│   │       │   ├── stock/         # Stock ingestion
│   │       │   ├── stores/        # Store management
│   │       │   ├── transfer/      # Inter-store transfer
│   │       │   └── dispose/       # Stock disposal
│   │       ├── sales/
│   │       │   ├── invoices/      # Invoice CRUD + stock deduction + financial entry
│   │       │   ├── quotations/    # Quotation CRUD (no stock deduction)
│   │       │   ├── returns/       # Return processing (stock reversal)
│   │       │   └── reports/       # Sales aggregation reports
│   │       ├── finance/
│   │       │   ├── accounts/      # Chart of accounts CRUD
│   │       │   ├── vouchers/      # Journal voucher CRUD + approve + reverse
│   │       │   ├── bills/         # Bills archive
│   │       │   ├── trial-balance/ # Trial balance query
│   │       │   └── statements/    # Account statement query
│   │       ├── hr/
│   │       │   ├── employees/     # Employee CRUD
│   │       │   ├── payroll/       # PaySheet generation and approval
│   │       │   └── vacations/     # Vacation request management
│   │       └── admin/
│   │           ├── users/         # System user management
│   │           ├── regions/       # Geographic lookup management
│   │           ├── agents/        # Sales agent/distributor management
│   │           ├── client-classes/# Client classification lookup
│   │           ├── departments/   # Department lookup
│   │           └── grade-levels/  # Salary grade level management
│   │
│   ├── components/
│   │   ├── layout/
│   │   │   ├── Sidebar.jsx        # Sidebar: all nav sections, active link detection
│   │   │   ├── Header.jsx         # Top header: session user info + sign-out button
│   │   │   └── Providers.jsx      # SessionProvider wrapper for NextAuth.js
│   │   ├── common/
│   │   │   └── DateRangePicker.js # Reusable from/to date range input component
│   │   ├── finance/
│   │   │   └── AccountTree.js     # 4-level collapsible account hierarchy browser
│   │   ├── forms/                 # (Reserved — form components)
│   │   ├── print/                 # (Reserved — print layout components)
│   │   └── ui/                    # (Reserved — generic UI components)
│   │
│   └── lib/
│       ├── auth.js                # NextAuth.js config (Credentials provider + JWT callbacks)
│       ├── db.js                  # Singleton MariaDB connection pool (primary DB access)
│       ├── prisma.js              # Prisma client singleton (for migrations only)
│       ├── utils.js               # spellNumber(), formatCurrency(), todayStr()
│       └── migrations/            # Manual SQL migration reference scripts
│
├── public/                        # Static public assets
├── .env.example                   # Environment variable template (safe to commit)
├── .env.local                     # Local secrets — git-ignored
├── .gitignore
├── next.config.mjs                # Next.js configuration
├── postcss.config.mjs             # PostCSS + Tailwind v4 configuration
├── jsconfig.json                  # JS path aliases: @/ → src/
├── eslint.config.mjs              # ESLint v9 flat config
├── prisma.config.js               # Prisma configuration (DATABASE_URL reference)
└── package.json
```

---

## Authentication & Security

### Provider: NextAuth.js v5 (Credentials + JWT)

Authentication is handled entirely by **NextAuth.js v5** configured in `src/lib/auth.js`.

**Login method:** Numeric User ID + bcrypt-hashed Password (no email required — matches original VB.NET login flow).

**Authentication flow:**

1. User submits User ID + Password on `/login`.
2. NextAuth `Credentials` provider calls the `authorize()` function.
3. `authorize()` queries the `Users` table via the MariaDB pool.
4. Checks `isActive` flag — inactive users are rejected.
5. Compares the submitted password against the stored bcrypt hash via `bcrypt.compare()`.
6. On success, returns `{ id, name, role }` which is encoded into a signed JWT.
7. The JWT is stored in a secure, HTTP-only cookie managed by NextAuth.

**JWT Callbacks:**

| Callback | Action |
|---|---|
| `jwt` | Injects `id` and `role` into the token on first sign-in |
| `session` | Exposes `id` and `role` on the client session object (`session.user.id`, `session.user.role`) |

**Session strategy:** `"jwt"` — fully stateless; no server-side session storage or database lookup required per request.

### Route Protection: Middleware (`src/middleware.js`)

All routes are protected by the Next.js edge middleware:

```
Public (no auth required):  /login, /api/auth/**
Protected (JWT required):    Everything else
```

If no valid JWT is found in the request, the middleware redirects to `/login?callbackUrl=<original-path>`. After successful login, NextAuth automatically redirects back to the original URL.

**Excluded from middleware matching:** `/_next/static/**`, `/_next/image/**`, `/favicon.ico`, `/public/**`.

### Password Security

- All passwords are hashed with **bcrypt** at cost factor **10**.
- Passwords from the original VB.NET system (stored in plain text) are **incompatible** — all users must be re-created.
- Passwords are never stored, logged, or returned in any API response.

---

## Module Reference

### Dashboard

**Route:** `/dashboard`
**File:** `src/app/(dashboard)/dashboard/page.js`

The home screen displayed immediately after login. Fetches live data from two API endpoints and renders:

**KPI Stat Cards** (each links to its respective module):

| Card | API Source | Metric |
|---|---|---|
| Total Clients | `/api/dashboard/stats` | Count of all registered clients |
| Invoices This Month | `/api/dashboard/stats` | Invoices created in the current calendar month |
| Stock Items | `/api/dashboard/stats` | Distinct stock entries across all stores and batches |
| Active Employees | `/api/dashboard/stats` | Employees with `isActive = true` |

**Quick Actions panel:** One-click links to the most commonly used create/new workflows:
- New Invoice → `/sales/invoices/new`
- Add Stock → `/inventory/stock`
- New Client → `/clients`
- New Quotation → `/sales/quotations/new`
- New Voucher → `/finance/vouchers/new`
- Barcode Labels → `/inventory/barcodes`

**Recent Activity feed** (`/api/dashboard/activity`): The 10 most recent `Transactions` records, color-coded by type:

| Transaction Type | Indicator Color |
|---|---|
| Journal Voucher | Indigo |
| Pay Voucher | Red |
| Receipt Voucher | Green |
| Sales Invoice | Amber |
| Returned Invoice | Orange |

**Module status cards:** Overview cards for ERP/Inventory, Financial System, and Human Resources — each with a live status badge and direct link.

---

### Inventory Module

**Base route:** `/inventory`

#### Items Registry

**Route:** `/inventory/items` | **API:** `/api/inventory/items`

Full CRUD interface for the product catalog (`ItemsRegistry` table).

| Field | Type | Description |
|---|---|---|
| `item` | `String` (UNIQUE) | Product name — globally unique constraint |
| `genericName` | `String?` | Generic/scientific name (for pharmaceuticals) |
| `pack` | `String?` | Pack size/description (e.g., "30 Tablets") |
| `wPrice` | `Float` | Wholesale price (default `0.00`) |
| `rPrice` | `Float` | Retail price (default `0.00`) |
| `companyName` | `String?` | Manufacturer/supplier name |

**Capabilities:** Paginated list, live search (name, generic name, company), inline create form, edit, delete.

#### Add to Stock

**Route:** `/inventory/stock` | **API:** `/api/inventory/stock`

Records incoming stock into the `Stock` table.

| Field | Type | Description |
|---|---|---|
| `storeName` | `String` | Destination store |
| `item` | `String` | Item name |
| `batchNo` | `String?` | Batch / lot number |
| `qntIn` | `Float` | Quantity received |
| `wPrice` | `Float` | Wholesale price at time of receipt |
| `rPrice` | `Float` | Retail price at time of receipt |
| `expireDate` | `DateTime?` | Expiry date for this batch |
| `details` | `String?` | Notes |
| `employee` | `String?` | Receiving employee |
| `transType` | `String?` | Transaction type label |
| `transDate` | `DateTime` | Date/time of receipt |

#### Stores Management

**Route:** `/inventory/stores` | **API:** `/api/inventory/stores`

Manages the `StoreName` lookup table. Stores are referenced by all stock, invoice, and transfer transactions.

#### Stock Transfer (Inter-Store)

**Route:** `/inventory/transfer` | **API:** local `api/` folder

Moves stock between stores. Atomically creates:
1. A `Stock` record with `qntOut` in the **source** store.
2. A `Stock` record with `qntIn` in the **destination** store.

Both records share the same batch reference and `transType = "Transfer"`.

#### Stock Disposal / Write-Off

**Route:** `/inventory/dispose` | **API:** local `api/` folder

Records disposal of expired or damaged stock. Creates a `Stock` record with `qntOut` and `transType = "Disposed"`.

#### Stock Status Report

**Route:** `/inventory/status` | **API:** local `api/` folder

Aggregated available-quantity report. Computes `SUM(qntIn) - SUM(qntOut)` per item/store/batch. Supports filtering by store, item name, and company.

#### Barcode & QR Code Generator

**Route:** `/inventory/barcodes`
**File:** `src/app/(dashboard)/inventory/barcodes/page.js`

A fully client-side label generation tool.

| Format | Library | Notes |
|---|---|---|
| `CODE128` | `jsbarcode` | General-purpose 1D barcode — works with any string |
| `EAN13` | `jsbarcode` | EAN-13 standard — requires exactly 12 or 13 digits |
| `QR` | `qrcode` | 2D QR code — rendered to HTML `<canvas>` |

**Full capabilities:**
- Search and select items from the registry (live search).
- Multi-select with checkboxes for bulk label printing.
- Choose barcode format per print run.
- Set label count per item (for printing full label sheets).
- Live preview: SVG for CODE128/EAN13, Canvas for QR.
- Inline barcode value editing with EAN13 digit validation.
- Save edited barcode value back to the `ItemsRegistry` record via API.
- Print label sheet with `@media print` layout (sidebar/header hidden).
- Toast notification system for save success/error feedback.

---

### Sales Module

**Base route:** `/sales`

#### Clients Registry

**Route:** `/clients` | **API:** `/api/clients`

Full CRUD for the client database (`Clients` table). This is the master client registry used for all invoices and financial transactions.

| Field | Type | Description |
|---|---|---|
| `name` | `String` | Client / pharmacy name |
| `licNo` | `String?` | License number |
| `taxNo` | `String?` | Tax registration number |
| `mobile` | `String?` | Primary mobile |
| `clientClass` | `String?` | Classification from `ClientClasses` lookup |
| `state` | `String?` | State — cascading from `Regions` lookup |
| `region` | `String?` | Region — filtered by selected state |
| `area` | `String?` | Area — filtered by selected region |
| `city` / `town` / `district` / `street` / `buildingNo` | `String?` | Full address fields |
| `salesMan` | `String?` | Assigned distributor from `AgentDistributors` |
| `medicalRepresentative` | `String?` | Assigned rep from `AgentRepresentatives` |
| `pharmacyOwner` | `String?` | Owner name |
| `pharmacyOwnerMob` | `String?` | Owner mobile |
| `pharmacyDoctor` | `String?` | In-house doctor |
| `pharmacyDoctorMob` | `String?` | Doctor mobile |
| `userName` | `String?` | Internal reference |

**Cascading Geography:** State selection dynamically loads Regions; Region selection loads Areas — matching the original VB.NET ComboBox cascade behavior.

**Capabilities:** Live search (debounced 300ms), inline registration form, full CRUD.

#### Sales Invoices

**Routes:**
- List: `/sales/invoices`
- Create: `/sales/invoices/new`
- View: `/sales/invoices/[invNo]`
- Print: `/sales/invoices/[invNo]/print`

**API:** `/api/sales/invoices`

Multi-line invoice creation with automatic stock deduction and financial ledger posting.

**Invoice Header:**

| Field | Description |
|---|---|
| `invNo` | Auto-incremented invoice number |
| `custId` / `custName` | Client reference (FK to `Clients`) |
| `storeName` | Source store for stock deduction |
| `employee` | Issuing employee name |
| `transDate` | Invoice date |

**Per Line Item:**

| Field | Description |
|---|---|
| `item` | Product name |
| `batchNo` | Batch/lot number |
| `pack` | Pack size |
| `price` | Unit wholesale price |
| `rPrice` | Unit retail price |
| `qnt` | Quantity sold |
| `disc` | Discount % |
| `vat` | VAT % |
| `netAmount` | `price × qnt × (1 - disc/100) × (1 + vat/100)` |
| `totalSdg` | Total in Sudanese Pounds |
| `amountInWords` | Output of `spellNumber()` — e.g., "Five Hundred SDG and No Piastre Only" |
| `prescription` | Prescription reference (pharmaceutical use) |

**On save, atomically:**
1. Inserts each line into `Invoices` table.
2. Creates a `Stock` record with `qntOut` for each line (deducts inventory).
3. Creates a `Transaction` record (`transType = "Sales Invoice"`) in the financial ledger.

**Print:** `/sales/invoices/[invNo]/print` renders a print-optimized layout. `window.print()` is called on the Print button click.

#### Quotations

**Routes:** `/sales/quotations`, `/sales/quotations/new`, `/sales/quotations/[invNo]`
**API:** `/api/sales/quotations`

Identical structure to Invoices using the `Quotations` table. **No stock deduction** or financial entry is created — quotations are price proposals only. Can be manually promoted to invoices.

#### Return Invoice

**Route:** `/sales/returns` | **API:** `/api/sales/returns`

Processes customer returns against existing invoices. On save:
- Creates a `Stock` record with `qntIn` (returns items to inventory).
- Creates a `Transaction` record (`transType = "Returned Invoice"`).

#### Sales Reports

**Route:** `/sales/reports` | **API:** `/api/sales/reports`

Date-range filtered analytics report. Aggregates from `Invoices` table grouped by item, client, or date. Print-ready via `@media print`.

---

### Finance Module

**Base route:** `/finance`

#### Chart of Accounts

**Route:** `/finance/accounts`
**API:** `/api/finance/accounts`
**Component:** `src/components/finance/AccountTree.js`

Manages the 4-level account hierarchy (`Accs` table):

| Level | Field | Example |
|---|---|---|
| Level 1 | `acc1` | Assets |
| Level 2 | `acc2` | Current Assets |
| Level 3 | `acc3` | Accounts Receivable |
| Level 4 | `acc4` | Client A/R |

The `AccountTree.js` component renders the full hierarchy as a collapsible tree for navigation and account selection in vouchers and statements.

#### Journal Vouchers

**Routes:** `/finance/vouchers`, `/finance/vouchers/new`, `/finance/vouchers/[moveNo]`, `/finance/vouchers/approve`, `/finance/vouchers/reverse`
**API:** `/api/finance/vouchers`

The core financial journal entry system. All financial movements (manual and auto-generated from invoices/returns) pass through the `Transactions` table.

**Key fields per voucher:**

| Field | Description |
|---|---|
| `moveNo` | Sequential journal entry / move number |
| `acc1` – `acc4` | Account hierarchy references |
| `totalValueIn` | Debit amount (SDG) |
| `totalValueOut` | Credit amount (SDG) |
| `transType` | "Journal Voucher", "Pay Voucher", "Receipt Voucher", "Sales Invoice", "Returned Invoice" |
| `paymentType` | Cash, Cheque, Bank Transfer, etc. |
| `source` | Source document reference |
| `writing` | Narration / description |
| `paperNo` | Physical voucher/paper number |
| `cheqDate` | Cheque date (if payment type is Cheque) |
| `approved` | Approval flag (Boolean) |
| `employee` | Entered by |

**Approve workflow:** All vouchers with `approved = false` are listed in `/finance/vouchers/approve` for bulk approval.

**Reverse workflow:** A posted voucher can be voided from `/finance/vouchers/reverse`, which creates an equal and opposite transaction entry.

#### Bills Archive

**Route:** `/finance/bills` | **API:** `/api/finance/bills`

Records and archives supplier/vendor bills. Lists all `transType = "Bill"` transactions. Supports filtering by date range and supplier.

#### Trial Balance

**Route:** `/finance/trial-balance` | **API:** `/api/finance/trial-balance`

Standard trial balance report. Aggregates all `Transactions` grouped by account hierarchy and computes:
- Total debits: `SUM(totalValueIn)`
- Total credits: `SUM(totalValueOut)`
- Net balance per account

Supports date-range filtering via the `DateRangePicker` component.

#### Account Statement

**Route:** `/finance/statements` | **API:** `/api/finance/statements`

Per-account transaction history with a running balance column. Filterable by account (any of the 4 levels), date range, and transaction type.

#### Additional Finance Routes (Groundwork)

| Route | Description | Status |
|---|---|---|
| `/finance/balance-sheet` | Balance sheet report | 🚧 Groundwork |
| `/finance/budget` | Budget management | 🚧 Groundwork |
| `/finance/cheques` | Cheque tracking | 🚧 Groundwork |
| `/finance/pay` | Pay voucher entry | 🚧 Groundwork |
| `/finance/receipt` | Receipt voucher entry | 🚧 Groundwork |

---

### HR Module

**Base route:** `/hr`

#### Employees

**Route:** `/hr/employees` | **API:** `/api/hr/employees`

Full employee registry with CRUD operations (`Employees` table).

| Field | Type | Description |
|---|---|---|
| `fullName` | `String` | Full name |
| `nationalId` | `String?` | National ID number |
| `mobile` | `String?` | Mobile number |
| `email` | `String?` | Email address |
| `dateOfBirth` | `DateTime?` | Date of birth |
| `dateOfJoining` | `DateTime?` | Employment start date |
| `departmentId` | `Int?` | FK → `Departments` |
| `jobDescriptionId` | `Int?` | FK → `JobDescriptions` |
| `gradeLevelId` | `Int?` | FK → `GradeLevels` |
| `basicSalary` | `Float` | Basic monthly salary |
| `contractType` | `String?` | Permanent, Contract, Part-time, etc. |
| `isActive` | `Boolean` | Active employment status |

**Related models:**
- `Department` — Bilingual department names (English + Arabic).
- `JobDescription` — Bilingual job titles (English + Arabic).
- `GradeLevel` — Salary grade with `basicSalary` and a JSON `allowances` field.

#### Payroll

**Route:** `/hr/payroll` | **API:** `/api/hr/payroll`

Monthly payroll processing system using the `PaySheets` table.

| Field | Description |
|---|---|
| `empNo` | FK → Employee |
| `month` | Pay month (1–12) |
| `year` | Pay year (e.g., 2026) |
| `basicSalary` | Salary for this period |
| `allowances` | Total allowances |
| `deductions` | Total deductions |
| `netPay` | `basicSalary + allowances - deductions` |
| `approved` | Approval status |

**Capabilities:** Generate payroll for all active employees for a given month/year, edit individual allowances/deductions, bulk approve.

#### Vacations

**Route:** `/hr/vacations` | **API:** `/api/hr/vacations`

Vacation request and approval workflow (`Vacations` table).

| Field | Description |
|---|---|
| `empNo` | FK → Employee |
| `startDate` / `endDate` | Vacation period |
| `type` | Annual, Sick, Emergency, Unpaid, etc. |
| `status` | "Pending" → "Approved" or "Rejected" |
| `notes` | Optional notes |

#### Additional HR Routes (Groundwork)

| Route | Description | Status |
|---|---|---|
| `/hr/departments` | Department CRUD (EN + AR names) | 🚧 Groundwork |
| `/hr/jobs` | Job description CRUD (EN + AR) | 🚧 Groundwork |
| `/hr/contracts` | Employee contract management | 🚧 Groundwork |
| `/hr/appraisals` | Performance appraisal records | 🚧 Groundwork |

---

### Administration Module

**Base route:** `/admin`

#### User Management

**Route:** `/admin/users` | **API:** `/api/admin/users`

Manages system users in the `Users` table.

| Field | Description |
|---|---|
| `fullName` | Display name |
| `pass` | bcrypt-hashed password |
| `role` | `"admin"` or `"user"` |
| `isActive` | Login access flag |

**Capabilities:** Create users, activate/deactivate accounts, change roles.

#### Regions & Areas

**Route:** `/admin/regions` | **API:** `/api/admin/regions`

3-level geographic hierarchy (`Regions` table) used for client address cascading:
```
State → Region → Area
```

The API supports these query parameters for cascading dropdowns:
- `?distinct=state` — Get unique states
- `?state=X&distinct=region` — Get regions within state X
- `?state=X&region=Y&distinct=area` — Get areas within region Y of state X

#### Sales Agents

**Route:** `/admin/sales-agents` | **API:** `/api/admin/agents?type=distributor`

Registry of sales agents and distributors (`AgentDistributors` table).

#### Medical Representatives

**Route:** `/admin/med-reps` | **API:** `/api/admin/agents?type=representative`

Registry of medical representatives (`AgentRepresentatives` table).

---

## API Routes Reference

All API routes are in `src/app/api/` using Next.js Route Handlers.

### Authentication

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/auth/[...nextauth]` | Sign in, sign out, session refresh, CSRF token |

### Dashboard

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/dashboard/stats` | `{ totalClients, invoicesThisMonth, stockItems, activeEmployees }` |
| `GET` | `/api/dashboard/activity` | Last 10 `Transactions` records |

### Clients

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/clients?q=<search>` | List clients with optional full-text search |
| `POST` | `/api/clients` | Create a new client |
| `PUT` | `/api/clients/[id]` | Update client by ID |
| `DELETE` | `/api/clients/[id]` | Delete client by ID |

### Inventory

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/inventory/items` | List items (`?q=<search>` supported) |
| `POST` | `/api/inventory/items` | Create item |
| `PUT` | `/api/inventory/items/[id]` | Update item |
| `DELETE` | `/api/inventory/items/[id]` | Delete item |
| `GET` | `/api/inventory/stock` | List stock transactions |
| `POST` | `/api/inventory/stock` | Record new stock receipt |
| `GET` | `/api/inventory/stores` | List store names |
| `POST` | `/api/inventory/stores` | Create store |
| `POST` | `/api/inventory/transfer` | Transfer stock between stores |
| `POST` | `/api/inventory/dispose` | Write off stock |

### Sales

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/sales/invoices` | List invoices (pagination, search, date range) |
| `POST` | `/api/sales/invoices` | Create invoice (Invoices + Stock + Transaction — atomic) |
| `GET` | `/api/sales/invoices/[invNo]` | Get all lines of a specific invoice |
| `GET` | `/api/sales/quotations` | List quotations |
| `POST` | `/api/sales/quotations` | Create quotation (no stock deduction) |
| `GET` | `/api/sales/quotations/[invNo]` | Get a specific quotation |
| `POST` | `/api/sales/returns` | Process return (stock reversal + reversal transaction) |
| `GET` | `/api/sales/reports` | Sales aggregation report (date range, group-by support) |

### Finance

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/finance/accounts` | List chart of accounts |
| `POST` | `/api/finance/accounts` | Create account |
| `PUT` | `/api/finance/accounts/[id]` | Update account |
| `DELETE` | `/api/finance/accounts/[id]` | Delete account |
| `GET` | `/api/finance/vouchers` | List vouchers |
| `POST` | `/api/finance/vouchers` | Create voucher |
| `GET` | `/api/finance/vouchers/[moveNo]` | Get specific voucher |
| `POST` | `/api/finance/vouchers/approve` | Bulk approve vouchers |
| `POST` | `/api/finance/vouchers/reverse` | Reverse a posted voucher |
| `GET` | `/api/finance/bills` | List bills |
| `POST` | `/api/finance/bills` | Record a bill |
| `GET` | `/api/finance/trial-balance` | Trial balance (`?from=YYYY-MM-DD&to=YYYY-MM-DD`) |
| `GET` | `/api/finance/statements` | Account statement (`?acc=X&from=&to=`) |

### HR

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/hr/employees` | List employees |
| `POST` | `/api/hr/employees` | Create employee |
| `GET` | `/api/hr/employees/[id]` | Get employee details |
| `PUT` | `/api/hr/employees/[id]` | Update employee |
| `DELETE` | `/api/hr/employees/[id]` | Delete employee |
| `GET` | `/api/hr/payroll` | List paysheets |
| `POST` | `/api/hr/payroll` | Generate paysheet for month/year |
| `PUT` | `/api/hr/payroll/[id]` | Edit paysheet (allowances, deductions) |
| `POST` | `/api/hr/payroll/approve` | Approve paysheet |
| `GET` | `/api/hr/vacations` | List vacation requests |
| `POST` | `/api/hr/vacations` | Submit vacation request |
| `PUT` | `/api/hr/vacations/[id]` | Update vacation status (Approve/Reject) |

### Administration

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/admin/users` | List system users |
| `POST` | `/api/admin/users` | Create system user |
| `PUT` | `/api/admin/users/[id]` | Update user (role, active status) |
| `GET` | `/api/admin/regions` | List regions (supports `?distinct=state/region/area`) |
| `POST` | `/api/admin/regions` | Create region entry |
| `GET` | `/api/admin/agents` | List agents (`?type=distributor` or `?type=representative`) |
| `POST` | `/api/admin/agents` | Create agent |
| `GET` | `/api/admin/client-classes` | List client classifications |
| `GET` | `/api/admin/departments` | List departments |
| `GET` | `/api/admin/grade-levels` | List salary grade levels |

---

## Database Schema (Prisma)

**File:** `prisma/schema.prisma` | **Provider:** `mysql` (MariaDB-compatible)

The schema defines **18 models** across 6 logical groups:

### Authentication & Users

| Model | Table | Key Fields |
|---|---|---|
| `User` | `Users` | `id` (PK, auto), `fullName`, `pass` (bcrypt hash), `role`, `isActive`, `createdAt` |

### Lookup / Reference Tables

| Model | Table | Purpose |
|---|---|---|
| `Region` | `Regions` | 3-level geography: `state`, `region`, `area` |
| `ClientClass` | `ClientClasses` | Client classification labels |
| `StoreName` | `StoreName` | Warehouse/store name registry |
| `Company` | `Companies` | Manufacturer/company lookup |
| `AgentDistributor` | `AgentDistributors` | Sales agent (distributor) registry |
| `AgentRepresentative` | `AgentRepresentatives` | Medical representative registry |
| `LockDate` | `LockDate` | Single-row accounting period lock date |

### Clients

| Model | Table | Relations |
|---|---|---|
| `Client` | `Clients` | Has many `Invoice[]`; has many `Transaction[]` |

### Inventory

| Model | Table | Key Fields |
|---|---|---|
| `ItemRegistry` | `ItemsRegistry` | `item` (UNIQUE), `genericName`, `pack`, `wPrice`, `rPrice`, `companyName` |
| `Stock` | `Stock` | `storeName`, `item`, `batchNo`, `qntIn`, `qntOut`, `wPrice`, `rPrice`, `expireDate`, `transType`, `transDate` |

### Sales

| Model | Table | Notes |
|---|---|---|
| `Invoice` | `Invoices` | One DB row per line item. Has FK to `Client`. |
| `Quotation` | `Quotations` | Same structure as Invoice; no FK to `Client`. |

### Finance

| Model | Table | Key Fields |
|---|---|---|
| `Account` | `Accs` | `acc1`, `acc2`, `acc3`, `acc4` (4-level hierarchy) |
| `Transaction` | `Transactions` | `moveNo`, `transType`, `totalIn`, `totalOut`, `totalValueIn`, `totalValueOut`, `paymentType`, `approved`, `cheqDate`, `writing`, `paperNo` |

### HR

| Model | Table | Relations / Notes |
|---|---|---|
| `Department` | `Departments` | `nameEn`, `nameAr`. Has many `Employee[]`. |
| `GradeLevel` | `GradeLevels` | `level`, `basicSalary`, `allowances` (JSON field) |
| `JobDescription` | `JobDescriptions` | `nameEn`, `nameAr`. Has many `Employee[]`. |
| `Employee` | `Employees` | Belongs to `Department`, `JobDescription`. Has many `PaySheet[]`, `Vacation[]`. |
| `PaySheet` | `PaySheets` | Monthly: `basicSalary`, `allowances`, `deductions`, `netPay`, `approved` |
| `Vacation` | `Vacations` | `startDate`, `endDate`, `type`, `status` (Pending/Approved/Rejected), `notes` |

---

## Shared Utilities & Libraries

**File:** `src/lib/utils.js`

### `spellNumber(amount: number): string`

Converts a numeric amount to its English written-out form for **Sudanese Pound (SDG)** currency with piastre sub-units.

**Ported from:** Original VB.NET `Other.vb` → `SpellNumber()` function.

```js
spellNumber(1500.50)
// → "One Thousand Five Hundred SDG and Fifty Piastres Only"

spellNumber(250)
// → "Two Hundred Fifty SDG and No Piastre Only"

spellNumber(0)
// → "No SDG and No Piastre Only"
```

**Implementation:** Chunks the integer part into groups of 3 (ones, thousands, millions, billions, trillions), handles teens (11–19), tens (20–90), and hundreds individually. Decimal portion is treated as piastres.

### `formatCurrency(num: number): string`

Formats a number as a 2-decimal-place string with locale-aware thousands separators (en-US format).

```js
formatCurrency(1500000.5)  // → "1,500,000.50"
formatCurrency(0)           // → "0.00"
```

### `todayStr(): string`

Returns today's date as an ISO `YYYY-MM-DD` string.

```js
todayStr()  // → "2026-08-29"
```

---

## Design System & Styling

**File:** `src/app/globals.css` (~15KB)

The entire application uses a **purpose-built CSS design system** defined with CSS custom properties. No external component library (MUI, shadcn, etc.) is used — all UI components are hand-crafted.

### CSS Variable Groups

| Group | Key Variables |
|---|---|
| **Backgrounds** | `--bg-primary`, `--bg-secondary`, `--bg-tertiary` |
| **Text** | `--text-primary`, `--text-secondary`, `--text-muted` |
| **Accent** | `--accent-primary`, `--accent-light`, `--accent-hover` |
| **Status** | `--success`, `--warning`, `--danger`, `--info` |
| **Borders** | `--border-primary`, `--border-secondary` |
| **Shadows** | `--shadow-sm`, `--shadow-md`, `--shadow-lg` |

### Layout Classes

| Class | Description |
|---|---|
| `.erp-sidebar` | Fixed-width sidebar with scrollable nav sections |
| `.erp-main` | Main content area offset from sidebar |
| `.erp-header` | Sticky top header bar |
| `.card` / `.card-header` / `.card-title` | Standard content card |

### Component Classes

| Class | Description |
|---|---|
| `.btn`, `.btn-primary`, `.btn-secondary`, `.btn-danger`, `.btn-sm` | Button variants |
| `.badge` | Status badge (inline) |
| `.form-group`, `.form-label`, `.form-input`, `.form-select` | Form field styling |
| `.table-wrapper`, `.erp-table`, `.table-empty` | Table + empty state |
| `.stat-card`, `.stat-card-hover`, `.stat-icon`, `.stat-value`, `.stat-label` | KPI stat cards |
| `.quick-link-card` | Dashboard quick action tiles |
| `.nav-item`, `.nav-item.active`, `.nav-icon`, `.nav-section-label` | Sidebar navigation |
| `.grid-2`, `.grid-3`, `.grid-4` | CSS Grid layout helpers |

### Animations

| Animation | Usage |
|---|---|
| `pulse` | Loading skeleton placeholders (used in stat cards while fetching) |
| `fadeIn` | Page content fade-in on mount |
| `slideIn` | Sidebar transition |

---

## Print & Reporting

All print output uses **browser-native printing** via `@media print` CSS rules — replacing Crystal Reports from the original system.

**How it works:**
1. Printable pages (e.g., `/sales/invoices/[invNo]/print`, `/inventory/barcodes`) render a full print layout.
2. When the user clicks **Print**, `window.print()` is invoked.
3. `@media print` CSS rules hide the sidebar, header, navigation buttons, and action bars, and reformat the content for A4 paper dimensions.
4. The browser's native print dialog appears, supporting PDF export or direct printer output.

**Pages with dedicated print layouts:**
- Sales Invoice print: `/sales/invoices/[invNo]/print`
- Quotation print: `/sales/quotations/[invNo]/print`
- Barcode label sheets: `/inventory/barcodes`
- Trial Balance report: `/finance/trial-balance`
- Sales Reports: `/sales/reports`

---

## Barcode & QR Code Generation

All barcode rendering is **client-side only** — no server-side image generation.

### CODE128 / EAN13 (via `jsbarcode`)

```js
// Rendered to an <svg> element in the DOM
JsBarcode(svgRef.current, value, { format: "CODE128", displayValue: true });
JsBarcode(svgRef.current, value, { format: "EAN13" }); // value must be 12–13 digits
```

### QR Codes (via `qrcode`)

```js
// Rendered to a <canvas> element in the DOM
QRCode.toCanvas(canvasRef.current, value, { width: 200 });
```

### Full Workflow

1. Navigate to `/inventory/barcodes`.
2. Search and find items from the registry (live search).
3. Select one or multiple items using checkboxes.
4. Choose the barcode format: CODE128, EAN13, or QR.
5. Set the label quantity (number of copies per item for sheet printing).
6. Preview updates live as you change item or format selection.
7. For EAN13: edit the barcode value inline (must be exactly 12–13 digits). Click Save to persist the value to the item record.
8. Click **Print Labels** to open the browser print dialog. The label sheet layout is automatically applied via `@media print`.

---

## Module Status Summary

| Module | Route | Status | Notes |
|---|---|---|---|
| **Authentication** | `/login` | ✅ Complete | JWT via NextAuth v5, bcrypt, User ID login |
| **Dashboard** | `/dashboard` | ✅ Complete | Live KPIs, quick actions, recent activity |
| **Items Registry** | `/inventory/items` | ✅ Complete | Full CRUD, live search, inline form |
| **Add to Stock** | `/inventory/stock` | ✅ Complete | Batch/store ingestion with stock records |
| **Stores Management** | `/inventory/stores` | ✅ Complete | Store name CRUD |
| **Stock Transfer** | `/inventory/transfer` | ✅ Complete | Inter-store transfer with dual stock entries |
| **Stock Disposal** | `/inventory/dispose` | ✅ Complete | Write-off with disposal record |
| **Stock Status** | `/inventory/status` | ✅ Complete | Available quantity report (SUM qntIn - qntOut) |
| **Barcode Generator** | `/inventory/barcodes` | ✅ Complete | CODE128, EAN13, QR; multi-select, print sheets |
| **Client Registry** | `/clients` | ✅ Complete | Full CRUD, cascading geography, live search |
| **Sales Invoices** | `/sales/invoices` | ✅ Complete | Multi-line, stock deduction, financial entry, print |
| **Quotations** | `/sales/quotations` | ✅ Complete | Full CRUD, no stock deduction, print layout |
| **Return Invoices** | `/sales/returns` | ✅ Complete | Return processing with stock reversal |
| **Sales Reports** | `/sales/reports` | ✅ Complete | Date-range filtered aggregation, printable |
| **Chart of Accounts** | `/finance/accounts` | ✅ Active | 4-level hierarchy CRUD + tree browser component |
| **Journal Vouchers** | `/finance/vouchers` | ✅ Active | Create, list, view, approve, reverse |
| **Bills Archive** | `/finance/bills` | ✅ Active | Supplier bill recording and archive |
| **Trial Balance** | `/finance/trial-balance` | ✅ Active | Date-range filtered aggregation |
| **Account Statement** | `/finance/statements` | ✅ Active | Per-account transaction history + running balance |
| **Balance Sheet** | `/finance/balance-sheet` | 🚧 Groundwork | Route + DB schema ready; UI placeholder |
| **Budget** | `/finance/budget` | 🚧 Groundwork | Route placeholder |
| **Cheques** | `/finance/cheques` | 🚧 Groundwork | Route placeholder |
| **Pay Voucher** | `/finance/pay` | 🚧 Groundwork | Route placeholder |
| **Receipt Voucher** | `/finance/receipt` | 🚧 Groundwork | Route placeholder |
| **Employees** | `/hr/employees` | ✅ Active | Full CRUD, department/job/grade linkage |
| **Payroll** | `/hr/payroll` | ✅ Active | Monthly generation, allowances, deductions, approval |
| **Vacations** | `/hr/vacations` | ✅ Active | Request submission and approval workflow |
| **Departments** | `/hr/departments` | 🚧 Groundwork | DB schema + API ready; UI placeholder |
| **Job Descriptions** | `/hr/jobs` | 🚧 Groundwork | DB schema + API ready; UI placeholder |
| **Contracts** | `/hr/contracts` | 🚧 Groundwork | Route placeholder |
| **Appraisals** | `/hr/appraisals` | 🚧 Groundwork | Route placeholder |
| **User Management** | `/admin/users` | ✅ Complete | Create users, activate/deactivate, role management |
| **Regions & Areas** | `/admin/regions` | ✅ Complete | Cascading State→Region→Area management |
| **Sales Agents** | `/admin/sales-agents` | ✅ Complete | Distributor registry |
| **Medical Reps** | `/admin/med-reps` | ✅ Complete | Medical representative registry |

**Legend:**
- ✅ **Complete** — Fully implemented with working UI and API.
- 🚧 **Groundwork** — Database schema and/or API routes exist; UI is a stub or placeholder.
- 🔜 **Planned** — Not yet started.

---

## Original System Reference

The legacy Oasis ERP was a **VB.NET WinForms** desktop application (Visual Studio 2010):

| Component | Legacy (VB.NET) | This Recreation (JavaScript) |
|---|---|---|
| Language | Visual Basic .NET | JavaScript (ES2022+) |
| UI Framework | WinForms + Telerik RadControls | React 19 + Next.js 16 App Router |
| Database | Microsoft SQL Server (local) | MariaDB / MySQL (local or remote) |
| Data Access | ADO.NET (raw SQL + stored procedures) | Raw `mariadb` pool + Prisma for schema |
| Authentication | Username + plain-text password | Numeric User ID + bcrypt hash (NextAuth.js v5 JWT) |
| Reporting | Crystal Reports | HTML/CSS `@media print` |
| Barcodes | BarcodeLib | `jsbarcode` + `qrcode` (client-side) |
| Amount in Words | `Other.vb → SpellNumber()` | `src/lib/utils.js → spellNumber()` |
| Deployment | Desktop installer (ClickOnce / MSI) | Web app (`npm run build && npm start`) |

**Faithfully preserved business logic:**
- Invoice net amount formula: `price × qnt × (1 - disc/100) × (1 + vat/100)`
- Double-entry: Stock deducted automatically on every invoice save.
- Financial ledger entries auto-created for every invoice and every return.
- SDG amount-in-words generation (piastres as the decimal sub-unit).
- Cascading geography (State → Region → Area) for client address forms.
- Approval workflows for journal vouchers and payroll.
- Batch/lot tracking in stock records with expiry date.
- Accounting period lock date (`LockDate` table).

---

## Development Notes & Known Gotchas

### Database Adapter Architecture

The `@prisma/adapter-mariadb` v7 causes `ERR_INVALID_ARG_TYPE` pool initialization errors on cold-start in the Next.js dev server. As a result, the architecture is split:

- **Prisma** — Schema management only (`npx prisma db push`). Do **not** use `prisma.findMany()` or similar in API routes.
- **`mariadb` pool** (`src/lib/db.js`) — All runtime queries. Use `pool.query("SELECT ...", [params])`.

### HMR & Connection Pool Singleton

In development (`NODE_ENV !== "production"`), the pool is cached on `globalThis._mariadbPool`. This prevents Next.js Hot Module Reloading from creating a new connection pool on every file save, which would exhaust the MariaDB connection limit. In production, the pool is naturally module-scoped.

### BigInt Handling

MariaDB returns `BIGINT` and auto-increment `INT` columns as JavaScript `BigInt` by default. The pool is configured with `bigIntAsNumber: true` to return all integers as regular JS `Number` values, preventing `TypeError: Cannot convert BigInt value to JSON` errors in API responses.

### Sessions & Expiry

- JWT sessions expire when the **browser is closed** (no persistent `maxAge` configured by default).
- To set a persistent session duration, add `session: { maxAge: 86400 }` (in seconds) to the NextAuth config in `src/lib/auth.js`.

### Password Migration

All passwords are bcrypt-hashed at **cost factor 10**. The original VB.NET system stored passwords in plain text — these are completely incompatible. **All user accounts must be re-created** using the seed script or the User Management UI.

### `spellNumber()` — SDG Currency Only

The utility in `src/lib/utils.js` is calibrated for **Sudanese Pounds (SDG)** with piastres as the sub-unit. If the business currency changes, update the string literals in the function (`"SDG"`, `"Piastre"`, `"Piastres"`).

### Adding New Schema Columns

If you add a column to `prisma/schema.prisma` and run `npx prisma db push`, you **must also update** the raw SQL `SELECT` statements in the corresponding API route handler to include the new column. The Prisma client is not used at runtime and will not automatically reflect schema changes in queries.

### ESLint

ESLint v9 with `eslint-config-next` is configured in `eslint.config.mjs` (flat config format). Run `npm run lint` before committing to check for issues.

### Path Aliases

The `@/` path alias maps to `src/` (configured in `jsconfig.json`). Use it everywhere:
```js
import pool from "@/lib/db";
import { spellNumber } from "@/lib/utils";
```
