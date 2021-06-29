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
    public class DeleteTimesheetsStatusTypesCommand : IRequest<Response<bool>>
    {
        public long TssTypeId { get; set; }
    }
    public class DeleteTimesheetsStatusTypesCommandHandler : IRequestHandler<DeleteTimesheetsStatusTypesCommand, Response<bool>>
    {
        private readonly IGenericRepositoryAsync<TimesheetsStatusTypes> _tssTypeRepository;
        private readonly IMapper _mapper;
        public DeleteTimesheetsStatusTypesCommandHandler(IMapper mapper, IGenericRepositoryAsync<TimesheetsStatusTypes> TssTypeRepository)
        {
            _tssTypeRepository = TssTypeRepository;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(DeleteTimesheetsStatusTypesCommand request, CancellationToken cancellationToken)
        {
            var tssType = await _tssTypeRepository.GetByIdAsync(request.TssTypeId);
            if (tssType == null)
            { throw new ApiException($"Timesheet Type Status Id = {request.TssTypeId} not found."); }
            else
            {
                var tsType = _mapper.Map<TimesheetsStatusTypes>(request);
                await _tssTypeRepository.DeleteAsync(tsType);
                return new Response<bool>(true);
            }
        }
    }
}
