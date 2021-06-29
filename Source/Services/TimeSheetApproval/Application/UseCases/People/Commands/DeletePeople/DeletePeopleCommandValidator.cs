using FluentValidation;

namespace TimeSheetApproval.Application.UseCases.People.Commands
{
    public class DeletePeopleCommandValidator : AbstractValidator<DeletePeopleCommand>
    {
        public DeletePeopleCommandValidator()
        {
            RuleFor(p => p.PeopleId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}