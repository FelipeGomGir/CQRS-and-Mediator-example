using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _repo;

        public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository repo)
        {
            this._mapper = mapper;
            this._repo = repo;
        }
        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // validate incoming data

            // Convert to domain entity type object
            var LeaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

            // add to database 
            await _repo.CreateAsync(LeaveTypeToCreate);

            //return record id
            return LeaveTypeToCreate.Id;
        }
    }
}
