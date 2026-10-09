export interface LeaveRequest {
  leaveRequestId: number;
  startDate: string;
  endDate: string; 
  status: string;
  hoursTaken:number;
}
export interface LeaveRequestRow extends LeaveRequest {
  hours: number;
}
 
export interface Employee{
  employeeId: number;
  employeeName: string; 
  departmentName: string;
  allocatedHrs: number;
  takenHrs:number;
  active:boolean; 
}
export interface EmployeeRow extends Employee {
  balanceHours: number;
}

export interface EmployeeDetails{
  employeeId: number;
  firstName: string;
  lastName: string;
  dateOfBirth:string;
  departmentId:number;
  departmentName:string;
  weeklyHours:number;
  role:number;
  active:boolean;
  managerId:number;
  managerName:string;
  userName:string;
  entitlementId:number;
  employeeName:string;
  leaveTypeId:number;
  leaveType:string;
  fiscalYearId:number;
  fiscalYear:string;
  allocatedHrs:number;
  takenHrs:number;
}

export interface LeaveRequestApproval {
    leaveRequestId: number;
    employeeId: number;
    employee: string;
    leaveTypeId: number;
    leaveType: string;
    startDate: string;
    endDate: string;
    reason: string;
    managerComment: string;
    status: string;
    approvedOn: string | null;
    hoursTaken: number;
}

export interface ApproveLeaveRequest {
    leaveRequestId: number;
    approvedBy: number;
    managerComment: string;
}

export interface LeaveRequestActionResponse {
    message: string;
}