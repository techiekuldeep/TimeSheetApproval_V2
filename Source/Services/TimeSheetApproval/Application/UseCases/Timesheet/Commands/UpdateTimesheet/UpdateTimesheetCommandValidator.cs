using AutoMapper;
using FluentValidation;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Domain.Entities;

namespace TimeSheetApproval.Application.UseCases.Timesheet.Commands
{
    public class UpdateTimesheetCommandValidator : AbstractValidator<UpdateTimesheetCommand>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Timesheet> _genRepo;
        private readonly IMapper _mapper;
        public UpdateTimesheetCommandValidator(IGenericRepositoryAsync<Domain.Entities.Timesheet> genRepo, IMapper mapper)
        {
            _genRepo = genRepo;
            _mapper = mapper;
            RuleFor(p => p.PeopleId)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is Required.");
            RuleFor(p => p.TssTypeId)
                    .NotEmpty().NotNull().WithMessage("{PropertyName} is Required.");
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
