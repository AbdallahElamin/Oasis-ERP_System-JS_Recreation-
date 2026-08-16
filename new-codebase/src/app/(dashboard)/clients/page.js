"use client";

import { useState, useEffect, useCallback } from "react";
import Link from "next/link";

export default function ClientsPage() {
  const [clients, setClients] = useState([]);
  const [total, setTotal] = useState(0);
  const [loading, setLoading] = useState(true);
  const [searchQ, setSearchQ] = useState("");
  const [showForm, setShowForm] = useState(false);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  // Lookup data
  const [states, setStates] = useState([]);
  const [regions, setRegions] = useState([]);
  const [areas, setAreas] = useState([]);
  const [clientClasses, setClientClasses] = useState([]);
  const [salesAgents, setSalesAgents] = useState([]);
  const [medReps, setMedReps] = useState([]);

  const [form, setForm] = useState({
    name: "", licNo: "", taxNo: "", mobile: "", clientClass: "",
    state: "", region: "", area: "", city: "", town: "", district: "",
    street: "", buildingNo: "", salesMan: "", medicalRepresentative: "",
    pharmacyOwner: "", pharmacyOwnerMob: "", pharmacyDoctor: "", pharmacyDoctorMob: "",
  });

  const fetchClients = useCallback(async () => {
    setLoading(true);
    const res = await fetch(`/api/clients?q=${encodeURIComponent(searchQ)}`);
    const data = await res.json();
    setClients(data.clients ?? []);
    setTotal(data.total ?? 0);
    setLoading(false);
  }, [searchQ]);

  useEffect(() => {
    const t = setTimeout(fetchClients, 300);
    return () => clearTimeout(t);
  }, [fetchClients]);

  useEffect(() => {
    // Fetch lookup data
    fetch("/api/admin/regions?distinct=state").then((r) => r.json()).then((d) => setStates(d ?? []));
    fetch("/api/admin/agents?type=distributor").then((r) => r.json()).then((d) => setSalesAgents(d ?? []));
    fetch("/api/admin/agents?type=representative").then((r) => r.json()).then((d) => setMedReps(d ?? []));
    fetch("/api/admin/client-classes").then((r) => r.json()).then((d) => setClientClasses(d ?? []));
  }, []);

  async function onStateChange(e) {
    const state = e.target.value;
    setForm((prev) => ({ ...prev, state, region: "", area: "" }));
    if (!state) { setRegions([]); setAreas([]); return; }
    const res = await fetch(`/api/admin/regions?state=${encodeURIComponent(state)}&distinct=region`);
    setRegions(await res.json() ?? []);
    setAreas([]);
  }

  async function onRegionChange(e) {
    const region = e.target.value;
    setForm((prev) => ({ ...prev, region, area: "" }));
    if (!region) { setAreas([]); return; }
    const res = await fetch(`/api/admin/regions?region=${encodeURIComponent(region)}&distinct=area`);
    setAreas(await res.json() ?? []);
  }

  function handleChange(e) {
    setForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  }

  async function handleSubmit(e) {
    e.preventDefault();
    setError("");
    setSaving(true);
    const res = await fetch("/api/clients", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form),
    });
    setSaving(false);
    if (res.ok) {
      const data = await res.json();
      setSuccess(`Client saved! ID: ${data.id}`);
      setForm({
        name: "", licNo: "", taxNo: "", mobile: "", clientClass: "",
        state: "", region: "", area: "", city: "", town: "", district: "",
        street: "", buildingNo: "", salesMan: "", medicalRepresentative: "",
        pharmacyOwner: "", pharmacyOwnerMob: "", pharmacyDoctor: "", pharmacyDoctorMob: "",
      });
      setShowForm(false);
      fetchClients();
      setTimeout(() => setSuccess(""), 4000);
    } else {
      const data = await res.json();
      setError(data.error || "Failed to save client.");
    }
  }

  return (
    <div>
      <div className="page-title-bar no-print">
        <div>
          <h1 className="page-title">Client Registry</h1>
          <p className="page-subtitle">{total} registered clients</p>
        </div>
        <button className="btn btn-primary" onClick={() => { setShowForm(true); setError(""); }}>
          + New Client
        </button>
      </div>

      {success && (
        <div style={{ padding: "0.75rem 1rem", marginBottom: "1rem", borderRadius: "0.5rem",
          background: "rgba(16,185,129,0.1)", border: "1px solid rgba(16,185,129,0.3)", color: "var(--success)" }}>
          {success}
        </div>
      )}

      {/* Add Client Form */}
      {showForm && (
        <div className="card" style={{ marginBottom: "1.5rem" }}>
          <div className="card-header">
            <h3 className="card-title">Register New Client</h3>
            <button className="btn btn-secondary btn-sm" onClick={() => setShowForm(false)}>Cancel</button>
          </div>
          <form onSubmit={handleSubmit}>
            <p style={{ fontSize: "0.8rem", color: "var(--text-muted)", marginBottom: "1rem", fontWeight: 600, textTransform: "uppercase", letterSpacing: "0.05em" }}>Basic Info</p>
            <div className="grid-3" style={{ marginBottom: "1.25rem" }}>
              <div className="form-group">
                <label className="form-label">Name *</label>
                <input name="name" className="form-control" value={form.name} onChange={handleChange} required />
              </div>
              <div className="form-group">
                <label className="form-label">License No. *</label>
                <input name="licNo" className="form-control" value={form.licNo} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label className="form-label">Tax No. *</label>
                <input name="taxNo" className="form-control" value={form.taxNo} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label className="form-label">Mobile *</label>
                <input name="mobile" className="form-control" value={form.mobile} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label className="form-label">Client Class *</label>
                <select name="clientClass" className="form-control" value={form.clientClass} onChange={handleChange}>
                  <option value="">Select class…</option>
                  {clientClasses.map((c) => <option key={c.id} value={c.name}>{c.name}</option>)}
                </select>
              </div>
            </div>

            <p style={{ fontSize: "0.8rem", color: "var(--text-muted)", marginBottom: "1rem", fontWeight: 600, textTransform: "uppercase", letterSpacing: "0.05em" }}>Location</p>
            <div className="grid-3" style={{ marginBottom: "1.25rem" }}>
              <div className="form-group">
                <label className="form-label">State *</label>
                <select name="state" className="form-control" value={form.state} onChange={onStateChange}>
                  <option value="">Select state…</option>
                  {states.map((s) => <option key={s} value={s}>{s}</option>)}
                </select>
              </div>
              <div className="form-group">
                <label className="form-label">Region *</label>
                <select name="region" className="form-control" value={form.region} onChange={onRegionChange} disabled={!form.state}>
                  <option value="">Select region…</option>
                  {regions.map((r) => <option key={r} value={r}>{r}</option>)}
                </select>
              </div>
              <div className="form-group">
                <label className="form-label">Area *</label>
                <select name="area" className="form-control" value={form.area} onChange={handleChange} disabled={!form.region}>
                  <option value="">Select area…</option>
                  {areas.map((a) => <option key={a} value={a}>{a}</option>)}
                </select>
              </div>
              <div className="form-group">
                <label className="form-label">City *</label>
                <input name="city" className="form-control" value={form.city} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label className="form-label">Town *</label>
                <input name="town" className="form-control" value={form.town} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label className="form-label">District *</label>
                <input name="district" className="form-control" value={form.district} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label className="form-label">Street *</label>
                <input name="street" className="form-control" value={form.street} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label className="form-label">Building No. *</label>
                <input name="buildingNo" className="form-control" value={form.buildingNo} onChange={handleChange} />
              </div>
            </div>

            <p style={{ fontSize: "0.8rem", color: "var(--text-muted)", marginBottom: "1rem", fontWeight: 600, textTransform: "uppercase", letterSpacing: "0.05em" }}>Sales & Representatives</p>
            <div className="grid-3" style={{ marginBottom: "1.25rem" }}>
              <div className="form-group">
                <label className="form-label">Sales Man *</label>
                <select name="salesMan" className="form-control" value={form.salesMan} onChange={handleChange}>
                  <option value="">Select agent…</option>
                  {salesAgents.map((a) => <option key={a.id} value={a.name}>{a.name}</option>)}
                </select>
              </div>
              <div className="form-group">
                <label className="form-label">Medical Representative *</label>
                <select name="medicalRepresentative" className="form-control" value={form.medicalRepresentative} onChange={handleChange}>
                  <option value="">Select rep…</option>
                  {medReps.map((r) => <option key={r.id} value={r.name}>{r.name}</option>)}
                </select>
              </div>
            </div>

            <p style={{ fontSize: "0.8rem", color: "var(--text-muted)", marginBottom: "1rem", fontWeight: 600, textTransform: "uppercase", letterSpacing: "0.05em" }}>Pharmacy Details</p>
            <div className="grid-3" style={{ marginBottom: "1.25rem" }}>
              <div className="form-group">
                <label className="form-label">Pharmacy Owner *</label>
                <input name="pharmacyOwner" className="form-control" value={form.pharmacyOwner} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label className="form-label">Owner Mobile *</label>
                <input name="pharmacyOwnerMob" className="form-control" value={form.pharmacyOwnerMob} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label className="form-label">Pharmacy Doctor *</label>
                <input name="pharmacyDoctor" className="form-control" value={form.pharmacyDoctor} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label className="form-label">Doctor Mobile *</label>
                <input name="pharmacyDoctorMob" className="form-control" value={form.pharmacyDoctorMob} onChange={handleChange} />
              </div>
            </div>

            {error && <p className="form-error" style={{ marginBottom: "0.75rem" }}>{error}</p>}
            <div style={{ display: "flex", gap: "0.5rem" }}>
              <button type="submit" className="btn btn-primary" disabled={saving}>{saving ? "Saving…" : "Save Client"}</button>
              <button type="button" className="btn btn-secondary" onClick={() => setShowForm(false)}>Cancel</button>
            </div>
          </form>
        </div>
      )}

      {/* Clients Table */}
      <div className="card">
        <div className="card-header">
          <span className="card-title">All Clients</span>
          <input type="text" className="form-control" style={{ width: "260px" }}
            placeholder="Search by name or mobile…" value={searchQ}
            onChange={(e) => setSearchQ(e.target.value)} />
        </div>
        <div className="data-table-wrapper">
          <table className="data-table">
            <thead>
              <tr>
                <th>ID</th><th>Name</th><th>Mobile</th><th>Class</th>
                <th>State</th><th>Sales Man</th><th>Med. Rep</th>
              </tr>
            </thead>
            <tbody>
              {loading ? (
                <tr><td colSpan={7} style={{ textAlign: "center", padding: "2rem", color: "var(--text-muted)" }}>Loading…</td></tr>
              ) : clients.length === 0 ? (
                <tr><td colSpan={7} style={{ textAlign: "center", padding: "2rem", color: "var(--text-muted)" }}>No clients found.</td></tr>
              ) : (
                clients.map((c) => (
                  <tr key={c.id}>
                    <td style={{ color: "var(--text-muted)" }}>{c.id}</td>
                    <td style={{ fontWeight: 500 }}>{c.name}</td>
                    <td>{c.mobile || "—"}</td>
                    <td>{c.clientClass ? <span className="badge badge-info">{c.clientClass}</span> : "—"}</td>
                    <td>{c.state || "—"}</td>
                    <td style={{ color: "var(--text-secondary)" }}>{c.salesMan || "—"}</td>
                    <td style={{ color: "var(--text-secondary)" }}>{c.medicalRepresentative || "—"}</td>
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
