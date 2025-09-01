using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _repo;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository repo)
        {
            this._repo = repo;
        }
        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // Validate Data
            var validator = new DeleteLeaveTypeCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                throw new NotFoundException("Leave Type invalid", validatorResult);

            // Retrive domain entity object
            var leaveTypeToDelete = await _repo.GetByIdAsync(request.Id);

            // Verify that record exists
            if (leaveTypeToDelete == null)
                throw new NotFoundException(nameof(LeaveType), request.Id);

            // Remove from data base 
            await _repo.DeleteAsync(leaveTypeToDelete);

            //return record id
            return Unit.Value;
            
        }
    }
}
