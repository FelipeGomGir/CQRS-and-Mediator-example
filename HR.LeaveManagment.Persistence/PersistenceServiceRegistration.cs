using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagment.Persistence.DatabaseContext;
using HR.LeaveManagment.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HrDatabaseContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("HrDataBaseConnectionString"));
            });
            // Scoped registration of the a GenericRepository 
            // In words I'm registering anthing that is a type of IGenericRepository and it's going to be implemented
            // by anthing that is a type of GenericRepository.
            // So when we add Persistence services we have the access to the IGenericRepository and GenericRepository through dependency injection.
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

            return services;
        }
    }
}
