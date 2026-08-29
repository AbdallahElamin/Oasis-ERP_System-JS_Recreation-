"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import { formatCurrency } from "@/lib/utils";

export default function InvoicesPage() {
  const [invoices, setInvoices] = useState([]);
  const [loading, setLoading] = useState(true);
  const [year, setYear] = useState(new Date().getFullYear());
  const [search, setSearch] = useState("");

  useEffect(() => {
    setLoading(true);
    fetch(`/api/sales/invoices?year=${year}&q=${encodeURIComponent(search)}`)
      .then((r) => r.json())
      .then(setInvoices)
      .finally(() => setLoading(false));
  }, [year, search]);

  const totalRevenue = invoices.reduce((sum, inv) => sum + (inv._sum?.netAmount ?? 0), 0);

  return (
    <div>
      <div className="page-title-bar no-print">
        <div>
          <h1 className="page-title">Sales Invoices</h1>
          <p className="page-subtitle">Invoice archive — {year}</p>
        </div>
        <Link href="/sales/invoices/new" className="btn btn-primary">+ New Invoice</Link>
      </div>

      {/* Stats row */}
      <div className="grid-3" style={{ marginBottom: "1.5rem" }}>
        <div className="stat-card">
          <div className="stat-icon" style={{ background: "rgba(99,102,241,0.2)" }}>
            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#6366f1" strokeWidth="2"><path d="M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2" /></svg>
          </div>
          <div>
            <div className="stat-value">{invoices.length}</div>
            <div className="stat-label">Total Invoices</div>
          </div>
        </div>
        <div className="stat-card">
          <div className="stat-icon" style={{ background: "rgba(16,185,129,0.2)" }}>
            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#10b981" strokeWidth="2"><path d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" /></svg>
          </div>
          <div>
            <div className="stat-value">{formatCurrency(totalRevenue)}</div>
            <div className="stat-label">Total Revenue (SDG)</div>
          </div>
        </div>
      </div>

      {/* Filter bar */}
      <div className="card">
        <div className="card-header">
          <span className="card-title">Invoices</span>
          <div style={{ display: "flex", gap: "0.5rem" }}>
            <select className="form-control" style={{ width: "120px" }} value={year}
              onChange={(e) => setYear(Number(e.target.value))}>
              {Array.from({ length: 5 }, (_, i) => new Date().getFullYear() - i).map((y) => (
                <option key={y} value={y}>{y}</option>
              ))}
            </select>
            <input type="text" className="form-control" style={{ width: "240px" }}
              placeholder="Search customer or item…"
              value={search} onChange={(e) => setSearch(e.target.value)} />
          </div>
        </div>

        <div className="data-table-wrapper">
          <table className="data-table">
            <thead>
              <tr>
                <th>Inv #</th><th>Customer</th><th>Items</th>
                <th>Discount</th><th>VAT</th><th>Net Amount</th><th>Date</th><th>By</th><th></th>
              </tr>
            </thead>
            <tbody>
              {loading ? (
                <tr><td colSpan={8} style={{ textAlign: "center", padding: "2rem", color: "var(--text-muted)" }}>Loading…</td></tr>
              ) : invoices.length === 0 ? (
                <tr><td colSpan={8} style={{ textAlign: "center", padding: "2rem", color: "var(--text-muted)" }}>No invoices found.</td></tr>
              ) : (
                invoices.map((inv) => (
                  <tr key={inv.invNo}>
                    <td style={{ fontWeight: 600, color: "var(--accent-light)" }}>{inv.invNo}</td>
                    <td style={{ fontWeight: 500 }}>{inv.custName}</td>
                    <td>{inv._count?.invNo ?? "—"}</td>
                    <td>{inv.disc}%</td>
                    <td>{inv.vat}%</td>
                    <td style={{ fontWeight: 600 }}>{formatCurrency(inv._sum?.netAmount ?? 0)} SDG</td>
                    <td style={{ color: "var(--text-muted)" }}>{new Date(inv.transDate).toLocaleDateString()}</td>
                    <td style={{ color: "var(--text-secondary)" }}>{inv.employee || "—"}</td>
                    <td>
                      <Link href={`/sales/invoices/${inv.invNo}/print?year=${year}`} target="_blank"
                        style={{ fontSize: "0.8rem", color: "var(--accent-light)", textDecoration: "none" }}
                        title="Print invoice">
                        🖨
                      </Link>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}
