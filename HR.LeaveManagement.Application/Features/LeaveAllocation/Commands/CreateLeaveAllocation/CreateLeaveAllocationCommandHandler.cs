using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
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
        private readonly IUserService _userService;

        public CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository repo, ILeaveTypeRepository repoType, IUserService userService )
        {
            _mapper = mapper;
            _repo = repo;
            _repoType = repoType;
            _userService = userService;
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
            var employees = await _userService.GetEmployees();

            //Get Period
            var period = DateTime.Now.Year;

            //Assign allocations IF an allocation doesn't exist for period and leave type
            var allocations = new List<Domain.LeaveAllocation>();
            foreach (var emp in employees)
            {
                var allocationExists = await _repo.AllocationExists(emp.Id, request.LeaveTypeId,period);
                if (allocationExists == false)
                {
                    allocations.Add(new Domain.LeaveAllocation
                    {
                        EmployeeId = emp.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period,
                    });
                }
            }
            if (allocations.Any())
            {
                await _repo.AddAllocations(allocations);
            }
            
            //var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);
            //await _repo.CreateAsync(leaveAllocation);
            return Unit.Value;
        }
    }
}
