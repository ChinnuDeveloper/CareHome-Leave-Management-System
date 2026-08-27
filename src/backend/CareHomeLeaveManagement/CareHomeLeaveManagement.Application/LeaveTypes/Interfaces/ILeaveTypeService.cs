using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Departments.DTOs;
using CareHomeLeaveManagement.Application.LeaveTypes.DTOs;

namespace CareHomeLeaveManagement.Application.LeaveTypes.Interfaces
{
    public interface ILeaveTypeService
    {
        Task<IEnumerable<LeaveTypeDto>> GetAllAsync();
    }
}
