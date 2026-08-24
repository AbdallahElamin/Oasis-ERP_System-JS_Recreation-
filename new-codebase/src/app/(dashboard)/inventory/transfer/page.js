"use client";

import { useState, useEffect } from "react";

export default function TransferPage() {
  const [stores, setStores] = useState([]);
  const [stockItems, setStockItems] = useState([]);

  const [form, setForm] = useState({
    fromStore: "", toStore: "", item: "", batchNo: "", pack: "", quantity: "",
  });
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState(null);
  const [msg, setMsg] = useState(null);

  useEffect(() => {
    Promise.all([
      fetch("/api/inventory/stores").then((r) => r.json()),
      fetch("/api/inventory/stock").then((r) => r.json()),
    ]).then(([s, st]) => {
      setStores(s);
      setStockItems(st);
    });
  }, []);

  // Available items in fromStore
  const availableItems = form.fromStore
    ? stockItems.filter((s) => s.StoreName === form.fromStore && Number(s.availableQnt) > 0)
    : [];

  function set(k, v) {
    setForm((f) => ({ ...f, [k]: v }));
    if (k === "fromStore") setForm((f) => ({ ...f, fromStore: v, item: "", batchNo: "", pack: "" }));
    if (k === "item") {
      const match = availableItems.find((s) => s.item === v);
      if (match) setForm((f) => ({ ...f, item: v, batchNo: match.BatchNo || "", pack: match.pack || "" }));
    }
  }

  async function handleSubmit(e) {
    e.preventDefault();
    setSaving(true);
    setError(null);
    try {
      const res = await fetch("/api/inventory/transfer", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(form),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Failed");
      setMsg(`Transferred ${form.quantity} units of "${form.item}" from ${form.fromStore} to ${form.toStore}`);
      setForm({ fromStore: "", toStore: "", item: "", batchNo: "", pack: "", quantity: "" });
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
          <h1 className="page-title">Transfer Items</h1>
          <p className="page-subtitle">Move stock between stores</p>
        </div>
      </div>

      <div className="card" style={{ maxWidth: "600px" }}>
        {error && <div className="alert alert-danger" style={{ marginBottom: "0.75rem" }}>{error}</div>}
        {msg && <div className="alert alert-success" style={{ marginBottom: "0.75rem" }}>{msg}</div>}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label className="form-label">From Store</label>
            <select className="input" value={form.fromStore} onChange={(e) => set("fromStore", e.target.value)} required>
              <option value="">Select source store…</option>
              {stores.map((s) => <option key={s.id} value={s.StoreName}>{s.StoreName}</option>)}
            </select>
          </div>

          <div className="form-group">
            <label className="form-label">To Store</label>
            <select className="input" value={form.toStore} onChange={(e) => set("toStore", e.target.value)} required>
              <option value="">Select destination store…</option>
              {stores.filter((s) => s.StoreName !== form.fromStore).map((s) => (
                <option key={s.id} value={s.StoreName}>{s.StoreName}</option>
              ))}
            </select>
          </div>

          <div className="form-group">
            <label className="form-label">Item</label>
            <select className="input" value={form.item} onChange={(e) => set("item", e.target.value)} required disabled={!form.fromStore}>
              <option value="">Select item…</option>
              {availableItems.map((s) => (
                <option key={`${s.item}-${s.BatchNo}`} value={s.item}>
                  {s.item} {s.BatchNo ? `· ${s.BatchNo}` : ""} (Qty: {Number(s.availableQnt).toFixed(0)})
                </option>
              ))}
            </select>
            {form.fromStore && availableItems.length === 0 && (
              <p style={{ fontSize: "0.8rem", color: "var(--danger)", marginTop: "0.25rem" }}>No available stock in this store</p>
            )}
          </div>

          <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: "0.75rem" }}>
            <div className="form-group">
              <label className="form-label">Batch No.</label>
              <input className="input" value={form.batchNo} onChange={(e) => set("batchNo", e.target.value)} placeholder="Auto-filled" />
            </div>
            <div className="form-group">
              <label className="form-label">Quantity</label>
              <input type="number" className="input" min="0.01" step="0.01" value={form.quantity} onChange={(e) => set("quantity", e.target.value)} required />
            </div>
          </div>

          <button type="submit" className="btn btn-primary" style={{ width: "100%" }} disabled={saving}>
            {saving ? "Transferring…" : "Transfer"}
          </button>
        </form>
      </div>
    </div>
  );
}
