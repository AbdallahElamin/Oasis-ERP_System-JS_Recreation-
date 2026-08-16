"use client";

import { useState, useEffect, useCallback } from "react";
import { spellNumber, formatCurrency } from "@/lib/utils";

export default function NewInvoicePage() {
  const [stores, setStores] = useState([]);
  const [allItems, setAllItems] = useState([]);
  const [stockItems, setStockItems] = useState([]);
  const [batches, setBatches] = useState([]);
  const [invoiceRows, setInvoiceRows] = useState([]);

  // Client search
  const [clientId, setClientId] = useState("");
  const [clientName, setClientName] = useState("");
  const [clientSearch, setClientSearch] = useState("");
  const [clientResults, setClientResults] = useState([]);
  const [showClientSearch, setShowClientSearch] = useState(false);

  // Current item selection
  const [sel, setSel] = useState({
    storeName: "", item: "", batchNo: "", pack: "",
    wPrice: "0", rPrice: "0", availableQnt: 0, qnt: 1, isBonus: false,
  });

  // Totals
  const [discPerc, setDiscPerc] = useState(0);
  const [vatPerc, setVatPerc] = useState(0);

  const [saving, setSaving] = useState(false);
  const [error, setError] = useState("");
  const [savedInvoice, setSavedInvoice] = useState(null);

  useEffect(() => {
    fetch("/api/inventory/stores").then((r) => r.json()).then(setStores).catch(() => {});
    fetch("/api/inventory/items").then((r) => r.json()).then(setAllItems).catch(() => {});
  }, []);

  // Client search with debounce
  useEffect(() => {
    if (!clientSearch.trim()) { setClientResults([]); return; }
    const t = setTimeout(async () => {
      const res = await fetch(`/api/clients?q=${encodeURIComponent(clientSearch)}&limit=8`);
      const data = await res.json();
      setClientResults(data.clients ?? []);
    }, 300);
    return () => clearTimeout(t);
  }, [clientSearch]);

  // When client ID entered manually (mirror of original txtClientID_KeyUp)
  async function lookupClientById(id) {
    if (!id) { setClientName(""); return; }
    const res = await fetch(`/api/clients/${id}`);
    if (res.ok) {
      const data = await res.json();
      setClientName(data.name);
    } else {
      setClientName("");
    }
  }

  function selectClient(c) {
    setClientId(String(c.id));
    setClientName(c.name);
    setClientSearch("");
    setClientResults([]);
    setShowClientSearch(false);
  }

  // When store changes: filter items
  async function onStoreChange(e) {
    const store = e.target.value;
    setSel((prev) => ({ ...prev, storeName: store, item: "", batchNo: "", pack: "", wPrice: "0", rPrice: "0", availableQnt: 0 }));
    setBatches([]);
    if (!store) { setStockItems([]); return; }
    const res = await fetch(`/api/inventory/stock?store=${encodeURIComponent(store)}`);
    const data = await res.json();
    setStockItems(data);
  }

  // When item changes: fill batches
  function onItemChange(e) {
    const item = e.target.value;
    const relBatches = stockItems.filter((s) => s.item === item);
    setSel((prev) => ({ ...prev, item, batchNo: "", pack: "", wPrice: "0", rPrice: "0", availableQnt: 0 }));
    setBatches(relBatches);
  }

  // When batch changes: fill prices and available quantity
  function onBatchChange(e) {
    const batchNo = e.target.value;
    const found = batches.find((b) => b.batchNo === batchNo);
    if (found) {
      setSel((prev) => ({
        ...prev, batchNo,
        pack: found.pack || "",
        wPrice: String(found.wPrice),
        rPrice: String(found.rPrice),
        availableQnt: found.availableQnt,
      }));
    }
  }

  // Compute row total
  function rowTotal(row) {
    return row.isBonus ? 0 : Number(row.wPrice) * Number(row.qnt);
  }

  // Compute invoice totals
  const subtotal = invoiceRows.reduce((sum, r) => sum + rowTotal(r), 0);
  const discAmt = subtotal * discPerc / 100;
  const vatAmt = (subtotal - discAmt) * vatPerc / 100;
  const netAmount = subtotal - discAmt + vatAmt;
  const amountInWords = spellNumber(netAmount);

  function handleAddRow() {
    if (!sel.item || !sel.batchNo || Number(sel.qnt) <= 0) {
      setError("Please select item, batch, and a valid quantity.");
      return;
    }
    setError("");
    setInvoiceRows((prev) => [...prev, { ...sel, id: Date.now() }]);
    setSel((prev) => ({
      ...prev, item: "", batchNo: "", pack: "", wPrice: "0", rPrice: "0", availableQnt: 0, qnt: 1, isBonus: false,
    }));
    setBatches([]);
  }

  function handleRemoveRow(id) {
    setInvoiceRows((prev) => prev.filter((r) => r.id !== id));
  }

  function handleClear() {
    setClientId(""); setClientName(""); setClientSearch("");
    setInvoiceRows([]);
    setSel({ storeName: "", item: "", batchNo: "", pack: "", wPrice: "0", rPrice: "0", availableQnt: 0, qnt: 1, isBonus: false });
    setDiscPerc(0); setVatPerc(0);
    setError("");
  }

  async function handleSave() {
    if (!clientName || invoiceRows.length === 0) {
      setError("Please select a client and add at least one item.");
      return;
    }
    setSaving(true); setError("");
    const res = await fetch("/api/sales/invoices", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        custId: clientId, custName: clientName,
        items: invoiceRows.map((r) => ({
          storeName: r.storeName, item: r.item, batchNo: r.batchNo,
          pack: r.pack, wPrice: r.wPrice, rPrice: r.rPrice, qnt: r.qnt,
          total: rowTotal(r), description: r.isBonus ? "Bonus" : "Sales",
        })),
        discPerc, vatPerc, netAmount, amountInWords,
      }),
    });
    setSaving(false);
    if (res.ok) {
      const data = await res.json();
      setSavedInvoice({ invNo: data.invNo, custName: clientName, date: new Date().toLocaleDateString(), rows: [...invoiceRows], discPerc, vatPerc, netAmount, amountInWords, subtotal, discAmt, vatAmt });
      handleClear();
    } else {
      const data = await res.json();
      setError(data.error || "Failed to save invoice.");
    }
  }

  // ── Print ──────────────────────────────────────────────────────────
  if (savedInvoice) {
    return <InvoicePrint invoice={savedInvoice} onBack={() => setSavedInvoice(null)} />;
  }

  // ── Form ───────────────────────────────────────────────────────────
  return (
    <div>
      <div className="page-title-bar no-print">
        <div>
          <h1 className="page-title">New Invoice</h1>
          <p className="page-subtitle">Create a sales invoice</p>
        </div>
        <div style={{ display: "flex", gap: "0.5rem" }}>
          <button className="btn btn-secondary" onClick={handleClear}>Clear</button>
        </div>
      </div>

      {error && (
        <div style={{ padding: "0.75rem 1rem", marginBottom: "1rem", borderRadius: "0.5rem",
          background: "rgba(239,68,68,0.1)", border: "1px solid rgba(239,68,68,0.25)", color: "var(--danger)" }}>
          {error}
        </div>
      )}

      {/* Client selector */}
      <div className="card" style={{ marginBottom: "1.25rem" }}>
        <div className="card-header"><h3 className="card-title">Customer</h3></div>
        <div style={{ display: "flex", gap: "0.75rem", alignItems: "flex-end" }}>
          <div className="form-group" style={{ width: "120px" }}>
            <label className="form-label">Customer ID</label>
            <input type="number" className="form-control" value={clientId}
              onChange={(e) => { setClientId(e.target.value); setClientName(""); }}
              onBlur={(e) => lookupClientById(e.target.value)}
              placeholder="ID" />
          </div>
          <div className="form-group" style={{ flex: 1 }}>
            <label className="form-label">Customer Name</label>
            <input type="text" className="form-control" value={clientName} readOnly
              placeholder="Auto-filled from ID" />
          </div>
          <div style={{ position: "relative" }}>
            <button className="btn btn-secondary" onClick={() => setShowClientSearch((v) => !v)}>
              🔍 Search
            </button>
            {showClientSearch && (
              <div style={{ position: "absolute", top: "100%", right: 0, zIndex: 100, width: "320px",
                background: "var(--bg-secondary)", border: "1px solid var(--border-default)", borderRadius: "0.5rem",
                padding: "0.75rem", marginTop: "0.25rem" }}>
                <input type="text" className="form-control" placeholder="Type name or mobile…"
                  value={clientSearch} onChange={(e) => setClientSearch(e.target.value)} autoFocus />
                <div style={{ marginTop: "0.5rem" }}>
                  {clientResults.map((c) => (
                    <div key={c.id} onClick={() => selectClient(c)}
                      style={{ padding: "0.5rem", cursor: "pointer", borderRadius: "0.25rem",
                        color: "var(--text-primary)", fontSize: "0.875rem" }}
                      onMouseEnter={(e) => (e.currentTarget.style.background = "var(--bg-elevated)")}
                      onMouseLeave={(e) => (e.currentTarget.style.background = "transparent")}>
                      <strong>{c.id}</strong> — {c.name} {c.mobile ? `(${c.mobile})` : ""}
                    </div>
                  ))}
                  {clientSearch && clientResults.length === 0 && (
                    <p style={{ color: "var(--text-muted)", fontSize: "0.82rem" }}>No results.</p>
                  )}
                </div>
              </div>
            )}
          </div>
        </div>
      </div>

      {/* Item Selector */}
      <div className="card" style={{ marginBottom: "1.25rem" }}>
        <div className="card-header"><h3 className="card-title">Add Item</h3></div>
        <div className="grid-3" style={{ marginBottom: "0.75rem" }}>
          <div className="form-group">
            <label className="form-label">Store *</label>
            <select className="form-control" value={sel.storeName} onChange={onStoreChange}>
              <option value="">Select store…</option>
              {stores.map((s) => <option key={s.id} value={s.storeName}>{s.storeName}</option>)}
            </select>
          </div>
          <div className="form-group">
            <label className="form-label">Item *</label>
            <select className="form-control" value={sel.item} onChange={onItemChange} disabled={!sel.storeName}>
              <option value="">Select item…</option>
              {[...new Set(stockItems.map((s) => s.item))].map((item) => <option key={item} value={item}>{item}</option>)}
            </select>
          </div>
          <div className="form-group">
            <label className="form-label">Batch No. *</label>
            <select className="form-control" value={sel.batchNo} onChange={onBatchChange} disabled={!sel.item}>
              <option value="">Select batch…</option>
              {batches.map((b) => <option key={b.batchNo} value={b.batchNo}>{b.batchNo}</option>)}
            </select>
          </div>
          <div className="form-group">
            <label className="form-label">Pack</label>
            <input className="form-control" value={sel.pack} readOnly />
          </div>
          <div className="form-group">
            <label className="form-label">W.Price</label>
            <input className="form-control" value={sel.wPrice} readOnly />
          </div>
          <div className="form-group">
            <label className="form-label">R.Price</label>
            <input className="form-control" value={sel.rPrice} readOnly />
          </div>
          <div className="form-group">
            <label className="form-label">Available Qty</label>
            <input className="form-control" value={sel.availableQnt} readOnly />
          </div>
          <div className="form-group">
            <label className="form-label">Quantity *</label>
            <input type="number" min="1" className="form-control" value={sel.qnt}
              onChange={(e) => setSel((prev) => ({ ...prev, qnt: e.target.value }))} />
          </div>
          <div className="form-group">
            <label className="form-label" style={{ display: "flex", alignItems: "center", gap: "0.5rem" }}>
              <input type="checkbox" checked={sel.isBonus}
                onChange={(e) => setSel((prev) => ({ ...prev, isBonus: e.target.checked }))} />
              Bonus (free item)
            </label>
          </div>
        </div>
        <button className="btn btn-secondary" onClick={handleAddRow}>+ Add to Invoice</button>
      </div>

      {/* Invoice Table */}
      {invoiceRows.length > 0 && (
        <div className="card" style={{ marginBottom: "1.25rem" }}>
          <div className="card-header"><h3 className="card-title">Invoice Items ({invoiceRows.length})</h3></div>
          <div className="data-table-wrapper">
            <table className="data-table">
              <thead>
                <tr>
                  <th>Store</th><th>Item</th><th>Batch</th><th>Pack</th>
                  <th>W.Price</th><th>R.Price</th><th>Qty</th><th>Total</th><th>Note</th><th></th>
                </tr>
              </thead>
              <tbody>
                {invoiceRows.map((r) => (
                  <tr key={r.id}>
                    <td>{r.storeName}</td><td>{r.item}</td><td>{r.batchNo}</td><td>{r.pack || "—"}</td>
                    <td>{r.wPrice}</td><td>{r.rPrice}</td><td>{r.qnt}</td>
                    <td style={{ fontWeight: 600 }}>{formatCurrency(rowTotal(r))}</td>
                    <td>{r.isBonus ? <span className="badge badge-warning">Bonus</span> : "Sales"}</td>
                    <td><button className="btn btn-danger btn-sm" onClick={() => handleRemoveRow(r.id)}>✕</button></td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          {/* Totals */}
          <div style={{ marginTop: "1rem", display: "flex", justifyContent: "flex-end" }}>
            <div style={{ width: "320px", display: "flex", flexDirection: "column", gap: "0.5rem" }}>
              <div style={{ display: "flex", justifyContent: "space-between" }}>
                <span style={{ color: "var(--text-muted)" }}>Subtotal</span>
                <span>{formatCurrency(subtotal)} SDG</span>
              </div>
              <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                <label style={{ color: "var(--text-muted)", display: "flex", alignItems: "center", gap: "0.5rem" }}>
                  Discount
                  <input type="number" min="0" max="100" className="form-control"
                    style={{ width: "60px" }} value={discPerc} onChange={(e) => setDiscPerc(Number(e.target.value))} />
                  %
                </label>
                <span style={{ color: "var(--danger)" }}>- {formatCurrency(discAmt)} SDG</span>
              </div>
              <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                <label style={{ color: "var(--text-muted)", display: "flex", alignItems: "center", gap: "0.5rem" }}>
                  VAT
                  <input type="number" min="0" max="100" className="form-control"
                    style={{ width: "60px" }} value={vatPerc} onChange={(e) => setVatPerc(Number(e.target.value))} />
                  %
                </label>
                <span style={{ color: "var(--success)" }}>+ {formatCurrency(vatAmt)} SDG</span>
              </div>
              <div style={{ display: "flex", justifyContent: "space-between", fontWeight: 700, fontSize: "1.1rem", borderTop: "1px solid var(--border-subtle)", paddingTop: "0.5rem" }}>
                <span>Net Amount</span>
                <span style={{ color: "var(--accent-light)" }}>{formatCurrency(netAmount)} SDG</span>
              </div>
              <div style={{ fontSize: "0.78rem", color: "var(--text-muted)", fontStyle: "italic" }}>
                {amountInWords}
              </div>
            </div>
          </div>

          <div style={{ marginTop: "1rem", display: "flex", gap: "0.5rem" }}>
            <button className="btn btn-primary btn-lg" onClick={handleSave} disabled={saving}>
              {saving ? "Saving…" : "💾 Save & Print Invoice"}
            </button>
            <button className="btn btn-secondary" onClick={handleClear}>Clear</button>
          </div>
        </div>
      )}
    </div>
  );
}

// ─── Print Layout ────────────────────────────────────────────────────────────
function InvoicePrint({ invoice, onBack }) {
  return (
    <div>
      <div className="no-print" style={{ marginBottom: "1rem", display: "flex", gap: "0.5rem" }}>
        <button className="btn btn-primary" onClick={() => window.print()}>🖨 Print Invoice</button>
        <button className="btn btn-secondary" onClick={onBack}>← New Invoice</button>
      </div>

      <div className="print-document" style={{
        background: "white", color: "#111", padding: "2rem", borderRadius: "0.5rem",
        border: "1px solid var(--border-subtle)", fontFamily: "sans-serif",
      }}>
        {/* Header */}
        <div style={{ display: "flex", justifyContent: "space-between", marginBottom: "1.5rem", borderBottom: "2px solid #111", paddingBottom: "1rem" }}>
          <div>
            <h2 style={{ fontSize: "1.4rem", fontWeight: 800, color: "#333" }}>Oasis ERP</h2>
            <p style={{ fontSize: "0.8rem", color: "#666" }}>Sales Invoice</p>
          </div>
          <div style={{ textAlign: "right" }}>
            <p style={{ fontWeight: 700, fontSize: "1rem" }}>Invoice # {invoice.invNo}</p>
            <p style={{ fontSize: "0.82rem", color: "#666" }}>Date: {invoice.date}</p>
          </div>
        </div>

        {/* Customer */}
        <div style={{ marginBottom: "1.5rem" }}>
          <p style={{ fontWeight: 600 }}>Customer: <span style={{ fontWeight: 400 }}>{invoice.custName}</span></p>
        </div>

        {/* Items table */}
        <table style={{ width: "100%", borderCollapse: "collapse", marginBottom: "1.5rem", fontSize: "0.85rem" }}>
          <thead>
            <tr style={{ background: "#f5f5f5" }}>
              {["#", "Item", "Batch", "Pack", "W.Price", "Qty", "Total", "Note"].map((h) => (
                <th key={h} style={{ border: "1px solid #ddd", padding: "0.5rem", textAlign: "left", fontWeight: 600 }}>{h}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            {invoice.rows.map((r, i) => (
              <tr key={r.id}>
                <td style={{ border: "1px solid #ddd", padding: "0.5rem" }}>{i + 1}</td>
                <td style={{ border: "1px solid #ddd", padding: "0.5rem", fontWeight: 500 }}>{r.item}</td>
                <td style={{ border: "1px solid #ddd", padding: "0.5rem" }}>{r.batchNo}</td>
                <td style={{ border: "1px solid #ddd", padding: "0.5rem" }}>{r.pack || "—"}</td>
                <td style={{ border: "1px solid #ddd", padding: "0.5rem" }}>{r.wPrice}</td>
                <td style={{ border: "1px solid #ddd", padding: "0.5rem" }}>{r.qnt}</td>
                <td style={{ border: "1px solid #ddd", padding: "0.5rem" }}>{formatCurrency(r.isBonus ? 0 : Number(r.wPrice) * Number(r.qnt))}</td>
                <td style={{ border: "1px solid #ddd", padding: "0.5rem" }}>{r.isBonus ? "Bonus" : "Sales"}</td>
              </tr>
            ))}
          </tbody>
        </table>

        {/* Totals */}
        <div style={{ display: "flex", justifyContent: "flex-end" }}>
          <div style={{ width: "280px" }}>
            {[
              ["Subtotal", formatCurrency(invoice.subtotal) + " SDG"],
              [`Discount (${invoice.discPerc}%)`, "- " + formatCurrency(invoice.discAmt) + " SDG"],
              [`VAT (${invoice.vatPerc}%)`, "+ " + formatCurrency(invoice.vatAmt) + " SDG"],
            ].map(([label, val]) => (
              <div key={label} style={{ display: "flex", justifyContent: "space-between", padding: "0.25rem 0", fontSize: "0.85rem" }}>
                <span style={{ color: "#666" }}>{label}</span><span>{val}</span>
              </div>
            ))}
            <div style={{ display: "flex", justifyContent: "space-between", borderTop: "2px solid #111", paddingTop: "0.5rem", fontWeight: 700, fontSize: "1rem", marginTop: "0.5rem" }}>
              <span>Net Amount</span><span>{formatCurrency(invoice.netAmount)} SDG</span>
            </div>
            <p style={{ fontSize: "0.78rem", color: "#666", fontStyle: "italic", marginTop: "0.35rem" }}>
              {invoice.amountInWords}
            </p>
          </div>
        </div>

        {/* Signature lines */}
        <div style={{ display: "flex", justifyContent: "space-between", marginTop: "3rem" }}>
          {["Authorized By", "Received By", "Accountant"].map((label) => (
            <div key={label} style={{ textAlign: "center", width: "30%" }}>
              <div style={{ borderTop: "1px solid #555", paddingTop: "0.5rem", fontSize: "0.8rem", color: "#555" }}>{label}</div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
