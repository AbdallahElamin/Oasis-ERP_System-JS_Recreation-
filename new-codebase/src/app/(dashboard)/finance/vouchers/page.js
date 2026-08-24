"use client";

import { useState, useEffect } from "react";
import Link from "next/link";

export default function VouchersPage() {
  const currentYear = new Date().getFullYear();
  const [year, setYear] = useState(currentYear);
  const [vouchers, setVouchers] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setLoading(true);
    fetch(`/api/finance/vouchers?year=${year}`)
      .then((r) => r.json())
      .then((d) => { setVouchers(d); setLoading(false); })
      .catch(() => setLoading(false));
  }, [year]);

  const years = Array.from({ length: 5 }, (_, i) => currentYear - i);

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Journal Vouchers</h1>
          <p className="page-subtitle">Balanced double-entry vouchers</p>
        </div>
        <Link href="/finance/vouchers/new" className="btn btn-primary">
          + New Voucher
        </Link>
      </div>

      <div className="card">
        <div className="card-header" style={{ gap: "0.75rem" }}>
          <h3 className="card-title">Vouchers</h3>
          <select
            className="input"
            style={{ width: "auto" }}
            value={year}
            onChange={(e) => setYear(Number(e.target.value))}
          >
            {years.map((y) => <option key={y} value={y}>{y}</option>)}
          </select>
        </div>

        {loading ? (
          <div className="table-empty">Loading…</div>
        ) : !vouchers.length ? (
          <div className="table-empty">No vouchers found for {year}.</div>
        ) : (
          <div className="table-wrapper">
            <table className="table">
              <thead>
                <tr>
                  <th>Move No.</th>
                  <th>Date</th>
                  <th>Employee</th>
                  <th className="text-right">Total Debit</th>
                  <th className="text-right">Total Credit</th>
                  <th>Lines</th>
                </tr>
              </thead>
              <tbody>
                {vouchers.map((v) => (
                  <tr key={v.MoveNo}>
                    <td><span className="badge">{v.MoveNo}</span></td>
                    <td>{v.TransDate ? new Date(v.TransDate).toLocaleDateString() : "—"}</td>
                    <td>{v.employee || "—"}</td>
                    <td className="text-right text-danger">
                      {Number(v.totalDebit).toLocaleString("en-US", { minimumFractionDigits: 2 })}
                    </td>
                    <td className="text-right text-success">
                      {Number(v.totalCredit).toLocaleString("en-US", { minimumFractionDigits: 2 })}
                    </td>
                    <td>{v.lineCount}</td>
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
