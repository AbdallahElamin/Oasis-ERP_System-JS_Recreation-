"use client";

import { useState, useEffect } from "react";
import { useParams, useSearchParams } from "next/navigation";

export default function QuotationPrintPage() {
  const params = useParams();
  const searchParams = useSearchParams();
  const invNo = params.invNo;
  const year = searchParams.get("year") || new Date().getFullYear();

  const [rows, setRows] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch(`/api/sales/quotations/${invNo}?year=${year}`)
      .then((r) => r.json())
      .then((data) => {
        setRows(Array.isArray(data) ? data : []);
        setLoading(false);
        setTimeout(() => window.print(), 600);
      })
      .catch(() => setLoading(false));
  }, [invNo, year]);

  if (loading) return <div style={{ padding: "2rem", textAlign: "center" }}>Loading quotation…</div>;
  if (!rows.length) return <div style={{ padding: "2rem" }}>Quotation not found.</div>;

  const h = rows[0];
  const subtotal = rows.reduce((s, r) => s + Number(r.Rpric) * Number(r.Qnt), 0);

  return (
    <div className="print-wrapper">
      <button className="no-print btn btn-secondary" style={{ marginBottom: "1rem" }} onClick={() => window.print()}>
        🖨 Print Quotation
      </button>

      <div className="print-header">
        <h1>PRICE QUOTATION</h1>
        <p>Oasis ERP System — This is not a tax invoice</p>
      </div>

      <div className="print-meta">
        <div className="print-meta-block">
          <strong>Quotation For</strong>
          {h.CustName}<br />
          ID: {h.CustID}
        </div>
        <div className="print-meta-block" style={{ textAlign: "right" }}>
          <strong>Quotation No.</strong>
          #{h.InvNo}<br />
          <strong>Date</strong>
          {new Date(h.TransDate).toLocaleDateString("en-GB")}<br />
          <strong>Prepared By</strong>
          {h.employee || "—"}
        </div>
      </div>

      <table className="print-table">
        <thead>
          <tr>
            <th>#</th><th>Item</th><th>Batch No.</th><th>Pack</th>
            <th style={{ textAlign: "right" }}>Unit Price</th>
            <th style={{ textAlign: "right" }}>Qty</th>
            <th style={{ textAlign: "right" }}>Total</th>
          </tr>
        </thead>
        <tbody>
          {rows.map((r, i) => (
            <tr key={r.SNo}>
              <td>{i + 1}</td><td>{r.item}</td><td>{r.BatchNo || "—"}</td><td>{r.pack || "—"}</td>
              <td style={{ textAlign: "right" }}>{Number(r.Rpric).toFixed(2)}</td>
              <td style={{ textAlign: "right" }}>{r.Qnt}</td>
              <td style={{ textAlign: "right" }}>{(Number(r.Rpric) * Number(r.Qnt)).toFixed(2)}</td>
            </tr>
          ))}
        </tbody>
        <tfoot>
          <tr><td colSpan={6} style={{ textAlign: "right" }}>Total</td><td style={{ textAlign: "right" }}>{subtotal.toFixed(2)}</td></tr>
        </tfoot>
      </table>

      <p style={{ fontSize: "0.78rem", color: "#666", marginTop: "1rem" }}>
        This quotation is valid for 30 days from the date above. Prices are subject to change without prior notice.
      </p>

      <div className="print-signatures">
        <div><span>Prepared By</span></div>
        <div><span>Authorized By</span></div>
        <div><span>Client Signature</span></div>
      </div>
    </div>
  );
}
