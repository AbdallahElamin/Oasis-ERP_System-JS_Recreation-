"use client";

import { useState } from "react";
import DateRangePicker from "@/components/common/DateRangePicker";

const today = new Date().toISOString().split("T")[0];
const yearStart = `${new Date().getFullYear()}-01-01`;

function fmt(n) {
  return Number(n || 0).toLocaleString("en-US", { minimumFractionDigits: 2 });
}

export default function TrialBalancePage() {
  const [range, setRange] = useState({ from: yearStart, to: today });
  const [rows, setRows] = useState([]);
  const [loaded, setLoaded] = useState(false);
  const [loading, setLoading] = useState(false);

  async function handleShow() {
    setLoading(true);
    try {
      const p = new URLSearchParams({ from: range.from, to: range.to });
      const res = await fetch(`/api/finance/trial-balance?${p}`);
      const data = await res.json();
      setRows(data);
      setLoaded(true);
    } finally {
      setLoading(false);
    }
  }

  const totalDebit = rows.reduce((s, r) => s + (Number(r.totalOut) || 0), 0);
  const totalCredit = rows.reduce((s, r) => s + (Number(r.totalIn) || 0), 0);

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Trial Balance</h1>
          <p className="page-subtitle">Account balances for a given period</p>
        </div>
      </div>

      <div className="card" style={{ marginBottom: "1rem" }}>
        <div style={{ display: "flex", alignItems: "center", gap: "1rem", flexWrap: "wrap" }}>
          <DateRangePicker from={range.from} to={range.to} onChange={setRange} />
          <button className="btn btn-primary" onClick={handleShow} disabled={loading}>
            {loading ? "Loading…" : "Show"}
          </button>
        </div>
      </div>

      {loaded && (
        <div className="card">
          <div className="card-header"><h3 className="card-title">Results</h3></div>
          {!rows.length ? (
            <div className="table-empty">No transactions found for this period.</div>
          ) : (
            <div className="table-wrapper">
              <table className="table">
                <thead>
                  <tr>
                    <th>Level 1</th><th>Level 2</th><th>Level 3</th><th>Level 4</th>
                    <th className="text-right">Debit</th>
                    <th className="text-right">Credit</th>
                  </tr>
                </thead>
                <tbody>
                  {rows.map((r, i) => (
                    <tr key={i}>
                      <td style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>{r.Acc1}</td>
                      <td style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>{r.Acc2}</td>
                      <td style={{ fontSize: "0.8rem" }}>{r.Acc3}</td>
                      <td style={{ fontWeight: 600 }}>{r.Acc4 || <span style={{ color: "var(--text-muted)" }}>—</span>}</td>
                      <td className="text-right text-danger">
                        {Number(r.totalOut) > 0 ? fmt(r.totalOut) : ""}
                      </td>
                      <td className="text-right text-success">
                        {Number(r.totalIn) > 0 ? fmt(r.totalIn) : ""}
                      </td>
                    </tr>
                  ))}
                </tbody>
                <tfoot>
                  <tr style={{ fontWeight: 700, borderTop: "2px solid var(--border-subtle)" }}>
                    <td colSpan={4} className="text-right" style={{ paddingRight: "1rem" }}>Total</td>
                    <td className="text-right text-danger">{fmt(totalDebit)}</td>
                    <td className="text-right text-success">{fmt(totalCredit)}</td>
                  </tr>
                  <tr>
                    <td colSpan={4} className="text-right" style={{ paddingRight: "1rem", fontSize: "0.8rem", color: "var(--text-muted)" }}>
                      Net Balance
                    </td>
                    <td colSpan={2} className="text-right" style={{ fontWeight: 700 }}>
                      <span style={{ color: totalCredit >= totalDebit ? "var(--success)" : "var(--danger)" }}>
                        {fmt(Math.abs(totalCredit - totalDebit))} {totalCredit >= totalDebit ? "Cr" : "Dr"}
                      </span>
                    </td>
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
