import LeaveRequestTable from "../components/DataGridTable/LeaveRequestTable";
import { useEffect, useState } from "react";
import { getLeaveRequests } from "../services/leaveRequestService";
import type { LeaveRequestRow } from "../types/LeaveRequest";

export default function StaffDashboardPage() {

  const [requests, setRequests] = useState<LeaveRequestRow[]>([]);
  const employeeId = 8;

  const calculateLeaveHours = (
  startDate: string,
  endDate: string
): number => {
  const start = new Date(startDate);
  const end = new Date(endDate); 

  const days =
    Math.floor(
      (end.getTime() - start.getTime()) /
        (1000 * 60 * 60 * 24)
    ) + 1;

  return days * 7.5;
};
  useEffect(() => {
    const loadLeaveRequests = async () => {
      try {
        const data = await getLeaveRequests(employeeId);
       
        const rows = data.map((request) => ({
  ...request,
  hours: calculateLeaveHours(
    request.startDate,
    request.endDate
  ),
}));

console.log(rows);
setRequests(rows);
      } catch (error) {
        console.error("Error loading leave requests:", error);
      }
    };

    loadLeaveRequests();
  }, [employeeId]);



  return (
    <>
      <div className="welcome-bar">

        <div>
          <div className="welcome-eyebrow">
            Welcome back
          </div>

          <h2 className="welcome-name">
            Bless Matt
          </h2>
        </div>

        <div className="leave-stat">

          <div className="leave-stat-label">
            Remaining Annual Leave
          </div>

          <div className="leave-stat-value">
            210 <span>hrs</span>
          </div>

        </div>

      </div>

      <div className="section-title">
        <h3>My Leave Requests</h3>
      </div>

      <LeaveRequestTable
  requests={ requests} />

    </>
  );
}