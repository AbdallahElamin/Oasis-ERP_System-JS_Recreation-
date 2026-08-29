"use client";

import { useState, useEffect, useRef, useCallback } from "react";
import { useRouter } from "next/navigation";

const BARCODE_TYPES = ["CODE128", "EAN13", "QR"];

export default function BarcodeGeneratorPage() {
  const router = useRouter();
  const svgRef = useRef(null);
  const canvasRef = useRef(null);

  const [items, setItems] = useState([]);
  const [search, setSearch] = useState("");
  const [selected, setSelected] = useState(new Set()); // Set of SNo numbers
  const [activeItem, setActiveItem] = useState(null);
  const [barcodeType, setBarcodeType] = useState("CODE128");
  const [labelCount, setLabelCount] = useState(1);
  const [editing, setEditing] = useState(false);
  const [editValue, setEditValue] = useState("");
  const [saving, setSaving] = useState(false);
  const [toast, setToast] = useState(null);

  const showToast = (msg, type = "success") => {
    setToast({ msg, type });
    setTimeout(() => setToast(null), 2500);
  };

  useEffect(() => {
    fetch("/api/inventory/items")
      .then((r) => r.json())
      .then((data) => setItems(Array.isArray(data) ? data : []));
  }, []);

  const filtered = items.filter((it) => {
    if (!search) return true;
    const s = search.toLowerCase();
    return (
      (it.item || "").toLowerCase().includes(s) ||
      (it.GenericName || "").toLowerCase().includes(s) ||
      (it.CompanyName || "").toLowerCase().includes(s)
    );
  });

  // Render barcode whenever activeItem or type changes
  const renderBarcode = useCallback(async () => {
    if (!activeItem?.Barcode) return;

    if (barcodeType === "QR") {
      if (!canvasRef.current) return;
      try {
        const QRCode = (await import("qrcode")).default;
        const qrData = `${activeItem.item}|${activeItem.pack || ""}|${activeItem.RPrice}`;
        await QRCode.toCanvas(canvasRef.current, qrData, { width: 160, margin: 2, color: { dark: "#000", light: "#fff" } });
      } catch (err) {
        console.error("QR render error:", err);
      }
    } else {
      if (!svgRef.current) return;
      try {
        const JsBarcode = (await import("jsbarcode")).default;
        let value = activeItem.Barcode;
        // EAN13 needs exactly 12 numeric digits (13th is checksum)
        if (barcodeType === "EAN13") {
          const digits = value.replace(/\D/g, "").padEnd(12, "0").slice(0, 12);
          value = digits;
        }
        JsBarcode(svgRef.current, value, {
          format: barcodeType,
          width: 2,
          height: 60,
          displayValue: true,
          fontSize: 13,
          margin: 10,
          background: "#ffffff",
          lineColor: "#000000",
        });
      } catch (err) {
        console.error("Barcode render error:", err);
      }
    }
  }, [activeItem, barcodeType]);

  useEffect(() => {
    renderBarcode();
  }, [renderBarcode]);

  function toggleItem(sno) {
    const item = items.find((i) => i.SNo === sno);
    setSelected((prev) => {
      const next = new Set(prev);
      if (next.has(sno)) { next.delete(sno); }
      else { next.add(sno); }
      return next;
    });
    setActiveItem(item);
    setBarcodeType(item?.BarcodeType || "CODE128");
    setEditing(false);
  }

  async function regenerate() {
    if (!activeItem) return;
    setSaving(true);
    try {
      const res = await fetch(`/api/inventory/items/${activeItem.SNo}/barcode`, { method: "POST" });
      const data = await res.json();
      setActiveItem((p) => ({ ...p, Barcode: data.barcode, BarcodeType: data.barcodeType }));
      setItems((prev) => prev.map((i) => i.SNo === activeItem.SNo ? { ...i, Barcode: data.barcode, BarcodeType: data.barcodeType } : i));
      showToast("Barcode regenerated");
    } finally {
      setSaving(false);
    }
  }

  async function saveCustomBarcode() {
    if (!editValue.trim()) return;
    setSaving(true);
    try {
      const res = await fetch(`/api/inventory/items/${activeItem.SNo}/barcode`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ barcode: editValue.trim(), barcodeType }),
      });
      const data = await res.json();
      setActiveItem((p) => ({ ...p, Barcode: data.Barcode, BarcodeType: data.BarcodeType }));
      setItems((prev) => prev.map((i) => i.SNo === activeItem.SNo ? { ...i, Barcode: data.Barcode } : i));
      setEditing(false); setEditValue("");
      showToast("Barcode updated");
    } finally {
      setSaving(false);
    }
  }

  function copySVG() {
    if (!svgRef.current) return;
    const svgText = new XMLSerializer().serializeToString(svgRef.current);
    navigator.clipboard.writeText(svgText).then(() => showToast("SVG copied to clipboard"));
  }

  function openLabelSheet() {
    const snos = Array.from(selected).join(",");
    if (!snos) return;
    window.open(`/inventory/barcodes/print?snos=${snos}&count=${labelCount}`, "_blank");
  }

  const selectedItems = items.filter((i) => selected.has(i.SNo));

  return (
    <div>
      {toast && (
        <div className="alert" style={{
          position: "fixed", top: "1rem", right: "1rem", zIndex: 9999,
          background: toast.type === "danger" ? "rgba(239,68,68,0.15)" : "rgba(16,185,129,0.15)",
          color: toast.type === "danger" ? "var(--danger)" : "var(--success)",
          border: `1px solid ${toast.type === "danger" ? "rgba(239,68,68,0.3)" : "rgba(16,185,129,0.3)"}`,
          borderRadius: "0.5rem", padding: "0.6rem 1rem", fontSize: "0.82rem", minWidth: "200px",
        }}>
          ✓ {toast.msg}
        </div>
      )}

      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Barcode Generator</h1>
          <p className="page-subtitle">Generate and print product barcodes before items enter stock</p>
        </div>
      </div>

      <div style={{ display: "grid", gridTemplateColumns: "320px 1fr", gap: "1rem", marginBottom: "1rem" }}>
        {/* Left — Item Selector */}
        <div className="card" style={{ display: "flex", flexDirection: "column", gap: "0.75rem" }}>
          <div className="card-header">
            <h3 className="card-title">Select Items</h3>
            <span className="badge" style={{ background: "rgba(99,102,241,0.15)", color: "var(--accent-light)" }}>
              {selected.size} selected
            </span>
          </div>

          <input className="input" placeholder="Search items…" value={search}
            onChange={(e) => setSearch(e.target.value)} />

          <div style={{ display: "flex", gap: "0.4rem" }}>
            <button className="btn btn-secondary btn-sm" style={{ flex: 1 }}
              onClick={() => { filtered.forEach((i) => { setSelected((s) => { const n = new Set(s); n.add(i.SNo); return n; }); }); if (filtered.length) setActiveItem(filtered[filtered.length - 1]); }}>
              Select All
            </button>
            <button className="btn btn-secondary btn-sm" style={{ flex: 1 }}
              onClick={() => { setSelected(new Set()); setActiveItem(null); }}>
              Clear
            </button>
          </div>

          <div style={{ overflowY: "auto", maxHeight: "480px", display: "flex", flexDirection: "column", gap: "2px" }}>
            {filtered.map((item) => (
              <label key={item.SNo} style={{
                display: "flex", alignItems: "flex-start", gap: "0.6rem", padding: "0.5rem 0.4rem",
                borderRadius: "0.375rem", cursor: "pointer",
                background: activeItem?.SNo === item.SNo ? "rgba(99,102,241,0.12)" : "transparent",
                transition: "background 0.1s",
              }}>
                <input type="checkbox" checked={selected.has(item.SNo)}
                  onChange={() => toggleItem(item.SNo)}
                  style={{ marginTop: "2px", accentColor: "var(--accent-primary)" }} />
                <div style={{ flex: 1, minWidth: 0 }}>
                  <div style={{ fontWeight: 500, fontSize: "0.82rem", color: "var(--text-primary)", overflow: "hidden", textOverflow: "ellipsis", whiteSpace: "nowrap" }}>
                    {item.item}
                  </div>
                  <div style={{ fontSize: "0.7rem", color: "var(--text-muted)" }}>
                    {item.pack || "—"} · {item.CompanyName || "—"}
                  </div>
                  {item.Barcode && (
                    <div style={{ fontSize: "0.68rem", color: "var(--accent-light)", fontFamily: "monospace" }}>
                      {item.Barcode}
                    </div>
                  )}
                </div>
              </label>
            ))}
            {!filtered.length && (
              <div className="table-empty">No items match your search.</div>
            )}
          </div>
        </div>

        {/* Right — Preview */}
        <div className="card">
          {!activeItem ? (
            <div className="table-empty" style={{ padding: "4rem" }}>
              Select an item from the list to preview its barcode.
            </div>
          ) : (
            <div style={{ display: "flex", flexDirection: "column", gap: "1rem" }}>
              <div className="card-header">
                <h3 className="card-title">Preview — {activeItem.item}</h3>
              </div>

              {/* Barcode preview */}
              <div style={{
                display: "flex", flexDirection: "column", alignItems: "center",
                background: "#ffffff", padding: "1.5rem 1rem", borderRadius: "0.5rem",
                border: "1px solid var(--border-subtle)", minHeight: "160px", justifyContent: "center",
              }}>
                <svg ref={svgRef} style={{ display: barcodeType !== "QR" ? "block" : "none" }} />
                <canvas ref={canvasRef} style={{ display: barcodeType === "QR" ? "block" : "none" }} />
                <div style={{ marginTop: "0.5rem", textAlign: "center", color: "#333", fontSize: "0.78rem" }}>
                  <div style={{ fontWeight: 600 }}>{activeItem.item}</div>
                  <div>{activeItem.pack || "—"} · R.Price: {Number(activeItem.RPrice).toFixed(2)}</div>
                </div>
              </div>

              {/* Type selector */}
              <div style={{ display: "flex", gap: "0.5rem", alignItems: "center" }}>
                <label className="form-label" style={{ margin: 0 }}>Type:</label>
                {BARCODE_TYPES.map((t) => (
                  <button key={t} className={`btn btn-sm ${barcodeType === t ? "btn-primary" : "btn-secondary"}`}
                    onClick={() => setBarcodeType(t)}>
                    {t}
                  </button>
                ))}
              </div>

              {/* Custom value edit */}
              {editing ? (
                <div style={{ display: "flex", gap: "0.5rem" }}>
                  <input className="input" style={{ flex: 1 }} placeholder="Custom barcode value"
                    value={editValue} onChange={(e) => setEditValue(e.target.value)}
                    onKeyDown={(e) => e.key === "Enter" && saveCustomBarcode()} autoFocus />
                  <button className="btn btn-primary" onClick={saveCustomBarcode} disabled={saving}>Save</button>
                  <button className="btn btn-secondary" onClick={() => { setEditing(false); setEditValue(""); }}>Cancel</button>
                </div>
              ) : (
                <div style={{ display: "flex", gap: "0.5rem", flexWrap: "wrap" }}>
                  <button className="btn btn-secondary" onClick={regenerate} disabled={saving}>
                    ↺ Regenerate
                  </button>
                  <button className="btn btn-secondary" onClick={() => { setEditing(true); setEditValue(activeItem.Barcode || ""); }}>
                    ✎ Custom Value
                  </button>
                  <button className="btn btn-secondary" onClick={copySVG} disabled={barcodeType === "QR"}>
                    📋 Copy SVG
                  </button>
                </div>
              )}
            </div>
          )}
        </div>
      </div>

      {/* Bottom bar — Batch print */}
      <div className="card">
        <div style={{ display: "flex", alignItems: "center", gap: "1.5rem", flexWrap: "wrap" }}>
          <div style={{ flex: 1, fontSize: "0.85rem", color: "var(--text-secondary)" }}>
            <strong style={{ color: "var(--text-primary)" }}>{selected.size}</strong> item(s) selected for batch printing
            {selectedItems.length > 0 && (
              <span style={{ color: "var(--text-muted)", fontSize: "0.75rem" }}>
                {" "}({selectedItems.map((i) => i.item).slice(0, 3).join(", ")}{selectedItems.length > 3 ? "…" : ""})
              </span>
            )}
          </div>

          <div style={{ display: "flex", alignItems: "center", gap: "0.5rem" }}>
            <span style={{ fontSize: "0.82rem", color: "var(--text-muted)" }}>Labels per item:</span>
            {[1, 2, 3, 4, 5].map((n) => (
              <button key={n} className={`btn btn-sm ${labelCount === n ? "btn-primary" : "btn-secondary"}`}
                onClick={() => setLabelCount(n)}>
                {n}
              </button>
            ))}
          </div>

          <button className="btn btn-primary" onClick={openLabelSheet} disabled={!selected.size}>
            🖨 Print Label Sheet
          </button>
        </div>
      </div>
    </div>
  );
}
