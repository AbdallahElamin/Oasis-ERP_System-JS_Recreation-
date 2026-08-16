"use client";

import { useState, useEffect, useCallback } from "react";
import Link from "next/link";

export default function ItemsRegistryPage() {
  const [items, setItems] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchQ, setSearchQ] = useState("");
  const [showForm, setShowForm] = useState(false);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState("");
  const [successMsg, setSuccessMsg] = useState("");

  const [form, setForm] = useState({
    item: "", genericName: "", pack: "", wPrice: "", rPrice: "", companyName: "",
  });

  const fetchItems = useCallback(async () => {
    setLoading(true);
    const res = await fetch(`/api/inventory/items?q=${encodeURIComponent(searchQ)}`);
    const data = await res.json();
    setItems(data);
    setLoading(false);
  }, [searchQ]);

  useEffect(() => {
    const timer = setTimeout(fetchItems, 300);
    return () => clearTimeout(timer);
  }, [fetchItems]);

  function handleChange(e) {
    setForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  }

  async function handleSubmit(e) {
    e.preventDefault();
    setError("");
    setSaving(true);
    const res = await fetch("/api/inventory/items", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        ...form,
        wPrice: parseFloat(form.wPrice) || 0,
        rPrice: parseFloat(form.rPrice) || 0,
      }),
    });
    setSaving(false);
    if (res.ok) {
      setSuccessMsg("Item saved successfully!");
      setForm({ item: "", genericName: "", pack: "", wPrice: "", rPrice: "", companyName: "" });
      setShowForm(false);
      fetchItems();
      setTimeout(() => setSuccessMsg(""), 3000);
    } else {
      const data = await res.json();
      setError(data.error || "Failed to save item.");
    }
  }

  return (
    <div>
      <div className="page-title-bar no-print">
        <div>
          <h1 className="page-title">Items Registry</h1>
          <p className="page-subtitle">Manage your product catalog — pricing and packaging</p>
        </div>
        <button className="btn btn-primary" onClick={() => { setShowForm(true); setError(""); }}>
          + Add Item
        </button>
      </div>

      {successMsg && (
        <div style={{ padding: "0.75rem 1rem", marginBottom: "1rem", borderRadius: "0.5rem",
          background: "rgba(16,185,129,0.1)", border: "1px solid rgba(16,185,129,0.3)", color: "var(--success)" }}>
          {successMsg}
        </div>
      )}

      {/* Add Item Form */}
      {showForm && (
        <div className="card" style={{ marginBottom: "1.5rem" }}>
          <div className="card-header">
            <h3 className="card-title">New Item</h3>
            <button className="btn btn-secondary btn-sm" onClick={() => setShowForm(false)}>Cancel</button>
          </div>
          <form onSubmit={handleSubmit}>
            <div className="grid-3" style={{ marginBottom: "1rem" }}>
              <div className="form-group">
                <label className="form-label">Item Name *</label>
                <input name="item" className="form-control" value={form.item} onChange={handleChange} required placeholder="e.g. Paracetamol 500mg" />
              </div>
              <div className="form-group">
                <label className="form-label">Generic Name</label>
                <input name="genericName" className="form-control" value={form.genericName} onChange={handleChange} placeholder="e.g. Paracetamol" />
              </div>
              <div className="form-group">
                <label className="form-label">Pack</label>
                <input name="pack" className="form-control" value={form.pack} onChange={handleChange} placeholder="e.g. Box/24" />
              </div>
              <div className="form-group">
                <label className="form-label">Wholesale Price (SDG)</label>
                <input name="wPrice" type="number" step="0.01" min="0" className="form-control" value={form.wPrice} onChange={handleChange} placeholder="0.00" />
              </div>
              <div className="form-group">
                <label className="form-label">Retail Price (SDG)</label>
                <input name="rPrice" type="number" step="0.01" min="0" className="form-control" value={form.rPrice} onChange={handleChange} placeholder="0.00" />
              </div>
              <div className="form-group">
                <label className="form-label">Company / Manufacturer</label>
                <input name="companyName" className="form-control" value={form.companyName} onChange={handleChange} placeholder="e.g. Pharma Co." />
              </div>
            </div>
            {error && <p className="form-error" style={{ marginBottom: "0.75rem" }}>{error}</p>}
            <div style={{ display: "flex", gap: "0.5rem" }}>
              <button type="submit" className="btn btn-primary" disabled={saving}>
                {saving ? "Saving…" : "Save Item"}
              </button>
              <button type="button" className="btn btn-secondary" onClick={() => setShowForm(false)}>Cancel</button>
            </div>
          </form>
        </div>
      )}

      {/* Search + Table */}
      <div className="card">
        <div className="card-header">
          <span className="card-title">All Items ({items.length})</span>
          <input
            type="text"
            className="form-control"
            style={{ width: "260px" }}
            placeholder="Search items…"
            value={searchQ}
            onChange={(e) => setSearchQ(e.target.value)}
          />
        </div>

        <div className="data-table-wrapper">
          <table className="data-table">
            <thead>
              <tr>
                <th>#</th>
                <th>Item Name</th>
                <th>Generic Name</th>
                <th>Pack</th>
                <th>W.Price</th>
                <th>R.Price</th>
                <th>Company</th>
              </tr>
            </thead>
            <tbody>
              {loading ? (
                <tr><td colSpan={7} style={{ textAlign: "center", padding: "2rem", color: "var(--text-muted)" }}>Loading…</td></tr>
              ) : items.length === 0 ? (
                <tr><td colSpan={7} style={{ textAlign: "center", padding: "2rem", color: "var(--text-muted)" }}>No items found.</td></tr>
              ) : (
                items.map((item, i) => (
                  <tr key={item.id}>
                    <td style={{ color: "var(--text-muted)" }}>{i + 1}</td>
                    <td style={{ fontWeight: 500 }}>{item.item}</td>
                    <td style={{ color: "var(--text-secondary)" }}>{item.genericName || "—"}</td>
                    <td>{item.pack || "—"}</td>
                    <td>{Number(item.wPrice).toFixed(2)}</td>
                    <td>{Number(item.rPrice).toFixed(2)}</td>
                    <td style={{ color: "var(--text-secondary)" }}>{item.companyName || "—"}</td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}
