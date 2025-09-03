using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class GetLeaveAllocationListHandler : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _repo;

        public GetLeaveAllocationListHandler(IMapper mapper, ILeaveAllocationRepository _repo)
        {
            this._mapper = mapper;
            this._repo = _repo;
        }
        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
        {
            // Query to database 
            var leaveAllocation = await _repo.GetLeaveAllocationsWithDetails();

            // Map from Entity to Dto
            var data = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocation);
            
            // Return data
            return data;
        }
    }
}
