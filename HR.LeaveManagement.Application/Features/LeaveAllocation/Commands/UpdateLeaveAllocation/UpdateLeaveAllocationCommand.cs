using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommand : IRequest<Unit>
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string NumberOfDays { get; set; }
        public int LeaveTypeId { get; set; } /// This is a foreing key between LeaveType and Leave Allocation 
        public int Period { get; set; }
    }
}
