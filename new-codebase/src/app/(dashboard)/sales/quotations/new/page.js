"use client";

import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";

function fmt(n) { return Number(n || 0).toLocaleString("en-US", { minimumFractionDigits: 2 }); }

export default function NewQuotationPage() {
  const router = useRouter();

  const [clients, setClients] = useState([]);
  const [stores, setStores] = useState([]);
  const [stockItems, setStockItems] = useState([]);

  const [custId, setCustId] = useState("");
  const [custName, setCustName] = useState("");
  const [items, setItems] = useState([]);
  const [discPerc, setDiscPerc] = useState(0);
  const [vatPerc, setVatPerc] = useState(0);

  const [lineStore, setLineStore] = useState("");
  const [lineItem, setLineItem] = useState("");
  const [lineQnt, setLineQnt] = useState("");

  const [saving, setSaving] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    Promise.all([
      fetch("/api/clients?limit=500").then((r) => r.json()),
      fetch("/api/inventory/stores").then((r) => r.json()),
      fetch("/api/inventory/stock").then((r) => r.json()),
    ]).then(([cl, st, sk]) => { setClients(cl.clients || cl); setStores(st); setStockItems(sk); });
  }, []);

  const storeStock = lineStore ? stockItems.filter((s) => s.StoreName === lineStore && Number(s.availableQnt) > 0) : [];

  function addItem() {
    if (!lineItem) return;
    const match = storeStock.find((s) => s.item === lineItem);
    if (!match) return;
    const qnt = parseFloat(lineQnt) || 1;
    setItems((prev) => [...prev, {
      storeName: lineStore, item: match.item, batchNo: match.BatchNo, pack: match.pack,
      wPrice: match.WPrice, rPrice: match.RPrice, qnt, total: qnt * match.RPrice,
    }]);
    setLineItem(""); setLineQnt("");
  }

  const subtotal = items.reduce((s, i) => s + i.total, 0);
  const discAmt = subtotal * (discPerc / 100);
  const vatAmt = (subtotal - discAmt) * (vatPerc / 100);
  const netAmount = subtotal - discAmt + vatAmt;

  async function handleSave() {
    if (!custId || !items.length) { setError("Select a client and add at least one item"); return; }
    setSaving(true); setError(null);
    try {
      const res = await fetch("/api/sales/quotations", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ custId, custName, items, discPerc, vatPerc, netAmount }),
      });
      const data = await res.json();
      if (!res.ok) throw new Error(data.error || "Failed");
      router.push("/sales/quotations");
    } catch (err) {
      setError(err.message);
    } finally {
      setSaving(false);
    }
  }

  return (
    <div>
      <div className="page-title-bar">
        <div><h1 className="page-title">New Quotation</h1></div>
        <div style={{ display: "flex", gap: "0.5rem" }}>
          <button className="btn btn-secondary" onClick={() => router.back()}>Cancel</button>
          <button className="btn btn-primary" onClick={handleSave} disabled={saving}>
            {saving ? "Saving…" : "Save Quotation"}
          </button>
        </div>
      </div>

      {error && <div className="alert alert-danger" style={{ marginBottom: "1rem" }}>{error}</div>}

      <div className="card" style={{ marginBottom: "1rem" }}>
        <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr 1fr", gap: "0.75rem" }}>
          <div className="form-group">
            <label className="form-label">Client</label>
            <select className="input" value={custId} onChange={(e) => {
              const cl = clients.find((c) => String(c.SNo) === e.target.value);
              setCustId(e.target.value); setCustName(cl?.name || "");
            }}>
              <option value="">Select client…</option>
              {clients.map((c) => <option key={c.SNo} value={c.SNo}>{c.name}</option>)}
            </select>
          </div>
          <div className="form-group">
            <label className="form-label">Discount %</label>
            <input type="number" className="input" min="0" max="100" value={discPerc} onChange={(e) => setDiscPerc(parseFloat(e.target.value) || 0)} />
          </div>
          <div className="form-group">
            <label className="form-label">VAT %</label>
            <input type="number" className="input" min="0" max="100" value={vatPerc} onChange={(e) => setVatPerc(parseFloat(e.target.value) || 0)} />
          </div>
        </div>

        <div style={{ display: "grid", gridTemplateColumns: "1fr 2fr auto", gap: "0.75rem", alignItems: "end", marginTop: "0.5rem" }}>
          <div className="form-group" style={{ marginBottom: 0 }}>
            <label className="form-label">Store</label>
            <select className="input" value={lineStore} onChange={(e) => { setLineStore(e.target.value); setLineItem(""); }}>
              <option value="">Store…</option>
              {stores.map((s) => <option key={s.id} value={s.StoreName}>{s.StoreName}</option>)}
            </select>
          </div>
          <div className="form-group" style={{ marginBottom: 0 }}>
            <label className="form-label">Item</label>
            <select className="input" value={lineItem} onChange={(e) => setLineItem(e.target.value)} disabled={!lineStore}>
              <option value="">Item…</option>
              {storeStock.map((s) => <option key={s.item} value={s.item}>{s.item} (R.P: {s.RPrice})</option>)}
            </select>
          </div>
          <div className="form-group" style={{ marginBottom: 0 }}>
            <label className="form-label">Qty</label>
            <input type="number" className="input" min="1" value={lineQnt} onChange={(e) => setLineQnt(e.target.value)} style={{ width: "90px" }} />
          </div>
        </div>
        <button className="btn btn-secondary" style={{ marginTop: "0.5rem" }} onClick={addItem}>+ Add Item</button>
      </div>

      <div className="card">
        {!items.length ? <div className="table-empty">No items added yet.</div> : (
          <div className="table-wrapper">
            <table className="table">
              <thead><tr><th>Item</th><th>Store</th><th>R.Price</th><th>Qty</th><th className="text-right">Total</th><th></th></tr></thead>
              <tbody>
                {items.map((it, i) => (
                  <tr key={i}>
                    <td style={{ fontWeight: 600 }}>{it.item}</td>
                    <td style={{ fontSize: "0.8rem" }}>{it.storeName}</td>
                    <td>{fmt(it.rPrice)}</td>
                    <td>{it.qnt}</td>
                    <td className="text-right">{fmt(it.total)}</td>
                    <td><button onClick={() => setItems((p) => p.filter((_, j) => j !== i))} style={{ background: "none", border: "none", color: "var(--danger)", cursor: "pointer" }}>×</button></td>
                  </tr>
                ))}
              </tbody>
              <tfoot>
                <tr><td colSpan={4} className="text-right">Subtotal</td><td className="text-right">{fmt(subtotal)}</td><td></td></tr>
                <tr><td colSpan={4} className="text-right">Discount ({discPerc}%)</td><td className="text-right text-danger">- {fmt(discAmt)}</td><td></td></tr>
                <tr><td colSpan={4} className="text-right">VAT ({vatPerc}%)</td><td className="text-right">{fmt(vatAmt)}</td><td></td></tr>
                <tr style={{ fontWeight: 700 }}><td colSpan={4} className="text-right">Net Total</td><td className="text-right text-success">{fmt(netAmount)}</td><td></td></tr>
              </tfoot>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}
