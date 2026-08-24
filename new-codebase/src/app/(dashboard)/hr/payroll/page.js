"use client";

import { useState, useEffect } from "react";

const MONTHS = ["January","February","March","April","May","June","July","August","September","October","November","December"];
const now = new Date();

export default function PayrollPage() {
  const [employees, setEmployees] = useState([]);
  const [paySheets, setPaySheets] = useState([]);
  const [loading, setLoading] = useState(true);
  const [month, setMonth] = useState(now.getMonth() + 1);
  const [year, setYear] = useState(now.getFullYear());
  const [showForm, setShowForm] = useState(false);
  const [form, setForm] = useState({ empNo: "", basicSalary: "", allowances: "0", deductions: "0" });
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState(null);

  function loadPaySheets() {
    setLoading(true);
    fetch(`/api/hr/payroll?year=${year}&month=${month}`)
      .then((r) => r.json())
      .then((d) => { setPaySheets(d); setLoading(false); })
      .catch(() => setLoading(false));
  }

  useEffect(() => {
    fetch("/api/hr/employees").then((r) => r.json()).then(setEmployees).catch(() => {});
  }, []);

  useEffect(loadPaySheets, [year, month]);

  const netPay = (parseFloat(form.basicSalary) || 0) + (parseFloat(form.allowances) || 0) - (parseFloat(form.deductions) || 0);

  async function handleSave(e) {
    e.preventDefault();
    setSaving(true); setError(null);
    try {
      const res = await fetch("/api/hr/payroll", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ ...form, month, year, netPay }),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Failed");
      setShowForm(false);
      setForm({ empNo: "", basicSalary: "", allowances: "0", deductions: "0" });
      loadPaySheets();
    } catch (err) {
      setError(err.message);
    } finally {
      setSaving(false);
    }
  }

  const totalNet = paySheets.reduce((s, r) => s + Number(r.NetPay || 0), 0);
  const years = [0,1,2].map((i) => now.getFullYear() - i);

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Payroll</h1>
          <p className="page-subtitle">Monthly salary sheets</p>
        </div>
        <button className="btn btn-primary" onClick={() => setShowForm((v) => !v)}>
          {showForm ? "Cancel" : "+ Add Entry"}
        </button>
      </div>

      {showForm && (
        <div className="card" style={{ marginBottom: "1rem", maxWidth: "480px" }}>
          {error && <div className="alert alert-danger" style={{ marginBottom: "0.75rem" }}>{error}</div>}
          <form onSubmit={handleSave}>
            <div className="form-group">
              <label className="form-label">Employee</label>
              <select className="input" required value={form.empNo} onChange={(e) => {
                const emp = employees.find((em) => String(em.EmpNo) === e.target.value);
                setForm((f) => ({ ...f, empNo: e.target.value, basicSalary: emp?.BasicSalary || "" }));
              }}>
                <option value="">Select…</option>
                {employees.map((e) => <option key={e.EmpNo} value={e.EmpNo}>{e.FullName}</option>)}
              </select>
            </div>
            {[["basicSalary","Basic Salary"],["allowances","Allowances"],["deductions","Deductions"]].map(([field, label]) => (
              <div className="form-group" key={field}>
                <label className="form-label">{label}</label>
                <input type="number" className="input" min="0" step="0.01" value={form[field]}
                  onChange={(e) => setForm((f) => ({ ...f, [field]: e.target.value }))} />
              </div>
            ))}
            <div style={{ padding: "0.75rem", background: "var(--bg-tertiary)", borderRadius: "0.5rem", marginBottom: "0.75rem" }}>
              <div style={{ display: "flex", justifyContent: "space-between" }}>
                <span style={{ color: "var(--text-muted)", fontSize: "0.85rem" }}>Net Pay</span>
                <span style={{ fontWeight: 700, color: netPay >= 0 ? "var(--success)" : "var(--danger)" }}>
                  {netPay.toLocaleString("en-US", { minimumFractionDigits: 2 })}
                </span>
              </div>
            </div>
            <button type="submit" className="btn btn-primary" style={{ width: "100%" }} disabled={saving}>
              {saving ? "Saving…" : `Save for ${MONTHS[month-1]} ${year}`}
            </button>
          </form>
        </div>
      )}

      <div className="card">
        <div className="card-header" style={{ gap: "0.75rem", flexWrap: "wrap" }}>
          <h3 className="card-title">Pay Sheet</h3>
          <select className="input" style={{ width: "auto" }} value={month} onChange={(e) => setMonth(Number(e.target.value))}>
            {MONTHS.map((m, i) => <option key={i} value={i+1}>{m}</option>)}
          </select>
          <select className="input" style={{ width: "auto" }} value={year} onChange={(e) => setYear(Number(e.target.value))}>
            {years.map((y) => <option key={y} value={y}>{y}</option>)}
          </select>
        </div>

        {loading ? <div className="table-empty">Loading…</div> : !paySheets.length ? (
          <div className="table-empty">No payroll entries for {MONTHS[month-1]} {year}.</div>
        ) : (
          <div className="table-wrapper">
            <table className="table">
              <thead>
                <tr>
                  <th>Emp No.</th><th>Full Name</th>
                  <th className="text-right">Basic</th><th className="text-right">Allowances</th>
                  <th className="text-right">Deductions</th><th className="text-right">Net Pay</th>
                  <th>Approved</th>
                </tr>
              </thead>
              <tbody>
                {paySheets.map((ps) => (
                  <tr key={ps.id}>
                    <td><span className="badge">{ps.EmpNo}</span></td>
                    <td style={{ fontWeight: 600 }}>{ps.FullName}</td>
                    <td className="text-right">{Number(ps.BasicSalary).toFixed(2)}</td>
                    <td className="text-right text-success">+{Number(ps.Allowances).toFixed(2)}</td>
                    <td className="text-right text-danger">-{Number(ps.Deductions).toFixed(2)}</td>
                    <td className="text-right" style={{ fontWeight: 700 }}>{Number(ps.NetPay).toFixed(2)}</td>
                    <td>
                      <span style={{ color: ps.Approved ? "var(--success)" : "var(--text-muted)", fontSize: "0.8rem" }}>
                        {ps.Approved ? "✓ Approved" : "Pending"}
                      </span>
                    </td>
                  </tr>
                ))}
              </tbody>
              <tfoot>
                <tr style={{ fontWeight: 700, borderTop: "2px solid var(--border-subtle)" }}>
                  <td colSpan={5} className="text-right">Total Net Pay</td>
                  <td className="text-right text-success">{totalNet.toLocaleString("en-US", { minimumFractionDigits: 2 })}</td>
                  <td></td>
                </tr>
              </tfoot>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}
