using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Departments.Interfaces;
using CareHomeLeaveManagement.Infrastructure.Repositories;
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

            return services;

        }
    }
}
