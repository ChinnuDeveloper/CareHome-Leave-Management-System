### Database Design Document

|||
| --- | --- |
| Version	 |	1.0 |
| Project Name	 |	Carehome Leave Management System |
| Database		|SQL Server Express |
| Prepared By	 |	Chinnu Rajan |
| Date	|	04 August 2026 |

### 1. Purpose
This document describes the database structure for the Carehome Leave Management System. It explains the database tables, relationships, primary keys, foreign keys, constraints, and enumerations used to support leave requests, approvals, and entitlement management.
### 2. Database Overview
The system contains the following tables.
|Table Name |	Description |
| --- | --- |
| Department |	Stores department information |
| Employee |	Stores employee details |
| Login	|	Stores user login credentials |
| LeaveType	| Stores different leave types |
| LeaveRequest |	Stores employee leave applications |
| LeaveHistory |	Stores history of leave modifications |
| FiscalYear |	Stores yearly leave allocation periods |
| Entitlement	|	Stores leave entitlement for employees |
| EntitlementHistory |	Stores changes made to leave entitlement 
### 3. Entity Relationship Diagram
Refer to ER-Diagram.png in the project repository.
### 4. Table Specifications
**4.1 Department**<br>
Stores department information and maximum concurrent leave allowed.
| Column | Data Type | Constraint |	Description |
| --- | --- | --- | --- |
| DepartmentId |	int	| PK |	Department identifier |
| DepartmentName |	nvarchar(100) |	NOT NULL |	Department name |
| MaxConcurrentLeave |	int |	NOT NULL |	Maximum employees allowed on leave simultaneously |

**4.2 Employee**<br>
Stores employee information.
| Column | Data Type | Constraint |	Description |
| --- | --- | --- | --- |
| EmployeeId | int | PK |	Unique identifier for each employee |
| FirstName |	nvarchar(150) |	NOT NULL |	Employee’s first name |
| LastName |	nvarchar(150) |	NOT NULL |	Employee’s last name |
| DateOfBirth	| datetime |	NOT NULL |	Employee’s date of birth |
| DepartmentId |	int	| FK |	References the department the employee belongs to. |
| WeeklyHours |	decimal(18,2) |	NOT NULL |	Employee's contracted weekly working hours. |
| Role	| nvarchar(50) |	NOT NULL |	Employee's role in the system |
| Active |	bit	| Default(True) |	Indicates whether the employee is currently active.|
| ManagerId	| int |	FK (Self Reference)	| References the employee's manager |

**4.3 Login**<br>
Stores login credentials for employees. Each employee has one unique login account used to access the system.
| Column | Data Type | Constraint |	Description |
| --- | --- | --- | --- |
| LoginId |	int |	PK |	Unique identifier for each login record. |
| EmployeeId |	int |	FK, UNIQUE |	References the employee associated with the login account. The UNIQUE constraint ensures each employee has only one login |
| UserName |	nvarchar(250) |	UNIQUE |	Unique username(email id) used by the employee to log in to the system. |
| Password |	nvarchar(256) |	NOT NULL |	Stores the employee's hashed password for authentication. |

**4.4 LeaveType**<br>
Stores the different types of leave that employees can request.
| Column | Data Type | Constraint |	Description |
| --- | --- | --- | --- |
| LeaveTypeId |	int |	PK | Unique identifier for each leave type. |
| Name |	nvarchar(50) |	NOT NULL	| Name of the leave type(e.g, Annual Leave, Birthday Leave) |
| RequiresEntitlement |	boolean |	Default(False) | Indicates whether the leave type deducts from the employee's leave entitlement. True means entitlement is required; False means no entitlement is deducted.|

**4.5 FiscalYear**<br>
Stores fiscal years used for allocating employee leave entitlement.
| Column | Data Type | Constraint |	Description |
| --- | --- | --- | --- |
| FiscalYearId |	int	| PK	| Unique identifier for each fiscal year record. |
| Year |	nvarchar(200) |	NOT NULL |	Fiscal year for which leave entitlement is allocated (e.g., 2026–2027). |
| GeneratedBy	| int |	FK |	References the employee (typically manager) who generated the leave allocation for the fiscal year. |
| GeneratedOn	| DateTime | NOT NULL |	Date and time on which the fiscal year record and leave allocation was generated. |

**4.6 Entitlement**<br>
Stores the leave entitlement allocated to each employee for a specific leave type and fiscal year.
| Column | Data Type | Constraint |	Description |
| --- | --- | --- | --- |
| EntitlementId |	int |	PK	| Unique identifier for each leave entitlement record.|
| EmployeeId |	int |	FK	| References the employee to whom the leave entitlement is allocated.|
| LeaveTypeId	|int	| FK	| References the type of leave associated with the entitlement|
| FiscalYearId	| int	| FK	| References the fiscal year for which the leave entitlement is allocated.|
| AllocatedHrs	| decimal(18,2)	| NOT NULL |	Total number of leave hours allocated to the employee for the specified leave type and fiscal year.|
| TakenHrs	| decimal(18,2)	| NOT NULL	| Total number of leave hours already used by the employee from the allocated entitlement.|

**4.7 EntitlementHistory**<br>
Stores audit history of employee leave entitlement changes.
| Column | Data Type | Constraint |	Description |
| --- | --- | --- | --- |
| EntitlementHistoryId |	int |	PK |	Unique identifier for each entitlement history record.|
| EntitlementId	| int |	FK |	References the Entitlement table and identifies the entitlement record that was modified.|
| OldAllocationHrs |	decimal(18,2) |	NOT NULL	| Stores the allocated leave hours before the modification.|
| OldTakenHrs	| decimal(18,2) |	NOT NULL |	Stores the taken leave hours before the modification. |
| NewAllocationHrs |	decimal(18,2) |	NOT NULL	| Stores the allocated leave hours after the modification. |
| NewTakenHrs	| decimal(18,2) |	NOT NULL |	Stores the taken leave hours after the modification. |
| Reason	| nvarchar(250)	| NULL |	Describes the reason for changing the entitlement details. |
| ModifiedBy |	int	| FK	| References the User who performed the entitlement update.|
| ModifiedOn	| datetime |	NOT NULL	| Stores the date and time when the entitlement change was made.

**4.8 LeaveRequest**<br>
Stores leave applications submitted by employees. This table captures leave request details, including the requested leave period, leave type, approval status, manager comments, and approval information. It supports the workflow for submitting, reviewing, approving, or rejecting employee leave requests.
| Column | Data Type | Constraint |	Description |
| --- | --- | --- | --- |
| LeaveRequestId |	int |	PK |	Unique identifier for each leave request. |
| EmployeeId |	int	| FK	| References the Employee table and identifies the employee who submitted the leave request.|
| LeaveTypeId |	int |	FK | References the LeaveType table and identifies the type of leave requested. |
| StartDate	| datetime	| NOT NULL |	The start date and time of the requested leave period. |
| EndDate	| datetime |	NOT NULL	| The end date and time of the requested leave period. |
| Reason	| nvarchar(250) |	NULL |	Optional explanation provided by the employee for the leave request.|
| ManagerComment |	nvarchar(250)	| NULL |	Comments provided by the manager during the approval process. |
| Status |	int |	Enum	| Represents the current status of the leave request |
| ApproverId	| int	| FK	| References the User who reviewed and approved or rejected the leave request. |
| ApprovedOn	| datetime	| NULL	| Stores the date and time when the leave request was approved or processed.|

**4.9 LeaveHistory**<br>
Stores audit history of changes made to leave requests. This table maintains previous and updated leave dates, status changes, and modification details to provide traceability of leave request amendments throughout the approval process.
| Column | Data Type | Constraint |	Description |
| --- | --- | --- | --- |
| LeaveHistoryId |	int	| PK	| Unique identifier for each leave history record.|
| LeaveRequestId |	int	| FK	| References the LeaveRequest table and identifies the leave request for which the change history is stored.|
| OldStartDate	| datetime |	NOT NULL	|Stores the original leave start date before the modification.|
| OldEndDate	| datetime	|NOT NULL	| Stores the original leave end date before the modification.|
| NewStartDate	| datetime |	NOT NULL	| Stores the updated leave start date after the modification.|
| NewEndDate	| datetime	| NOT NULL	| Stores the updated leave end date after the modification.|
| Reason	| nvarchar(250)	| NULL	| Describes the reason for the leave request change or status update.|
| Status	| int	| Enum	| Represents the status of the leave request at the time of the history record|
| CreatedBy	| int	|FK	| References the User who created the leave history record.|
| CreatedOn	|datetime	|NOT NULL	|Stores the date and time when the leave history record was created.|

### 5. Relationships
| Parent |	Child |	Relationship |
| --- | --- | --- |
| Department	| Employee	| One-to-Many|
| Employee	| Login	| One-to-One|
| Employee	| Employee (Manager)	| One-to-Many |
| Employee	| LeaveRequest	| One-to-Many |
| Employee	| Entitlement	| One-to-Many |
| Employee	| FiscalYear	| One-to-Many (GeneratedBy) |
| Employee	| LeaveHistory	| One-to-Many |
| Employee	| EntitlementHistory	| One-to-Many |
| LeaveType	| LeaveRequest	| One-to-Many |
| LeaveType	| Entitlement	| One-to-Many |
| FiscalYear	| Entitlement	| One-to-Many |
| Entitlement	| EntitlementHistory	| One-to-Many |
| LeaveRequest	| LeaveHistory	| One-to-Many |

### 6. Enumerations
**6.1 Leave Status**
| Value |	Description |
| --- | --- |
| 0	| Pending |
| 1	| Approved |
| 2	| Rejected |
| 3	| Cancelled |

Used in:<br>
•	LeaveRequest.Status <br>
•	LeaveHistory.Status 

**6.2 Employee Role**
| Value	| Description |
| --- | --- |
| 0	| Staff |
| 1	| Manager |

Used in:<br>
•	Employee.Role

#### 7. Business Rules
•	An employee belongs to one department. <br>
•	A department can have many employees. <br>
•	Each employee has one login account. <br>
•	An employee may have a manager. <br>
•	Only one manager can approve a leave request.<br> 
•	Department leave limits must not exceed MaxConcurrentLeave. <br>
•	Annual leave entitlement is allocated per fiscal year. <br>
•	Changes to entitlement are recorded in EntitlementHistory. <br>
•	Updates to approved leave requests are recorded in LeaveHistory. 











