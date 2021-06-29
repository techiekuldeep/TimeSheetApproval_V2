using FluentValidation;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Domain.Entities;

namespace TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Commands
{
    public class CreateTimesheetsStatusTypeCommandValidator : AbstractValidator<CreateTimesheetsStatusTypeCommand>
    {
        private readonly IGenericRepositoryAsync<TimesheetsStatusTypes> _genRepo;
        public CreateTimesheetsStatusTypeCommandValidator(IGenericRepositoryAsync<TimesheetsStatusTypes> genRepo)
        {
            _genRepo = genRepo;
            RuleFor(p => p.TssTypeName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 30 characters.")
                .MustAsync(async (x, cancellation) =>
                {
                    bool exists = await _genRepo.IsRecordExistForString("TssTypeName", x);
                    return !exists;
                }).WithMessage("{PropertyName} Must be unique");
        }
    }
}
