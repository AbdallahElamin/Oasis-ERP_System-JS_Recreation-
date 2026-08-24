export const metadata = { title: "Return Invoice — Oasis ERP" };

export default function Page() {
  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Return Invoice</h1>
          <p className="page-subtitle">Reverse sales and restore stock</p>
        </div>
      </div>
      <div className="card" style={{ textAlign: "center", padding: "3rem" }}>
        <div style={{ fontSize: "3rem", marginBottom: "1rem" }}>🔄</div>
        <h3 style={{ color: "var(--text-primary)", marginBottom: "0.5rem" }}>Coming in Phase 3</h3>
        <p style={{ color: "var(--text-muted)", fontSize: "0.875rem", maxWidth: "380px", margin: "0 auto" }}>
          Return invoice processing will reverse the original invoice's stock deductions and create correcting journal entries.
        </p>
      </div>
    </div>
  );
}
