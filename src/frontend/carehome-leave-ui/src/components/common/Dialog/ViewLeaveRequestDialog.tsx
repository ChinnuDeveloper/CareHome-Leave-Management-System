import CloseIcon from "@mui/icons-material/Close";
import {
  Dialog,
  DialogContent,
  IconButton,
  Typography,
  Box,
  Button,
} from "@mui/material";
import type { LeaveRequestDetails } from "../../../types/LeaveRequestDetails";
import "../../../styles/ViewLeaveRequestDialog.css";

interface ViewLeaveRequestDialogProps {
  open: boolean;
  request: LeaveRequestDetails | null;
  onClose: () => void;
}

const formatDate = (date: string) =>
  new Date(date).toLocaleDateString("en-GB", {
    day: "2-digit",
    month: "short",
    year: "2-digit",
  });

const ViewLeaveRequestDialog = ({
  open,
  request,
  onClose,
}: ViewLeaveRequestDialogProps) => {
  if (!request) return null;

  return (
    <Dialog open={open} onClose={onClose} slotProps={{
    paper: {
      sx: {
        width: "450px",
        maxWidth: "90vw",
      },
    },
  }}>
      <Box className="view-dialog-header">
        <Box>
          <Typography className="dialog-subtitle">
            MY LEAVE REQUEST
          </Typography>
          <Typography className="dialog-title">
            View Leave Request
          </Typography>
        </Box>

        <IconButton onClick={onClose} className="close-btn">
          <CloseIcon />
        </IconButton>
      </Box>

      <DialogContent className="view-dialog-content">
        <Box className="field">
          <label>Status</label>
          <div className="readonly-box">{request.status}</div>
        </Box>

        <Box className="date-grid">
          <Box className="field">
            <label>Start Date</label>
            <div className="readonly-box">
              {formatDate(request.startDate)}
            </div>
          </Box>

          <Box className="field">
            <label>End Date</label>
            <div className="readonly-box">
              {formatDate(request.endDate)}
            </div>
          </Box>
        </Box> 
       
        <Box className="field">
          <label>Hours</label>
          <div className="readonly-box">{request.hoursTaken}</div>
        </Box>

        <Box className="field">
          <label>Reason</label>
          <div className="readonly-box">{request.reason || "-"}</div>
        </Box>

        <Box className="field">
          <label>Manager Comments</label>
          <div className="readonly-box">
            {request.managerComment || "-"}
          </div>
        </Box>

        <Box className="dialog-actions">
          <Button variant="outlined" onClick={onClose}>
            Cancel
          </Button>
        </Box>
      </DialogContent>
    </Dialog>
  );
};

export default ViewLeaveRequestDialog;