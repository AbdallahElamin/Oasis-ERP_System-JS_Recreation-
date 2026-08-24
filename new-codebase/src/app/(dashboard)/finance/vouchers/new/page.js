"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import AccountTree from "@/components/finance/AccountTree";

const today = new Date().toISOString().split("T")[0];

function fmt(n) {
  return Number(n || 0).toLocaleString("en-US", { minimumFractionDigits: 2 });
}

export default function NewVoucherPage() {
  const router = useRouter();

  // Voucher header
  const [date, setDate] = useState(today);

  // Current line being built
  const [line, setLine] = useState({ acc1: "", acc2: "", acc3: "", acc4: "", type: "Debit", amount: "", description: "" });

  // Voucher lines already added
  const [lines, setLines] = useState([]);

  // Save state
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState(null);

  // Handle tree node selection → fill acc1-4 on current line
  function handleTreeSelect(node) {
    setLine((l) => ({
      ...l,
      acc1: node.acc1 || "",
      acc2: node.acc2 || "",
      acc3: node.acc3 || "",
      acc4: node.acc4 || "",
    }));
  }

  function addLine() {
    if (!line.acc1 || !line.acc2 || !line.acc3 || !line.acc4) {
      setError("Select an account from the tree (all 4 levels required)");
      return;
    }
    const amount = parseFloat(line.amount);
    if (!amount || amount <= 0) {
      setError("Enter a valid amount");
      return;
    }
    setError(null);
    setLines((prev) => [...prev, {
      acc1: line.acc1, acc2: line.acc2, acc3: line.acc3, acc4: line.acc4,
      description: line.description,
      debit: line.type === "Debit" ? amount : 0,
      credit: line.type === "Credit" ? amount : 0,
    }]);
    setLine((l) => ({ ...l, amount: "", description: "", acc1: "", acc2: "", acc3: "", acc4: "" }));
  }

  function removeLine(idx) {
    setLines((prev) => prev.filter((_, i) => i !== idx));
  }

  const totalDebit = lines.reduce((s, l) => s + l.debit, 0);
  const totalCredit = lines.reduce((s, l) => s + l.credit, 0);
  const balance = totalCredit - totalDebit;
  const isBalanced = Math.abs(balance) < 0.001;

  async function handleSave() {
    if (!lines.length) { setError("Add at least one line"); return; }
    if (!isBalanced) { setError(`Voucher not balanced. Difference: ${fmt(Math.abs(balance))}`); return; }

    setSaving(true);
    setError(null);
    try {
      const res = await fetch("/api/finance/vouchers", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ date, lines }),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Failed to save");
      router.push("/finance/vouchers");
    } catch (err) {
      setError(err.message);
    } finally {
      setSaving(false);
    }
  }

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">New Journal Voucher</h1>
          <p className="page-subtitle">Add balanced debit and credit lines</p>
        </div>
        <div style={{ display: "flex", gap: "0.5rem" }}>
          <button className="btn btn-secondary" onClick={() => router.back()}>Cancel</button>
          <button
            className="btn btn-primary"
            onClick={handleSave}
            disabled={saving || !isBalanced || !lines.length}
          >
            {saving ? "Saving…" : "Save Voucher"}
          </button>
        </div>
      </div>

      {error && (
        <div className="alert alert-danger" style={{ marginBottom: "1rem" }}>{error}</div>
      )}

      <div style={{ display: "grid", gridTemplateColumns: "280px 1fr", gap: "1.5rem", alignItems: "start" }}>
        {/* CoA Tree */}
        <div className="card" style={{ maxHeight: "75vh", overflowY: "auto", position: "sticky", top: "1rem" }}>
          <div className="card-header"><h3 className="card-title">Accounts</h3></div>
          <p style={{ fontSize: "0.75rem", color: "var(--text-muted)", marginBottom: "0.5rem", padding: "0 0.25rem" }}>
            Click a leaf account to fill fields →
          </p>
          <AccountTree onSelect={handleTreeSelect} selectable="leaves" />
        </div>

        {/* Voucher Builder */}
        <div>
          {/* Date + "Add Line" form */}
          <div className="card" style={{ marginBottom: "1rem" }}>
            <div className="card-header"><h3 className="card-title">Voucher Details</h3></div>
            <div style={{ display: "grid", gridTemplateColumns: "auto auto auto 1fr auto", gap: "0.75rem", alignItems: "end" }}>
              <div className="form-group" style={{ marginBottom: 0 }}>
                <label className="form-label">Date</label>
                <input type="date" className="input" value={date} onChange={(e) => setDate(e.target.value)} />
              </div>
              <div className="form-group" style={{ marginBottom: 0 }}>
                <label className="form-label">Account</label>
                <input
                  className="input"
                  value={line.acc4 ? `${line.acc3} › ${line.acc4}` : ""}
                  readOnly
                  placeholder="Select from tree"
                  style={{ minWidth: "200px", color: "var(--text-muted)" }}
                />
              </div>
              <div className="form-group" style={{ marginBottom: 0 }}>
                <label className="form-label">Type</label>
                <select className="input" value={line.type} onChange={(e) => setLine((l) => ({ ...l, type: e.target.value }))}>
                  <option>Debit</option>
                  <option>Credit</option>
                </select>
              </div>
              <div className="form-group" style={{ marginBottom: 0 }}>
                <label className="form-label">Description</label>
                <input className="input" placeholder="Description…" value={line.description}
                  onChange={(e) => setLine((l) => ({ ...l, description: e.target.value }))} />
              </div>
              <div className="form-group" style={{ marginBottom: 0 }}>
                <label className="form-label">Amount</label>
                <input className="input" type="number" min="0" step="0.01" placeholder="0.00" value={line.amount}
                  onChange={(e) => setLine((l) => ({ ...l, amount: e.target.value }))}
                  onKeyDown={(e) => { if (e.key === "Enter") addLine(); }} />
              </div>
            </div>
            <button className="btn btn-secondary" style={{ marginTop: "0.75rem" }} onClick={addLine}>
              + Add Line
            </button>
          </div>

          {/* Voucher lines table */}
          <div className="card">
            <div className="card-header"><h3 className="card-title">Voucher Lines</h3></div>
            {!lines.length ? (
              <div className="table-empty">No lines added yet. Select an account and add lines above.</div>
            ) : (
              <div className="table-wrapper">
                <table className="table">
                  <thead>
                    <tr>
                      <th>Acc1</th><th>Acc2</th><th>Acc3</th><th>Acc4</th>
                      <th>Description</th>
                      <th className="text-right">Debit</th>
                      <th className="text-right">Credit</th>
                      <th></th>
                    </tr>
                  </thead>
                  <tbody>
                    {lines.map((l, i) => (
                      <tr key={i}>
                        <td style={{ fontSize: "0.8rem" }}>{l.acc1}</td>
                        <td style={{ fontSize: "0.8rem" }}>{l.acc2}</td>
                        <td style={{ fontSize: "0.8rem" }}>{l.acc3}</td>
                        <td style={{ fontWeight: 600 }}>{l.acc4}</td>
                        <td style={{ color: "var(--text-muted)", fontSize: "0.8rem" }}>{l.description || "—"}</td>
                        <td className="text-right text-danger">{l.debit > 0 ? fmt(l.debit) : ""}</td>
                        <td className="text-right text-success">{l.credit > 0 ? fmt(l.credit) : ""}</td>
                        <td>
                          <button
                            onClick={() => removeLine(i)}
                            style={{ background: "none", border: "none", color: "var(--danger)", cursor: "pointer", fontSize: "1rem" }}
                          >×</button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                  <tfoot>
                    <tr style={{ fontWeight: 700, borderTop: "2px solid var(--border-subtle)" }}>
                      <td colSpan={5} style={{ textAlign: "right", paddingRight: "1rem" }}>Totals</td>
                      <td className="text-right text-danger">{fmt(totalDebit)}</td>
                      <td className="text-right text-success">{fmt(totalCredit)}</td>
                      <td></td>
                    </tr>
                    <tr>
                      <td colSpan={5} style={{ textAlign: "right", paddingRight: "1rem", fontSize: "0.8rem", color: "var(--text-muted)" }}>
                        Balance
                      </td>
                      <td colSpan={2} className="text-right" style={{ fontWeight: 700, color: isBalanced ? "var(--success)" : "var(--danger)" }}>
                        {isBalanced ? "✓ Balanced" : fmt(Math.abs(balance))}
                      </td>
                      <td></td>
                    </tr>
                  </tfoot>
                </table>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
