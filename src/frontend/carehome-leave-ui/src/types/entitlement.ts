export interface Entitlements{
    entitlementId: number,
    employeeId: number,
    employeeName: string,
    leaveTypeId: number,
    leaveType: string,
    fiscalYearId: number,
    fiscalYear: string,
    weeklyHours: number,
    allocatedHrs: number,
    takenHrs: number,
    departmentName: string,
    active: boolean
} 
export interface FiscalYear {
    fiscalYearId: number;
    year: string;
}
export interface CreateEntitlement {
  fiscalYearId: number|null;
  generatedBy: number;
  generatedOn: string;
  calculationRule: number;
}

export interface UpdateEntitlement{
  allocatedHrs: number;
  takenHrs: number;
  weeklyHours: number;
  reason: string;
  modifiedBy: number;
}