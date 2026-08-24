"use client";

/**
 * DateRangePicker — controlled date range inputs (From / To)
 *
 * Props:
 *   from (string YYYY-MM-DD) — controlled
 *   to   (string YYYY-MM-DD) — controlled
 *   onChange({ from, to })   — callback
 *   label (string)           — optional label prefix, default "Period"
 */
export default function DateRangePicker({ from, to, onChange, label = "Period" }) {
  return (
    <div style={{ display: "flex", alignItems: "center", gap: "0.75rem", flexWrap: "wrap" }}>
      {label && (
        <span style={{ fontSize: "0.85rem", color: "var(--text-muted)", fontWeight: 500 }}>
          {label}:
        </span>
      )}
      <div style={{ display: "flex", alignItems: "center", gap: "0.5rem" }}>
        <label style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>From</label>
        <input
          type="date"
          className="input"
          style={{ padding: "0.35rem 0.6rem", fontSize: "0.85rem" }}
          value={from}
          onChange={(e) => onChange({ from: e.target.value, to })}
        />
      </div>
      <div style={{ display: "flex", alignItems: "center", gap: "0.5rem" }}>
        <label style={{ fontSize: "0.8rem", color: "var(--text-muted)" }}>To</label>
        <input
          type="date"
          className="input"
          style={{ padding: "0.35rem 0.6rem", fontSize: "0.85rem" }}
          value={to}
          onChange={(e) => onChange({ from, to: e.target.value })}
        />
      </div>
    </div>
  );
}
