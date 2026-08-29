"use client";

import { useState, useEffect } from "react";
import { useParams, useSearchParams } from "next/navigation";

export default function BillPrintPage() {
  const params = useParams();
  const searchParams = useSearchParams();
  const moveNo = parseInt(params.moveNo, 10);
  const year = searchParams.get("year") || new Date().getFullYear();

  const [lines, setLines] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch(`/api/finance/vouchers/lines?moveNo=${moveNo}&year=${year}`)
      .then((r) => r.json())
      .then((data) => {
        setLines(Array.isArray(data) ? data : []);
        setLoading(false);
        setTimeout(() => window.print(), 600);
      })
      .catch(() => setLoading(false));
  }, [moveNo, year]);

  if (loading) return <div style={{ padding: "2rem", textAlign: "center" }}>Loading voucher…</div>;
  if (!lines.length) return <div style={{ padding: "2rem" }}>Bill #{moveNo} not found.</div>;

  const h = lines[0];
  const transType = h.TransType || "Payment Voucher";
  const isReceipt = transType.toLowerCase().includes("receipt");
  const totalAmount = lines.reduce((s, l) =>
    s + Number(l.TotalIn || 0) + Number(l.TotalOut || 0), 0);

  return (
    <div className="print-wrapper">
      <button className="no-print btn btn-secondary" style={{ marginBottom: "1rem" }} onClick={() => window.print()}>
        🖨 Print Voucher
      </button>

      <div className="print-header">
        <h1>{isReceipt ? "RECEIPT VOUCHER" : "PAYMENT VOUCHER"}</h1>
        <p>Oasis ERP System — Financial System</p>
      </div>

      <div className="print-meta">
        <div className="print-meta-block">
          <strong>Voucher No.</strong>#{moveNo}
          {h.PaperNo && <><br /><strong>Paper No.</strong>{h.PaperNo}</>}
          <br /><strong>Source / Payee</strong>{h.Source || "—"}
        </div>
        <div className="print-meta-block" style={{ textAlign: "right" }}>
          <strong>Date</strong>
          {h.TransDate ? new Date(h.TransDate).toLocaleDateString("en-GB") : "—"}<br />
          <strong>Payment Method</strong>
          {h.PaymentType === "B" ? "Bank Cheque" : "Cash"}<br />
          {h.CheqDate && (
            <><strong>Cheque Date</strong>{new Date(h.CheqDate).toLocaleDateString("en-GB")}<br /></>
          )}
          <strong>Prepared By</strong>{h.employee || "—"}
        </div>
      </div>

      <table className="print-table">
        <thead>
          <tr>
            <th>Account</th><th>Description</th>
            <th style={{ textAlign: "right" }}>Amount</th>
          </tr>
        </thead>
        <tbody>
          {lines.map((l, i) => (
            <tr key={i}>
              <td>{[l.Acc3, l.Acc4].filter(Boolean).join(" › ") || "—"}</td>
              <td>{l.Ref || l.Source || "—"}</td>
              <td style={{ textAlign: "right" }}>
                {(Number(l.TotalIn) + Number(l.TotalOut)).toFixed(2)}
              </td>
            </tr>
          ))}
        </tbody>
        <tfoot>
          <tr>
            <td colSpan={2} style={{ textAlign: "right" }}>Total</td>
            <td style={{ textAlign: "right" }}>{totalAmount.toFixed(2)}</td>
          </tr>
        </tfoot>
      </table>

      {h.Writting && (
        <div className="print-in-words">
          <strong>Amount in Words:</strong> {h.Writting}
        </div>
      )}

      <div className="print-signatures">
        <div><span>Prepared By</span></div>
        <div><span>Approved By</span></div>
        <div><span>{isReceipt ? "Paid By" : "Received By"}</span></div>
      </div>
    </div>
  );
}
