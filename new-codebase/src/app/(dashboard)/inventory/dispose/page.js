"use client";

import { useState, useEffect } from "react";

export default function DisposePage() {
  const [stores, setStores] = useState([]);
  const [stockItems, setStockItems] = useState([]);
  const [form, setForm] = useState({ storeName: "", item: "", batchNo: "", pack: "", quantity: "", reason: "" });
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState(null);
  const [msg, setMsg] = useState(null);

  useEffect(() => {
    Promise.all([
      fetch("/api/inventory/stores").then((r) => r.json()),
      fetch("/api/inventory/stock").then((r) => r.json()),
    ]).then(([s, st]) => { setStores(s); setStockItems(st); });
  }, []);

  const availableItems = form.storeName
    ? stockItems.filter((s) => s.StoreName === form.storeName && Number(s.availableQnt) > 0)
    : [];

  function set(k, v) {
    setForm((f) => ({ ...f, [k]: v }));
    if (k === "storeName") setForm((f) => ({ ...f, storeName: v, item: "", batchNo: "" }));
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
      const res = await fetch("/api/inventory/dispose", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(form),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Failed");
      setMsg(`Disposed ${form.quantity} units of "${form.item}" from ${form.storeName}`);
      setForm({ storeName: "", item: "", batchNo: "", pack: "", quantity: "", reason: "" });
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
          <h1 className="page-title">Dispose Items</h1>
          <p className="page-subtitle">Write off expired or damaged stock</p>
        </div>
      </div>

      <div className="card" style={{ maxWidth: "600px" }}>
        {error && <div className="alert alert-danger" style={{ marginBottom: "0.75rem" }}>{error}</div>}
        {msg && <div className="alert alert-success" style={{ marginBottom: "0.75rem" }}>{msg}</div>}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label className="form-label">Store</label>
            <select className="input" value={form.storeName} onChange={(e) => set("storeName", e.target.value)} required>
              <option value="">Select store…</option>
              {stores.map((s) => <option key={s.id} value={s.StoreName}>{s.StoreName}</option>)}
            </select>
          </div>

          <div className="form-group">
            <label className="form-label">Item</label>
            <select className="input" value={form.item} onChange={(e) => set("item", e.target.value)} required disabled={!form.storeName}>
              <option value="">Select item…</option>
              {availableItems.map((s) => (
                <option key={`${s.item}-${s.BatchNo}`} value={s.item}>
                  {s.item} {s.BatchNo ? `· ${s.BatchNo}` : ""} (Qty: {Number(s.availableQnt).toFixed(0)})
                </option>
              ))}
            </select>
          </div>

          <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: "0.75rem" }}>
            <div className="form-group">
              <label className="form-label">Batch No.</label>
              <input className="input" value={form.batchNo} readOnly />
            </div>
            <div className="form-group">
              <label className="form-label">Quantity to Dispose</label>
              <input type="number" className="input" min="0.01" step="0.01" value={form.quantity} onChange={(e) => set("quantity", e.target.value)} required />
            </div>
          </div>

          <div className="form-group">
            <label className="form-label">Reason</label>
            <textarea className="input" rows={3} placeholder="Expired / Damaged / Other…" value={form.reason} onChange={(e) => set("reason", e.target.value)} style={{ resize: "vertical" }} />
          </div>

          <button type="submit" className="btn btn-danger" style={{ width: "100%" }} disabled={saving}>
            {saving ? "Saving…" : "Dispose Items"}
          </button>
        </form>
      </div>
    </div>
  );
}
