"use client";

import { useState, useEffect } from "react";

export default function SalesAgentsPage() {
  const [agents, setAgents] = useState({ distributors: [], representatives: [] });
  const [tab, setTab] = useState("dist");
  const [loading, setLoading] = useState(true);
  const [form, setForm] = useState({ agentName: "", type: "distributor", region: "" });
  const [showForm, setShowForm] = useState(false);
  const [saving, setSaving] = useState(false);

  function load() {
    setLoading(true);
    Promise.all([
      fetch("/api/admin/agents?type=distributor").then((r) => r.json()).catch(() => []),
      fetch("/api/admin/agents?type=representative").then((r) => r.json()).catch(() => []),
    ]).then(([dist, reps]) => {
      setAgents({ distributors: dist, representatives: reps });
      setLoading(false);
    });
  }

  useEffect(load, []);

  async function handleAdd(e) {
    e.preventDefault();
    setSaving(true);
    try {
      await fetch("/api/admin/agents", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(form),
      });
      setForm({ agentName: "", type: "distributor", region: "" });
      setShowForm(false);
      load();
    } finally {
      setSaving(false);
    }
  }

  const rows = tab === "dist" ? agents.distributors : agents.representatives;

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Sales Agents</h1>
          <p className="page-subtitle">Distributors and medical representatives</p>
        </div>
        <button className="btn btn-primary" onClick={() => setShowForm((v) => !v)}>
          {showForm ? "Cancel" : "+ Add Agent"}
        </button>
      </div>

      {showForm && (
        <div className="card" style={{ marginBottom: "1rem", maxWidth: "480px" }}>
          <form onSubmit={handleAdd}>
            <div className="form-group">
              <label className="form-label">Agent Name</label>
              <input className="input" required value={form.agentName} onChange={(e) => setForm((f) => ({ ...f, agentName: e.target.value }))} />
            </div>
            <div className="form-group">
              <label className="form-label">Type</label>
              <select className="input" value={form.type} onChange={(e) => setForm((f) => ({ ...f, type: e.target.value }))}>
                <option value="distributor">Distributor</option>
                <option value="representative">Medical Representative</option>
              </select>
            </div>
            <div className="form-group">
              <label className="form-label">Region</label>
              <input className="input" placeholder="Region (optional)" value={form.region} onChange={(e) => setForm((f) => ({ ...f, region: e.target.value }))} />
            </div>
            <button type="submit" className="btn btn-primary" style={{ width: "100%" }} disabled={saving}>
              {saving ? "Saving…" : "Add Agent"}
            </button>
          </form>
        </div>
      )}

      <div className="card">
        <div style={{ display: "flex", gap: "0.5rem", marginBottom: "1rem" }}>
          {[["dist", "Distributors"], ["reps", "Representatives"]].map(([key, label]) => (
            <button key={key} className={`btn ${tab === key ? "btn-primary" : "btn-secondary"}`} onClick={() => setTab(key)}>
              {label} ({(key === "dist" ? agents.distributors : agents.representatives).length})
            </button>
          ))}
        </div>

        {loading ? <div className="table-empty">Loading…</div> : !rows.length ? (
          <div className="table-empty">No {tab === "dist" ? "distributors" : "representatives"} found.</div>
        ) : (
          <div className="table-wrapper">
            <table className="table">
              <thead><tr><th>ID</th><th>Name</th><th>Region</th></tr></thead>
              <tbody>
                {rows.map((a) => (
                  <tr key={a.id || a.SNo}>
                    <td style={{ color: "var(--text-muted)", fontSize: "0.8rem" }}>{a.id || a.SNo}</td>
                    <td style={{ fontWeight: 600 }}>{a.agentName || a.AgentName || a.name}</td>
                    <td style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>{a.region || a.Region || "—"}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}
