import { Box, Button, Dialog, DialogContent, IconButton, Typography, type SxProps, type Theme } from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import { ChevronDown } from "lucide-react";
import { useEffect, useState } from "react";
import type { Department } from "../../../types/department";
import { getDepartments } from "../../../services/departmentService";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import CommonDateTimePicker from "../CommonDateTimePicker";
import type { Dayjs } from "dayjs";
import dayjs from "dayjs";
import "../../../styles/Employee.css";
import { createEmployee } from "../../../services/employeeService";


interface CreateEmployeeDialogProps {
  open: boolean; 
  onClose: () => void;
} 

const CreateEmployeeDialog = ({
  open, 
  onClose}:CreateEmployeeDialogProps) => {

    const [selectedDepartment, setSelectedDepartment] = useState<number | null>(null); 
    const [departments, setDepartments] = useState<Department[]>([]);
    const [dobDate, setDobDate] = useState<Dayjs | null>(dayjs());
    const [email, setEmail] = useState("");  
    const [weeklyHours, setWeeklyHours] = useState("");
    const [entitlementHours, setEntitlementHours] = useState("");
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [password, setPassword] = useState(""); 

    type FormErrors = {
                      firstName?: string;
                      email?: string;
                      department?: string;
                      weeklyHours?: string;
                      password?: string;
                    };                    
    const [errors, setErrors] = useState<FormErrors>({});

    const validateForm = (): boolean => {
      const newErrors: FormErrors = {};
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

      if (!firstName.trim()) {
        newErrors.firstName = "First name is required";
      }

      if (!email.trim()) {
        newErrors.email = "Email is required";
      } else if (!emailRegex.test(email)) {
        newErrors.email = "Enter a valid email address";
      }

      if (!selectedDepartment) {
        newErrors.department = "Department is required";
      }

      if (!weeklyHours.trim()) {
        newErrors.weeklyHours = "Weekly hours is required";
      }

      if (!password.trim()) {
        newErrors.password = "Password is required";
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

    const handleDepartmentChange = async (
      e: React.ChangeEvent<HTMLSelectElement>
    ) => {
      const value = e.target.value;
    
      if (value === "") {
        setSelectedDepartment(null); 
        return;
      } 
      setSelectedDepartment(Number(value));     
      setErrors((prev) => ({ ...prev, department: "" }));        
    };

    useEffect(() => {
      const loadDepartments = async () => {
        try {
          const data = await getDepartments();
          setDepartments(data);
        } catch (error) {
          console.error("Failed to load leave types:", error);
        }
      };
    
      loadDepartments();
    }, []);

    const clearForm = () => {
      setFirstName("");  
      setLastName(""); 
      setPassword("");
      setEmail("");
      setWeeklyHours("");
      setEntitlementHours("");  
      setSelectedDepartment(null)
    };

    const handleSubmit = async () => {  
        
        if (!validateForm()) return; 

        try {
            const employeeData = {
            firstName: firstName,
            lastName: lastName,
            dateOfBirth: dobDate?.toISOString() ?? "",
            departmentId: selectedDepartment ?? 0,
            weeklyHours: Number(weeklyHours),
            role: 1,
            managerId: null,
            emailId: email,
            password: password,
            }; 

            await createEmployee(employeeData);

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
            EMPLOYEES
          </Typography>
          <Typography className="dialog-title">
            Add New Employee
          </Typography>
        </Box>

        <IconButton onClick={onClose} className="close-btn">
          <CloseIcon />
        </IconButton>
      </Box>
      <DialogContent className="view-dialog-content">
        <Box className="date-grid">
            <Box className="field">
                <label>First Name</label>
                <div>
                    <input type="text" className={`form-input ${errors.firstName ? "input-error" : ""}`} value={firstName}
                    onChange={(e) => {
                                      setFirstName(e.target.value); 
                                     setErrors((prev) => ({ ...prev, firstName: "" }));
                                      }}
                    />
                    {errors.firstName && (<p className="error-text">{errors.firstName}</p>)}
                  </div>
            </Box>
            <Box className="field">
                <label>Last Name</label>
                <div>
                    <input type="text" className="form-input" value={lastName} onChange={(e) => setLastName(e.target.value)} />
                </div>
            </Box>
            </Box>
            <Box className="field">
                <label>Email Id</label>
                <div>
                <input type="email" className={`form-input ${errors.email ? "input-error" : ""}`} value={email}
                    onChange={(e) => {
                        setEmail(e.target.value);
                        setErrors((prev) => ({ ...prev, email: "" }));
                    }}
                    placeholder="example@email.com"/>
                 {errors.email && (<p className="error-text">{errors.email}</p>)}
                </div> 
            </Box>
            <Box className="date-grid">
             <Box className="field">
                <label>Date of Birth</label> 
                <div className="date-input"> 
                     <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <CommonDateTimePicker value={dobDate} onChange={setDobDate}  /> 
                    </LocalizationProvider>
                </div> 
             </Box>
             {/* <Box className="field">
                <label>Department</label>
                <div className="select-wrapper">
                    <select
                      value={selectedDepartment ?? ""}
                      onChange={handleDepartmentChange}
                    >
                      <option value="" disabled>
                        Select
                      </option>
                
                      {departments.map((department) => (
                        <option
                          key={department.departmentId}
                          value={department.departmentId}
                        >
                          {department.departmentName}
                        </option>
                      ))}
                    </select>
                
                    <ChevronDown
                      size={18}
                      className="select-icon"
                    />
                </div>
             </Box> */}
             <Box className="field">
  <label>Department</label>

  <div className="select-wrapper">
    <select
      className={errors.department ? "input-error" : ""}
      value={selectedDepartment ?? ""}
      onChange={handleDepartmentChange}
    >
      <option value="" disabled>
        Select
      </option>

      {departments.map((department) => (
        <option
          key={department.departmentId}
          value={department.departmentId}
        >
          {department.departmentName}
        </option>
      ))}
    </select>

    <ChevronDown size={18} className="select-icon" />
  </div>

  {errors.department && (
    <p className="error-text">{errors.department}</p>
  )}
</Box>
            </Box>
            <Box className="date-grid">
                <Box className="field">
                    <label>Employment Status</label> 
                    <div className="select-wrapper">
                        <select>
                            <option value="true">Active</option>
                            <option value="false">Inactive</option>
                        </select>
                     <ChevronDown size={18} className="select-icon" />
                    </div>
                </Box>
                <Box className="field">
                    <label>Weekly Hours</label> 
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
            </Box>
            <Box className="date-grid">
                <Box className="field">
                    <label>Annual Leave Entitlement</label> 
                    <div>
                    <input type="text" className="form-input" value={entitlementHours}
                    onChange={(e) => {
                        const value = e.target.value;

                        if (/^\d*\.?\d*$/.test(value)) {
                        setEntitlementHours(value);
                        }
                        }} 
                    />
                    </div>
                </Box>
                <Box className="field">
                    <label>Password</label> 
                    <div>
                    {/* <input type="text" className="form-input" value={password} onChange={(e) => setPassword(e.target.value)}/> */}
                    <input type="text" className={`form-input ${errors.password ? "input-error" : ""}`} value={password} 
                    onChange={(e) => {setPassword(e.target.value); 
                                  setErrors((prev) => ({ ...prev, password: "" }));
                                    }} 
                    />
                    {errors.password && (<p className="error-text">{errors.password}</p>)}
                    </div>
                </Box>
            </Box>
            <Box className="dialog-actions">
              <Button variant="outlined" onClick={()=>{clearForm();onClose()}} sx={cancelButtonSx}>Cancel</Button>
              <button type="button" className="submit-button" onClick={handleSubmit}>Save</button>
            </Box>
         
      </DialogContent>

      </Dialog>
);
};
export default CreateEmployeeDialog;