"use client";

import { useState, useEffect } from "react";

const today = new Date().toISOString().split("T")[0];
const VAC_TYPES = ["Annual Leave","Sick Leave","Emergency","Unpaid","Other"];

export default function VacationsPage() {
  const [vacations, setVacations] = useState([]);
  const [employees, setEmployees] = useState([]);
  const [loading, setLoading] = useState(true);
  const [year, setYear] = useState(new Date().getFullYear());
  const [showForm, setShowForm] = useState(false);
  const [form, setForm] = useState({ empNo: "", startDate: today, endDate: today, type: "Annual Leave", notes: "" });
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState(null);

  function loadVacations() {
    setLoading(true);
    fetch(`/api/hr/vacations?year=${year}`)
      .then((r) => r.json())
      .then((d) => { setVacations(d); setLoading(false); })
      .catch(() => setLoading(false));
  }

  useEffect(() => {
    fetch("/api/hr/employees").then((r) => r.json()).then(setEmployees).catch(() => {});
  }, []);

  useEffect(loadVacations, [year]);

  function getDays(start, end) {
    const diff = new Date(end) - new Date(start);
    return Math.max(1, Math.round(diff / (1000 * 60 * 60 * 24)) + 1);
  }

  async function handleSave(e) {
    e.preventDefault();
    setSaving(true); setError(null);
    try {
      const res = await fetch("/api/hr/vacations", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(form),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Failed");
      setShowForm(false);
      setForm({ empNo: "", startDate: today, endDate: today, type: "Annual Leave", notes: "" });
      loadVacations();
    } catch (err) {
      setError(err.message);
    } finally {
      setSaving(false);
    }
  }

  const statusColor = { Pending: "var(--warning)", Approved: "var(--success)", Rejected: "var(--danger)" };

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Vacations</h1>
          <p className="page-subtitle">Leave requests and approvals</p>
        </div>
        <button className="btn btn-primary" onClick={() => setShowForm((v) => !v)}>
          {showForm ? "Cancel" : "+ Request Leave"}
        </button>
      </div>

      {showForm && (
        <div className="card" style={{ marginBottom: "1rem", maxWidth: "480px" }}>
          {error && <div className="alert alert-danger" style={{ marginBottom: "0.75rem" }}>{error}</div>}
          <form onSubmit={handleSave}>
            <div className="form-group">
              <label className="form-label">Employee</label>
              <select className="input" required value={form.empNo} onChange={(e) => setForm((f) => ({ ...f, empNo: e.target.value }))}>
                <option value="">Select…</option>
                {employees.map((e) => <option key={e.EmpNo} value={e.EmpNo}>{e.FullName}</option>)}
              </select>
            </div>
            <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: "0.75rem" }}>
              <div className="form-group"><label className="form-label">Start Date</label>
                <input type="date" className="input" value={form.startDate} onChange={(e) => setForm((f) => ({ ...f, startDate: e.target.value }))} /></div>
              <div className="form-group"><label className="form-label">End Date</label>
                <input type="date" className="input" value={form.endDate} min={form.startDate} onChange={(e) => setForm((f) => ({ ...f, endDate: e.target.value }))} /></div>
            </div>
            <div className="form-group"><label className="form-label">Leave Type</label>
              <select className="input" value={form.type} onChange={(e) => setForm((f) => ({ ...f, type: e.target.value }))}>
                {VAC_TYPES.map((t) => <option key={t}>{t}</option>)}
              </select>
            </div>
            <div className="form-group"><label className="form-label">Notes</label>
              <textarea className="input" rows={2} value={form.notes} onChange={(e) => setForm((f) => ({ ...f, notes: e.target.value }))} /></div>
            <button type="submit" className="btn btn-primary" style={{ width: "100%" }} disabled={saving}>
              {saving ? "Saving…" : `Submit Request (${getDays(form.startDate, form.endDate)} day${getDays(form.startDate, form.endDate) > 1 ? "s" : ""})`}
            </button>
          </form>
        </div>
      )}

      <div className="card">
        <div className="card-header" style={{ gap: "0.75rem" }}>
          <h3 className="card-title">Leave Requests</h3>
          <select className="input" style={{ width: "auto" }} value={year} onChange={(e) => setYear(Number(e.target.value))}>
            {[0,1,2].map((i) => <option key={i} value={new Date().getFullYear() - i}>{new Date().getFullYear() - i}</option>)}
          </select>
        </div>
        {loading ? <div className="table-empty">Loading…</div> : !vacations.length ? (
          <div className="table-empty">No leave requests for {year}.</div>
        ) : (
          <div className="table-wrapper">
            <table className="table">
              <thead>
                <tr><th>Employee</th><th>Type</th><th>From</th><th>To</th><th>Days</th><th>Status</th><th>Notes</th></tr>
              </thead>
              <tbody>
                {vacations.map((v) => (
                  <tr key={v.id}>
                    <td style={{ fontWeight: 600 }}>{v.FullName}</td>
                    <td><span className="badge">{v.type}</span></td>
                    <td style={{ fontSize: "0.8rem" }}>{new Date(v.StartDate).toLocaleDateString()}</td>
                    <td style={{ fontSize: "0.8rem" }}>{new Date(v.EndDate).toLocaleDateString()}</td>
                    <td>{getDays(v.StartDate, v.EndDate)}</td>
                    <td><span style={{ color: statusColor[v.status] || "var(--text-muted)", fontWeight: 500, fontSize: "0.82rem" }}>{v.status}</span></td>
                    <td style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>{v.notes || "—"}</td>
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
