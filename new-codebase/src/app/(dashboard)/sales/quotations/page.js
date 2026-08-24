"use client";

import { useState, useEffect } from "react";
import Link from "next/link";

export default function QuotationsPage() {
  const currentYear = new Date().getFullYear();
  const [year, setYear] = useState(currentYear);
  const [quotations, setQuotations] = useState([]);
  const [loading, setLoading] = useState(true);
  const [q, setQ] = useState("");

  useEffect(() => {
    setLoading(true);
    const params = new URLSearchParams({ year });
    if (q) params.set("q", q);
    fetch(`/api/sales/quotations?${params}`)
      .then((r) => r.json())
      .then((d) => { setQuotations(d); setLoading(false); })
      .catch(() => setLoading(false));
  }, [year, q]);

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Quotations</h1>
          <p className="page-subtitle">Price proposals — no stock deduction</p>
        </div>
        <Link href="/sales/quotations/new" className="btn btn-primary">+ New Quotation</Link>
      </div>

      <div className="card">
        <div className="card-header" style={{ gap: "0.75rem", flexWrap: "wrap" }}>
          <h3 className="card-title">All Quotations</h3>
          <input className="input" placeholder="Search client or item…" value={q}
            onChange={(e) => setQ(e.target.value)} style={{ flex: 1, maxWidth: "280px" }} />
          <select className="input" style={{ width: "auto" }} value={year} onChange={(e) => setYear(Number(e.target.value))}>
            {[0, 1, 2, 3, 4].map((i) => <option key={i} value={currentYear - i}>{currentYear - i}</option>)}
          </select>
        </div>

        {loading ? <div className="table-empty">Loading…</div> : !quotations.length ? (
          <div className="table-empty">No quotations found.</div>
        ) : (
          <div className="table-wrapper">
            <table className="table">
              <thead>
                <tr>
                  <th>Quote No.</th><th>Client</th><th>Date</th>
                  <th className="text-right">Total (SDG)</th><th>Employee</th>
                </tr>
              </thead>
              <tbody>
                {quotations.map((q) => (
                  <tr key={q.InvNo}>
                    <td><span className="badge">{q.InvNo}</span></td>
                    <td>{q.CustName}</td>
                    <td style={{ fontSize: "0.8rem" }}>{q.TransDate ? new Date(q.TransDate).toLocaleDateString() : "—"}</td>
                    <td className="text-right">{Number(q.totalSdg || 0).toLocaleString("en-US", { minimumFractionDigits: 2 })}</td>
                    <td style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>{q.employee}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}
