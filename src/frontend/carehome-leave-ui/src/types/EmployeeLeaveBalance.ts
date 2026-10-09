export interface EmployeeLeaveBalance{
   employeeId: number,
   leaveTypeId: number,
   fiscalYearId: number,
   weeklyHours: number,
   entitlementHours: number,
   usedHours: number,
   remainingHours: number
}
export interface CreateEmployeeRequest {
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  departmentId: number;
  weeklyHours: number;
  role: number;
  managerId: number| null;
  emailId: string;
  password: string;
}

export interface UpdateEmployeeRequest extends CreateEmployeeRequest {
   active:boolean;
} 