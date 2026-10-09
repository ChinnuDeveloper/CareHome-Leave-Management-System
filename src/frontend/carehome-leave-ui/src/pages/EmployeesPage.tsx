import { useEffect, useState } from "react"; 
import type { EmployeeRow } from "../types/LeaveRequest";
import EmployeeTable from "../components/DataGridTable/EmployeeTable";
import { getEmployee } from "../services/employeeService";
import "../styles/Employee.css"; 
import CreateEmployeeDialog from "../components/common/Dialog/CreateEmployeeDialog";

export default function EmployeePage() {

  const [requests, setRequests] = useState<EmployeeRow[]>([]);
  const fiscalYearId = 1; 
  const leaveTypeId=1;

  const [openCreateDialog, setOpenCreateDialog] = useState(false);

 
 const loadData = async () => {
    try {
      const data = await getEmployee(fiscalYearId,leaveTypeId);

       const rows = data.map((request) => ({
        ...request,
        balanceHours:
          (request.allocatedHrs ?? 0) - (request.takenHrs ?? 0),
      }));

      setRequests(rows);
    } catch (error) {
      console.error("Error loading data:", error);
    }
  };

  
   
 useEffect(() => {
  const fetchData = async () => {
    try {
      const data = await getEmployee(fiscalYearId, leaveTypeId);

      const rows = data.map((request) => ({
        ...request,
        balanceHours:
          (request.allocatedHrs ?? 0) - (request.takenHrs ?? 0),
      }));

      setRequests(rows);
    } catch (error) {
      console.error("Error loading data:", error);
    }
  };

  fetchData();
}, [fiscalYearId, leaveTypeId]);

const handleSubmit = async () => {  setOpenCreateDialog(true);};

  return (
    <>       
      <div className="leave-stat" style={{ marginBottom: "10px" }}>
        <button
              type="button"
              className="new-button"
              onClick={handleSubmit}>Add New</button>
        </div> 
    <EmployeeTable requests={ requests} onRefresh={loadData}/>
    <CreateEmployeeDialog open={openCreateDialog} onClose={() => {setOpenCreateDialog(false); loadData();}} />
    </>
  );
}