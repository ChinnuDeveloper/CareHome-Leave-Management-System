import type { GridColDef } from "@mui/x-data-grid";
import CommonDataGrid from "../common/CommonDataGrid"; 
import EditButton from "../common/EditButton"; 
import { useState } from "react";  
import EditEntitlementDialog from "../common/Dialog/EditEntitlementDialog";
import type { Entitlements } from "../../types/entitlement";

interface EntitlementTableProps {
  requests: Entitlements[];
  onRefresh: () => Promise<void>;
}

export default function EntitlementTable({
  requests,
  onRefresh,
}: EntitlementTableProps) {

  const [open, setOpen] = useState(false);
  const [selectedEntitlementId, setSelectedEntitlementId] = useState<number>();  
  
  const handleView = async (row: Entitlements) => {
  try {
    setSelectedEntitlementId(row.entitlementId);
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
 		
const columns: GridColDef<Entitlements>[] = [
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
      field: "weeklyHours",
      headerName: "Weekly Hours",
      flex: 1,
      minWidth: 140,
      align: "center",
      headerAlign: "center" 
    },  
    {
      field: "allocatedHrs",
      headerName: "Entitled (hrs)",
      type: "number",
      flex: 0.8,
      minWidth: 100,
      align: "center",
      headerAlign: "center",
      valueFormatter: (value) => Number(value).toFixed(1),
    },	
    {
      field: "takenHrs",
      headerName: "Taken (hrs)",
      flex: 1,
      minWidth: 140,
      align: "center",
      headerAlign: "center",
      valueFormatter: (value) => Number(value).toFixed(1),
    },
    {
      field: "balanceHours",
      headerName: "Balance (hrs)",
      sortable: false,
      filterable: false,
      flex: 1,
      minWidth: 120,
      align: "center",
      headerAlign: "center",
      valueFormatter: (value) => Number(value).toFixed(1),
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

    <EditEntitlementDialog open={open} entitlementId={selectedEntitlementId} onClose={handleClose} />
    </>
  );
}