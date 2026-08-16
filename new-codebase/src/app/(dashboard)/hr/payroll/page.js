export const metadata = { title: "payroll — Oasis ERP" };

export default function Page() {
  return (
    <div>
      <div className="page-title-bar">
        <div>
          <h1 className="page-title">payroll</h1>
          <p className="page-subtitle">This module is under construction — groundwork phase.</p>
        </div>
      </div>
      <div className="card" style={{ textAlign: "center", padding: "3rem" }}>
        <div style={{ fontSize: "3rem", marginBottom: "1rem" }}>🚧</div>
        <h3 style={{ color: "var(--text-primary)", marginBottom: "0.5rem" }}>Coming Soon</h3>
        <p style={{ color: "var(--text-muted)", fontSize: "0.875rem" }}>
          The <strong>payroll</strong> feature is planned for an upcoming phase.
        </p>
      </div>
    </div>
  );
}
