import Sidebar from "@/components/layout/Sidebar";
import Header from "@/components/layout/Header";
import Providers from "@/components/layout/Providers";

export default function DashboardLayout({ children }) {
  return (
    <Providers>
      <div className="erp-layout">
        <Sidebar />
        <div className="erp-main">
          <Header />
          <main className="erp-content animate-fade-in">{children}</main>
        </div>
      </div>
    </Providers>
  );
}
