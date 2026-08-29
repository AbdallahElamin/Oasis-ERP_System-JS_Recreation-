"use client";

import { useState, useEffect } from "react";
import Link from "next/link";

const QUICK_LINKS = [
  { label: "New Invoice", href: "/sales/invoices/new", desc: "Create a sales invoice" },
  { label: "Add Stock", href: "/inventory/stock", desc: "Add items to inventory" },
  { label: "New Client", href: "/clients", desc: "Register a new client" },
  { label: "New Quotation", href: "/sales/quotations/new", desc: "Prepare a price quotation" },
  { label: "New Voucher", href: "/finance/vouchers/new", desc: "Record a journal entry" },
  { label: "Barcode Labels", href: "/inventory/barcodes", desc: "Generate product barcodes" },
];

const MODULES = [
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
    status: "Active",
    href: "/finance/accounts",
    color: "#6366f1",
  },
  {
    title: "Human Resources",
    desc: "Employees, payroll, vacations, contracts, and appraisals.",
    status: "Active",
    href: "/hr/employees",
    color: "#a78bfa",
  },
];

const TRANS_TYPE_COLOR = {
  "Journal Voucher": "#6366f1",
  "Pay Voucher": "#ef4444",
  "Receipt Voucher": "#10b981",
  "Sales Invoice": "#f59e0b",
  "Returned Invoice": "#f97316",
};

function StatCard({ label, value, icon, color, href, loading }) {
  return (
    <Link href={href} style={{ textDecoration: "none" }}>
      <div className="stat-card stat-card-hover">
        <div className="stat-icon" style={{ background: color + "20" }}>
          <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24"
            fill="none" stroke={color} strokeWidth="2">
            <path d={icon} />
          </svg>
        </div>
        <div>
          <div className="stat-value">
            {loading ? (
              <span style={{ display: "inline-block", width: "3rem", height: "1.5rem",
                background: "var(--bg-tertiary)", borderRadius: "0.25rem", animation: "pulse 1.5s infinite" }} />
            ) : value}
          </div>
          <div className="stat-label">{label}</div>
        </div>
      </div>
    </Link>
  );
}

export default function DashboardPage() {
  const [stats, setStats] = useState(null);
  const [activity, setActivity] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    Promise.all([
      fetch("/api/dashboard/stats").then((r) => r.json()).catch(() => null),
      fetch("/api/dashboard/activity").then((r) => r.json()).catch(() => []),
    ]).then(([s, a]) => {
      setStats(s);
      setActivity(Array.isArray(a) ? a : []);
      setLoading(false);
    });
  }, []);

  const STAT_CARDS = [
    {
      label: "Total Clients",
      value: stats?.totalClients?.toLocaleString() ?? "—",
      icon: "M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0z",
      color: "#6366f1",
      href: "/clients",
    },
    {
      label: "Invoices This Month",
      value: stats?.invoicesThisMonth?.toLocaleString() ?? "—",
      icon: "M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2",
      color: "#10b981",
      href: "/sales/invoices",
    },
    {
      label: "Stock Items",
      value: stats?.stockItems?.toLocaleString() ?? "—",
      icon: "M19 11H5m14 0a2 2 0 012 2v6a2 2 0 01-2 2H5a2 2 0 01-2-2v-6a2 2 0 012-2m14 0V9a2 2 0 00-2-2M5 11V9a2 2 0 012-2m0 0V5a2 2 0 012-2h6a2 2 0 012 2v2M7 7h10",
      color: "#f59e0b",
      href: "/inventory/items",
    },
    {
      label: "Active Employees",
      value: stats?.activeEmployees?.toLocaleString() ?? "—",
      icon: "M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z",
      color: "#a78bfa",
      href: "/hr/employees",
    },
  ];

  return (
    <div>
      {/* Welcome */}
      <div style={{ marginBottom: "1.5rem" }}>
        <h2 style={{ fontSize: "1.4rem", fontWeight: 700, color: "var(--text-primary)" }}>
          Welcome back 👋
        </h2>
        <p style={{ color: "var(--text-muted)", fontSize: "0.85rem", marginTop: "0.25rem" }}>
          Here&apos;s what&apos;s happening in your ERP system today.
        </p>
      </div>

      {/* Stat Cards */}
      <div className="grid-4" style={{ marginBottom: "1.5rem" }}>
        {STAT_CARDS.map((s) => (
          <StatCard key={s.href} {...s} loading={loading} />
        ))}
      </div>

      <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: "1rem", marginBottom: "1.5rem" }}>
        {/* Quick Actions */}
        <div className="card">
          <div className="card-header"><h3 className="card-title">Quick Actions</h3></div>
          <div className="grid-3" style={{ gap: "0.5rem" }}>
            {QUICK_LINKS.map((link) => (
              <Link key={link.href} href={link.href} className="quick-link-card">
                <div style={{ fontWeight: 600, fontSize: "0.82rem", color: "var(--accent-light)", marginBottom: "0.2rem" }}>
                  {link.label}
                </div>
                <div style={{ fontSize: "0.72rem", color: "var(--text-muted)" }}>{link.desc}</div>
              </Link>
            ))}
          </div>
        </div>

        {/* Recent Activity */}
        <div className="card">
          <div className="card-header"><h3 className="card-title">Recent Activity</h3></div>
          {loading ? (
            <div className="table-empty">Loading…</div>
          ) : !activity.length ? (
            <div className="table-empty">No recent transactions.</div>
          ) : (
            <div style={{ display: "flex", flexDirection: "column", gap: "0.4rem" }}>
              {activity.map((tx) => {
                const amount = (Number(tx.TotalIn) + Number(tx.TotalValueIn)) ||
                               (Number(tx.TotalOut) + Number(tx.TotalValueOut));
                const isIn = (Number(tx.TotalIn) + Number(tx.TotalValueIn)) > 0;
                const color = TRANS_TYPE_COLOR[tx.TransType] || "var(--text-muted)";
                return (
                  <div key={tx.SNo} style={{
                    display: "flex", justifyContent: "space-between", alignItems: "center",
                    padding: "0.4rem 0.5rem", borderRadius: "0.375rem",
                    background: "var(--bg-tertiary)", fontSize: "0.78rem",
                  }}>
                    <div style={{ display: "flex", alignItems: "center", gap: "0.5rem" }}>
                      <span style={{
                        display: "inline-block", width: "0.4rem", height: "0.4rem",
                        borderRadius: "50%", background: color, flexShrink: 0,
                      }} />
                      <div>
                        <div style={{ color: "var(--text-secondary)", fontWeight: 500 }}>
                          {tx.TransType || "Transaction"} #{tx.MoveNo}
                        </div>
                        <div style={{ color: "var(--text-muted)", fontSize: "0.7rem" }}>
                          {tx.Acc4 || tx.Ref || "—"} · {tx.employee || "—"}
                        </div>
                      </div>
                    </div>
                    <div style={{ textAlign: "right" }}>
                      <div style={{ fontWeight: 600, color: isIn ? "var(--success)" : "var(--danger)", whiteSpace: "nowrap" }}>
                        {isIn ? "+" : "-"}{Number(amount).toLocaleString("en-US", { minimumFractionDigits: 2 })}
                      </div>
                      <div style={{ color: "var(--text-muted)", fontSize: "0.68rem" }}>
                        {new Date(tx.TransDate).toLocaleDateString()}
                      </div>
                    </div>
                  </div>
                );
              })}
            </div>
          )}
        </div>
      </div>

      {/* Module status */}
      <div className="grid-3">
        {MODULES.map((mod) => (
          <div key={mod.title} className="card">
            <div style={{ display: "flex", justifyContent: "space-between", marginBottom: "0.75rem" }}>
              <span style={{ fontWeight: 600, color: "var(--text-primary)" }}>{mod.title}</span>
              <span className="badge" style={{ background: mod.color + "20", color: mod.color }}>
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
