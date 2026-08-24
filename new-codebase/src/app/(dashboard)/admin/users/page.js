"use client";

import { useState, useEffect } from "react";

const ROLES = ["Admin", "User", "Accountant", "Warehouse"];

export default function UsersPage() {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [form, setForm] = useState({ fullName: "", password: "", role: "User" });
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState(null);

  function load() {
    setLoading(true);
    fetch("/api/admin/users")
      .then((r) => r.json())
      .then((d) => { setUsers(d); setLoading(false); })
      .catch(() => setLoading(false));
  }

  useEffect(load, []);

  async function handleAdd(e) {
    e.preventDefault();
    setSaving(true); setError(null);
    try {
      const res = await fetch("/api/admin/users", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(form),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Failed");
      setForm({ fullName: "", password: "", role: "User" });
      setShowForm(false);
      load();
    } catch (err) {
      setError(err.message);
    } finally {
      setSaving(false);
    }
  }

  async function toggleActive(user) {
    await fetch(`/api/admin/users/${user.id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ isActive: !user.IsActive }),
    });
    load();
  }

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">User Management</h1>
          <p className="page-subtitle">Manage ERP system users and roles</p>
        </div>
        <button className="btn btn-primary" onClick={() => setShowForm((v) => !v)}>
          {showForm ? "Cancel" : "+ Add User"}
        </button>
      </div>

      {showForm && (
        <div className="card" style={{ marginBottom: "1rem", maxWidth: "480px" }}>
          <div className="card-header"><h3 className="card-title">New User</h3></div>
          {error && <div className="alert alert-danger" style={{ marginBottom: "0.75rem" }}>{error}</div>}
          <form onSubmit={handleAdd}>
            <div className="form-group">
              <label className="form-label">Full Name</label>
              <input className="input" required value={form.fullName} onChange={(e) => setForm((f) => ({ ...f, fullName: e.target.value }))} />
            </div>
            <div className="form-group">
              <label className="form-label">Password</label>
              <input type="password" className="input" required value={form.password} onChange={(e) => setForm((f) => ({ ...f, password: e.target.value }))} />
            </div>
            <div className="form-group">
              <label className="form-label">Role</label>
              <select className="input" value={form.role} onChange={(e) => setForm((f) => ({ ...f, role: e.target.value }))}>
                {ROLES.map((r) => <option key={r}>{r}</option>)}
              </select>
            </div>
            <button type="submit" className="btn btn-primary" style={{ width: "100%" }} disabled={saving}>
              {saving ? "Creating…" : "Create User"}
            </button>
          </form>
        </div>
      )}

      <div className="card">
        <div className="card-header"><h3 className="card-title">All Users</h3></div>
        {loading ? <div className="table-empty">Loading…</div> : (
          <div className="table-wrapper">
            <table className="table">
              <thead>
                <tr>
                  <th>ID</th><th>Full Name</th><th>Role</th><th>Status</th><th>Created</th><th></th>
                </tr>
              </thead>
              <tbody>
                {users.map((u) => (
                  <tr key={u.id}>
                    <td style={{ color: "var(--text-muted)", fontSize: "0.8rem" }}>{u.id}</td>
                    <td style={{ fontWeight: 600 }}>{u.FullName}</td>
                    <td><span className="badge">{u.role}</span></td>
                    <td>
                      <span style={{ color: u.IsActive ? "var(--success)" : "var(--danger)", fontWeight: 500, fontSize: "0.82rem" }}>
                        {u.IsActive ? "Active" : "Inactive"}
                      </span>
                    </td>
                    <td style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>
                      {u.CreatedAt ? new Date(u.CreatedAt).toLocaleDateString() : "—"}
                    </td>
                    <td>
                      <button
                        className={`btn btn-sm ${u.IsActive ? "btn-danger" : "btn-secondary"}`}
                        onClick={() => toggleActive(u)}
                      >
                        {u.IsActive ? "Deactivate" : "Activate"}
                      </button>
                    </td>
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
