"use client";

import { useState, useEffect } from "react";

const today = new Date().toISOString().split("T")[0];

export default function EmployeesPage() {
  const [employees, setEmployees] = useState([]);
  const [departments, setDepartments] = useState([]);
  const [gradeLevels, setGradeLevels] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [q, setQ] = useState("");
  const [form, setForm] = useState({
    fullName: "", nationalId: "", mobile: "", email: "",
    dateOfBirth: "", dateOfJoining: today,
    departmentId: "", gradeLevelId: "", basicSalary: "", contractType: "Permanent",
  });
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState(null);

  function load() {
    setLoading(true);
    Promise.all([
      fetch(`/api/hr/employees${q ? `?q=${encodeURIComponent(q)}` : ""}`).then((r) => r.json()),
      fetch("/api/admin/departments").then((r) => r.json()).catch(() => []),
      fetch("/api/admin/grade-levels").then((r) => r.json()).catch(() => []),
    ]).then(([emp, dep, gl]) => {
      setEmployees(emp);
      setDepartments(dep);
      setGradeLevels(gl);
      setLoading(false);
    }).catch(() => setLoading(false));
  }

  useEffect(load, [q]);

  async function handleAdd(e) {
    e.preventDefault();
    setSaving(true); setError(null);
    try {
      const res = await fetch("/api/hr/employees", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(form),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Failed");
      setShowForm(false);
      setForm({
        fullName: "", nationalId: "", mobile: "", email: "",
        dateOfBirth: "", dateOfJoining: today,
        departmentId: "", gradeLevelId: "", basicSalary: "", contractType: "Permanent",
      });
      load();
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
          <h1 className="page-title">Employees</h1>
          <p className="page-subtitle">Employee registry and profiles</p>
        </div>
        <button className="btn btn-primary" onClick={() => setShowForm((v) => !v)}>
          {showForm ? "Cancel" : "+ Add Employee"}
        </button>
      </div>

      {showForm && (
        <div className="card" style={{ marginBottom: "1rem" }}>
          <div className="card-header"><h3 className="card-title">New Employee</h3></div>
          {error && <div className="alert alert-danger" style={{ marginBottom: "0.75rem" }}>{error}</div>}
          <form onSubmit={handleAdd}>
            <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr 1fr", gap: "0.75rem" }}>
              {[
                { field: "fullName", label: "Full Name", required: true },
                { field: "nationalId", label: "National ID" },
                { field: "mobile", label: "Mobile" },
                { field: "email", label: "Email", type: "email" },
                { field: "dateOfBirth", label: "Date of Birth", type: "date" },
                { field: "dateOfJoining", label: "Date of Joining", type: "date" },
                { field: "basicSalary", label: "Basic Salary", type: "number" },
              ].map(({ field, label, required, type = "text" }) => (
                <div className="form-group" key={field} style={{ marginBottom: 0 }}>
                  <label className="form-label">{label}</label>
                  <input
                    className="input" type={type} required={required}
                    value={form[field]}
                    onChange={(e) => setForm((f) => ({ ...f, [field]: e.target.value }))}
                  />
                </div>
              ))}
              <div className="form-group" style={{ marginBottom: 0 }}>
                <label className="form-label">Department</label>
                <select className="input" value={form.departmentId} onChange={(e) => setForm((f) => ({ ...f, departmentId: e.target.value }))}>
                  <option value="">Select…</option>
                  {departments.map((d) => <option key={d.id} value={d.id}>{d.name}</option>)}
                </select>
              </div>
              <div className="form-group" style={{ marginBottom: 0 }}>
                <label className="form-label">Grade Level</label>
                <select className="input" value={form.gradeLevelId} onChange={(e) => setForm((f) => ({ ...f, gradeLevelId: e.target.value }))}>
                  <option value="">Select…</option>
                  {gradeLevels.map((g) => <option key={g.id} value={g.id}>{g.level}</option>)}
                </select>
              </div>
              <div className="form-group" style={{ marginBottom: 0 }}>
                <label className="form-label">Contract Type</label>
                <select className="input" value={form.contractType} onChange={(e) => setForm((f) => ({ ...f, contractType: e.target.value }))}>
                  {["Permanent", "Contract", "Part-time", "Intern"].map((c) => <option key={c}>{c}</option>)}
                </select>
              </div>
            </div>
            <button type="submit" className="btn btn-primary" style={{ marginTop: "1rem" }} disabled={saving}>
              {saving ? "Saving…" : "Create Employee"}
            </button>
          </form>
        </div>
      )}

      <div className="card">
        <div className="card-header" style={{ gap: "0.75rem" }}>
          <h3 className="card-title">All Employees</h3>
          <input className="input" placeholder="Search name, ID, mobile…" value={q} onChange={(e) => setQ(e.target.value)} style={{ maxWidth: "280px" }} />
        </div>
        {loading ? <div className="table-empty">Loading…</div> : !employees.length ? (
          <div className="table-empty">No employees found.</div>
        ) : (
          <div className="table-wrapper">
            <table className="table">
              <thead>
                <tr>
                  <th>Emp No.</th><th>Full Name</th><th>Department</th><th>Grade</th><th>Contract</th>
                  <th className="text-right">Basic Salary</th><th>Status</th><th>Joined</th>
                </tr>
              </thead>
              <tbody>
                {employees.map((e) => (
                  <tr key={e.EmpNo}>
                    <td><span className="badge">{e.EmpNo}</span></td>
                    <td style={{ fontWeight: 600 }}>{e.FullName}</td>
                    <td style={{ fontSize: "0.8rem" }}>{e.DepartmentName || "—"}</td>
                    <td style={{ fontSize: "0.8rem" }}>{e.GradeLevel || "—"}</td>
                    <td style={{ fontSize: "0.8rem" }}>{e.ContractType || "—"}</td>
                    <td className="text-right">
                      {Number(e.BasicSalary || 0).toLocaleString("en-US", { minimumFractionDigits: 2 })}
                    </td>
                    <td>
                      <span style={{ color: e.IsActive ? "var(--success)" : "var(--danger)", fontWeight: 500, fontSize: "0.82rem" }}>
                        {e.IsActive ? "Active" : "Inactive"}
                      </span>
                    </td>
                    <td style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>
                      {e.DateOfJoining ? new Date(e.DateOfJoining).toLocaleDateString() : "—"}
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
