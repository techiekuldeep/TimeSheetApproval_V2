using FluentValidation;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Domain.Entities;

namespace TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Commands
{
    public class UpdateTimesheetsStatusTypeCommandValidator : AbstractValidator<CreateTimesheetsStatusTypeCommand>
    {
        private readonly IGenericRepositoryAsync<TimesheetsStatusTypes> _genRepo;
        public UpdateTimesheetsStatusTypeCommandValidator(IGenericRepositoryAsync<TimesheetsStatusTypes> genRepo)
        {
            _genRepo = genRepo;
            RuleFor(p => p)
                .MustAsync(async (x, cancellation) =>
                {
                    var entity = await _genRepo.GetRecordByColumn("TssTypeName", x.TssTypeName);
                    if (entity != null)
                        return entity.TssTypeName == x.TssTypeName && entity.TssTypeId == x.TssTypeId;
                    return true;
                })
            .WithMessage(p => $"Already Record exist with TssTypeName - {p.TssTypeName}");
        }
    }
}
