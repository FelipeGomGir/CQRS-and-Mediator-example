using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _repo;

        public GetLeaveAllocationDetailsHandler(IMapper mapper, ILeaveAllocationRepository _repo)
        {
            this._mapper = mapper;
            this._repo = _repo;
        }

        public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
        {
            // Query to DB
            var leaveAllocation = await _repo.GetLeaveAllocationWithDetails(request.Id);
            return _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);
        }
    }
}
