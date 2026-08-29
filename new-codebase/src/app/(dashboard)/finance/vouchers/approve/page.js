"use client";

import { useState, useEffect, useCallback } from "react";
import Link from "next/link";

export default function VoucherApprovePage() {
  const [pending, setPending] = useState([]);
  const [selectedMoveNo, setSelectedMoveNo] = useState(null);
  const [lines, setLines] = useState([]);
  const [loadingList, setLoadingList] = useState(true);
  const [loadingLines, setLoadingLines] = useState(false);
  const [approving, setApproving] = useState(false);
  const [deleting, setDeleting] = useState(false);
  const [toast, setToast] = useState(null);

  const showToast = (msg, type = "success") => {
    setToast({ msg, type });
    setTimeout(() => setToast(null), 3000);
  };

  const loadPending = useCallback(async () => {
    setLoadingList(true);
    const res = await fetch("/api/finance/vouchers/approve");
    const data = await res.json();
    setPending(Array.isArray(data) ? data : []);
    setLoadingList(false);
  }, []);

  useEffect(() => { loadPending(); }, [loadPending]);

  async function selectVoucher(moveNo) {
    setSelectedMoveNo(moveNo);
    setLoadingLines(true);
    const year = new Date().getFullYear(); // default; pending ones should be current year
    const res = await fetch(`/api/finance/vouchers/lines?moveNo=${moveNo}&year=${year}`);
    const data = await res.json();
    setLines(Array.isArray(data) ? data : []);
    setLoadingLines(false);
  }

  async function approveVoucher() {
    setApproving(true);
    try {
      await fetch("/api/finance/vouchers/approve", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ moveNo: selectedMoveNo }),
      });
      showToast(`Voucher #${selectedMoveNo} approved.`);
      setSelectedMoveNo(null); setLines([]);
      loadPending();
    } finally {
      setApproving(false);
    }
  }

  async function deleteVoucher() {
    if (!confirm(`Delete Voucher #${selectedMoveNo}? This cannot be undone.`)) return;
    setDeleting(true);
    try {
      await fetch("/api/finance/vouchers/approve", {
        method: "DELETE",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ moveNo: selectedMoveNo }),
      });
      showToast(`Voucher #${selectedMoveNo} deleted.`, "danger");
      setSelectedMoveNo(null); setLines([]);
      loadPending();
    } finally {
      setDeleting(false);
    }
  }

  const totalDebit = lines.reduce((s, l) => s + Number(l.TotalValueOut || 0), 0);
  const totalCredit = lines.reduce((s, l) => s + Number(l.TotalValueIn || 0), 0);
  const isBalanced = Math.abs(totalDebit - totalCredit) < 0.01;

  return (
    <div>
      {toast && (
        <div className="alert" style={{
          position: "fixed", top: "1rem", right: "1rem", zIndex: 9999,
          background: toast.type === "danger" ? "rgba(239,68,68,0.15)" : "rgba(16,185,129,0.15)",
          color: toast.type === "danger" ? "var(--danger)" : "var(--success)",
          border: `1px solid ${toast.type === "danger" ? "rgba(239,68,68,0.3)" : "rgba(16,185,129,0.3)"}`,
          borderRadius: "0.5rem", padding: "0.75rem 1.25rem", fontSize: "0.85rem", minWidth: "220px",
        }}>
          {toast.type === "danger" ? "🗑 " : "✓ "}{toast.msg}
        </div>
      )}

      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Approve Vouchers</h1>
          <p className="page-subtitle">Review and approve pending journal entries</p>
        </div>
        <button className="btn btn-secondary" onClick={loadPending}>↻ Refresh</button>
      </div>

      <div style={{ display: "grid", gridTemplateColumns: "280px 1fr", gap: "1rem" }}>
        {/* Pending list */}
        <div className="card" style={{ height: "fit-content" }}>
          <div className="card-header">
            <h3 className="card-title">Pending</h3>
            <span className="badge" style={{ background: "rgba(239,68,68,0.15)", color: "var(--danger)" }}>
              {pending.length}
            </span>
          </div>
          {loadingList ? (
            <div className="table-empty">Loading…</div>
          ) : !pending.length ? (
            <div className="table-empty">No pending vouchers. 🎉</div>
          ) : (
            <div style={{ display: "flex", flexDirection: "column", gap: "0.25rem" }}>
              {pending.map((p) => (
                <button key={p.MoveNo} onClick={() => selectVoucher(p.MoveNo)}
                  style={{
                    display: "flex", flexDirection: "column", textAlign: "left",
                    padding: "0.65rem 0.75rem", borderRadius: "0.375rem", border: "none",
                    background: selectedMoveNo === p.MoveNo ? "rgba(99,102,241,0.15)" : "var(--bg-tertiary)",
                    color: selectedMoveNo === p.MoveNo ? "var(--accent-light)" : "var(--text-secondary)",
                    cursor: "pointer", transition: "background 0.15s",
                  }}>
                  <span style={{ fontWeight: 600 }}>#{p.MoveNo}</span>
                  <span style={{ fontSize: "0.72rem", color: "var(--text-muted)" }}>
                    {p.TransType} · {new Date(p.TransDate).toLocaleDateString()}
                  </span>
                </button>
              ))}
            </div>
          )}
        </div>

        {/* Voucher detail */}
        <div className="card">
          {!selectedMoveNo ? (
            <div className="table-empty" style={{ padding: "4rem" }}>
              Select a voucher from the list to review it.
            </div>
          ) : loadingLines ? (
            <div className="table-empty">Loading lines…</div>
          ) : (
            <>
              <div className="card-header">
                <h3 className="card-title">Voucher #{selectedMoveNo}</h3>
                <span className="badge" style={{
                  background: isBalanced ? "rgba(16,185,129,0.15)" : "rgba(239,68,68,0.15)",
                  color: isBalanced ? "var(--success)" : "var(--danger)",
                }}>
                  {isBalanced ? "Balanced ✓" : "Not balanced ✗"}
                </span>
              </div>

              <div className="table-wrapper">
                <table className="table">
                  <thead>
                    <tr><th>Acc1</th><th>Acc2</th><th>Acc3</th><th>Acc4</th>
                        <th>Description</th><th className="text-right">Debit</th><th className="text-right">Credit</th></tr>
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
                <Link href={`/finance/vouchers/${selectedMoveNo}/print`} target="_blank" className="btn btn-secondary">
                  🖨 Print
                </Link>
                <button className="btn btn-danger" onClick={deleteVoucher} disabled={deleting}>
                  {deleting ? "Deleting…" : "🗑 Delete"}
                </button>
                <button className="btn btn-primary" onClick={approveVoucher}
                  disabled={approving || !isBalanced}
                  title={!isBalanced ? "Voucher must be balanced before approving" : ""}>
                  {approving ? "Approving…" : "✓ Approve"}
                </button>
              </div>
            </>
          )}
        </div>
      </div>
    </div>
  );
}
