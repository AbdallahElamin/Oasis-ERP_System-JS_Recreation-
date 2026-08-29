"use client";

import { Suspense, useState, useEffect, useRef } from "react";
import { useSearchParams } from "next/navigation";

function BarcodePrintContent() {
  const searchParams = useSearchParams();
  const snos = (searchParams.get("snos") || "").split(",").map(Number).filter(Boolean);
  const count = Math.max(1, Math.min(10, parseInt(searchParams.get("count") || "1", 10)));

  const [items, setItems] = useState([]);
  const [loading, setLoading] = useState(true);
  const renderedRef = useRef(false);

  useEffect(() => {
    if (!snos.length) { setLoading(false); return; }
    fetch(`/api/inventory/items/barcodes?snos=${snos.join(",")}`)
      .then((r) => r.json())
      .then((data) => {
        setItems(Array.isArray(data) ? data : []);
        setLoading(false);
      });
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (loading || renderedRef.current) return;
    renderedRef.current = true;

    (async () => {
      const JsBarcode = (await import("jsbarcode")).default;
      const QRCode = (await import("qrcode")).default;

      const svgEls = document.querySelectorAll("[data-barcode-svg]");
      for (const el of svgEls) {
        const value = el.getAttribute("data-barcode-value");
        const type = el.getAttribute("data-barcode-type");
        if (!value) continue;
        try {
          if (type === "QR") {
            const canvas = document.createElement("canvas");
            await QRCode.toCanvas(canvas, value, { width: 120, margin: 1 });
            el.replaceWith(canvas);
          } else {
            let v = value;
            if (type === "EAN13") v = value.replace(/\D/g, "").padEnd(12, "0").slice(0, 12);
            JsBarcode(el, v, { format: type, width: 1.5, height: 45, displayValue: false, margin: 4 });
          }
        } catch (e) {
          console.warn("Barcode render error:", e);
        }
      }

      setTimeout(() => window.print(), 700);
    })();
  }, [loading]);

  if (loading) return <div style={{ padding: "2rem", textAlign: "center" }}>Preparing label sheet…</div>;
  if (!items.length) return <div style={{ padding: "2rem" }}>No items found for the given IDs.</div>;

  const labels = [];
  for (const item of items) {
    for (let i = 0; i < count; i++) {
      labels.push(item);
    }
  }

  return (
    <div>
      <div className="no-print" style={{ padding: "1rem", display: "flex", gap: "1rem", alignItems: "center", borderBottom: "1px solid #ddd", marginBottom: "1rem" }}>
        <button onClick={() => window.print()}
          style={{ padding: "0.4rem 1rem", background: "#6366f1", color: "white", border: "none", borderRadius: "0.375rem", cursor: "pointer" }}>
          🖨 Print Label Sheet
        </button>
        <span style={{ fontSize: "0.82rem", color: "#555" }}>
          {items.length} item(s) × {count} label(s) = {labels.length} total labels
        </span>
      </div>

      <div className="label-grid" style={{ padding: "4px" }}>
        {labels.map((item, i) => (
          <div key={i} className="label-cell">
            <svg
              data-barcode-svg=""
              data-barcode-value={item.Barcode || `OAS-${String(item.SNo).padStart(4, "0")}`}
              data-barcode-type={item.BarcodeType || "CODE128"}
            />
            <div style={{ fontFamily: "monospace", fontSize: "8px", marginTop: "2px", fontWeight: "bold" }}>
              {item.Barcode || `OAS-${String(item.SNo).padStart(4, "0")}`}
            </div>
            <div style={{ fontSize: "8px", fontWeight: "600", marginTop: "2px" }}>
              {(item.item || "").slice(0, 28)}
            </div>
            <div style={{ fontSize: "7px", color: "#555" }}>
              {item.pack || ""}{item.pack && " · "}R.Price: {Number(item.RPrice || 0).toFixed(2)}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default function BarcodePrintPage() {
  return (
    <Suspense fallback={<div style={{ padding: "2rem", textAlign: "center" }}>Loading…</div>}>
      <BarcodePrintContent />
    </Suspense>
  );
}
