using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Exceptions;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.Wrappers;
using TimeSheetApproval.Domain.Entities;
namespace TimeSheetApproval.Application.UseCases.People.Commands
{
    public class DeletePeopleCommand : IRequest<Response<bool>>
    {
        public long PeopleId { get; set; }
    }
    public class DeletePeopleCommandHandler : IRequestHandler<DeletePeopleCommand, Response<bool>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.People> _pplRepo;
        private readonly IMapper _mapper;
        public DeletePeopleCommandHandler(IGenericRepositoryAsync<Domain.Entities.People> PplRepo, IMapper mapper)
        {
            _pplRepo = PplRepo;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(DeletePeopleCommand request, CancellationToken cancellationToken)
        {
            var pplType = await _pplRepo.GetByIdAsync(request.PeopleId);
            if (pplType == null)
            { throw new ApiException($"People with Id = {request.PeopleId} not found."); }
            else
            {
                var pplObj = _mapper.Map<Domain.Entities.People>(request);
                await _pplRepo.DeleteAsync(pplObj);
                return new Response<bool>(true);
            }
        }
    }
}
