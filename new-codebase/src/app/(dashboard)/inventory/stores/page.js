"use client";

import { useState, useEffect } from "react";

export default function StoresPage() {
  const [stores, setStores] = useState([]);
  const [loading, setLoading] = useState(true);
  const [newName, setNewName] = useState("");
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState(null);
  const [msg, setMsg] = useState(null);

  function load() {
    setLoading(true);
    fetch("/api/inventory/stores")
      .then((r) => r.json())
      .then((d) => { setStores(d); setLoading(false); })
      .catch(() => setLoading(false));
  }

  useEffect(load, []);

  async function handleAdd(e) {
    e.preventDefault();
    if (!newName.trim()) return;
    setSaving(true);
    setError(null);
    try {
      const res = await fetch("/api/inventory/stores", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ storeName: newName }),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Failed");
      setMsg("Store added successfully");
      setNewName("");
      load();
    } catch (err) {
      setError(err.message);
    } finally {
      setSaving(false);
    }
  }

  async function handleDelete(id) {
    if (!confirm("Delete this store?")) return;
    await fetch("/api/inventory/stores", {
      method: "DELETE",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ id }),
    });
    load();
  }

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Stores</h1>
          <p className="page-subtitle">Manage warehouse and storage locations</p>
        </div>
      </div>

      <div style={{ display: "grid", gridTemplateColumns: "1fr 320px", gap: "1.5rem", alignItems: "start" }}>
        <div className="card">
          <div className="card-header"><h3 className="card-title">All Stores</h3></div>
          {loading ? (
            <div className="table-empty">Loading…</div>
          ) : !stores.length ? (
            <div className="table-empty">No stores yet. Add one →</div>
          ) : (
            <div className="table-wrapper">
              <table className="table">
                <thead><tr><th>ID</th><th>Store Name</th><th></th></tr></thead>
                <tbody>
                  {stores.map((s) => (
                    <tr key={s.id}>
                      <td style={{ color: "var(--text-muted)", fontSize: "0.8rem" }}>{s.id}</td>
                      <td style={{ fontWeight: 600 }}>{s.StoreName}</td>
                      <td>
                        <button className="btn btn-danger btn-sm" onClick={() => handleDelete(s.id)}>Delete</button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>

        <div className="card">
          <div className="card-header"><h3 className="card-title">Add Store</h3></div>
          {error && <div className="alert alert-danger" style={{ marginBottom: "0.75rem" }}>{error}</div>}
          {msg && <div className="alert alert-success" style={{ marginBottom: "0.75rem" }}>{msg}</div>}
          <form onSubmit={handleAdd}>
            <div className="form-group">
              <label className="form-label">Store Name</label>
              <input className="input" placeholder="Main Warehouse…" value={newName} onChange={(e) => setNewName(e.target.value)} required />
            </div>
            <button type="submit" className="btn btn-primary" style={{ width: "100%" }} disabled={saving}>
              {saving ? "Saving…" : "Add Store"}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
