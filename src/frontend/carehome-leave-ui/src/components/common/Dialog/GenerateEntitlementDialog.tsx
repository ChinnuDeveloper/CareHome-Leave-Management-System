import { Box, Button, Dialog, DialogContent, IconButton, Typography, type SxProps, type Theme } from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import { ChevronDown } from "lucide-react";
import { useEffect, useState } from "react";  
import "../../../styles/Employee.css"; 
import { createEntitlement, entitlementExists, getFiscalYear } from "../../../services/entitlementService";
import type { FiscalYear } from "../../../types/entitlement";


interface CreateEmployeeDialogProps {
  open: boolean; 
  onClose: () => void;
} 

const GenerateEntitlementDialog = ({
  open, 
  onClose}:CreateEmployeeDialogProps) => {

    const [selectedYear, setSelectedYear] = useState<number | null>(null); 
    const [fiscalYear, setFiscalYear] = useState<FiscalYear[]>([]); 
    const [weeklyHours, setWeeklyHours] = useState("5.6");  
    const loginUser=6;

    type FormErrors = {  
                      fiscalYear?: string;
                      weeklyHours?: string; 
                    };                    
    const [errors, setErrors] = useState<FormErrors>({});

    const validateForm = (): boolean => {
        const newErrors: FormErrors = {};

        if (selectedYear === null || selectedYear === undefined) {
            newErrors.fiscalYear = "Year is required";
        }

        if (!weeklyHours.trim()) {
            newErrors.weeklyHours = "Multiplier is required";
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

    const handleYearChange = (
            e: React.ChangeEvent<HTMLSelectElement>
            ) => {
            const value = e.target.value;
 

            if (value === "") {
                setSelectedYear(null);
                return;
            }

            setSelectedYear(Number(value));

            setErrors((prev) => ({
                ...prev,
                fiscalYear: "",
            }));
            };

    useEffect(() => {
      const loadFiscalYears = async () => {
        try {
          const data = await getFiscalYear();
          setFiscalYear(data);
        } catch (error) {
          console.error("Failed to load fiscal year:", error);
        }
      };
    
      loadFiscalYears(); 
    }, []);

    const clearForm = () => { 
      setSelectedYear(null)
      setWeeklyHours("5.6");
    };

    const handleSubmit = async () => {  
        
        if (!validateForm()) return; 

        try { 
            const entitlementData = {  
                fiscalYearId: Number(selectedYear),
                generatedBy: Number(loginUser),
                generatedOn: new Date().toISOString(),
                calculationRule: Number(weeklyHours)
            }; 

            const exists = await entitlementExists(Number(selectedYear));
            if (exists) {
                alert("Sorry, entitlement already exists for this fiscal year.");
                return;
            }

             const confirmed = window.confirm(
                "Are you sure you want to generate entitlement for the selected fiscal year?"
            );

            if (!confirmed) {
                return;
            }
            
            await createEntitlement(entitlementData);

            alert("Employee created successfully!");
            clearForm();
            onClose();
        } catch (error) {
            console.error("Failed to create employee:", error);
        }
    };
    
    return (
     <Dialog open={open} 
            onClose={(_event, reason) => {
                if (reason === "backdropClick" || reason === "escapeKeyDown") {
                    return;
                }
            onClose();
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
            Generate Leave Entitlement
          </Typography>
        </Box>

        <IconButton onClick={onClose} className="close-btn">
          <CloseIcon />
        </IconButton>
      </Box>
      <DialogContent className="view-dialog-content">        
             <Box className="field">
                <label>Year</label>
                <div className="select-wrapper">
                    <select
                    className={errors.fiscalYear ? "input-error" : ""}
                    value={selectedYear ?? ""}
                    onChange={handleYearChange}
                    >
                    <option value="" disabled>
                        Select
                    </option>

                    {fiscalYear.map((fiscalYear) => (
                        <option
                        key={fiscalYear.fiscalYearId}
                        value={fiscalYear.fiscalYearId}
                        >
                        {fiscalYear.year}
                        </option>
                    ))}
                    </select>
                    <ChevronDown size={18} className="select-icon" />
                </div>
            {errors.fiscalYear && (
                <p className="error-text">{errors.fiscalYear}</p>
            )}
            </Box>
            <Box className="field">
                <br/>
                    <label>Calculation Rule — Weekly Hours × Multiplier</label> 
                    <div>
                    <input type="text" className={`form-input ${errors.weeklyHours ? "input-error" : ""}`} value={weeklyHours}
                    onChange={(e) => {
                        const value = e.target.value;

                        if (/^\d*\.?\d*$/.test(value)) {
                        setWeeklyHours(value);
                        setErrors((prev) => ({ ...prev, weeklyHours: "" }));
                        }
                    }}/> 
                    {errors.weeklyHours && (<p className="error-text">{errors.weeklyHours}</p>)}
                    </div>
                </Box>

              <Box className="field">
                <br/>
                <label>This calculates each employee's annual entitlement as their weekly contracted hours multiplied by the figure above, applied across all departments for the selected year.</label>
                <br/>
              </Box> 

            <Box className="dialog-actions">
              <Button variant="outlined" onClick={()=>{clearForm();onClose()}} sx={cancelButtonSx}>Cancel</Button>
              <button type="button" className="submit-button" onClick={handleSubmit}>Generate</button>
            </Box>
         
      </DialogContent>

      </Dialog>
);
};
export default GenerateEntitlementDialog;