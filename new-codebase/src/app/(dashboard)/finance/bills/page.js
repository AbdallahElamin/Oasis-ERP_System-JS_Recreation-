"use client";

import { useState } from "react";
import DateRangePicker from "@/components/common/DateRangePicker";
import AccountTree from "@/components/finance/AccountTree";

const today = new Date().toISOString().split("T")[0];
const yearStart = `${new Date().getFullYear()}-01-01`;

function fmt(n) {
  return Number(n || 0).toLocaleString("en-US", { minimumFractionDigits: 2 });
}

function BillForm({ type, onSuccess }) {
  const [date, setDate] = useState(today);
  const [source, setSource] = useState("");
  const [description, setDescription] = useState("");
  const [amountInWords, setAmountInWords] = useState("");
  const [paymentType, setPaymentType] = useState("cash");
  const [chequeNo, setChequeNo] = useState("");
  const [bankName, setBankName] = useState("");
  const [chequeDate, setChequeDate] = useState(today);
  const [lines, setLines] = useState([]);
  const [currentLine, setCurrentLine] = useState({ acc1: "", acc2: "", acc3: "", acc4: "", amount: "" });
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState(null);

  function handleTreeSelect(node) {
    setCurrentLine((l) => ({
      ...l,
      acc1: node.acc1 || "", acc2: node.acc2 || "",
      acc3: node.acc3 || "", acc4: node.acc4 || "",
    }));
  }

  function addLine() {
    if (!currentLine.acc4) { setError("Select an account (all 4 levels)"); return; }
    const amount = parseFloat(currentLine.amount);
    if (!amount || amount <= 0) { setError("Enter a valid amount"); return; }
    setError(null);
    setLines((prev) => [...prev, { ...currentLine, amount }]);
    setCurrentLine((l) => ({ ...l, amount: "" }));
  }

  const total = lines.reduce((s, l) => s + l.amount, 0);

  async function handleSave() {
    if (!source.trim()) { setError("Source / payee is required"); return; }
    if (!lines.length) { setError("Add at least one line"); return; }
    setSaving(true);
    setError(null);
    try {
      const res = await fetch(`/api/finance/bills/${type}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          date, source, description, amountInWords, paymentType,
          chequeNo: paymentType === "bank" ? chequeNo : null,
          bankName: paymentType === "bank" ? bankName : null,
          chequeDate: paymentType === "bank" ? chequeDate : null,
          lines,
        }),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Failed");
      setSource(""); setDescription(""); setLines([]);
      setAmountInWords(""); setChequeNo(""); setBankName("");
      onSuccess?.(`Saved — Move No. ${data.moveNo}, Bill No. ${data.paperNo}`);
    } catch (err) {
      setError(err.message);
    } finally {
      setSaving(false);
    }
  }

  return (
    <div style={{ display: "grid", gridTemplateColumns: "260px 1fr", gap: "1rem" }}>
      {/* Tree */}
      <div className="card" style={{ maxHeight: "60vh", overflowY: "auto" }}>
        <div className="card-header" style={{ marginBottom: "0.25rem" }}>
          <h3 className="card-title" style={{ fontSize: "0.85rem" }}>Accounts</h3>
        </div>
        <AccountTree onSelect={handleTreeSelect} selectable="leaves" />
      </div>

      {/* Form */}
      <div>
        {error && <div className="alert alert-danger" style={{ marginBottom: "0.75rem" }}>{error}</div>}

        <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr 1fr", gap: "0.75rem", marginBottom: "0.75rem" }}>
          <div className="form-group"><label className="form-label">Date</label><input type="date" className="input" value={date} onChange={(e) => setDate(e.target.value)} /></div>
          <div className="form-group"><label className="form-label">Source / Payee</label><input className="input" placeholder="Name…" value={source} onChange={(e) => setSource(e.target.value)} /></div>
          <div className="form-group"><label className="form-label">Description</label><input className="input" placeholder="Purpose…" value={description} onChange={(e) => setDescription(e.target.value)} /></div>
        </div>

        <div style={{ display: "flex", gap: "0.75rem", marginBottom: "0.75rem" }}>
          <div className="form-group" style={{ flex: "0 0 auto" }}>
            <label className="form-label">Payment</label>
            <select className="input" value={paymentType} onChange={(e) => setPaymentType(e.target.value)}>
              <option value="cash">Cash</option>
              <option value="bank">Bank Cheque</option>
            </select>
          </div>
          {paymentType === "bank" && <>
            <div className="form-group"><label className="form-label">Bank Name</label><input className="input" placeholder="Bank ABC…" value={bankName} onChange={(e) => setBankName(e.target.value)} /></div>
            <div className="form-group"><label className="form-label">Cheque No.</label><input className="input" placeholder="CHQ-001" value={chequeNo} onChange={(e) => setChequeNo(e.target.value)} /></div>
            <div className="form-group"><label className="form-label">Cheque Date</label><input type="date" className="input" value={chequeDate} onChange={(e) => setChequeDate(e.target.value)} /></div>
          </>}
        </div>

        {/* Add line row */}
        <div className="card" style={{ marginBottom: "0.75rem" }}>
          <div style={{ display: "grid", gridTemplateColumns: "1fr auto", gap: "0.75rem", alignItems: "end" }}>
            <div className="form-group" style={{ marginBottom: 0 }}>
              <label className="form-label">Account</label>
              <input className="input" value={currentLine.acc4 ? `${currentLine.acc3} › ${currentLine.acc4}` : ""} readOnly placeholder="Select from tree" style={{ color: "var(--text-muted)" }} />
            </div>
            <div className="form-group" style={{ marginBottom: 0 }}>
              <label className="form-label">Amount</label>
              <input type="number" className="input" min="0" step="0.01" placeholder="0.00" value={currentLine.amount} onChange={(e) => setCurrentLine((l) => ({ ...l, amount: e.target.value }))} onKeyDown={(e) => { if (e.key === "Enter") addLine(); }} />
            </div>
          </div>
          <button className="btn btn-secondary" style={{ marginTop: "0.5rem" }} onClick={addLine}>+ Add Line</button>
        </div>

        {/* Lines table */}
        {lines.length > 0 && (
          <div className="table-wrapper" style={{ marginBottom: "0.75rem" }}>
            <table className="table">
              <thead><tr><th>Acc3</th><th>Acc4</th><th className="text-right">Amount</th><th></th></tr></thead>
              <tbody>
                {lines.map((l, i) => (
                  <tr key={i}>
                    <td>{l.acc3}</td><td style={{ fontWeight: 600 }}>{l.acc4}</td>
                    <td className="text-right">{fmt(l.amount)}</td>
                    <td><button onClick={() => setLines((p) => p.filter((_, j) => j !== i))} style={{ background: "none", border: "none", color: "var(--danger)", cursor: "pointer" }}>×</button></td>
                  </tr>
                ))}
                <tr style={{ fontWeight: 700 }}>
                  <td colSpan={2} className="text-right">Total</td>
                  <td className="text-right">{fmt(total)}</td>
                  <td></td>
                </tr>
              </tbody>
            </table>
          </div>
        )}

        <div className="form-group">
          <label className="form-label">Amount in Words</label>
          <input className="input" placeholder="Three thousand pounds only…" value={amountInWords} onChange={(e) => setAmountInWords(e.target.value)} />
        </div>

        <button className="btn btn-primary" onClick={handleSave} disabled={saving} style={{ width: "100%" }}>
          {saving ? "Saving…" : `Save ${type === "pay" ? "Payment" : "Receipt"} Voucher`}
        </button>
      </div>
    </div>
  );
}

export default function BillsPage() {
  const [tab, setTab] = useState("archive"); // "archive" | "pay" | "receipt"
  const [range, setRange] = useState({ from: yearStart, to: today });
  const [filter, setFilter] = useState("all"); // "all" | "pay" | "receipt"
  const [bills, setBills] = useState([]);
  const [loaded, setLoaded] = useState(false);
  const [loading, setLoading] = useState(false);
  const [successMsg, setSuccessMsg] = useState(null);

  async function loadBills() {
    setLoading(true);
    try {
      const p = new URLSearchParams({ from: range.from, to: range.to });
      if (filter !== "all") p.set("type", filter);
      const res = await fetch(`/api/finance/bills?${p}`);
      setBills(await res.json());
      setLoaded(true);
    } finally {
      setLoading(false);
    }
  }

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Bills</h1>
          <p className="page-subtitle">Pay vouchers · Receipt vouchers · Archive</p>
        </div>
        <div style={{ display: "flex", gap: "0.5rem" }}>
          {["archive", "pay", "receipt"].map((t) => (
            <button key={t} className={`btn ${tab === t ? "btn-primary" : "btn-secondary"}`} onClick={() => setTab(t)}>
              {t === "archive" ? "Archive" : t === "pay" ? "+ Pay Voucher" : "+ Receipt Voucher"}
            </button>
          ))}
        </div>
      </div>

      {successMsg && (
        <div className="alert alert-success" style={{ marginBottom: "1rem" }}>
          {successMsg}
          <button onClick={() => setSuccessMsg(null)} style={{ background: "none", border: "none", cursor: "pointer", marginLeft: "0.5rem", color: "inherit" }}>×</button>
        </div>
      )}

      {tab === "archive" && (
        <div className="card">
          <div className="card-header" style={{ gap: "1rem", flexWrap: "wrap" }}>
            <DateRangePicker from={range.from} to={range.to} onChange={setRange} />
            <select className="input" style={{ width: "auto" }} value={filter} onChange={(e) => setFilter(e.target.value)}>
              <option value="all">All Bills</option>
              <option value="pay">Pay Vouchers</option>
              <option value="receipt">Receipt Vouchers</option>
            </select>
            <button className="btn btn-primary" onClick={loadBills} disabled={loading}>{loading ? "Loading…" : "Show"}</button>
          </div>

          {loaded && (
            bills.length === 0 ? (
              <div className="table-empty">No bills found for this period.</div>
            ) : (
              <div className="table-wrapper">
                <table className="table">
                  <thead>
                    <tr>
                      <th>Move No.</th><th>Bill No.</th><th>Type</th><th>Payment</th>
                      <th>Source</th><th>Date</th><th className="text-right">Total In</th><th className="text-right">Total Out</th>
                    </tr>
                  </thead>
                  <tbody>
                    {bills.map((b, i) => (
                      <tr key={i}>
                        <td><span className="badge">{b.MoveNo}</span></td>
                        <td>{b.SNo2}</td>
                        <td><span className={`badge ${b.TransType === "Pay Voucher" ? "badge-danger" : "badge-success"}`}>{b.TransType}</span></td>
                        <td>{b.PaymentType === "C" ? "Cash" : "Bank"}</td>
                        <td>{b.Source}</td>
                        <td style={{ fontSize: "0.8rem" }}>{b.TransDate ? new Date(b.TransDate).toLocaleDateString() : "—"}</td>
                        <td className="text-right text-success">{Number(b.totalIn) > 0 ? fmt(b.totalIn) : ""}</td>
                        <td className="text-right text-danger">{Number(b.totalOut) > 0 ? fmt(b.totalOut) : ""}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )
          )}
        </div>
      )}

      {tab === "pay" && (
        <div className="card">
          <div className="card-header"><h3 className="card-title">New Payment Voucher</h3></div>
          <BillForm type="pay" onSuccess={(msg) => { setSuccessMsg(msg); setTab("archive"); }} />
        </div>
      )}

      {tab === "receipt" && (
        <div className="card">
          <div className="card-header"><h3 className="card-title">New Receipt Voucher</h3></div>
          <BillForm type="receipt" onSuccess={(msg) => { setSuccessMsg(msg); setTab("archive"); }} />
        </div>
      )}
    </div>
  );
}
