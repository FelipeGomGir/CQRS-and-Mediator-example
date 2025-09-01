using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
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

        public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository repo)
        {
            this._mapper = mapper;
            this._repo = repo;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // Validate incoming data

            // Convert to domain entity object
            var LeaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

            //Add to data base
            await _repo.UpdateAsync(LeaveTypeToUpdate);

            // Return unit value
            return Unit.Value;
        }
    }
}
