"use client";

import { signOut, useSession } from "next-auth/react";
import Link from "next/link";

export default function Header({ title, subtitle }) {
  const { data: session } = useSession();

  return (
    <header className="erp-header">
      {/* Left: Page Title */}
      <div>
        {title && <h1 style={{ fontSize: "1.05rem", fontWeight: 600, color: "var(--text-primary)" }}>{title}</h1>}
        {subtitle && <p style={{ fontSize: "0.78rem", color: "var(--text-muted)", marginTop: "0.1rem" }}>{subtitle}</p>}
      </div>

      {/* Right: User Info + Sign Out */}
      <div style={{ display: "flex", alignItems: "center", gap: "1rem" }}>
        {session?.user && (
          <div style={{ display: "flex", alignItems: "center", gap: "0.625rem" }}>
            {/* Avatar */}
            <div
              style={{
                width: "1.875rem",
                height: "1.875rem",
                borderRadius: "50%",
                background: "var(--accent-primary)",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                fontSize: "0.75rem",
                fontWeight: 700,
                color: "white",
                flexShrink: 0,
              }}
            >
              {session.user.name?.charAt(0).toUpperCase() ?? "U"}
            </div>
            <div style={{ lineHeight: 1.3 }}>
              <div style={{ fontSize: "0.82rem", fontWeight: 600, color: "var(--text-primary)" }}>
                {session.user.name}
              </div>
              <div style={{ fontSize: "0.7rem", color: "var(--text-muted)" }}>
                {session.user.role ?? "User"}
              </div>
            </div>
          </div>
        )}

        <button
          className="btn btn-secondary btn-sm"
          onClick={() => signOut({ callbackUrl: "/login" })}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="14"
            height="14"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            strokeWidth="2"
          >
            <path d="M9 21H5a2 2 0 01-2-2V5a2 2 0 012-2h4M16 17l5-5-5-5M21 12H9" />
          </svg>
          Sign Out
        </button>
      </div>
    </header>
  );
}
