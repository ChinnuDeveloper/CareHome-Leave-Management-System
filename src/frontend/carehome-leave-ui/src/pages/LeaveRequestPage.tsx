import {  ChevronDown } from "lucide-react"; 
import "../styles/LeaveRequestPage.css"; 
import { useEffect, useState } from "react";
import type { LeaveType } from "../types/LeaveType";
import { getLeaveBalance, getLeaveTypes } from "../services/leaveRequestService"; 
import dayjs, { Dayjs } from "dayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs"; 
import CommonDateTimePicker from "../components/common/CommonDateTimePicker";
import { createLeaveRequest } from "../services/leaveRequestService";

const LeaveRequestPage = () => { 

  const [leaveTypes, setLeaveTypes] = useState<LeaveType[]>([]);
  const [selectedLeaveType, setSelectedLeaveType] = useState<number | null>(null); 

  const [startDate, setStartDate] = useState<Dayjs | null>(dayjs());
  const [endDate, setEndDate] = useState<Dayjs | null>(dayjs().add(1, "hour"));
  const [reason, setReason] = useState(""); 
  const [remainingHours, setRemainingHours] = useState<number>(0);
  const [dailyHours, setDailyHours] = useState<number>(0);
  const employeeId = 8;
  const fiscalYearId=1;

 const totalDays =
  startDate && endDate
    ? Math.max(0, endDate.diff(startDate, "day") + 1)
    : 0;
    
 const handleLeaveTypeChange = async (
  e: React.ChangeEvent<HTMLSelectElement>
) => {
  const value = e.target.value;

  if (value === "") {
    setSelectedLeaveType(null);
    setRemainingHours(0);
    setDailyHours(0);
    return;
  }

  const leaveTypeId = Number(value);

  const selectedType = leaveTypes.find(
    (leaveType) => leaveType.leaveTypeId === leaveTypeId
  );

  if (!selectedType) {
    return;
  }

  setSelectedLeaveType(leaveTypeId);

  // If entitlement is not required, don't call the balance API
  if (!selectedType.requiresEntitlement) {
    setRemainingHours(0);
    setDailyHours(0);
    return;
  }

  // Only Annual Leave (or other entitlement-based leave)
  // reaches this point
  try {
    const balance = await getLeaveBalance(
      employeeId,
      leaveTypeId,
      fiscalYearId
    );

    setRemainingHours(balance.remainingHours);
    setDailyHours(balance.weeklyHours / 5);
  } catch (error) {
    console.error("Failed to load leave balance:", error);
    setRemainingHours(0);
    setDailyHours(0);
  }
};


const handleSubmit = async () => {
  if (!startDate || !endDate) {
    alert("Please select start and end date.");
    return;
  }
 
  try {
    const request = {
      employeeId: employeeId,
      leaveTypeId: selectedLeaveType,
      startDate: startDate.toISOString(),
      endDate: endDate.toISOString(),
      reason: reason.trim(),
      hoursTaken:selectedHours
    };
 
    const result = await createLeaveRequest(request);

    console.log("Leave request saved:", result);

    alert("Leave request submitted successfully.");
    clearForm();
  } catch (error) {
    console.error("Error saving leave request:", error);
    alert("Failed to submit leave request.");
  }
};
 

  useEffect(() => {
  const loadLeaveTypes = async () => {
    try {
      const data = await getLeaveTypes();
      setLeaveTypes(data);
    } catch (error) {
      console.error("Failed to load leave types:", error);
    }
  };

  loadLeaveTypes();
}, []);

const clearForm = () => {
  setSelectedLeaveType(null);  
  setReason("");
  setRemainingHours(0);
  setDailyHours(0);
};

const selectedHours = totalDays * dailyHours;

  return (
    <div className="leave-request-page">

      <div className="leave-request-container">

        {/* Header */}
        <div className="leave-request-header">

          <div>
            <div className="leave-request-eyebrow">
              ADD A NEW LEAVE REQUEST
            </div> 
          </div>
        </div>
 

        {/* Form Card */}
        <div className="leave-form-card">

          {/* Leave Type */}
          <div className="form-group">
  <label>Leave Type</label>

  <div className="select-wrapper">
    <select
      value={selectedLeaveType ?? ""}
      onChange={handleLeaveTypeChange}
    >
      <option value="" disabled>
        Select
      </option>

      {leaveTypes.map((leaveType) => (
        <option
          key={leaveType.leaveTypeId}
          value={leaveType.leaveTypeId}
        >
          {leaveType.name}
        </option>
      ))}
    </select>

    <ChevronDown
      size={18}
      className="select-icon"
    />
  </div>
</div>
          {/* Dates */}

  <LocalizationProvider dateAdapter={AdapterDayjs}>
    <div className="form-row">
      <div className="form-group">
        <label>Start Date</label> 
        <CommonDateTimePicker value={startDate} onChange={setStartDate} />
      </div>
      <div className="form-group">
        <label>End Date</label>
        <CommonDateTimePicker value={endDate} onChange={setEndDate} minDateTime={startDate ?? undefined} />
  </div>
</div>  
</LocalizationProvider> 

<div className="form-row">
  <div className="form-group">
    <label>Total Hours</label>

    <input
      type="text"
      className="form-input" 
      value={selectedHours}
      readOnly
    />
  </div>

  <div className="form-group">
    <label>Remaining Hours</label>

    <input
      type="text"
      className="form-input readonly-input"
      value={`${remainingHours} hrs`}
      readOnly
    />
  </div>

</div>
           

          {/* Reason */}
          <div className="form-group">
            <label>Reason</label>

            <textarea 
              className="form-input"
              value={reason}
              onChange={(e) => setReason(e.target.value)}
              placeholder="Enter reason for leave"
            />
          </div>

          {/* Actions */}
          <div className="form-actions">

            <button
              type="button"
              className="cancel-button"
              onClick={clearForm} 
            >
              Clear
            </button>

            <button
              type="button"
              className="submit-button"
              onClick={handleSubmit}
            >
              Submit
            </button>

          </div>

        </div>

      </div>

    </div>
  );
};

export default LeaveRequestPage;