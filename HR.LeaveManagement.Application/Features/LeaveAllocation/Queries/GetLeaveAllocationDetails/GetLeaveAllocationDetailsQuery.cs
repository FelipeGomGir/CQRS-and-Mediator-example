using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsQuery: IRequest<LeaveAllocationDetailsDto>
    {
        // Here we put the params that the Query requires
        public int Id { get; set; }
    }
}
