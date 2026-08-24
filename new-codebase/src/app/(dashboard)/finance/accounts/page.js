"use client";

import { useState, useEffect } from "react";
import AccountTree from "@/components/finance/AccountTree";

export default function ChartOfAccountsPage() {
  const [selected, setSelected] = useState(null);
  const [balance, setBalance] = useState(null);
  const [loadingBal, setLoadingBal] = useState(false);

  // Add account form state
  const [form, setForm] = useState({ acc1: "", acc2: "", acc3: "", acc4: "" });
  const [saving, setSaving] = useState(false);
  const [msg, setMsg] = useState(null);
  const [treeKey, setTreeKey] = useState(0); // force tree reload

  // Fetch balance when a leaf is selected
  useEffect(() => {
    if (!selected || selected.level < 3) {
      setBalance(null);
      return;
    }
    setLoadingBal(true);
    const p = new URLSearchParams({
      acc1: selected.acc1,
      acc2: selected.acc2,
      acc3: selected.acc3,
      acc4: selected.acc4,
    });
    fetch(`/api/finance/statements/balance?${p}`)
      .then((r) => r.json())
      .then((d) => setBalance(d.balance ?? null))
      .catch(() => setBalance(null))
      .finally(() => setLoadingBal(false));
  }, [selected]);

  // When a tree node is clicked, auto-fill form levels up to that depth
  function handleTreeSelect(node) {
    setSelected(node);
    setForm({
      acc1: node.acc1 || "",
      acc2: node.acc2 || "",
      acc3: node.acc3 || "",
      acc4: node.acc4 || "",
    });
  }

  async function handleAdd(e) {
    e.preventDefault();
    setSaving(true);
    setMsg(null);
    try {
      const res = await fetch("/api/finance/accounts", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(form),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Failed");
      setMsg({ type: "success", text: "Account added successfully" });
      setForm({ acc1: "", acc2: "", acc3: "", acc4: "" });
      setSelected(null);
      setTreeKey((k) => k + 1);
    } catch (err) {
      setMsg({ type: "error", text: err.message });
    } finally {
      setSaving(false);
    }
  }

  async function handleDelete() {
    if (!selected || selected.level < 3) return;
    if (!confirm(`Delete account "${selected.acc4}"? This cannot be undone.`)) return;
    try {
      const res = await fetch("/api/finance/accounts", {
        method: "DELETE",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(selected),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Failed");
      setMsg({ type: "success", text: "Account deleted" });
      setSelected(null);
      setTreeKey((k) => k + 1);
    } catch (err) {
      setMsg({ type: "error", text: err.message });
    }
  }

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Chart of Accounts</h1>
          <p className="page-subtitle">4-level account hierarchy · click a leaf account to view its balance</p>
        </div>
      </div>

      <div style={{ display: "grid", gridTemplateColumns: "1fr 360px", gap: "1.5rem", alignItems: "start" }}>
        {/* Tree panel */}
        <div className="card" style={{ minHeight: "60vh", maxHeight: "75vh", overflowY: "auto" }}>
          <div className="card-header">
            <h3 className="card-title">Account Hierarchy</h3>
          </div>

          <AccountTree
            key={treeKey}
            onSelect={handleTreeSelect}
            selectedNode={selected}
          />

          {/* Balance display for selected leaf */}
          {selected?.level === 3 && (
            <div
              style={{
                marginTop: "1rem",
                padding: "0.875rem",
                borderRadius: "0.5rem",
                background: "var(--bg-tertiary)",
                border: "1px solid var(--border-subtle)",
              }}
            >
              <div style={{ fontSize: "0.75rem", color: "var(--text-muted)", marginBottom: "0.25rem" }}>
                Balance — {selected.acc1} › {selected.acc2} › {selected.acc3} › {selected.acc4}
              </div>
              {loadingBal ? (
                <div style={{ color: "var(--text-muted)", fontSize: "0.85rem" }}>Calculating…</div>
              ) : (
                <div
                  style={{
                    fontSize: "1.4rem",
                    fontWeight: 700,
                    color: balance >= 0 ? "var(--success)" : "var(--danger)",
                  }}
                >
                  {balance !== null
                    ? balance.toLocaleString("en-US", { minimumFractionDigits: 2 })
                    : "—"}
                </div>
              )}
              {selected?.level === 3 && (
                <button
                  className="btn btn-danger btn-sm"
                  style={{ marginTop: "0.75rem" }}
                  onClick={handleDelete}
                >
                  Delete Account
                </button>
              )}
            </div>
          )}
        </div>

        {/* Add account panel */}
        <div className="card">
          <div className="card-header">
            <h3 className="card-title">Add New Account</h3>
          </div>
          <p style={{ fontSize: "0.8rem", color: "var(--text-muted)", marginBottom: "1rem" }}>
            Click a node in the tree to auto-fill parent levels, then fill in the remaining fields.
          </p>

          {msg && (
            <div
              className={`alert ${msg.type === "success" ? "alert-success" : "alert-danger"}`}
              style={{ marginBottom: "1rem" }}
            >
              {msg.text}
            </div>
          )}

          <form onSubmit={handleAdd}>
            {["acc1", "acc2", "acc3", "acc4"].map((field, i) => (
              <div key={field} className="form-group">
                <label className="form-label">Level {i + 1}</label>
                <input
                  className="input"
                  placeholder={["Assets / Liabilities…", "Current Assets…", "Cash & Banks…", "Cash on Hand…"][i]}
                  value={form[field]}
                  onChange={(e) => setForm((f) => ({ ...f, [field]: e.target.value }))}
                  required
                />
              </div>
            ))}
            <button
              type="submit"
              className="btn btn-primary"
              style={{ width: "100%", marginTop: "0.5rem" }}
              disabled={saving}
            >
              {saving ? "Saving…" : "Add Account"}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
