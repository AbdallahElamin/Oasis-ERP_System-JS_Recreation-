"use client";

import { useState } from "react";
import AccountTree from "@/components/finance/AccountTree";
import DateRangePicker from "@/components/common/DateRangePicker";

const today = new Date().toISOString().split("T")[0];
const yearStart = `${new Date().getFullYear()}-01-01`;

function fmt(n) {
  return Number(n || 0).toLocaleString("en-US", { minimumFractionDigits: 2 });
}

export default function StatementsPage() {
  const [selected, setSelected] = useState(null);
  const [range, setRange] = useState({ from: yearStart, to: today });
  const [result, setResult] = useState(null);
  const [loading, setLoading] = useState(false);

  async function handleShow() {
    if (!selected) return;
    setLoading(true);
    try {
      const p = new URLSearchParams({ from: range.from, to: range.to, acc1: selected.acc1 });
      if (selected.acc2) p.set("acc2", selected.acc2);
      if (selected.acc3) p.set("acc3", selected.acc3);
      if (selected.acc4) p.set("acc4", selected.acc4);

      const res = await fetch(`/api/finance/statements?${p}`);
      const data = await res.json();
      setResult(data);
    } finally {
      setLoading(false);
    }
  }

  // Calculate running balance as rows accumulate
  function buildRunning(rows, openingBalance) {
    let running = openingBalance;
    return rows.map((r) => {
      running += (Number(r.credit) || 0) - (Number(r.debit) || 0);
      return { ...r, running };
    });
  }

  const runningRows = result ? buildRunning(result.rows, result.openingBalance) : [];

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Account Statement</h1>
          <p className="page-subtitle">Select an account from the tree, then choose a period</p>
        </div>
      </div>

      <div style={{ display: "grid", gridTemplateColumns: "280px 1fr", gap: "1.5rem", alignItems: "start" }}>
        {/* Tree panel */}
        <div className="card" style={{ maxHeight: "80vh", overflowY: "auto", position: "sticky", top: "1rem" }}>
          <div className="card-header"><h3 className="card-title">Accounts</h3></div>
          <AccountTree onSelect={setSelected} selectedNode={selected} />
        </div>

        {/* Statement panel */}
        <div>
          {/* Controls */}
          <div className="card" style={{ marginBottom: "1rem" }}>
            <div style={{ display: "flex", alignItems: "center", gap: "1rem", flexWrap: "wrap" }}>
              <div style={{ flex: 1, minWidth: 0 }}>
                {selected ? (
                  <span style={{ fontWeight: 600, color: "var(--accent-light)" }}>
                    {[selected.acc1, selected.acc2, selected.acc3, selected.acc4]
                      .filter(Boolean)
                      .join(" › ")}
                  </span>
                ) : (
                  <span style={{ color: "var(--text-muted)", fontSize: "0.875rem" }}>No account selected</span>
                )}
              </div>
              <DateRangePicker from={range.from} to={range.to} onChange={setRange} label="" />
              <button
                className="btn btn-primary"
                onClick={handleShow}
                disabled={loading || !selected}
              >
                {loading ? "Loading…" : "Show Statement"}
              </button>
            </div>
          </div>

          {/* Statement table */}
          {result && (
            <div className="card">
              <div className="card-header">
                <h3 className="card-title">Statement</h3>
                <span style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>
                  {range.from} — {range.to}
                </span>
              </div>
              <div className="table-wrapper">
                <table className="table">
                  <thead>
                    <tr>
                      <th>Move No.</th>
                      <th>Date</th>
                      <th>Description / Ref</th>
                      <th>Type</th>
                      <th className="text-right">Credit (In)</th>
                      <th className="text-right">Debit (Out)</th>
                      <th className="text-right">Balance</th>
                    </tr>
                  </thead>
                  <tbody>
                    {/* Opening balance row */}
                    <tr style={{ background: "rgba(99,102,241,0.06)" }}>
                      <td>—</td>
                      <td style={{ color: "var(--text-muted)", fontSize: "0.8rem" }}>Opening</td>
                      <td style={{ fontWeight: 600 }}>Opening Balance</td>
                      <td>—</td>
                      <td className="text-right"></td>
                      <td className="text-right"></td>
                      <td className="text-right" style={{ fontWeight: 700 }}>
                        <span style={{ color: result.openingBalance >= 0 ? "var(--success)" : "var(--danger)" }}>
                          {fmt(Math.abs(result.openingBalance))} {result.openingBalance >= 0 ? "Cr" : "Dr"}
                        </span>
                      </td>
                    </tr>

                    {runningRows.map((r, i) => (
                      <tr key={i}>
                        <td><span className="badge">{r.MoveNo}</span></td>
                        <td style={{ fontSize: "0.8rem" }}>
                          {r.TransDate ? new Date(r.TransDate).toLocaleDateString() : "—"}
                        </td>
                        <td style={{ color: "var(--text-muted)", fontSize: "0.82rem" }}>{r.Ref || r.TransType}</td>
                        <td style={{ fontSize: "0.8rem" }}>{r.TransType}</td>
                        <td className="text-right text-success">{Number(r.credit) > 0 ? fmt(r.credit) : ""}</td>
                        <td className="text-right text-danger">{Number(r.debit) > 0 ? fmt(r.debit) : ""}</td>
                        <td className="text-right" style={{ fontWeight: 600 }}>
                          <span style={{ color: r.running >= 0 ? "var(--success)" : "var(--danger)" }}>
                            {fmt(Math.abs(r.running))} {r.running >= 0 ? "Cr" : "Dr"}
                          </span>
                        </td>
                      </tr>
                    ))}

                    {!result.rows.length && (
                      <tr>
                        <td colSpan={7} style={{ textAlign: "center", color: "var(--text-muted)", padding: "1.5rem" }}>
                          No transactions in this period
                        </td>
                      </tr>
                    )}
                  </tbody>
                </table>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
