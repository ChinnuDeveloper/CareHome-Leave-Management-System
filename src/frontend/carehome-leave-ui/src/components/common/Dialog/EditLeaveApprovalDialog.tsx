import { Box, Button, Dialog, DialogContent, IconButton, Typography, type SxProps, type Theme } from "@mui/material";
import CloseIcon from "@mui/icons-material/Close"; 
import { useEffect, useState } from "react"; 
import "../../../styles/Employee.css";  
import { approveLeaveRequest, getLeaveRequestById, rejectLeaveRequest } from "../../../services/leaveRequestService";


interface EditLeaveApprovalDialogProps {
  open: boolean; 
  requestId: number;
  onClose: (refresh?: boolean) => void;
} 

const EditLeaveApprovalDialog = ({
  open, 
  requestId,
  onClose}:EditLeaveApprovalDialogProps) => { 
    const userId=8; 
     
    const[employee,setEmployee]=useState(""); 
    const[leaveType,setLeaveType]=useState(""); 
    const[startDate,setStartDate]=useState("");
    const[endDate,setEndDate]=useState("");
    const[requestedHours,setRequestedHours]=useState("");
    const[reason,setReason]=useState("");
    const[managerComment,setManagerComment]=useState(""); 

    useEffect(() => {
        if (!open || requestId === null) {
            return;
        }

        const loadLeaveRequest = async () => {
            try {
            const data = await getLeaveRequestById(requestId); 

            setEmployee(data.employee?? ""); 
            setLeaveType(data.leaveType??"");
            setStartDate(data.startDate??"");
            setEndDate(data.endDate??"");
            setRequestedHours(data.hoursTaken?.toString() ?? 0);
            setReason(data.reason??"");
            setManagerComment(data.managerComment??""); 
            } 
            catch (error) {
                    console.error("Failed to load leave request details:", error);
            }             
        };

        loadLeaveRequest();
        }, [open, requestId]); 

    type FormErrors = { 
                        managerComment?: string; 
                    };                    
    const [errors, setErrors] = useState<FormErrors>({});

    const validateForm = (): boolean => {
      const newErrors: FormErrors = {};  
    
    if (!managerComment) {
        newErrors.managerComment = "Manager Comment is required";
      } 

      setErrors(newErrors);

      return Object.keys(newErrors).length === 0;
    };  

    const cancelButtonSx: SxProps<Theme> = {
                height: "var(--control-height, 40px)",
                padding: "0 22px",
                borderRadius: "var(--control-radius, 12px)",
                borderColor: "var(--field-border, #e6e0d0)",
                backgroundColor: "#fff",
                color: "#1a1f1a",
                fontSize: 14,
                fontWeight: 600,
                textTransform: "none",
                boxShadow: "none",
                "&:hover": {
                    backgroundColor: "#faf8f3",
                    borderColor: "var(--field-border, #e6e0d0)",
                    boxShadow: "none",
                },
                };

     
 

    const clearForm = () => {
       setManagerComment("");    
    };

    const handleApprove = async () => {
        if (!validateForm()) return; 
    try {
        await approveLeaveRequest({
            leaveRequestId: requestId,
            approvedBy: userId,
            managerComment: managerComment,
        });
 
        alert("Leave Approved successfully!");
        clearForm();
        onClose(true);
    } catch (error) {
        console.error("Failed to approve leave request:", error);
    }
    };

     const handleReject = async () => {
        if (!validateForm()) return; 

        const confirmed = window.confirm(
        "Are you sure you want to reject this leave request?"
    );

    if (!confirmed) return;

    try {
        await rejectLeaveRequest({
            leaveRequestId: requestId,
            approvedBy: userId,
            managerComment: managerComment,
        });
 
        alert("Leave Rejected successfully!");
        clearForm();
        onClose(true);
    } catch (error) {
        console.error("Failed to reject a leave request:", error);
    }
    }; 
    
    const formatDate = (date: string) => {
    if (!date) return "";

    const d = new Date(date);

    return `${String(d.getDate()).padStart(2, "0")}-${d.toLocaleString(
        "en-GB",
        { month: "short" }
    )}-${String(d.getFullYear()).slice(-2)}`;
};

    return (
     <Dialog open={open} 
            onClose={(_event, reason) => {
                if (reason === "backdropClick" || reason === "escapeKeyDown") {
                    return;
                }
            onClose(false);
            }} 
        slotProps={{
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
            REQUESTS
          </Typography>
          <Typography className="dialog-title">
            Leave Request
          </Typography>
        </Box>

        <IconButton onClick={()=>onClose(false)} className="close-btn">
          <CloseIcon />
        </IconButton>
      </Box>
      <DialogContent className="view-dialog-content"> 
        <Box className="date-grid"> 
             <Box className="field">
                <label>Employee</label>
                <div><input type="text" className="form-input" value={employee} readOnly/></div>
            </Box> 
             <Box className="field">
                <label>Leave Type</label>
                <div><input type="text" className="form-input" value={leaveType} readOnly/></div>
            </Box> 
        </Box>
        <Box className="date-grid">
             <Box className="field">
                <label>Start Date</label>
                <div><input type="text" className="form-input" value={formatDate(startDate)} readOnly/></div>
            </Box> 
             <Box className="field">
                <label>End Date</label>
                <div><input type="text" className="form-input" value={formatDate(endDate)} readOnly/></div>
            </Box> 
        </Box>
            
             <Box className="date-grid"> 
                <Box className="field">
                    <label>Requested Hours</label> 
                    <div><input type="text" className="form-input" value={requestedHours} readOnly/></div>
                </Box>               
             </Box>
              <Box className="field">
                    <label>Reason</label>
                    <div><input type="text" className="form-input" value={reason} readOnly/></div>
                </Box> 
             <Box className="field">
                    <label>Manager Comment</label>
                    <div>
                        <input type="text" className="form-input" value={managerComment} 
                        style={{ backgroundColor: "#fff", color: "#000" }}
                        onChange={(e) => setManagerComment(e.target.value)} />
                        {errors.managerComment && (<p className="error-text">{errors.managerComment}</p>)}
                    </div>
             </Box>     
            <Box className="dialog-actions" sx={{ display: "flex",justifyContent: "space-between",alignItems: "center",width: "100%"}}>                 
             <Button variant="outlined" onClick={()=>{clearForm();onClose()}} sx={cancelButtonSx}>Cancel</Button>
            <Box sx={{ display: "flex", gap: 1 }}>
             <button type="button" className="submit-button" onClick={handleReject}  
             style={{backgroundColor: "#d32f2f",borderColor: "#d32f2f",color: "#fff",}}>Reject</button>
             <button type="button" className="submit-button" onClick={handleApprove}>Approve</button>
            </Box>  
            </Box>
      </DialogContent>

      </Dialog>
);
};
export default EditLeaveApprovalDialog;