using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class LeaveAllocationDto
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string NumberOfDays { get; set; }
        public LeaveTypeDto? LeaveType { get; set; } /// This is a foreing key between LeaveType and Leave Allocation 
        public int LeaveTypeId { get; set; } /// This is a foreing key between LeaveType and Leave Allocation 
        public int Period { get; set; }
    }
}
