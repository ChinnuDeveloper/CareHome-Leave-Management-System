using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Departments.Interfaces;
using CareHomeLeaveManagement.Application.Departments.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CareHomeLeaveManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IDepartmentService, DepartmentService>();

            return services;
        }
    }
}
