import type { GridColDef } from "@mui/x-data-grid";
import CommonDataGrid from "../common/CommonDataGrid";
import StatusBadge from "../common/StatusBadge";
import EditButton from "../common/EditButton";
import type { LeaveRequestRow } from "../../types/LeaveRequest";
import ViewLeaveRequestDialog from "../common/Dialog/ViewLeaveRequestDialog";
import {getLeaveRequestById } from "../../services/leaveRequestService";
import { useState } from "react";
import type { LeaveRequestDetails } from "../../types/LeaveRequestDetails";

interface LeaveRequestTableProps {
  requests: LeaveRequestRow[];
}

export default function LeaveRequestTable({
  requests,
}: LeaveRequestTableProps) {

  const [open, setOpen] = useState(false);
  const [selectedRequest, setSelectedRequest] = useState<LeaveRequestDetails | null>(null);
  const formatDate = (dateString: string): string => {
  const date = new Date(dateString);

  return `${date
    .getDate()
    .toString()
    .padStart(2, "0")}-${date.toLocaleString("en-GB", {
    month: "short",
  })}-${date.getFullYear().toString().slice(-2)}`;
};
  
const handleView = async (row: LeaveRequestRow) => {
  try {
    const request = await getLeaveRequestById(row.leaveRequestId);
    setSelectedRequest(request);
    setOpen(true);
  } catch (error) {
    console.error("Failed to load leave request details", error);
  }
};

const handleClose = () => {
  setOpen(false);
  setSelectedRequest(null);
};

  const columns: GridColDef<LeaveRequestRow>[] = [
    {
      field: "startDate",
      headerName: "Start Date",
      flex: 1,
      minWidth: 140,
      align: "center",
      headerAlign: "center",
      renderCell: (params) => formatDate(params.value)
    },
    {
      field: "endDate",
      headerName: "End Date",
      flex: 1,
      minWidth: 140,
      align: "center",
      headerAlign: "center",
      renderCell: (params) => formatDate(params.value)
    },
    {
      field: "hours",
      headerName: "Hours",
      type: "number",
      flex: 0.8,
      minWidth: 100,
      align: "center",
      headerAlign: "center",
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
    <CommonDataGrid
      rows={requests}
      columns={columns}
      getRowId={(row) => row.leaveRequestId}
    />

    <ViewLeaveRequestDialog  open={open}  request={selectedRequest}  onClose={handleClose} />
</>
  );
}