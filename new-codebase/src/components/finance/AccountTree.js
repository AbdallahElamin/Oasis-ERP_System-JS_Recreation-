"use client";

import { useState, useEffect } from "react";

/**
 * AccountTree — reusable 4-level collapsible CoA tree
 *
 * Props:
 *   onSelect({ acc1, acc2, acc3, acc4, level }) — fires when any node is clicked
 *   selectedNode — currently selected node (for highlighting)
 *   selectable — "all" | "leaves" (default: "all")
 */
export default function AccountTree({ onSelect, selectedNode, selectable = "all" }) {
  const [tree, setTree] = useState([]);
  const [expanded, setExpanded] = useState({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch("/api/finance/accounts")
      .then((r) => r.json())
      .then((data) => {
        setTree(data);
        setLoading(false);
      })
      .catch(() => setLoading(false));
  }, []);

  function toggle(key) {
    setExpanded((prev) => ({ ...prev, [key]: !prev[key] }));
  }

  function isSelected(node) {
    if (!selectedNode) return false;
    return (
      selectedNode.acc1 === node.acc1 &&
      selectedNode.acc2 === (node.acc2 || null) &&
      selectedNode.acc3 === (node.acc3 || null) &&
      selectedNode.acc4 === (node.acc4 || null)
    );
  }

  function handleSelect(node) {
    if (selectable === "leaves" && node.level < 3) return;
    onSelect?.(node);
  }

  if (loading) {
    return (
      <div style={{ padding: "1rem", color: "var(--text-muted)", fontSize: "0.85rem" }}>
        Loading accounts…
      </div>
    );
  }

  if (!tree.length) {
    return (
      <div style={{ padding: "1rem", color: "var(--text-muted)", fontSize: "0.85rem" }}>
        No accounts found. Add accounts to get started.
      </div>
    );
  }

  return (
    <div className="account-tree">
      {tree.map((l1) => {
        const l1Key = l1.label;
        const l1Open = expanded[l1Key] !== false; // open by default
        return (
          <div key={l1Key} className="tree-l1">
            <div
              className={`tree-node tree-node-l1 ${isSelected({ acc1: l1.label, level: 0 }) ? "tree-selected" : ""}`}
              onClick={() => {
                toggle(l1Key);
                handleSelect({ acc1: l1.label, acc2: null, acc3: null, acc4: null, level: 0 });
              }}
            >
              <span className="tree-caret">{l1Open ? "▾" : "▸"}</span>
              <span className="tree-label">{l1.label}</span>
            </div>
            {l1Open &&
              l1.children?.map((l2) => {
                const l2Key = `${l1Key}|${l2.label}`;
                const l2Open = expanded[l2Key] !== false;
                return (
                  <div key={l2Key} className="tree-l2">
                    <div
                      className={`tree-node tree-node-l2 ${isSelected({ acc1: l1.label, acc2: l2.label, level: 1 }) ? "tree-selected" : ""}`}
                      onClick={(e) => {
                        e.stopPropagation();
                        toggle(l2Key);
                        handleSelect({ acc1: l1.label, acc2: l2.label, acc3: null, acc4: null, level: 1 });
                      }}
                    >
                      <span className="tree-caret">{l2Open ? "▾" : "▸"}</span>
                      <span className="tree-label">{l2.label}</span>
                    </div>
                    {l2Open &&
                      l2.children?.map((l3) => {
                        const l3Key = `${l2Key}|${l3.label}`;
                        const l3Open = expanded[l3Key] !== false;
                        return (
                          <div key={l3Key} className="tree-l3">
                            <div
                              className={`tree-node tree-node-l3 ${isSelected({ acc1: l1.label, acc2: l2.label, acc3: l3.label, level: 2 }) ? "tree-selected" : ""}`}
                              onClick={(e) => {
                                e.stopPropagation();
                                toggle(l3Key);
                                handleSelect({ acc1: l1.label, acc2: l2.label, acc3: l3.label, acc4: null, level: 2 });
                              }}
                            >
                              <span className="tree-caret">{l3Open ? "▾" : "▸"}</span>
                              <span className="tree-label">{l3.label}</span>
                            </div>
                            {l3Open &&
                              l3.children?.map((l4) => (
                                <div key={l4.label} className="tree-l4">
                                  <div
                                    className={`tree-node tree-node-l4 tree-leaf ${isSelected({ acc1: l1.label, acc2: l2.label, acc3: l3.label, acc4: l4.label, level: 3 }) ? "tree-selected" : ""}`}
                                    onClick={(e) => {
                                      e.stopPropagation();
                                      handleSelect({ acc1: l1.label, acc2: l2.label, acc3: l3.label, acc4: l4.label, level: 3 });
                                    }}
                                  >
                                    <span className="tree-leaf-icon">◉</span>
                                    <span className="tree-label">{l4.label}</span>
                                  </div>
                                </div>
                              ))}
                          </div>
                        );
                      })}
                  </div>
                );
              })}
          </div>
        );
      })}
    </div>
  );
}
