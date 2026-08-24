"use client";

import { useState, useEffect } from "react";

export default function RegionsPage() {
  const [regions, setRegions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [selectedState, setSelectedState] = useState(null);
  const [selectedRegion, setSelectedRegion] = useState(null);
  const [newItem, setNewItem] = useState({ state: "", region: "", area: "" });
  const [saving, setSaving] = useState(false);
  const [msg, setMsg] = useState(null);

  function load() {
    setLoading(true);
    fetch("/api/admin/regions")
      .then((r) => r.json())
      .then((d) => { setRegions(d); setLoading(false); })
      .catch(() => setLoading(false));
  }

  useEffect(load, []);

  // Derive unique states
  const states = [...new Set(regions.map((r) => r.state).filter(Boolean))];

  // Regions for selected state
  const stateRegions = selectedState
    ? [...new Set(regions.filter((r) => r.state === selectedState).map((r) => r.region).filter(Boolean))]
    : [];

  // Areas for selected region
  const regionAreas = selectedRegion
    ? regions.filter((r) => r.state === selectedState && r.region === selectedRegion).map((r) => r.area).filter(Boolean)
    : [];

  async function handleAdd(e) {
    e.preventDefault();
    setSaving(true);
    try {
      const res = await fetch("/api/admin/regions", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newItem),
      });
      if (!res.ok) throw new Error((await res.json()).error || "Failed");
      setMsg("Added successfully");
      setNewItem({ state: "", region: "", area: "" });
      load();
    } catch (err) {
      setMsg(`Error: ${err.message}`);
    } finally {
      setSaving(false);
    }
  }

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Regions &amp; Areas</h1>
          <p className="page-subtitle">Manage geographic hierarchy for sales routing</p>
        </div>
      </div>

      <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr 1fr 300px", gap: "1rem", alignItems: "start" }}>
        {/* States */}
        <div className="card">
          <div className="card-header"><h3 className="card-title">States</h3></div>
          {loading ? <div className="table-empty">Loading…</div> : !states.length ? (
            <div className="table-empty">No states yet.</div>
          ) : states.map((s) => (
            <div
              key={s}
              onClick={() => { setSelectedState(s); setSelectedRegion(null); }}
              style={{
                padding: "0.5rem 0.75rem", cursor: "pointer", borderRadius: "0.375rem",
                background: selectedState === s ? "rgba(99,102,241,0.12)" : "transparent",
                color: selectedState === s ? "var(--accent-light)" : "var(--text-primary)",
                fontWeight: selectedState === s ? 600 : 400,
                transition: "background 0.15s",
              }}
            >
              {s}
            </div>
          ))}
        </div>

        {/* Regions */}
        <div className="card">
          <div className="card-header"><h3 className="card-title">Regions {selectedState && `— ${selectedState}`}</h3></div>
          {!selectedState ? <div className="table-empty">Select a state first.</div> :
            !stateRegions.length ? <div className="table-empty">No regions.</div> :
            stateRegions.map((r) => (
              <div key={r}
                onClick={() => setSelectedRegion(r)}
                style={{
                  padding: "0.5rem 0.75rem", cursor: "pointer", borderRadius: "0.375rem",
                  background: selectedRegion === r ? "rgba(99,102,241,0.12)" : "transparent",
                  color: selectedRegion === r ? "var(--accent-light)" : "var(--text-primary)",
                  fontWeight: selectedRegion === r ? 600 : 400,
                }}
              >{r}</div>
            ))
          }
        </div>

        {/* Areas */}
        <div className="card">
          <div className="card-header"><h3 className="card-title">Areas {selectedRegion && `— ${selectedRegion}`}</h3></div>
          {!selectedRegion ? <div className="table-empty">Select a region first.</div> :
            !regionAreas.length ? <div className="table-empty">No areas.</div> :
            regionAreas.map((a) => (
              <div key={a} style={{ padding: "0.5rem 0.75rem", color: "var(--text-secondary)", fontSize: "0.875rem" }}>
                {a}
              </div>
            ))
          }
        </div>

        {/* Add form */}
        <div className="card">
          <div className="card-header"><h3 className="card-title">Add Entry</h3></div>
          {msg && <div className="alert alert-success" style={{ marginBottom: "0.75rem", fontSize: "0.8rem" }}>{msg}</div>}
          <form onSubmit={handleAdd}>
            {["state", "region", "area"].map((field) => (
              <div className="form-group" key={field}>
                <label className="form-label" style={{ textTransform: "capitalize" }}>{field}</label>
                <input className="input" value={newItem[field]} onChange={(e) => setNewItem((f) => ({ ...f, [field]: e.target.value }))} required />
              </div>
            ))}
            <button type="submit" className="btn btn-primary" style={{ width: "100%" }} disabled={saving}>
              {saving ? "Saving…" : "Add"}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
