import apiClient from "../api/apiClient";
import type { CreateLeaveRequest } from "../types/CreateLeaveRequest";
import type { EmployeeLeaveBalance } from "../types/EmployeeLeaveBalance";
import type { ApproveLeaveRequest, LeaveRequest, LeaveRequestActionResponse, LeaveRequestApproval } from "../types/LeaveRequest"; 
import type { LeaveRequestDetails } from "../types/LeaveRequestDetails";
import type {LeaveType} from "../types/LeaveType";

export const getLeaveRequests = async (employeeId: number): Promise<LeaveRequest[]> => {
  const response = await apiClient.get<LeaveRequest[]>(
    `/LeaveRequests/employee/${employeeId}`
  ); 
  return response.data;
};

export const getLeaveTypes = async (): Promise<LeaveType[]> => {
  const response = await apiClient.get<LeaveType[]>(
    "/LeaveTypes"
  );

  return response.data;
};

export const createLeaveRequest = async (
  request: CreateLeaveRequest
) => {
  const response = await apiClient.post(
    "/LeaveRequests",
    request
  );

  return response.data;
};

export const getLeaveBalance = async (
  employeeId: number,
  leaveTypeId: number,
  fiscalYearId: number
): Promise<EmployeeLeaveBalance> => {
  const response = await apiClient.get<EmployeeLeaveBalance>(
    `/Employees/${employeeId}/leave-balance`,
    {
      params: {
        leaveTypeId,
        fiscalYearId,
      },
    }
  );

  return response.data;
};

export const getLeaveRequestById = async (
  leaveRequestId: number
): Promise<LeaveRequestDetails> => {
  const response = await apiClient.get<LeaveRequestDetails>(
    `/LeaveRequests/${leaveRequestId}`
  );

  return response.data;
};

export const getLeaveRequestsByEmployee = async (
    startDate: string,
    endDate: string
): Promise<LeaveRequestApproval[]> => { 
    const response = await apiClient.get<LeaveRequestApproval[]>(
        `/LeaveRequests/employee/${startDate}/${endDate}`
    );

    return response.data;
};

export const approveLeaveRequest = async (
    request: ApproveLeaveRequest
): Promise<LeaveRequestActionResponse> => { 

    const response = await apiClient.put("/LeaveRequests/approve", request);

     return response.data;
};

export const rejectLeaveRequest = async (
    request: ApproveLeaveRequest
): Promise<LeaveRequestActionResponse> => { 

    const response = await apiClient.put("/LeaveRequests/reject", request);

     return response.data;
};
 