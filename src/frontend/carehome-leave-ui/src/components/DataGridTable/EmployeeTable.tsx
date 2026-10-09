import type { GridColDef } from "@mui/x-data-grid";
import CommonDataGrid from "../common/CommonDataGrid";
import StatusBadge from "../common/StatusBadge";
import EditButton from "../common/EditButton";
import type { EmployeeRow } from "../../types/LeaveRequest"; 
import { useState } from "react"; 
import EditEmployeeDialog from "../common/Dialog/EditEmployeeDialog";

interface EmployeeTableProps {
  requests: EmployeeRow[];
  onRefresh: () => Promise<void>;
}

export default function EmployeeTable({
  requests,
  onRefresh,
}: EmployeeTableProps) {

  const [open, setOpen] = useState(false);
  const [selectedEmployeeId, setSelectedEmployeeId] = useState<number>();  
  
  const handleView = async (row: EmployeeRow) => {
  try {
    setSelectedEmployeeId(row.employeeId);
    setOpen(true);
  } catch (error) {
    console.error("Failed to load leave request details", error);
  }
};

const handleClose = async(refresh=false) => {
  setOpen(false);  

  if(refresh){
    await onRefresh();
  }
}; 
 
const columns: GridColDef<EmployeeRow>[] = [
    {
      field: "employeeName",
      headerName: "Employee Name",
      flex: 1,
      minWidth: 140,
      align: "center",
      headerAlign: "center" 
    },
    {
      field: "departmentName",
      headerName: "Department",
      flex: 1,
      minWidth: 140,
      align: "center",
      headerAlign: "center" 
    },  		
    {
      field: "allocatedHrs",
      headerName: "Leave Entitled (hrs)",
      type: "number",
      flex: 0.8,
      minWidth: 100,
      align: "center",
      headerAlign: "center",
    },	
    {
      field: "takenHrs",
      headerName: "Leave Taken (hrs)",
      flex: 1,
      minWidth: 140,
      align: "center",
      headerAlign: "center"
    },
    {
      field: "balanceHours",
      headerName: "Leave Balance (hrs)",
      sortable: false,
      filterable: false,
      flex: 1,
      minWidth: 120,
      align: "center",
      headerAlign: "center"
    },
    {
      field: "active",
      headerName: "Status",
      flex: 1,
      minWidth: 140,
      align: "center",
      headerAlign: "center",
      renderCell: (params) => (
      <StatusBadge
      status={params.value ? "Active" : "Inactive"} /> )
    },
    {
          field: "action",
          headerName: "Action",
          sortable: false,
          filterable: false,
          flex: 1,
          minWidth: 120,
          align: "center",
          headerAlign: "center",
          renderCell: (params) => (
             <EditButton onClick={() => handleView(params.row)} />
          ),
    },
  ];

  return (
    <>
    <CommonDataGrid rows={requests} columns={columns} getRowId={(row) => row.employeeId}/>

    <EditEmployeeDialog open={open} employeeId={selectedEmployeeId} onClose={handleClose} />
    </>
  );
}