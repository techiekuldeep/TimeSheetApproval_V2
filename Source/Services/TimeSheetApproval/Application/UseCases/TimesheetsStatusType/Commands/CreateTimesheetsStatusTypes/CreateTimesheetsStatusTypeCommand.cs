using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.Wrappers;
using TimeSheetApproval.Domain.Entities;

namespace TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Commands
{
    public class CreateTimesheetsStatusTypeCommand : IRequest<Response<long>>
    {
        public long TssTypeId { get; set; }
        public string TssTypeName { get; set; }
    }
    public class CreateTimesheetsStatusTypeCommandHandler : IRequestHandler<CreateTimesheetsStatusTypeCommand, Response<long>>
    {
        private readonly IGenericRepositoryAsync<TimesheetsStatusTypes> _tssTypeRepository;
        private readonly IMapper _mapper;
        public CreateTimesheetsStatusTypeCommandHandler(IGenericRepositoryAsync<TimesheetsStatusTypes> TssTypeRepository, IMapper mapper)
        {
            _tssTypeRepository = TssTypeRepository;
            _mapper = mapper;
        }
        public async Task<Response<long>> Handle(CreateTimesheetsStatusTypeCommand request, CancellationToken cancellationToken)
        {
            var tsType = _mapper.Map<TimesheetsStatusTypes>(request);
            await _tssTypeRepository.AddAsync(tsType);
            return new Response<long>(tsType.TssTypeId);
        }
    }
}
