using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Common.Interfaces;
using CareHomeLeaveManagement.Application.Employees.DTOs;
using CareHomeLeaveManagement.Application.Employees.Interfaces;
using CareHomeLeaveManagement.Domain.Entities;
using CareHomeLeaveManagement.Domain.Enums;

namespace CareHomeLeaveManagement.Application.Employees.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILoginRepository _loginRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public EmployeeService(IEmployeeRepository employeeRepository, ILoginRepository loginRepository, 
            IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _employeeRepository = employeeRepository;
            _loginRepository = loginRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if(employee == null)
                return false; 
            

            await _employeeRepository.DeleteAsync(employee);
           
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<EmployeeDto?> CreateAsync(CreateEmployeeRequest request)
        {
            var employee = new Employee(
                request.FirstName,
                request.LastName,
                request.DateOfBirth,
                request.DepartmentId,
                request.WeeklyHours,
                request.Role,
                request.ManagerId
                );

            var passwordHash = _passwordHasher.HashPassword(request.Password);
            
            var login = new Login(
                employee,
                request.EmailId,
                passwordHash
                );

            await _employeeRepository.AddAsync(employee);
            await _loginRepository.AddAsync(login);

            await _unitOfWork.SaveChangesAsync();

            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                DepartmentId = employee.DepartmentId, 
                WeeklyHours = employee.WeeklyHours,
                Role = employee.Role,
                Active = employee.Active,
                ManagerId = employee.ManagerId,
                ManagerName = employee.Manager != null ? $"{employee.Manager.FirstName} {employee.Manager.LastName}": null,
                UserName = login.UserName
            };
        }
 

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            return employees.Select(e => new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                DateOfBirth = e.DateOfBirth,
                DepartmentId = e.DepartmentId,
                WeeklyHours = e.WeeklyHours,
                Role = e.Role,
                Active = e.Active,
                ManagerId = e.ManagerId,
                ManagerName = e.Manager != null ? $"{e.Manager.FirstName} {e.Manager.LastName}" : null,
                UserName = e.Login?.UserName
            });
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
           var employee= await _employeeRepository.GetByIdAsync(id);

            if(employee == null)
            {
                return null;
            }

            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department.DepartmentName,
                WeeklyHours = employee.WeeklyHours,
                Role = employee.Role,
                Active = employee.Active,
                ManagerId = employee.ManagerId,
                ManagerName = employee.Manager != null ? $"{employee.Manager.FirstName} {employee.Manager.LastName}" : null,
                UserName= employee.Login?.UserName
            };
        }

        public async Task<EmployeeDto?> UpdateAsync(int id, UpdateEmployeeRequest request)
        {
            var employee=await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return null;
            }

            employee.Update(
                request.FirstName,
                request.LastName,
                request.DateOfBirth,
                request.DepartmentId,
                request.WeeklyHours,
                request.Role,
                request.ManagerId,
                request.Active);

            if(employee.Login !=null)
            {
                if(!string.IsNullOrEmpty(request.EmailId))
                {
                    employee.Login.ChangeUserName(request.EmailId);
                }

                if(!string.IsNullOrEmpty(request.Password))
                {
                    var passwordHash= _passwordHasher.HashPassword(request.Password);

                    employee.Login.ChangePassword(passwordHash);
                }
            }

            
            await _unitOfWork.SaveChangesAsync();

            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                DepartmentId = employee.DepartmentId,
                WeeklyHours = employee.WeeklyHours,
                Role = employee.Role,
                Active = employee.Active,
                ManagerId = employee.ManagerId,
                ManagerName = employee.Manager != null ? $"{employee.Manager.FirstName} {employee.Manager.LastName}" : null,
            };

        }
    }
}
