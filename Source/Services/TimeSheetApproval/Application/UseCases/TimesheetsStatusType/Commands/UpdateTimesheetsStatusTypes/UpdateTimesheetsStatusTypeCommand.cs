using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Exceptions;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.Wrappers;
using TimeSheetApproval.Domain.Entities;

namespace TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Commands
{
    public class UpdateTimesheetsStatusTypeCommand : IRequest<Response<long>>
    {
        public long TssTypeId { get; set; }
        public string TssTypeName { get; set; }
    }
    public class UpdateTimesheetsStatusTypeCommandHandler : IRequestHandler<UpdateTimesheetsStatusTypeCommand, Response<long>>
    {
        private readonly IGenericRepositoryAsync<TimesheetsStatusTypes> _tssTypeRepository;
        private readonly IMapper _mapper;
        public UpdateTimesheetsStatusTypeCommandHandler(IMapper mapper, IGenericRepositoryAsync<TimesheetsStatusTypes> TssTypeRepository)
        {
            _tssTypeRepository = TssTypeRepository;
            _mapper = mapper;
        }
        public async Task<Response<long>> Handle(UpdateTimesheetsStatusTypeCommand request, CancellationToken cancellationToken)
        {
            var tssType = await _tssTypeRepository.GetByIdAsync(request.TssTypeId);
            if (tssType == null)
            { throw new ApiException($"Timesheet Type Status Id = {request.TssTypeId} not found."); }
            else
            {
                var tsType = _mapper.Map<TimesheetsStatusTypes>(request);
                await _tssTypeRepository.UpdateAsync(tsType);
                return new Response<long>(tsType.TssTypeId);
            }
        }
    }
}
