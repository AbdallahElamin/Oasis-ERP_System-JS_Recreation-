"use client";

import { useState, useEffect } from "react";

export default function StockStatusPage() {
  const [stores, setStores] = useState([]);
  const [stock, setStock] = useState([]);
  const [loading, setLoading] = useState(true);
  const [storeFilter, setStoreFilter] = useState("");
  const [itemFilter, setItemFilter] = useState("");

  useEffect(() => {
    Promise.all([
      fetch("/api/inventory/stores").then((r) => r.json()),
      fetch("/api/inventory/stock").then((r) => r.json()),
    ]).then(([s, st]) => {
      setStores(s);
      setStock(st);
      setLoading(false);
    }).catch(() => setLoading(false));
  }, []);

  const filtered = stock.filter((r) => {
    const storeOk = !storeFilter || r.StoreName === storeFilter;
    const itemOk = !itemFilter || r.item?.toLowerCase().includes(itemFilter.toLowerCase());
    return storeOk && itemOk;
  });

  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Stock Status</h1>
          <p className="page-subtitle">Current inventory levels across all stores</p>
        </div>
      </div>

      <div className="card" style={{ marginBottom: "1rem" }}>
        <div style={{ display: "flex", gap: "0.75rem", alignItems: "center", flexWrap: "wrap" }}>
          <select
            className="input"
            style={{ width: "auto" }}
            value={storeFilter}
            onChange={(e) => setStoreFilter(e.target.value)}
          >
            <option value="">All Stores</option>
            {stores.map((s) => <option key={s.id} value={s.StoreName}>{s.StoreName}</option>)}
          </select>
          <input
            className="input"
            style={{ flex: 1, maxWidth: "280px" }}
            placeholder="Search item…"
            value={itemFilter}
            onChange={(e) => setItemFilter(e.target.value)}
          />
          <span style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>
            {filtered.length} result{filtered.length !== 1 ? "s" : ""}
          </span>
        </div>
      </div>

      <div className="card">
        {loading ? (
          <div className="table-empty">Loading stock…</div>
        ) : !filtered.length ? (
          <div className="table-empty">No items match your filters.</div>
        ) : (
          <div className="table-wrapper">
            <table className="table">
              <thead>
                <tr>
                  <th>Store</th>
                  <th>Item</th>
                  <th>Batch No.</th>
                  <th>Pack</th>
                  <th className="text-right">W. Price</th>
                  <th className="text-right">R. Price</th>
                  <th className="text-right">Available</th>
                </tr>
              </thead>
              <tbody>
                {filtered.map((r, i) => {
                  const qty = Number(r.availableQnt || 0);
                  const isLow = qty <= 0;
                  return (
                    <tr key={i} style={isLow ? { background: "rgba(239,68,68,0.05)" } : {}}>
                      <td style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>{r.StoreName}</td>
                      <td style={{ fontWeight: 600 }}>{r.item}</td>
                      <td style={{ fontSize: "0.8rem" }}>{r.BatchNo || "—"}</td>
                      <td style={{ fontSize: "0.8rem" }}>{r.pack || "—"}</td>
                      <td className="text-right" style={{ fontSize: "0.85rem" }}>
                        {Number(r.WPrice || 0).toFixed(2)}
                      </td>
                      <td className="text-right" style={{ fontSize: "0.85rem" }}>
                        {Number(r.RPrice || 0).toFixed(2)}
                      </td>
                      <td className="text-right">
                        <span
                          style={{
                            fontWeight: 700,
                            color: isLow ? "var(--danger)" : qty < 10 ? "var(--warning)" : "var(--success)",
                          }}
                        >
                          {qty.toFixed(0)}
                        </span>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}
