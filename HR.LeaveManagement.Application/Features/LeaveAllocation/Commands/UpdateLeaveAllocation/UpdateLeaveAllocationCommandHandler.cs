using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public UpdateLeaveAllocationCommandHandler(
            IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository,
            ILeaveAllocationRepository leaveAllocationRepository)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {   /// Validation
            var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository,_leaveAllocationRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Allocation", validationResult);
            }
            // The clean way of updating data is to fetch the original record on DB
            ///Get the Object from the DB
            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.EmployeeId);
            // It would not pass this part if it does not exists. This is already validated on the validator
            // so it can be removed.
            if (leaveAllocation is null)
            {
                throw new NotFoundException(nameof(LeaveAllocation), request.EmployeeId);
            }
            // Then map the request object to leaveAllocation
            // what this would do is to retain the original values and override the new values
            /// Add the new data to the object that already exists.
            _mapper.Map(request,leaveAllocation);
            // It does notreturn a value so Unit.Value is used 
            return Unit.Value;
        }
    }
}
