using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class GetLeaveTypeDetailsHandler : IRequestHandler<GetLeaveTypesDetailsQuery, List<LeaveTypeDetailsDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _repo;

        public GetLeaveTypeDetailsHandler(IMapper mapper, ILeaveTypeRepository repo)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        public async Task<List<LeaveTypeDetailsDto>> Handle(GetLeaveTypesDetailsQuery request, CancellationToken cancellationToken)
        {
            // Query to database
            var leaveTypesDetail = await _repo.GetByIdAsync(request.Id);

            // Verify that record exists
            if (leaveTypesDetail == null)
                throw new NotFoundException(nameof(LeaveType), request.Id);

            // Convert from entity to Dto 
            var data = _mapper.Map<List<LeaveTypeDetailsDto>>(leaveTypesDetail);
            //_mapper.Map<TDestination>(source);

            //return Dto
            return data;
        }
    }
}
