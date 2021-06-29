using FluentValidation;

namespace TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Commands.DeleteTimesheetsStatusTypes
{
    public class DeleteTimesheetsStatusTypesCommandValidator : AbstractValidator<DeleteTimesheetsStatusTypesCommand>
    {
        public DeleteTimesheetsStatusTypesCommandValidator()
        {
            RuleFor(p => p.TssTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
