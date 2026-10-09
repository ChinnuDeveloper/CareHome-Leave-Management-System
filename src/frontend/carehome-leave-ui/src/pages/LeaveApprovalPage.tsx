import { useState } from "react"; 
import type {  LeaveRequestApproval } from "../types/LeaveRequest";  
import "../styles/LeaveApproval.css";    
import { Box } from "@mui/material"; 
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import CommonDateTimePicker from "../components/common/CommonDateTimePicker";
import type { Dayjs } from "dayjs";
import dayjs from "dayjs";
import LeaveApprovalTable from "../components/DataGridTable/LeaveApprovalTable";
import { getLeaveRequestsByEmployee } from "../services/leaveRequestService";

export default function LeaveApprovalPage() {

  const [startDate, setStartDate] = useState<Dayjs | null>(dayjs());
  const [endDate, setEndDate] = useState<Dayjs | null>(dayjs().add(1, "hour"));  

  const [leaveRequests, setLeaveRequests] = useState<LeaveRequestApproval[]>([]);
  const [selectedStatus, setSelectedStatus] = useState<string>("Pending");


  const handleRefresh = async () => {
    try {
        if (!startDate || !endDate) {
            return;
        }  
        const data = await getLeaveRequestsByEmployee(
            startDate.format("YYYY-MM-DD HH:mm:ss.SSS"),
            endDate.format("YYYY-MM-DD HH:mm:ss.SSS")
        ); 
        
        setLeaveRequests(data);
    } catch (error) {
        console.error("Error loading leave requests:", error);
    }
};

const filteredRequests = leaveRequests.filter(
    (request) => request.status === selectedStatus
);
    
  return (
    <>   
        <Box className="date-grid">
            <Box className="field">
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <div className="form-row">
                    <div className="form-group">
                        <label>Start Date</label> 
                        <CommonDateTimePicker value={startDate} onChange={setStartDate} />
                    </div>
                    <div className="form-group">
                        <label>End Date</label>
                        <CommonDateTimePicker value={endDate} onChange={setEndDate} minDateTime={startDate ?? undefined} />
                    </div>
                     <div className="form-group"> 
                        <button type="button" className="new-button" onClick={handleRefresh}>Refresh</button> 
                     </div>
                </div>  
                </LocalizationProvider>                  
            </Box>            
        </Box>
          
        <div className="status-filter">
            <button
                type="button"
                className={selectedStatus === "Pending" ? "status-button active" : "status-button"}
                onClick={() => setSelectedStatus("Pending")}>
                Pending {leaveRequests.filter(x => x.status === "Pending").length}
            </button>

            <button
                type="button"
                className={selectedStatus === "Approved" ? "status-button active" : "status-button"}
                onClick={() => setSelectedStatus("Approved")}>
                Approved {leaveRequests.filter(x => x.status === "Approved").length}
            </button>

            <button
                type="button"
                className={selectedStatus === "Rejected" ? "status-button active" : "status-button"}
                onClick={() => setSelectedStatus("Rejected")}>
                Rejected {leaveRequests.filter(x => x.status === "Rejected").length}
            </button>

            <button
                type="button"
                className={selectedStatus === "Cancelled" ? "status-button active" : "status-button"}
                onClick={() => setSelectedStatus("Cancelled")}>
                Cancelled {leaveRequests.filter(x => x.status === "Cancelled").length}
            </button>
        </div>
        
    <LeaveApprovalTable requests={filteredRequests} onRefresh={handleRefresh}/>
   </>
  );
}