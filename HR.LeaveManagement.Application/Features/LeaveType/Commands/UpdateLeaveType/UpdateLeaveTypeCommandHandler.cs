using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _repo;
        private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

        public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository repo, IAppLogger<UpdateLeaveTypeCommandHandler> logger)
        {
            this._mapper = mapper;
            this._repo = repo;
            this._logger = logger;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // Validate incoming data
            var validator = new UpdateLeaveTypeCommandValidator(_repo);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveType), request.Id);
                throw new BadRequestException("Invalid Leave Type", validationResult);
            }

            // Convert to domain entity object
            var LeaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

            //Add to data base
            await _repo.UpdateAsync(LeaveTypeToUpdate);

            // Return unit value
            return Unit.Value;
        }
    }
}
