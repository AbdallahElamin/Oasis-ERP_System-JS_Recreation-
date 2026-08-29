"use client";

import { useState, useEffect } from "react";
import { useParams, useSearchParams } from "next/navigation";

export default function VoucherPrintPage() {
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

  const totalDebit = lines.reduce((s, l) => s + Number(l.TotalValueOut || 0), 0);
  const totalCredit = lines.reduce((s, l) => s + Number(l.TotalValueIn || 0), 0);
  const date = lines[0]?.TransDate;
  const employee = lines[0]?.employee;

  return (
    <div className="print-wrapper">
      <button className="no-print btn btn-secondary" style={{ marginBottom: "1rem" }} onClick={() => window.print()}>
        🖨 Print Voucher
      </button>

      {loading ? (
        <div style={{ textAlign: "center", padding: "2rem" }}>Loading voucher…</div>
      ) : !lines.length ? (
        <div style={{ padding: "2rem" }}>Voucher #{moveNo} not found.</div>
      ) : (
        <>
          <div className="print-header">
            <h1>JOURNAL VOUCHER</h1>
            <p>Oasis ERP System — Financial System</p>
          </div>

          <div className="print-meta">
            <div className="print-meta-block">
              <strong>Voucher No.</strong>#{moveNo}
            </div>
            <div className="print-meta-block" style={{ textAlign: "right" }}>
              <strong>Date</strong>
              {date ? new Date(date).toLocaleDateString("en-GB") : "—"}<br />
              <strong>Prepared By</strong>{employee || "—"}
            </div>
          </div>

          <table className="print-table">
            <thead>
              <tr>
                <th>Acc1</th><th>Acc2</th><th>Acc3</th><th>Acc4</th>
                <th>Description</th>
                <th style={{ textAlign: "right" }}>Debit</th>
                <th style={{ textAlign: "right" }}>Credit</th>
              </tr>
            </thead>
            <tbody>
              {lines.map((l, i) => (
                <tr key={i}>
                  <td style={{ fontSize: "0.75rem" }}>{l.Acc1 || "—"}</td>
                  <td style={{ fontSize: "0.75rem" }}>{l.Acc2 || "—"}</td>
                  <td style={{ fontSize: "0.75rem" }}>{l.Acc3 || "—"}</td>
                  <td style={{ fontWeight: 600 }}>{l.Acc4 || "—"}</td>
                  <td style={{ fontSize: "0.78rem" }}>{l.Ref || "—"}</td>
                  <td style={{ textAlign: "right" }}>
                    {Number(l.TotalValueOut) > 0 ? Number(l.TotalValueOut).toFixed(2) : ""}
                  </td>
                  <td style={{ textAlign: "right" }}>
                    {Number(l.TotalValueIn) > 0 ? Number(l.TotalValueIn).toFixed(2) : ""}
                  </td>
                </tr>
              ))}
            </tbody>
            <tfoot>
              <tr>
                <td colSpan={5} style={{ textAlign: "right" }}>Totals</td>
                <td style={{ textAlign: "right" }}>{totalDebit.toFixed(2)}</td>
                <td style={{ textAlign: "right" }}>{totalCredit.toFixed(2)}</td>
              </tr>
            </tfoot>
          </table>

          <div className="print-signatures">
            <div><span>Prepared By</span></div>
            <div><span>Reviewed By</span></div>
            <div><span>Approved By</span></div>
          </div>
        </>
      )}
    </div>
  );
}
