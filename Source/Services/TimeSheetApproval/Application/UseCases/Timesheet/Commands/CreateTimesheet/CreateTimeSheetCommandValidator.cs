using FluentValidation;

namespace TimeSheetApproval.Application.UseCases.Timesheet.Commands
{
    public class CreateTimeSheetCommandValidator : AbstractValidator<CreateTimesheetCommand>
    {
        public CreateTimeSheetCommandValidator()
        {
            RuleFor(p => p.PeopleId)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is Required.");
            RuleFor(p => p.TssTypeId)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is Required.");
                //.Equal(1).WithMessage("Please enter Valid Timesheet Status TypeId.");
            RuleFor(p => p.TimesheetDate)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is Required.");
            RuleFor(p => p.WorkFromTime)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is Required.");
            RuleFor(p => p.WorkToTime)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is Required.");
            RuleFor(p => p.WorkTotalTime)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is Required.");
        }
    }
}
