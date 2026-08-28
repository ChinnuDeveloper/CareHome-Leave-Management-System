using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Common.Interfaces;
using CareHomeLeaveManagement.Application.Departments.Interfaces;
using CareHomeLeaveManagement.Application.Employees.Interfaces;
using CareHomeLeaveManagement.Application.LeaveTypes.Interfaces;
using CareHomeLeaveManagement.Infrastructure.Persistence;
using CareHomeLeaveManagement.Infrastructure.Repositories;
using CareHomeLeaveManagement.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareHomeLeaveManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
              this IServiceCollection services,
              IConfiguration configuration)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
             
            return services;

        }
    }
}
