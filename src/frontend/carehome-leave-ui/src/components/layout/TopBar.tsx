import { useLocation } from "react-router-dom";
import "../../styles/topbar.css"; 


export default function TopBar() {
  const location = useLocation();

  const pageTitles: Record<string, string> = {
    "/": "My Dashboard",
    "/leaverequestpage": "Leave Request",
    "/employeesPage": "Employees",
    "/entitlementsPage": "Entitlement",
    "/leaveApprovalPage":"Leave Requests"
  };

  const pageTitle = pageTitles[location.pathname] || "My Dashboard";

  return (
    <header className="topbar">

      <div className="topbar-title">
        <div className="eyebrow">
          Leave Management System
        </div>

        <h1>{pageTitle}</h1>
      </div>

      <div className="topbar-user">

        <div className="user-chip">

          <div className="avatar">
            BM
          </div>

          <div className="user-meta">
            <div className="uname">
              Bless Matt
            </div>

            <div className="urole">
              Kitchen Staff
            </div>
          </div>

        </div>

        <button className="logout-btn">
          Log out
        </button>

      </div>

    </header>
  );
}