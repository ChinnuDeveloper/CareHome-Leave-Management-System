export interface CreateLeaveRequest {
  employeeId: number;
  leaveTypeId: number|null;
  startDate: string;
  endDate: string;
  reason: string;
}