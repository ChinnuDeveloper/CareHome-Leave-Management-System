export interface LeaveRequestDetails{
    leaveRequestId: number,
    employeeId: number,
    employee:string,
    leaveTypeId:number,
    leaveType:string,
    startDate:string,
    endDate:string,
    reason:string,
    managerComment:string,
    status:string,
    approvedOn:string,
    hoursTaken:number
}