"use client";

import { useState, useEffect, useCallback } from "react";

export default function AddToStockPage() {
  const [items, setItems] = useState([]);
  const [stores, setStores] = useState([]);
  const [entries, setEntries] = useState([]);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const [form, setForm] = useState({
    storeName: "", item: "", pack: "", batchNo: "",
    qntIn: "", wPrice: "", rPrice: "", expireDate: "", details: "",
    hasExpiry: false,
  });

  // Fetch lookup lists
  useEffect(() => {
    fetch("/api/inventory/items").then((r) => r.json()).then(setItems);
    fetch("/api/inventory/stores").then((r) => r.json()).then((d) => setStores(d));
  }, []);

  function handleChange(e) {
    const { name, value, type, checked } = e.target;
    setForm((prev) => ({ ...prev, [name]: type === "checkbox" ? checked : value }));
  }

  // Auto-fill price when item is selected
  function handleItemChange(e) {
    const itemName = e.target.value;
    const found = items.find((i) => i.item === itemName);
    setForm((prev) => ({
      ...prev,
      item: itemName,
      wPrice: found ? String(found.wPrice) : "",
      rPrice: found ? String(found.rPrice) : "",
      pack: found?.pack || "",
    }));
  }

  function handleAddRow() {
    if (!form.storeName || !form.item || !form.qntIn || !form.batchNo) {
      setError("Please fill in store, item, batch number, and quantity.");
      return;
    }
    setError("");
    setEntries((prev) => [
      ...prev,
      { ...form, id: Date.now() },
    ]);
    setForm((prev) => ({
      ...prev, item: "", pack: "", batchNo: "", qntIn: "",
      wPrice: "", rPrice: "", expireDate: "", details: "", hasExpiry: false,
    }));
  }

  function handleRemoveRow(id) {
    setEntries((prev) => prev.filter((r) => r.id !== id));
  }

  async function handleSave() {
    if (entries.length === 0) {
      setError("Please add at least one item.");
      return;
    }
    setSaving(true);
    setError("");
    const res = await fetch("/api/inventory/stock", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ entries }),
    });
    setSaving(false);
    if (res.ok) {
      setSuccess("Stock saved successfully!");
      setEntries([]);
      setTimeout(() => setSuccess(""), 3000);
    } else {
      const data = await res.json();
      setError(data.error || "Failed to save stock.");
    }
  }

  return (
    <div>
      <div className="page-title-bar no-print">
        <div>
          <h1 className="page-title">Add Items to Stock</h1>
          <p className="page-subtitle">Receive new inventory into your stores</p>
        </div>
      </div>

      {success && (
        <div style={{ padding: "0.75rem 1rem", marginBottom: "1rem", borderRadius: "0.5rem",
          background: "rgba(16,185,129,0.1)", border: "1px solid rgba(16,185,129,0.3)", color: "var(--success)" }}>
          {success}
        </div>
      )}

      {/* Entry Form */}
      <div className="card" style={{ marginBottom: "1.5rem" }}>
        <div className="card-header">
          <h3 className="card-title">Add Entry</h3>
        </div>
        <div className="grid-3" style={{ marginBottom: "1rem" }}>
          <div className="form-group">
            <label className="form-label">Store Name *</label>
            <select name="storeName" className="form-control" value={form.storeName} onChange={handleChange}>
              <option value="">Select store…</option>
              {stores.map((s) => <option key={s.id} value={s.storeName}>{s.storeName}</option>)}
            </select>
          </div>
          <div className="form-group">
            <label className="form-label">Item *</label>
            <select name="item" className="form-control" value={form.item} onChange={handleItemChange}>
              <option value="">Select item…</option>
              {items.map((i) => <option key={i.id} value={i.item}>{i.item}</option>)}
            </select>
          </div>
          <div className="form-group">
            <label className="form-label">Pack</label>
            <input name="pack" className="form-control" value={form.pack} onChange={handleChange} placeholder="e.g. Box/24" />
          </div>
          <div className="form-group">
            <label className="form-label">Batch No. *</label>
            <input name="batchNo" className="form-control" value={form.batchNo} onChange={handleChange} placeholder="Batch number" />
          </div>
          <div className="form-group">
            <label className="form-label">Quantity *</label>
            <input name="qntIn" type="number" min="0" className="form-control" value={form.qntIn} onChange={handleChange} placeholder="0" />
          </div>
          <div className="form-group">
            <label className="form-label">Wholesale Price</label>
            <input name="wPrice" type="number" step="0.01" min="0" className="form-control" value={form.wPrice} onChange={handleChange} placeholder="0.00" />
          </div>
          <div className="form-group">
            <label className="form-label">Retail Price</label>
            <input name="rPrice" type="number" step="0.01" min="0" className="form-control" value={form.rPrice} onChange={handleChange} placeholder="0.00" />
          </div>
          <div className="form-group">
            <label className="form-label" style={{ display: "flex", alignItems: "center", gap: "0.5rem" }}>
              <input type="checkbox" name="hasExpiry" checked={form.hasExpiry} onChange={handleChange} />
              Has Expiry Date
            </label>
            {form.hasExpiry && (
              <input name="expireDate" type="date" className="form-control" value={form.expireDate} onChange={handleChange} />
            )}
          </div>
          <div className="form-group">
            <label className="form-label">Remarks</label>
            <input name="details" className="form-control" value={form.details} onChange={handleChange} placeholder="Optional notes" />
          </div>
        </div>
        {error && <p className="form-error" style={{ marginBottom: "0.75rem" }}>{error}</p>}
        <button className="btn btn-secondary" onClick={handleAddRow}>+ Add to List</button>
      </div>

      {/* Entries grid */}
      {entries.length > 0 && (
        <div className="card" style={{ marginBottom: "1.5rem" }}>
          <div className="card-header">
            <h3 className="card-title">Items to Add ({entries.length})</h3>
          </div>
          <div className="data-table-wrapper">
            <table className="data-table">
              <thead>
                <tr>
                  <th>Store</th><th>Item</th><th>Pack</th><th>Batch</th>
                  <th>Qty</th><th>W.Price</th><th>R.Price</th><th>Expiry</th><th>Notes</th><th></th>
                </tr>
              </thead>
              <tbody>
                {entries.map((row) => (
                  <tr key={row.id}>
                    <td>{row.storeName}</td><td>{row.item}</td><td>{row.pack || "—"}</td>
                    <td>{row.batchNo}</td><td>{row.qntIn}</td>
                    <td>{row.wPrice}</td><td>{row.rPrice}</td>
                    <td>{row.hasExpiry ? row.expireDate : "—"}</td>
                    <td>{row.details || "—"}</td>
                    <td>
                      <button className="btn btn-danger btn-sm" onClick={() => handleRemoveRow(row.id)}>✕</button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
          <div style={{ marginTop: "1rem" }}>
            <button className="btn btn-primary" onClick={handleSave} disabled={saving}>
              {saving ? "Saving…" : "Save All to Stock"}
            </button>
          </div>
        </div>
      )}
    </div>
  );
}
