export const metadata = { title: "Sales Reports — Oasis ERP" };

export default function Page() {
  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">Sales Reports</h1>
          <p className="page-subtitle">Detailed analytics and print-ready reports</p>
        </div>
      </div>
      <div className="card" style={{ textAlign: "center", padding: "3rem" }}>
        <div style={{ fontSize: "3rem", marginBottom: "1rem" }}>📊</div>
        <h3 style={{ color: "var(--text-primary)", marginBottom: "0.5rem" }}>Coming in Phase 3</h3>
        <p style={{ color: "var(--text-muted)", fontSize: "0.875rem", maxWidth: "380px", margin: "0 auto" }}>
          Sales reports with date-range filtering, client breakdowns, and print-ready HTML layouts will be implemented alongside the PDF generation system.
        </p>
      </div>
    </div>
  );
}
