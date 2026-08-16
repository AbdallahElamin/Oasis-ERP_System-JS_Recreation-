# Oasis ERP System — JavaScript Recreation

A modern full-stack JavaScript web application recreating the legacy VB.NET WinForms Oasis ERP System.

---

## Tech Stack

| Layer | Technology |
|---|---|
| **Frontend** | React + Next.js 14 (App Router) |
| **Styling** | Tailwind CSS + Custom CSS Design System |
| **Backend API** | Next.js API Routes (App Router) |
| **Authentication** | NextAuth.js v5 (Credentials + JWT) |
| **ORM** | Prisma |
| **Database** | MariaDB (MySQL-compatible) |
| **Password Hashing** | bcryptjs |

---

## Prerequisites

1. **Node.js** 18+ — [Download](https://nodejs.org)
2. **MariaDB** running locally (or MySQL)
3. **npm** (comes with Node.js)

---

## Setup Instructions

### 1. Clone & Install Dependencies

```bash
# From the new-codebase directory:
npm install
```

### 2. Configure Environment Variables

Copy the example file and fill in your values:

```bash
cp .env.example .env.local
```

Edit `.env.local`:

```env
# MariaDB connection — adjust user, password, host, port, and database name as needed
DATABASE_URL="mysql://root:@localhost:3306/oasis_erp"

# NextAuth.js secret — MUST be changed in production
# Generate one: openssl rand -base64 32
NEXTAUTH_SECRET="your-super-secret-key-change-this"
NEXTAUTH_URL="http://localhost:3000"
```

### 3. Set Up the Database

Make sure MariaDB is running, then create the database:

```sql
CREATE DATABASE oasis_erp CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

Then run Prisma migrations to create all tables:

```bash
npx prisma db push
```

### 4. Seed the First User

You'll need at least one user to log in. Run this from the `new-codebase` directory:

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

> The default credentials are: **User ID** = `1` (or whatever was printed), **Password** = `admin123`

### 5. Run the Development Server

```bash
npm run dev
```

Open your browser at [http://localhost:3000](http://localhost:3000)

---

## Project Structure

```
new-codebase/
├── prisma/
│   └── schema.prisma          # Full database schema (MariaDB)
├── src/
│   ├── app/
│   │   ├── (dashboard)/       # All authenticated pages
│   │   │   ├── dashboard/     # Home/overview
│   │   │   ├── inventory/     # Items, stock, stores
│   │   │   ├── sales/         # Invoices, quotations
│   │   │   ├── clients/       # Client registry
│   │   │   ├── finance/       # (Groundwork) Chart of accounts, vouchers
│   │   │   └── hr/            # (Groundwork) Employees, payroll
│   │   ├── api/               # Next.js API routes
│   │   ├── login/             # Login page
│   │   └── globals.css        # Full custom CSS design system
│   ├── components/
│   │   └── layout/            # Sidebar, Header, Providers
│   └── lib/
│       ├── auth.js            # NextAuth.js configuration
│       ├── prisma.js          # Prisma client singleton
│       └── utils.js           # spellNumber, formatCurrency helpers
└── .env.example               # Environment variable template
```

---

## Modules Status

| Module | Status | Notes |
|---|---|---|
| **Authentication** | ✅ Complete | Login with User ID + bcrypt hashed password |
| **Items Registry** | ✅ Complete | CRUD for product catalog |
| **Add to Stock** | ✅ Complete | Batch/store stock ingestion with transaction |
| **Client Registry** | ✅ Complete | Full client registration with cascading geography |
| **Sales Invoices** | ✅ Complete | Create, list, print. Auto-deducts stock + creates financial entries |
| **Financial System** | 🚧 Groundwork | Routes and DB schema ready, UI placeholder |
| **HR Module** | 🚧 Groundwork | Routes and DB schema ready, UI placeholder |
| **Quotations** | 🔜 Planned | Same as invoices, no stock deduction |
| **Stock Status** | 🔜 Planned | Grouped stock report |
| **Print / Reports** | ✅ Partial | Invoice print layout via HTML/CSS @media print |

---

## Development Notes

- **Sessions**: JWT-based via NextAuth.js. Sessions expire after the browser closes.
- **Passwords**: All passwords are bcrypt-hashed (cost factor 10). The original system stored plain text — these are incompatible and all users must be re-created.
- **Print**: Invoice printing uses browser native print with `@media print` CSS. Click *Print Invoice* after saving an invoice.
- **Amount in Words**: Implemented in `src/lib/utils.js` as `spellNumber()` — ported from the original VB.NET `Other.vb` module.

---

## Original System Reference

The legacy system was a VB.NET WinForms desktop application (Visual Studio 2010) using:
- Microsoft SQL Server (local)
- Crystal Reports for all printing
- Telerik RadControls for UI
- BarcodeLib for barcode generation

This recreation maps that architecture to a modern web stack while preserving all business logic.
