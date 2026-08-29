"use client";

import { useState } from "react";

export default function ReturnsPage() {
  const [invNo, setInvNo] = useState("");
  const [year, setYear] = useState(new Date().getFullYear());
  const [invoice, setInvoice] = useState(null);
  const [loading, setLoading] = useState(false);
  const [processing, setProcessing] = useState(false);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);
  // selected quantities — keyed by line sno
  const [returnQtys, setReturnQtys] = useState({});

  async function lookupInvoice() {
    if (!invNo) return;
    setLoading(true); setError(null); setInvoice(null); setSuccess(null);
    try {
      const res = await fetch(`/api/sales/returns?invNo=${invNo}&year=${year}`);
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Not found");
      setInvoice(data);
      // Default return qty = original qty for each line
      const qtys = {};
      data.lines.forEach((l) => { qtys[l.sno] = l.qnt; });
      setReturnQtys(qtys);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  }

  async function processReturn() {
    if (!invoice) return;
    setProcessing(true); setError(null);
    try {
      const linesToReturn = invoice.lines
        .filter((l) => Number(returnQtys[l.sno] || 0) > 0)
        .map((l) => ({ ...l, qnt: Number(returnQtys[l.sno]) }));

      if (!linesToReturn.length) throw new Error("No lines selected for return");

      const res = await fetch("/api/sales/returns", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          invNo: invoice.header.invNo,
          year,
          custId: invoice.header.custId,
          custName: invoice.header.custName,
          netAmount: invoice.header.netAmount,
          lines: linesToReturn,
        }),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Failed");
      setSuccess(`Return processed successfully. Reversing Journal Entry #${data.moveNo} created.`);
      setInvoice(null); setInvNo(""); setReturnQtys({});
    } catch (err) {
      setError(err.message);
    } finally {
      setProcessing(false);
    }
  }

  const years = [0, 1, 2].map((i) => new Date().getFullYear() - i);

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Return Invoice</h1>
          <p className="page-subtitle">Reverse a sales invoice and restore stock</p>
        </div>
      </div>

      {/* Lookup */}
      <div className="card" style={{ marginBottom: "1rem", maxWidth: "520px" }}>
        <div className="card-header"><h3 className="card-title">Look Up Invoice</h3></div>
        <div style={{ display: "flex", gap: "0.75rem", alignItems: "flex-end" }}>
          <div className="form-group" style={{ flex: 1, marginBottom: 0 }}>
            <label className="form-label">Invoice No.</label>
            <input
              className="input" type="number" placeholder="e.g. 42" value={invNo}
              onChange={(e) => setInvNo(e.target.value)}
              onKeyDown={(e) => e.key === "Enter" && lookupInvoice()}
            />
          </div>
          <div className="form-group" style={{ width: "110px", marginBottom: 0 }}>
            <label className="form-label">Year</label>
            <select className="input" value={year} onChange={(e) => setYear(Number(e.target.value))}>
              {years.map((y) => <option key={y} value={y}>{y}</option>)}
            </select>
          </div>
          <button className="btn btn-primary" onClick={lookupInvoice} disabled={loading || !invNo} style={{ marginBottom: 0 }}>
            {loading ? "Loading…" : "Load"}
          </button>
        </div>
      </div>

      {error && <div className="alert alert-danger" style={{ marginBottom: "1rem" }}>{error}</div>}
      {success && (
        <div className="alert alert-success" style={{ marginBottom: "1rem" }}>
          ✓ {success}
        </div>
      )}

      {invoice && (
        <>
          {/* Invoice header */}
          <div className="card" style={{ marginBottom: "1rem" }}>
            <div className="card-header"><h3 className="card-title">Invoice #{invoice.header.invNo} Details</h3></div>
            <div style={{ display: "grid", gridTemplateColumns: "repeat(4, 1fr)", gap: "1rem", fontSize: "0.85rem" }}>
              {[
                ["Client ID", invoice.header.custId],
                ["Client Name", invoice.header.custName],
                ["Net Amount", Number(invoice.header.netAmount).toFixed(2)],
                ["Date", new Date(invoice.header.transDate).toLocaleDateString()],
                ["Discount", `${invoice.header.disc}%`],
                ["VAT", `${invoice.header.vat}%`],
                ["Amount in Words", invoice.header.amountInWords],
                ["Processed by", invoice.header.employee],
              ].map(([k, v]) => (
                <div key={k}>
                  <div style={{ color: "var(--text-muted)", fontSize: "0.75rem", marginBottom: "0.2rem" }}>{k}</div>
                  <div style={{ color: "var(--text-primary)", fontWeight: 500 }}>{v || "—"}</div>
                </div>
              ))}
            </div>
          </div>

          {/* Lines */}
          <div className="card" style={{ marginBottom: "1rem" }}>
            <div className="card-header">
              <h3 className="card-title">Items to Return</h3>
              <span style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>
                Adjust quantities to do a partial return
              </span>
            </div>
            <div className="table-wrapper">
              <table className="table">
                <thead>
                  <tr>
                    <th>Store</th><th>Item</th><th>Batch</th><th>Pack</th>
                    <th className="text-right">W.Price</th><th className="text-right">R.Price</th>
                    <th className="text-right">Original Qty</th>
                    <th className="text-right">Return Qty</th>
                  </tr>
                </thead>
                <tbody>
                  {invoice.lines.map((line) => (
                    <tr key={line.sno}>
                      <td style={{ fontSize: "0.8rem" }}>{line.storeName}</td>
                      <td style={{ fontWeight: 500 }}>{line.item}</td>
                      <td style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>{line.batchNo || "—"}</td>
                      <td style={{ fontSize: "0.8rem" }}>{line.pack || "—"}</td>
                      <td className="text-right">{Number(line.price).toFixed(2)}</td>
                      <td className="text-right">{Number(line.rpric).toFixed(2)}</td>
                      <td className="text-right">{line.qnt}</td>
                      <td className="text-right" style={{ width: "100px" }}>
                        <input
                          type="number" className="input" style={{ width: "80px", textAlign: "right", padding: "0.25rem 0.5rem" }}
                          min="0" max={line.qnt} step="1"
                          value={returnQtys[line.sno] ?? line.qnt}
                          onChange={(e) => setReturnQtys((q) => ({ ...q, [line.sno]: e.target.value }))}
                        />
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>

            <div style={{ display: "flex", justifyContent: "flex-end", gap: "0.75rem", marginTop: "1rem" }}>
              <button className="btn btn-secondary" onClick={() => { setInvoice(null); setInvNo(""); }}>
                Cancel
              </button>
              <button className="btn btn-danger" onClick={processReturn} disabled={processing}>
                {processing ? "Processing…" : "Confirm Return"}
              </button>
            </div>
          </div>
        </>
      )}
    </div>
  );
}
