"use client";
import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";

export default function FormPage() {
  const [field1, setField1] = useState("");
  const [field2, setField2] = useState("");
  const [submitted, setSubmitted] = useState(false);
  const [loggedInUser, setLoggedInUser] = useState<string | null>(null);
  const router = useRouter();

  useEffect(() => {
    const user = localStorage.getItem("loggedInUser");
    if (user) setLoggedInUser(user);
    else router.replace("/");
  }, [router]);

  const handleLogout = () => {
    localStorage.removeItem("loggedInUser");
    setLoggedInUser(null);
    window.location.href = "/";
  };

  if (!loggedInUser) return null;

  return (
    <div style={{ minHeight: "100vh", display: "flex", alignItems: "center", justifyContent: "center", background: "#f8fafc", position: "relative" }}>
      <div style={{ position: "fixed", top: "1.5rem", right: "2rem", background: "#6366f1", color: "#fff", padding: "0.5rem 1.25rem", borderRadius: "2rem", fontWeight: 600, fontSize: "1.1rem", zIndex: 1000, boxShadow: "0 2px 8px rgba(99, 102, 241, 0.12)", display: "flex", alignItems: "center" }}>
        <i className="bi bi-person-badge-fill" style={{ fontSize: 20, marginRight: 8, verticalAlign: "middle" }}></i>
        <span>{loggedInUser}</span>
        <button onClick={handleLogout} style={{ marginLeft: 12, background: "#fff", color: "#6366f1", border: "none", borderRadius: "1.5rem", padding: "0.3rem 1.1rem", fontSize: "1rem", fontWeight: 600, cursor: "pointer", transition: "background 0.2s, color 0.2s", boxShadow: "0 1px 4px rgba(99, 102, 241, 0.08)" }} title="Logout">Logout</button>
      </div>
      <div style={{ background: "#fff", padding: "2.5rem 2rem", borderRadius: "1.25rem", boxShadow: "0 8px 32px rgba(60, 72, 100, 0.18)", width: 400 }}>
        <h2 style={{ fontSize: "1.5rem", fontWeight: 700, marginBottom: "1.5rem", color: "#1e293b" }}>Temporary Form</h2>
        <form onSubmit={e => { e.preventDefault(); setSubmitted(true); }} style={{ display: "flex", flexDirection: "column", gap: "1rem" }}>
          <label>
            Field 1
            <input
              type="text"
              value={field1}
              onChange={e => setField1(e.target.value)}
              style={{ width: "100%", padding: "0.75rem", borderRadius: "0.5rem", border: "1px solid #cbd5e1", marginTop: 4 }}
              required
            />
          </label>
          <label>
            Field 2
            <input
              type="text"
              value={field2}
              onChange={e => setField2(e.target.value)}
              style={{ width: "100%", padding: "0.75rem", borderRadius: "0.5rem", border: "1px solid #cbd5e1", marginTop: 4 }}
              required
            />
          </label>
          <button type="submit" style={{ background: "#6366f1", color: "#fff", fontWeight: 600, border: "none", borderRadius: "0.5rem", padding: "0.75rem", cursor: "pointer", fontSize: "1.1rem" }}>
            Submit
          </button>
        </form>
        {submitted && <div style={{ marginTop: 16, color: "#22c55e", fontWeight: 500 }}>Form submitted! (This is a placeholder.)</div>}
      </div>
    </div>
  );
} 