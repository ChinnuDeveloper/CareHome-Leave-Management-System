import apiClient from "../api/apiClient";
import type { CreateEmployeeRequest, UpdateEmployeeRequest } from "../types/EmployeeLeaveBalance";
import type { Employee, EmployeeDetails } from "../types/LeaveRequest";

export const getEmployee = async (
  fiscalYearId: number,
  leaveTypeId:number
): Promise<Employee[]> => {
  const response = await apiClient.get<Employee[]>(
    `/Employees/${fiscalYearId}/${leaveTypeId}`
  );

  return response.data;
};

export const createEmployee = async (
  employee: CreateEmployeeRequest
): Promise<Employee> => {
  const response = await apiClient.post<Employee>(
    "/Employees",
    employee
  );

  return response.data;
};

export const getEmployeeDetails = async (
  employeeId: number,
  fiscalYearId: number,
  leaveTypeId:number
): Promise<EmployeeDetails> => {
  const response = await apiClient.get<EmployeeDetails>(
    `/Employees/${employeeId}/${fiscalYearId}/${leaveTypeId}`
  );

  return response.data;
};

export const updateEmployee = async (
  id: number,
  fiscalYearId: number,
  leaveTypeId: number,
  employee: UpdateEmployeeRequest
): Promise<Employee> => {
  const response = await apiClient.put<Employee>(
    `/Employees/${id}/${fiscalYearId}/${leaveTypeId}`,
    employee
  );

  return response.data;
};
 
export const deleteEmployee = async (
  id: number,
  fiscalYearId: number,
  leaveTypeId: number
): Promise<void> => {
  await apiClient.delete(
    `/Employees/${id}/${fiscalYearId}/${leaveTypeId}`
  );
};