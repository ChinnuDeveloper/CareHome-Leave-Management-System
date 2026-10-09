import { Box, Button, Dialog, DialogContent, IconButton, Typography, type SxProps, type Theme } from "@mui/material";
import CloseIcon from "@mui/icons-material/Close"; 
import { useEffect, useState } from "react"; 
import "../../../styles/Employee.css"; 
import { getEntitlementDetails, updateEntitlement } from "../../../services/entitlementService";


interface EditEntitlementDialogProps {
  open: boolean; 
  entitlementId: number;
  onClose: (refresh?: boolean) => void;
} 

const EditEntitlementDialog = ({
  open, 
  entitlementId,
  onClose}:EditEntitlementDialogProps) => { 
    const userId=8; 
     
    const[employee,setEmployee]=useState(""); 
    const[weeklyHours,setWeeklyHours]=useState("");
    const[entitlement,setEntitlement]=useState("");
    const[used,setUsed]=useState("");
    const [remaining, setRemaining] = useState("");
    const[changeReason,setChangeReason]=useState("");

    useEffect(() => {
        if (!open || entitlementId === null) {
            return;
        }

        const loadEmployee = async () => {
            try {
            const data = await getEntitlementDetails(entitlementId); 

            setEmployee(data.employeeName?? "");
            setWeeklyHours(data.weeklyHours?.toString() ?? 0);
            setEntitlement(data.allocatedHrs?.toString()?? 0);
            setUsed(data.takenHrs?.toString() ?? 0);  
            const remainValue = (data.allocatedHrs || 0) - (data.takenHrs || 0);
            setRemaining(remainValue.toFixed(1)?.toString()??0);
            } 
            catch (error) {
                    console.error("Failed to load entitlement details:", error);
            }             
        };

        loadEmployee();
        }, [open, entitlementId]); 

    type FormErrors = {
                        entitlement?: string;
                        used?: string;
                        changeReason?: string; 
                    };                    
    const [errors, setErrors] = useState<FormErrors>({});

    const validateForm = (): boolean => {
      const newErrors: FormErrors = {}; 

      if (!entitlement.trim()) {
        newErrors.entitlement = "Entitlement is required";
      }

     if (!used) {
        newErrors.used = "Used is required";
      }
    
    if (!changeReason) {
        newErrors.changeReason = "Change reason is required";
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
       setEmployee("");  
       setWeeklyHours(""); 
       setEntitlement("");
       setUsed("");
       setRemaining("");  
       setChangeReason(""); 
    };

    const handleSubmit = async () => {  
        
        if (!validateForm()) return; 

        try {
            const entitlementData = {
                allocatedHrs:Number(parseFloat(entitlement).toFixed(1)),
                takenHrs: Number(parseFloat(used).toFixed(1)),
                weeklyHours: Number(parseFloat(weeklyHours).toFixed(1)),
                reason: changeReason,
                modifiedBy:userId
            };  

            await updateEntitlement(entitlementId,entitlementData);

            alert("Entitlement details updated successfully!");
            clearForm();
            onClose(true);
        } catch (error) {
            console.error("Failed to update entitlement:", error);
        }
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
            ENTITLEMENT
          </Typography>
          <Typography className="dialog-title">
            Edit Leave Entitlement
          </Typography>
        </Box>

        <IconButton onClick={()=>onClose(false)} className="close-btn">
          <CloseIcon />
        </IconButton>
      </Box>
      <DialogContent className="view-dialog-content"> 
            <Box className="field">
                <label>Employee</label>
                <div><input type="text" className="form-input" value={employee} readOnly/></div>
            </Box>  
             <Box className="date-grid"> 
                <Box className="field">
                    <label>Weekly Hours</label> 
                    <div><input type="text" className="form-input" value={weeklyHours} readOnly/></div>
                </Box>

                <Box className="field">
                    <label>Entitlement</label>
                    <div>
                        <input type="text" className="form-input" value={entitlement} 
                        style={{ backgroundColor: "#fff", color: "#000" }}
                        onChange={(e) => {
                        const value = e.target.value;
                        if (/^\d*\.?\d*$/.test(value)) {
                        setEntitlement(value);
                        const remainingValue =
                        ((parseFloat(value) || 0) - (parseFloat(used) || 0)).toFixed(1);

                        setRemaining(remainingValue.toString());
                        setErrors((prev) => ({ ...prev, entitlement: "" }));}}}/>
                     {errors.entitlement && (<p className="error-text">{errors.entitlement}</p>)}
                    </div>
                </Box>
             </Box>
             <Box className="date-grid">                 
                <Box className="field">
                    <label>Used</label>
                    <div>
                        <input type="text" className="form-input" value={used} 
                         style={{ backgroundColor: "#fff", color: "#000" }}
                         onChange={(e) => {
                        const value = e.target.value;
                        if (/^\d*\.?\d*$/.test(value)) {
                        setUsed(value);

                        const remainingValue =
                            ((parseFloat(entitlement) || 0) - (parseFloat(value) || 0)).toFixed(1);
 
                        setRemaining(remainingValue.toString());
                        setErrors((prev) => ({ ...prev, used: "" }));}}}/> 
                        {errors.used && (<p className="error-text">{errors.used}</p>)}
                    </div>
                </Box>    
                <Box className="field">
                    <label>Remaining</label>
                    <div>
                        <input type="text" className="form-input" value={remaining} readOnly />
                    </div>
                </Box>
            </Box> 
             <Box className="field">
                    <label>Change Reason</label>
                    <div>
                        <input type="text" className="form-input" value={changeReason} 
                        style={{ backgroundColor: "#fff", color: "#000" }}
                        onChange={(e) => setChangeReason(e.target.value)} />
                        {errors.changeReason && (<p className="error-text">{errors.changeReason}</p>)}
                    </div>
             </Box>     
            <Box className="dialog-actions">                 
             <Button variant="outlined" onClick={()=>{clearForm();onClose()}} sx={cancelButtonSx}>Cancel</Button>
             <button type="button" className="submit-button" onClick={handleSubmit}>Save Changes</button>
            </Box>  
      </DialogContent>

      </Dialog>
);
};
export default EditEntitlementDialog;