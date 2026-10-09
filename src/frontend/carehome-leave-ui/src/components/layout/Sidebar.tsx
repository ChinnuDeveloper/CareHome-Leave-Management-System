import { NavLink } from "react-router-dom";
import { Home, CalendarPlus , Droplets  } from "lucide-react";

export default function Sidebar(){
    return(
        <aside className="sidebar">
            <div className="side-logo">
                {/* <div className="logo-icon">♥</div> */}
                <Droplets size={18} />
                <div className="name">
                    L<span>eave Manager</span>
                </div>
            </div>

            <nav>
                <NavLink to="/staffdashboardpage">
                <Home size={18}/>
                <span>Dashboard</span>
                </NavLink>

                <NavLink to="/leaverequestpage">
                <CalendarPlus size={18} />
                <span>Request Leave</span>
                </NavLink>  

                <NavLink to="/employeesPage">
                <CalendarPlus size={18} />
                <span>Employees</span>
                </NavLink>  
                
                <NavLink to="/entitlementsPage">
                <CalendarPlus size={18} />
                <span>Entitlement</span>
                </NavLink>  

                <NavLink to="/leaveApprovalPage">
                <CalendarPlus size={18}/>
                <span>Leave Approval</span>
                </NavLink>

            </nav> 

        </aside>
    );
}