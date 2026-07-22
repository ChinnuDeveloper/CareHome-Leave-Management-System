# ﻿Requirements Verification Document

## Revision History

| Version | Date | Author Description |
|---|---|---|
| 0.1 | 08 Jul 2026 | Chinnu Rajan	Initial draft created |
| 1.0 | 13 Jul 2026 | Chinnu Rajan	Updated following stakeholder review and incorporated agreed clarifications |
| 1.1 | 20 Jul 2026 | Chinnu Rajan  Stakeholder reviewed, approved and baseline version created for development |

## Stakeholder Review Summary
A requirements review was conducted with the Care Home Manager/Stakeholder on 13 July 2026 via phone.
The business requirements, assumptions, and clarification points were discussed. The agreed changes have been incorporated into this document and the requirements have been updated accordingly.

| Document Information | Details |
|---|---|
| Project | Care Home Leave Management System |
| Document Version | 1.1 |
| Document Owner | Chinnu Rajan |
| Date Created | 08 July 2026 |
| Last Updated | 13 July 2026 |
| Document Status | Approved |
| Reviewers | Care Home Manager /Stakeholder |
| Review Date | 13 July 2026 |

## 1. Purpose
The purpose of this document is to capture and validate the agreed business requirements for the Care Home Leave Management System. The requirements have been reviewed with the stakeholder and reflect the agreed business processes that will be used during solution design and development.

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
| Staff | •	Submit leave requests |
| Manager | •	Review, approve or reject leave requests <br> •	Configure leave entitlement, manage leave records |	

## 5. Functional Requirements (Features and actions the system should provide)
| ID | Requirement |
|------ | ----- |
| FR-001 | Employees shall be able to select leave start date, end date, and submit a reason/comment if required. |
| FR-002 |	Managers shall be able to review pending leave requests. |
| FR-003 | Managers shall be able to approve leave requests. |
| FR-004 | Managers shall be able to reject leave requests and provide comments. |
| FR-005 | The system shall display the employee’s remaining leave balance before submitting a leave request. |
| FR-006 | The system shall automatically update the employee's remaining leave balance when a leave request is approved. |
| FR-007 | Managers shall be able to configure annual leave entitlement at the beginning of each financial year. |
| FR-008 | Managers shall be able to modify employee leave entitlement during the financial year when required. |
| FR-009 | Managers shall be able to cancel an approved leave request and provide a reason. |
| FR-010 | Managers shall be able to reschedule an approved leave request and provide a reason for the change. |
| FR-011 | The system shall allow eligible employees to request one day of Birthday Leave, either on their birthday or during their birthday week, in accordance with the care home's leave policy. |

## 5.1  Leave Types

The system shall support the following leave types:
| Leave Type | Description |
| --- | --- | 
| Annual Leave | Employee annual leave entitlement calculated based on the care home's leave policy. |
| Birthday Leave | Employee may request one day of leave on their birthday or during their birthday week, subject to the care home's policy and approval process. |

## 6. Business Rules (Rules the system must follow based on the care home's current process)

**BR-001**
The care home has four departments:  
-	Carers
-	Domestic Workers
-	Nurses
-	Kitchen Staff

**BR-002**  
•	A maximum number of employees may be on annual leave at the same time.

| Department | Maximum Employees on Leave |
| --- | --- |
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

**BR-007**

The system shall support a Birthday Leave type. 

Eligible employees may request one day of Birthday Leave to be taken either:  
- on their birthday, or
- during the same calendar week as their birthday

 Birthday Leave is subject to manager approval and department availability.
 
**BR-008**

Leave entitlement shall be managed on a financial-year basis.
The financial year shall run from 1 April to 31 March.
 
## 7. Stakeholder Clarification
| ID  |  Agreed Clarification  |
| --- | --- |
| CL-001 | Leave availability is checked against the employee's primary department. |
| CL-002 | Employees may cancel pending leave requests. |
| CL-003 | Leave overlap validation includes both pending and approved requests. |
| CL-004 | Managers may modify approved leave requests by cancelling or rescheduling them. |
| CL-005 | Birthday Leave is available once per year and may be taken on the employee's birthday or within their birthday week. |
| CL-006 | Managers must provide a reason when cancelling or rescheduling an approved leave request. |
| CL-007 | Half-day leave is not included in the initial release. |

## 8. Non-Functional Requirements
| ID | Requirement |
| --- | --- |
| NFR-001 | The system shall authenticate users before accessing leave information. |
| NFR-002 | Users shall only access information permitted by their role. |
| NFR-003 | The system shall maintain an audit trail of leave request changes. |
| NFR-004 | The system shall be available during normal working hours. |
| NFR-005 | The system shall protect employee personal information. |

## 9. Out of Scope (Initial Release)
The following features are not included in the initial version.  
- Shift scheduling
- Payroll integration
- Mobile application
- Multi-site care home support
- Half-day leave requests

## 10. Stakeholder Review

The requirements documented in this specification were reviewed with the Care Home Manager/Stakeholder on 13 July 2026.
The review confirmed the business processes, functional requirements, business rules and agreed clarifications. The feedback received during the review has been incorporated into this version of the document and will be used as the baseline for the system design and development phases.

## 11. Requirements Approval and Sign-Off

The requirements specification was reviewed and agreed with the Care Home Manager/Stakeholder on 20 July 2026.
The stakeholder confirmed that the documented requirements, business rules, assumptions and clarifications accurately represent the agreed business process.
This document is approved as the baseline requirement specification for the Care Home Leave Management System.
Any future changes will be managed through a formal change request process.
