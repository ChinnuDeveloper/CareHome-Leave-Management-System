# ﻿Requirements Verification Document

| Document Information | Details |
|---|---|
| Project | Care Home Leave Management System |
| Document Version | 0.1 (Draft) |
| Document Owner | Chinnu Rajan |
| Date Created | 08 July 2026 |
| Document Status | Pending Stakeholder Review |
| Reviewers | Care Home Manager / Stakeholder |
| Review Date | TBD | 

## 1. Purpose
The purpose of this document is to document and validate the business requirements discussed with the stakeholder. Stakeholders are requested to review and confirm that the requirements accurately represent the expected business processes before solution design and development begin.

## 2. Business Problem
The care home currently manages annual leave using a manual paper-based process.

The current process presents several challenges:

- Annual leave applications are completed manually. 
- The approval process is time-consuming. 
- Calculating remaining leave balance is performed manually. 
- Identifying conflicts between employee leave requests and department availability is difficult. 
- Managers spend significant time checking department availability before approving leave. 

The objective is to replace the manual process with a web-based leave management system.

## 3. Project Objectives

The proposed system should:

✅ Allow employees to submit annual leave requests online.  
✅ Allow managers to review leave requests.  
✅ Allow managers to approve or reject requests. 
✅ Automatically calculate remaining leave.  
✅ Automatically identify conflicting leave requests and prevent requests that exceed department leave capacity limits.  
✅ Reduce manual administration.


## 4. Stakeholders

| Role | Responsibilities |
|---|---|
| Employee | Submit leave requests |
| Manager | Review, approve/reject requests, manage entitlement |

 
## 5. Functional Requirements (Features and actions the system should provide)

| ID | Requirement |
|---|---|
| FR-001 | Employees shall be able to select leave start date, end date, and submit a reason/comment if required. |
| FR-002 | Managers shall be able to review pending leave requests. |
| FR-003 | Managers shall be able to approve leave requests. |
| FR-004 | Managers shall be able to reject leave requests and provide comments. |
| FR-005 | The system shall display the employee’s remaining leave balance before submitting a leave request. |
| FR-006 | The system shall automatically update the employee's remaining leave balance when a leave request is approved. |
| FR-007 | Managers shall be able to configure annual leave entitlement at the beginning of each financial year. |
| FR-008 | Managers shall be able to modify employee leave entitlement during the financial year when required. |
| FR-009 | Managers shall be able to cancel an approved leave request |
| FR-010 | Managers shall be able to reschedule an approved leave request. |
 	 

## 6. Business Rules (Rules the system must follow based on the care home's current process)

**BR-001**

The care home has four departments:
- Carers
- Domestic Workers
- Nurses
- Kitchen Staff

**BR-002**

•	A maximum number of employees may be on annual leave at the same time.

| Department | Maximum Employees on Leave |
|---|---:|
| Carers | 2 |
| Domestic Workers | 1 |
| Nurses | 1 |
| Kitchen Staff | 1 | 

**BR-003**

Before a leave request is submitted, the system shall check the number of approved leave requests for the selected dates.

**BR-004**

When an employee submits a leave request, the system shall check the number of employees from the same department who already have approved leave or pending leave requests for the selected dates.

**BR-005**

If the requested leave period exceeds the maximum permitted number of employees allowed to be absent from that department, the system shall:

- display an appropriate message to the employee, and 
- prevent submission of the leave request. 

**BR-006**

Remaining leave entitlement shall be calculated as:

Remaining Leave =Entitled Leave− Approved Leave 

## 7. Assumptions

The following assumptions have been made based on the initial discussion.

- Leave entitlement is managed on a financial-year basis.

## 8. Questions Requiring Clarification

The following items require confirmation before development begins.

| ID | Question |
|---|---|
| Q-001 | For employees who work in multiple departments, should leave availability be checked against their primary department or all associated departments? |
| Q-002 | Can employee cancel a pending request? |
| Q-003 | When an employee submits a leave request, should the system check for overlapping leave requests from the same department? If so, should the overlap check include only approved leave requests, or should it also include pending leave requests? |
| Q-004 | Can managers modify leave requests, or only approve/reject them? |
| Q-005 | Should manager provide a reason when cancelling or rescheduling leave? |
| Q-006 | Can employee apply for half-day leave? | 

## 9. Non-Functional Requirements

| ID | Requirement |
|---|---|
| NFR-001 | The system shall authenticate users before accessing leave information. |
| NFR-002 | Users shall only access information permitted by their role. |
| NFR-003 | The system shall maintain an audit trail of leave request changes. |
| NFR-004 | The system shall be available during normal working hours. |
| NFR-005 | The system shall protect employee personal information. | 

## 10. Out of Scope (Initial Release)

The following features are not included in the initial version. 

- Shift scheduling 
- Payroll integration
- Mobile application 
- Multi-site care home support 

## 11. Stakeholder Review
This document has been prepared based on the initial understanding of the care home leave management process.

The stakeholder is requested to review the documented requirements and confirm:

- The requirements accurately reflect the expected business processes.
- Any missing requirements or additional functionality have been identified.
- Any incorrect assumptions have been highlighted.
- The clarification questions listed in this document have been answered.
  
Any feedback or agreed changes will be incorporated into the requirements document before proceeding to the design and development phase.

