import { auth } from "@/lib/auth";
import Link from "next/link";

export const metadata = {
  title: "Dashboard — Oasis ERP",
};

const STAT_CARDS = [
  {
    label: "Total Clients",
    value: "—",
    icon: "M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0z",
    color: "#6366f1",
    href: "/clients",
  },
  {
    label: "Invoices This Month",
    value: "—",
    icon: "M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2",
    color: "#10b981",
    href: "/sales/invoices",
  },
  {
    label: "Stock Items",
    value: "—",
    icon: "M19 11H5m14 0a2 2 0 012 2v6a2 2 0 01-2 2H5a2 2 0 01-2-2v-6a2 2 0 012-2m14 0V9a2 2 0 00-2-2M5 11V9a2 2 0 012-2m0 0V5a2 2 0 012-2h6a2 2 0 012 2v2M7 7h10",
    color: "#f59e0b",
    href: "/inventory/items",
  },
  {
    label: "Active Employees",
    value: "—",
    icon: "M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z",
    color: "#a78bfa",
    href: "/hr/employees",
  },
];

const QUICK_LINKS = [
  { label: "New Invoice", href: "/sales/invoices/new", desc: "Create a sales invoice" },
  { label: "Add Stock", href: "/inventory/stock", desc: "Add items to inventory" },
  { label: "New Client", href: "/clients/new", desc: "Register a new client" },
  { label: "New Quotation", href: "/sales/quotations/new", desc: "Prepare a price quotation" },
  { label: "New Voucher", href: "/finance/vouchers/new", desc: "Record a journal entry" },
  { label: "New Employee", href: "/hr/employees/new", desc: "Add staff profile" },
];

export default async function DashboardPage() {
  const session = await auth();

  return (
    <div>
      {/* Welcome banner */}
      <div style={{ marginBottom: "1.5rem" }}>
        <h2 style={{ fontSize: "1.4rem", fontWeight: 700, color: "var(--text-primary)" }}>
          Welcome back, {session?.user?.name ?? "User"} 👋
        </h2>
        <p style={{ color: "var(--text-muted)", fontSize: "0.85rem", marginTop: "0.25rem" }}>
          Here&apos;s what&apos;s happening in your ERP system today.
        </p>
      </div>

      {/* Stat Cards */}
      <div className="grid-4" style={{ marginBottom: "1.5rem" }}>
        {STAT_CARDS.map((stat) => (
          <Link key={stat.href} href={stat.href} style={{ textDecoration: "none" }}>
            <div
              className="stat-card"
              style={{ cursor: "pointer", transition: "border-color 0.15s" }}
              onMouseEnter={(e) =>
                (e.currentTarget.style.borderColor = stat.color + "60")
              }
              onMouseLeave={(e) =>
                (e.currentTarget.style.borderColor = "var(--border-subtle)")
              }
            >
              <div
                className="stat-icon"
                style={{ background: stat.color + "20" }}
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="20"
                  height="20"
                  viewBox="0 0 24 24"
                  fill="none"
                  stroke={stat.color}
                  strokeWidth="2"
                >
                  <path d={stat.icon} />
                </svg>
              </div>
              <div>
                <div className="stat-value">{stat.value}</div>
                <div className="stat-label">{stat.label}</div>
              </div>
            </div>
          </Link>
        ))}
      </div>

      {/* Quick Actions */}
      <div className="card">
        <div className="card-header">
          <h3 className="card-title">Quick Actions</h3>
        </div>
        <div className="grid-3">
          {QUICK_LINKS.map((link) => (
            <Link key={link.href} href={link.href} style={{ textDecoration: "none" }}>
              <div
                style={{
                  padding: "1rem",
                  borderRadius: "0.5rem",
                  border: "1px solid var(--border-subtle)",
                  background: "var(--bg-primary)",
                  cursor: "pointer",
                  transition: "all 0.15s ease",
                }}
                onMouseEnter={(e) => {
                  e.currentTarget.style.borderColor = "var(--accent-primary)";
                  e.currentTarget.style.background = "rgba(99,102,241,0.05)";
                }}
                onMouseLeave={(e) => {
                  e.currentTarget.style.borderColor = "var(--border-subtle)";
                  e.currentTarget.style.background = "var(--bg-primary)";
                }}
              >
                <div
                  style={{
                    fontWeight: 600,
                    fontSize: "0.875rem",
                    color: "var(--accent-light)",
                    marginBottom: "0.25rem",
                  }}
                >
                  {link.label}
                </div>
                <div style={{ fontSize: "0.78rem", color: "var(--text-muted)" }}>
                  {link.desc}
                </div>
              </div>
            </Link>
          ))}
        </div>
      </div>

      {/* Module status */}
      <div
        className="grid-3"
        style={{ marginTop: "1.5rem" }}
      >
        {[
          {
            title: "ERP / Inventory",
            desc: "Manage stock, invoices, quotations, and client registry.",
            status: "Active",
            href: "/inventory/items",
            color: "#10b981",
          },
          {
            title: "Financial System",
            desc: "Chart of accounts, vouchers, trial balance, and statements.",
            status: "Groundwork",
            href: "/finance/accounts",
            color: "#f59e0b",
          },
          {
            title: "Human Resources",
            desc: "Employees, payroll, vacations, contracts, and appraisals.",
            status: "Groundwork",
            href: "/hr/employees",
            color: "#a78bfa",
          },
        ].map((mod) => (
          <div key={mod.title} className="card">
            <div style={{ display: "flex", justifyContent: "space-between", marginBottom: "0.75rem" }}>
              <span style={{ fontWeight: 600, color: "var(--text-primary)" }}>{mod.title}</span>
              <span
                className="badge"
                style={{
                  background: mod.color + "20",
                  color: mod.color,
                }}
              >
                {mod.status}
              </span>
            </div>
            <p style={{ fontSize: "0.8rem", color: "var(--text-muted)", marginBottom: "0.875rem" }}>
              {mod.desc}
            </p>
            <Link href={mod.href} className="btn btn-secondary btn-sm">
              Open Module →
            </Link>
          </div>
        ))}
      </div>
    </div>
  );
}
