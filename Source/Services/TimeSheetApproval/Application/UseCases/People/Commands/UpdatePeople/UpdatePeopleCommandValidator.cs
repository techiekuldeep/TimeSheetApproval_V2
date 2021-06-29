using FluentValidation;

namespace TimeSheetApproval.Application.UseCases.People.Commands
{
    public class UpdatePeopleCommandValidator : AbstractValidator<UpdatePeopleCommand>
    {
        public UpdatePeopleCommandValidator()
        {
            RuleFor(p => p.PeopleFirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            RuleFor(p => p.PeopleLastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            RuleFor(p => p.Gender)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
        }
    }
}