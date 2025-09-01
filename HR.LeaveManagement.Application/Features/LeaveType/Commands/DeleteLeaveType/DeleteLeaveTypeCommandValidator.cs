using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandValidator : AbstractValidator<DeleteLeaveTypeCommand>
    {
        public DeleteLeaveTypeCommandValidator()
        {
            RuleFor(p => p)
                .NotNull()
                .WithMessage("The Leave Type does not exist.");
        }
    }
}
