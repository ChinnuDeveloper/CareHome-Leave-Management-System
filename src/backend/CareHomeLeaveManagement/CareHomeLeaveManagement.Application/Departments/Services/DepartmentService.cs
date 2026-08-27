using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Departments.DTOs;
using CareHomeLeaveManagement.Application.Departments.Interfaces;

namespace CareHomeLeaveManagement.Application.Departments.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
           var departments= await _repository.GetAllAsync();

            return departments.Select(d => new DepartmentDto
            {
                DepartmentId = d.DepartmentId,
                DepartmentName = d.DepartmentName,
                MaxConcurrentLeave=d.MaxConcurrentLeave
            });
        }
    }
}
