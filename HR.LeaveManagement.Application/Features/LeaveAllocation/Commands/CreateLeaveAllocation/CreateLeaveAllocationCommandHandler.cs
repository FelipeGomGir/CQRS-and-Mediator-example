using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _repo;
        private readonly ILeaveTypeRepository _repoType;

        public CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository repo, ILeaveTypeRepository repoType )
        {
            this._mapper = mapper;
            this._repo = repo;
            this._repoType = repoType;
        }
        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            //Validate Data
            var validator = new CreateLeaveAllocationCommandValidator(_repo);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Allocation",validationResult);

            // Get Leave type for allocations
            var leaveType = await _repoType.GetByIdAsync(request.LeaveTypeId);

            // Get employees

            //Get Period

            //Assign allocations
            var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);
            await _repo.CreateAsync(leaveAllocation);
            return Unit.Value;
        }
    }
}
