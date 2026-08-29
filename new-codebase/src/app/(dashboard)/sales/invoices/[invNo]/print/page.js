"use client";

import { useState, useEffect } from "react";
import { useParams, useSearchParams } from "next/navigation";

export default function InvoicePrintPage() {
  const params = useParams();
  const searchParams = useSearchParams();
  const invNo = params.invNo;
  const year = searchParams.get("year") || new Date().getFullYear();

  const [rows, setRows] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch(`/api/sales/invoices/${invNo}?year=${year}`)
      .then((r) => r.json())
      .then((data) => {
        setRows(Array.isArray(data) ? data : []);
        setLoading(false);
        // Auto-print after render
        setTimeout(() => window.print(), 600);
      })
      .catch(() => setLoading(false));
  }, [invNo, year]);

  if (loading) return <div style={{ padding: "2rem", textAlign: "center" }}>Loading invoice…</div>;
  if (!rows.length) return <div style={{ padding: "2rem" }}>Invoice not found.</div>;

  const h = rows[0]; // header info from first row
  const subtotal = rows.reduce((s, r) => s + Number(r.Rpric) * Number(r.Qnt), 0);
  const disc = Number(h.Disc || 0);
  const vat = Number(h.VAT || 0);
  const net = Number(h.NetAmount || 0);

  return (
    <div className="print-wrapper">
      <button className="no-print btn btn-secondary" style={{ marginBottom: "1rem" }} onClick={() => window.print()}>
        🖨 Print Invoice
      </button>

      <div className="print-header">
        <h1>TAX INVOICE</h1>
        <p>Oasis ERP System — Official Sales Invoice</p>
      </div>

      <div className="print-meta">
        <div className="print-meta-block">
          <strong>Client</strong>
          {h.CustName}<br />
          ID: {h.CustID}
        </div>
        <div className="print-meta-block" style={{ textAlign: "right" }}>
          <strong>Invoice No.</strong>
          #{h.InvNo}<br />
          <strong>Date</strong>
          {new Date(h.TransDate).toLocaleDateString("en-GB")}<br />
          <strong>Processed By</strong>
          {h.employee || "—"}
        </div>
      </div>

      <table className="print-table">
        <thead>
          <tr>
            <th>#</th><th>Item</th><th>Batch No.</th><th>Pack</th>
            <th style={{ textAlign: "right" }}>W.Price</th>
            <th style={{ textAlign: "right" }}>R.Price</th>
            <th style={{ textAlign: "right" }}>Qty</th>
            <th style={{ textAlign: "right" }}>Total</th>
          </tr>
        </thead>
        <tbody>
          {rows.map((r, i) => (
            <tr key={r.SNo}>
              <td>{i + 1}</td>
              <td>{r.item}</td>
              <td>{r.BatchNo || "—"}</td>
              <td>{r.pack || "—"}</td>
              <td style={{ textAlign: "right" }}>{Number(r.price).toFixed(2)}</td>
              <td style={{ textAlign: "right" }}>{Number(r.Rpric).toFixed(2)}</td>
              <td style={{ textAlign: "right" }}>{r.Qnt}</td>
              <td style={{ textAlign: "right" }}>{(Number(r.Rpric) * Number(r.Qnt)).toFixed(2)}</td>
            </tr>
          ))}
        </tbody>
      </table>

      <div className="print-totals">
        <div className="print-totals-grid">
          <span className="label">Subtotal</span><span className="value">{subtotal.toFixed(2)}</span>
          <span className="label">Discount ({disc}%)</span><span className="value">-{(subtotal * disc / 100).toFixed(2)}</span>
          <span className="label">VAT ({vat}%)</span><span className="value">+{(subtotal * vat / 100).toFixed(2)}</span>
          <div className="grand"><span className="label">Net Amount</span></div>
          <div className="grand"><span className="value">{net.toFixed(2)}</span></div>
        </div>
      </div>

      {h.AmountInWords && (
        <div className="print-in-words">
          <strong>Amount in Words:</strong> {h.AmountInWords}
        </div>
      )}

      <div className="print-signatures">
        <div><span>Prepared By</span></div>
        <div><span>Approved By</span></div>
        <div><span>Received By</span></div>
      </div>
    </div>
  );
}
