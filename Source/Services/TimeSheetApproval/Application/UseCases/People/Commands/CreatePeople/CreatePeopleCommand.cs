using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.Wrappers;
using TimeSheetApproval.Domain.Entities;

namespace TimeSheetApproval.Application.UseCases.People.Commands
{

    public class CreatePeopleCommand : IRequest<Response<long>>
    {
        public long PeopleId { get; set; }
        public string PeopleFirstName { get; set; }
        public string PeopleLastName { get; set; }
        public string Gender { get; set; }
        public double HourlyRate { get; set; }
        public string BankAccount { get; set; }
    }
    public class CreatePeopleCommandHandler : IRequestHandler<CreatePeopleCommand, Response<long>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.People> _pplRepo;
        private readonly IMapper _mapper;
        public CreatePeopleCommandHandler(IGenericRepositoryAsync<Domain.Entities.People> PplRepo, IMapper mapper)
        {
            _pplRepo = PplRepo;
            _mapper = mapper;
        }
        public async Task<Response<long>> Handle(CreatePeopleCommand request, CancellationToken cancellationToken)
        {
            var pplObj = _mapper.Map<Domain.Entities.People>(request);
            await _pplRepo.AddAsync(pplObj);
            return new Response<long>(pplObj.PeopleId);
        }
    }
}
