import type { GridColDef } from "@mui/x-data-grid";
import CommonDataGrid from "../common/CommonDataGrid"; 
import EditButton from "../common/EditButton"; 
import { useState } from "react";   
import StatusBadge from "../common/StatusBadge";
import type { LeaveRequestApproval } from "../../types/LeaveRequest";
import EditLeaveApprovalDialog from "../common/Dialog/EditLeaveApprovalDialog";

interface LeaveApprovalTableProps {
  requests: LeaveRequestApproval[];
  onRefresh: () => Promise<void>;
}

export default function EntitlementTable({
  requests,
  onRefresh,
}: LeaveApprovalTableProps) {

  const [open, setOpen] = useState(false);
  const [selectedLeaveRequestId, setSelectedLeaveRequestId] = useState<number>();  
  const formatDate = (dateString: string): string => {
  const date = new Date(dateString);

  return `${date
    .getDate()
    .toString()
    .padStart(2, "0")}-${date.toLocaleString("en-GB", {
    month: "short",
  })}-${date.getFullYear().toString().slice(-2)}`;
};
  
  const handleView = async (row: LeaveRequestApproval) => {
  try {
    setSelectedLeaveRequestId(row.leaveRequestId);
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

const columns: GridColDef<LeaveRequestApproval>[] = [
    {
      field: "employee",
      headerName: "Employee",
      flex: 1,
      minWidth: 140,
      align: "center",
      headerAlign: "center" 
    },
    {
      field: "leaveType",
      headerName: "Leave Type",
      flex: 1,
      minWidth: 140,
      align: "center",
      headerAlign: "center" 
    },      
    {
      field: "startDate",
      headerName: "From Date",
      flex: 1,
      minWidth: 140,
      align: "center",
      headerAlign: "center",
      renderCell: (params) => formatDate(params.value)
    },  
    {
      field: "endDate",
      headerName: "To Date",
      type: "number",
      flex: 0.8,
      minWidth: 100,
      align: "center",
      headerAlign: "center", 
      renderCell: (params) => formatDate(params.value)
    },	 
    {
          field: "status",
          headerName: "Status",
          flex: 1,
          minWidth: 140,
          align: "center",
          headerAlign: "center",
          renderCell: (params) => (
            <StatusBadge status={params.value} />
          ),
    },	 
    {
          field: "reason",
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
    <CommonDataGrid rows={requests} columns={columns} getRowId={(row) => row.leaveRequestId}/>

    <EditLeaveApprovalDialog open={open} requestId={selectedLeaveRequestId} onClose={handleClose} />
    </>
  );
}