using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagment.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Persistence.Repositories
{
    // Here we inherit from two because Generic provides generic functions and ILeaveTypeRepository brings the custom contracts.
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            return await _context.LeaveTypes.AnyAsync(q => q.Name == name)==false;
        }
    }


}
