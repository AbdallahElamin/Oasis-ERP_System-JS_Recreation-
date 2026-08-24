import { NextResponse } from "next/server";
import pool from "@/lib/db";
import { auth } from "@/lib/auth";

// ── Helpers ────────────────────────────────────────────────────────
/** Build the 4-level nested tree from flat Accs rows */
function buildTree(rows) {
  const map = new Map();

  for (const row of rows) {
    const { Acc1, Acc2, Acc3, Acc4 } = row;
    if (!Acc1) continue;

    if (!map.has(Acc1)) map.set(Acc1, new Map());
    const l2map = map.get(Acc1);

    if (Acc2) {
      if (!l2map.has(Acc2)) l2map.set(Acc2, new Map());
      const l3map = l2map.get(Acc2);

      if (Acc3) {
        if (!l3map.has(Acc3)) l3map.set(Acc3, new Set());
        const l4set = l3map.get(Acc3);

        if (Acc4) l4set.add(Acc4);
      }
    }
  }

  const tree = [];
  for (const [l1, l2map] of map) {
    const l1node = { label: l1, children: [] };
    for (const [l2, l3map] of l2map) {
      const l2node = { label: l2, children: [] };
      for (const [l3, l4set] of l3map) {
        const l3node = {
          label: l3,
          children: [...l4set].map((l4) => ({ label: l4 })),
        };
        l2node.children.push(l3node);
      }
      l1node.children.push(l2node);
    }
    tree.push(l1node);
  }

  return tree;
}

// ── GET — returns full CoA as nested tree ─────────────────────────
export async function GET() {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const rows = await pool.query(
    "SELECT DISTINCT Acc1, Acc2, Acc3, Acc4 FROM Accs WHERE Acc1 IS NOT NULL ORDER BY Acc1, Acc2, Acc3, Acc4"
  );

  return NextResponse.json(buildTree(rows));
}

// ── POST — add a new leaf account ────────────────────────────────
export async function POST(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { acc1, acc2, acc3, acc4 } = await request.json();

  if (!acc1?.trim() || !acc2?.trim() || !acc3?.trim() || !acc4?.trim()) {
    return NextResponse.json({ error: "All 4 account levels are required" }, { status: 400 });
  }

  const existing = await pool.query(
    "SELECT id FROM Accs WHERE Acc1=? AND Acc2=? AND Acc3=? AND Acc4=? LIMIT 1",
    [acc1.trim(), acc2.trim(), acc3.trim(), acc4.trim()]
  );
  if (existing.length > 0) {
    return NextResponse.json({ error: "Account already exists" }, { status: 409 });
  }

  const result = await pool.query(
    "INSERT INTO Accs (Acc1, Acc2, Acc3, Acc4) VALUES (?, ?, ?, ?)",
    [acc1.trim(), acc2.trim(), acc3.trim(), acc4.trim()]
  );

  return NextResponse.json(
    { id: Number(result.insertId), acc1, acc2, acc3, acc4 },
    { status: 201 }
  );
}

// ── DELETE — remove account (only if no transactions reference it) ─
export async function DELETE(request) {
  const session = await auth();
  if (!session) return NextResponse.json({ error: "Unauthorized" }, { status: 401 });

  const { acc1, acc2, acc3, acc4 } = await request.json();

  const txRows = await pool.query(
    "SELECT COUNT(*) AS cnt FROM Transactions WHERE Acc1=? AND Acc2=? AND Acc3=? AND Acc4=?",
    [acc1, acc2, acc3, acc4]
  );
  if (Number(txRows[0].cnt) > 0) {
    return NextResponse.json(
      { error: "Cannot delete: this account has transaction history" },
      { status: 409 }
    );
  }

  await pool.query(
    "DELETE FROM Accs WHERE Acc1=? AND Acc2=? AND Acc3=? AND Acc4=?",
    [acc1, acc2, acc3, acc4]
  );

  return NextResponse.json({ success: true });
}
