using FluentValidation;

namespace TimeSheetApproval.Application.UseCases.Timesheet.Commands.DeleteTimesheet
{
    public class DeleteTimeSheetCommandValidator : AbstractValidator<DeleteTimesheetCommand>
    {
        public DeleteTimeSheetCommandValidator()
        {
            RuleFor(p => p.TimesheetId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
