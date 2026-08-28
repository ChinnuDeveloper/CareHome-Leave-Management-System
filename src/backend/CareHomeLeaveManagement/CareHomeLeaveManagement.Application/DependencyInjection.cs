using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Departments.Interfaces;
using CareHomeLeaveManagement.Application.Departments.Services;
using CareHomeLeaveManagement.Application.Employees.Interfaces;
using CareHomeLeaveManagement.Application.Employees.Services;
using CareHomeLeaveManagement.Application.LeaveTypes.Interfaces;
using CareHomeLeaveManagement.Application.LeaveTypes.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CareHomeLeaveManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ILeaveTypeService, LeaveTypeService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
