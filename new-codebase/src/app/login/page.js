"use client";

import { useState, Suspense } from "react";
import { signIn } from "next-auth/react";
import { useRouter, useSearchParams } from "next/navigation";

function LoginForm() {
  const router = useRouter();
  const searchParams = useSearchParams();
  const callbackUrl = searchParams.get("callbackUrl") || "/dashboard";

  const [userId, setUserId] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  async function handleSubmit(e) {
    e.preventDefault();
    if (!userId.trim() || !password.trim()) return;

    setLoading(true);
    setError("");

    const result = await signIn("credentials", {
      userId,
      password,
      redirect: false,
    });

    setLoading(false);

    if (result?.error) {
      setError("Invalid User ID or password. Please try again.");
    } else {
      router.push(callbackUrl);
    }
  }

  return (
    <form onSubmit={handleSubmit} style={{ display: "flex", flexDirection: "column", gap: "1.25rem" }}>
      <div className="form-group">
        <label htmlFor="userId" className="form-label">
          User ID
        </label>
        <input
          id="userId"
          type="number"
          className="form-control"
          placeholder="Enter your user ID"
          value={userId}
          onChange={(e) => {
            setUserId(e.target.value);
            setError("");
          }}
          required
          autoFocus
        />
      </div>

      <div className="form-group">
        <label htmlFor="password" className="form-label">
          Password
        </label>
        <input
          id="password"
          type="password"
          className="form-control"
          placeholder="Enter your password"
          value={password}
          onChange={(e) => {
            setPassword(e.target.value);
            setError("");
          }}
          required
        />
      </div>

      {error && (
        <div
          style={{
            padding: "0.625rem 0.875rem",
            borderRadius: "0.375rem",
            background: "rgba(239,68,68,0.1)",
            border: "1px solid rgba(239,68,68,0.25)",
            color: "var(--danger)",
            fontSize: "0.82rem",
          }}
        >
          {error}
        </div>
      )}

      <button
        type="submit"
        className="btn btn-primary btn-lg"
        disabled={loading}
        style={{ width: "100%", marginTop: "0.25rem" }}
      >
        {loading ? "Signing in…" : "Sign In"}
      </button>
    </form>
  );
}

export default function LoginPage() {
  return (
    <div
      style={{
        minHeight: "100vh",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        background: "var(--bg-primary)",
        padding: "1rem",
      }}
    >
      {/* Background glow */}
      <div
        style={{
          position: "fixed",
          top: "20%",
          left: "50%",
          transform: "translateX(-50%)",
          width: "600px",
          height: "400px",
          background: "radial-gradient(ellipse, rgba(99,102,241,0.15) 0%, transparent 70%)",
          pointerEvents: "none",
        }}
      />

      <div
        className="animate-fade-in"
        style={{
          width: "100%",
          maxWidth: "400px",
          position: "relative",
        }}
      >
        {/* Logo */}
        <div style={{ textAlign: "center", marginBottom: "2rem" }}>
          <div
            style={{
              width: "3.5rem",
              height: "3.5rem",
              borderRadius: "1rem",
              background: "linear-gradient(135deg, var(--accent-primary), #7c3aed)",
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              fontSize: "1.5rem",
              fontWeight: 800,
              color: "white",
              margin: "0 auto 1rem",
              boxShadow: "0 8px 32px rgba(99,102,241,0.35)",
            }}
          >
            O
          </div>
          <h1 style={{ fontSize: "1.5rem", fontWeight: 700, color: "var(--text-primary)" }}>
            Oasis ERP System
          </h1>
          <p style={{ fontSize: "0.85rem", color: "var(--text-muted)", marginTop: "0.3rem" }}>
            Sign in to your account to continue
          </p>
        </div>

        {/* Card */}
        <div
          style={{
            background: "var(--bg-secondary)",
            border: "1px solid var(--border-subtle)",
            borderRadius: "0.875rem",
            padding: "2rem",
          }}
        >
          <Suspense fallback={<div style={{ textAlign: "center", padding: "2rem" }}>Loading...</div>}>
            <LoginForm />
          </Suspense>
        </div>

        <p
          style={{
            textAlign: "center",
            marginTop: "1.5rem",
            fontSize: "0.78rem",
            color: "var(--text-muted)",
          }}
        >
          Oasis ERP System &copy; {new Date().getFullYear()}
        </p>
      </div>
    </div>
  );
}
