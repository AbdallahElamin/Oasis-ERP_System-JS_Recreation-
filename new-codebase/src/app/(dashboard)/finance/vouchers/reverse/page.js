"use client";

import { useState } from "react";
import Link from "next/link";

export default function VoucherReversePage() {
  const [moveNo, setMoveNo] = useState("");
  const [year, setYear] = useState(new Date().getFullYear());
  const [lines, setLines] = useState([]);
  const [status, setStatus] = useState(null); // "active" | "reversed"
  const [loading, setLoading] = useState(false);
  const [reversing, setReversing] = useState(false);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);

  async function loadVoucher() {
    if (!moveNo) return;
    setLoading(true); setError(null); setSuccess(null); setLines([]);
    try {
      const res = await fetch(`/api/finance/vouchers/lines?moveNo=${moveNo}&year=${year}`);
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Not found");
      if (!data.length) throw new Error("Voucher not found");
      setLines(data);
      setStatus(data.some((l) => l.Reversed) ? "reversed" : "active");
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  }

  async function reverseVoucher() {
    setReversing(true); setError(null);
    try {
      const res = await fetch("/api/finance/vouchers/reverse", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ moveNo: parseInt(moveNo), year }),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Reversal failed");
      setSuccess(`Voucher #${moveNo} reversed. New reversing voucher: #${data.newMoveNo}`);
      setStatus("reversed");
      setLines((l) => l.map((x) => ({ ...x, Reversed: 1 })));
    } catch (err) {
      setError(err.message);
    } finally {
      setReversing(false);
    }
  }

  const totalDebit = lines.reduce((s, l) => s + Number(l.TotalValueOut || 0), 0);
  const totalCredit = lines.reduce((s, l) => s + Number(l.TotalValueIn || 0), 0);
  const years = [0, 1, 2].map((i) => new Date().getFullYear() - i);

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Reverse Voucher</h1>
          <p className="page-subtitle">Look up a journal voucher and create a reversing entry</p>
        </div>
      </div>

      {/* Lookup */}
      <div className="card" style={{ marginBottom: "1rem", maxWidth: "480px" }}>
        <div className="card-header"><h3 className="card-title">Look Up Voucher</h3></div>
        <div style={{ display: "flex", gap: "0.75rem", alignItems: "flex-end" }}>
          <div className="form-group" style={{ flex: 1, marginBottom: 0 }}>
            <label className="form-label">Move No.</label>
            <input className="input" type="number" placeholder="e.g. 15"
              value={moveNo} onChange={(e) => setMoveNo(e.target.value)}
              onKeyDown={(e) => e.key === "Enter" && loadVoucher()} />
          </div>
          <div className="form-group" style={{ width: "110px", marginBottom: 0 }}>
            <label className="form-label">Year</label>
            <select className="input" value={year} onChange={(e) => setYear(Number(e.target.value))}>
              {years.map((y) => <option key={y} value={y}>{y}</option>)}
            </select>
          </div>
          <button className="btn btn-primary" onClick={loadVoucher} disabled={loading || !moveNo}>
            {loading ? "Loading…" : "Load"}
          </button>
        </div>
      </div>

      {error && <div className="alert alert-danger" style={{ marginBottom: "1rem" }}>{error}</div>}
      {success && <div className="alert alert-success" style={{ marginBottom: "1rem" }}>✓ {success}</div>}

      {lines.length > 0 && (
        <div className="card">
          <div className="card-header">
            <h3 className="card-title">Voucher #{moveNo} — {lines[0].TransType}</h3>
            <span className="badge" style={{
              background: status === "reversed" ? "rgba(239,68,68,0.15)" : "rgba(16,185,129,0.15)",
              color: status === "reversed" ? "var(--danger)" : "var(--success)",
            }}>
              {status === "reversed" ? "Reversed" : "Active"}
            </span>
          </div>

          <div style={{ display: "flex", gap: "1rem", fontSize: "0.82rem", color: "var(--text-muted)", marginBottom: "0.75rem" }}>
            <span>Date: <strong style={{ color: "var(--text-secondary)" }}>{new Date(lines[0].TransDate).toLocaleDateString()}</strong></span>
            <span>Employee: <strong style={{ color: "var(--text-secondary)" }}>{lines[0].employee || "—"}</strong></span>
          </div>

          <div className="table-wrapper">
            <table className="table">
              <thead>
                <tr><th>Acc1</th><th>Acc2</th><th>Acc3</th><th>Acc4</th><th>Description</th>
                    <th className="text-right">Debit</th><th className="text-right">Credit</th></tr>
              </thead>
              <tbody>
                {lines.map((l, i) => (
                  <tr key={i}>
                    <td style={{ fontSize: "0.75rem" }}>{l.Acc1 || "—"}</td>
                    <td style={{ fontSize: "0.75rem" }}>{l.Acc2 || "—"}</td>
                    <td style={{ fontSize: "0.75rem" }}>{l.Acc3 || "—"}</td>
                    <td style={{ fontWeight: 500 }}>{l.Acc4 || "—"}</td>
                    <td style={{ fontSize: "0.8rem" }}>{l.Ref || "—"}</td>
                    <td className="text-right">{Number(l.TotalValueOut) > 0 ? Number(l.TotalValueOut).toFixed(2) : ""}</td>
                    <td className="text-right">{Number(l.TotalValueIn) > 0 ? Number(l.TotalValueIn).toFixed(2) : ""}</td>
                  </tr>
                ))}
              </tbody>
              <tfoot>
                <tr style={{ fontWeight: 700 }}>
                  <td colSpan={5} className="text-right">Totals</td>
                  <td className="text-right">{totalDebit.toFixed(2)}</td>
                  <td className="text-right">{totalCredit.toFixed(2)}</td>
                </tr>
              </tfoot>
            </table>
          </div>

          <div style={{ display: "flex", gap: "0.75rem", justifyContent: "flex-end", marginTop: "1rem" }}>
            <Link href={`/finance/vouchers/${moveNo}/print?year=${year}`}
              target="_blank" className="btn btn-secondary">
              🖨 Print
            </Link>
            {status === "active" && (
              <button className="btn btn-danger" onClick={() => {
                if (confirm(`Are you sure you want to reverse Voucher #${moveNo}? This cannot be undone.`))
                  reverseVoucher();
              }} disabled={reversing}>
                {reversing ? "Reversing…" : "↩ Reverse Voucher"}
              </button>
            )}
          </div>
        </div>
      )}
    </div>
  );
}
