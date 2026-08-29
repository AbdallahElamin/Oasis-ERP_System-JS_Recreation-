"use client";

import { useState } from "react";

const today = new Date().toISOString().split("T")[0];
const startOfYear = `${new Date().getFullYear()}-01-01`;
const MONTHS = ["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"];

function MiniBar({ value, max }) {
  const pct = max > 0 ? Math.round((value / max) * 100) : 0;
  return (
    <div style={{ display: "flex", alignItems: "center", gap: "0.5rem" }}>
      <div style={{ flex: 1, height: "6px", background: "var(--bg-tertiary)", borderRadius: "3px", overflow: "hidden" }}>
        <div style={{ width: `${pct}%`, height: "100%", background: "var(--accent-primary)", borderRadius: "3px", transition: "width 0.4s" }} />
      </div>
      <span style={{ fontSize: "0.7rem", color: "var(--text-muted)", minWidth: "2.5rem", textAlign: "right" }}>{pct}%</span>
    </div>
  );
}

export default function SalesReportsPage() {
  const [tab, setTab] = useState("by-item");
  const [from, setFrom] = useState(startOfYear);
  const [to, setTo] = useState(today);
  const [year, setYear] = useState(new Date().getFullYear());
  const [rows, setRows] = useState([]);
  const [loading, setLoading] = useState(false);
  const [generated, setGenerated] = useState(false);

  async function generate() {
    setLoading(true); setGenerated(false);
    try {
      let url = `/api/sales/reports?type=${tab}`;
      if (tab === "by-month") url += `&year=${year}`;
      else url += `&from=${from}&to=${to}`;
      const res = await fetch(url);
      const data = await res.json();
      setRows(Array.isArray(data) ? data : []);
      setGenerated(true);
    } finally {
      setLoading(false);
    }
  }

  const maxSales = rows.reduce((m, r) => Math.max(m, Number(r.totalSales || 0)), 0);

  const grandTotal = rows.reduce((s, r) => s + Number(r.totalSales || 0), 0);

  const TABS = [
    { key: "by-item", label: "By Item" },
    { key: "by-month", label: "By Month" },
    { key: "by-client", label: "By Client" },
  ];

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Sales Reports</h1>
          <p className="page-subtitle">Analyse sales performance across items, months, and clients</p>
        </div>
        {generated && rows.length > 0 && (
          <button className="btn btn-secondary" onClick={() => window.print()}>
            🖨 Print
          </button>
        )}
      </div>

      <div className="card" style={{ marginBottom: "1rem" }}>
        {/* Tabs */}
        <div style={{ display: "flex", gap: "0.5rem", marginBottom: "1rem" }}>
          {TABS.map((t) => (
            <button key={t.key} className={`btn ${tab === t.key ? "btn-primary" : "btn-secondary"}`}
              onClick={() => { setTab(t.key); setRows([]); setGenerated(false); }}>
              {t.label}
            </button>
          ))}
        </div>

        {/* Filters */}
        <div style={{ display: "flex", gap: "0.75rem", alignItems: "flex-end", flexWrap: "wrap" }}>
          {tab === "by-month" ? (
            <div className="form-group" style={{ marginBottom: 0 }}>
              <label className="form-label">Year</label>
              <select className="input" value={year} onChange={(e) => setYear(Number(e.target.value))}>
                {[0,1,2].map((i) => <option key={i} value={new Date().getFullYear()-i}>{new Date().getFullYear()-i}</option>)}
              </select>
            </div>
          ) : (
            <>
              <div className="form-group" style={{ marginBottom: 0 }}>
                <label className="form-label">From</label>
                <input type="date" className="input" value={from} onChange={(e) => setFrom(e.target.value)} />
              </div>
              <div className="form-group" style={{ marginBottom: 0 }}>
                <label className="form-label">To</label>
                <input type="date" className="input" value={to} onChange={(e) => setTo(e.target.value)} />
              </div>
            </>
          )}
          <button className="btn btn-primary" onClick={generate} disabled={loading} style={{ marginBottom: 0 }}>
            {loading ? "Generating…" : "Generate Report"}
          </button>
        </div>
      </div>

      {generated && (
        <div className="card">
          <div className="card-header">
            <h3 className="card-title">
              {tab === "by-item" ? "Sales by Item" : tab === "by-month" ? `Sales by Month — ${year}` : "Sales by Client"}
            </h3>
            <span style={{ fontSize: "0.82rem", color: "var(--text-muted)" }}>
              Grand Total: <strong style={{ color: "var(--text-primary)" }}>
                {grandTotal.toLocaleString("en-US", { minimumFractionDigits: 2 })}
              </strong>
            </span>
          </div>

          {!rows.length ? (
            <div className="table-empty">No data for the selected period.</div>
          ) : (
            <div className="table-wrapper">
              <table className="table">
                <thead>
                  <tr>
                    <th>#</th>
                    <th>{tab === "by-item" ? "Item" : tab === "by-month" ? "Month" : "Client"}</th>
                    {tab !== "by-month" && <th className="text-right">Total Qty</th>}
                    <th className="text-right">Invoice Count</th>
                    <th className="text-right">Total Sales</th>
                    <th style={{ width: "140px" }}>Share</th>
                  </tr>
                </thead>
                <tbody>
                  {rows.map((r, i) => {
                    const label = tab === "by-item" ? r.item
                      : tab === "by-month" ? MONTHS[(Number(r.mo) - 1)]
                      : r.CustName;
                    return (
                      <tr key={i}>
                        <td style={{ color: "var(--text-muted)", fontSize: "0.8rem" }}>{i + 1}</td>
                        <td style={{ fontWeight: 500 }}>{label}</td>
                        {tab !== "by-month" && (
                          <td className="text-right" style={{ fontSize: "0.85rem" }}>
                            {Number(r.totalQty || 0).toLocaleString()}
                          </td>
                        )}
                        <td className="text-right" style={{ fontSize: "0.85rem" }}>{r.invoiceCount}</td>
                        <td className="text-right" style={{ fontWeight: 600 }}>
                          {Number(r.totalSales || 0).toLocaleString("en-US", { minimumFractionDigits: 2 })}
                        </td>
                        <td><MiniBar value={Number(r.totalSales)} max={maxSales} /></td>
                      </tr>
                    );
                  })}
                </tbody>
                <tfoot>
                  <tr style={{ fontWeight: 700, borderTop: "2px solid var(--border-subtle)" }}>
                    <td colSpan={tab === "by-month" ? 3 : 4} className="text-right">Grand Total</td>
                    <td className="text-right" style={{ color: "var(--success)" }}>
                      {grandTotal.toLocaleString("en-US", { minimumFractionDigits: 2 })}
                    </td>
                    <td></td>
                  </tr>
                </tfoot>
              </table>
            </div>
          )}
        </div>
      )}
    </div>
  );
}
