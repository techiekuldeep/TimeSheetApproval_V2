using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.Parameters;
using TimeSheetApproval.Application.Wrappers;
using TimeSheetApproval.Domain.Entities;

namespace TimeSheetApproval.Application.UseCases.People.Queries
{
    public class GetAllPeoplesbyFiltersQuery : IRequest<PagedResponse<IEnumerable<Domain.Entities.People>>>
    {
        public SearchFilters SearchFilter { get; set; }
    }
    public class GetAllPeoplesbyFiltersQueryHandler : IRequestHandler<GetAllPeoplesbyFiltersQuery, PagedResponse<IEnumerable<Domain.Entities.People>>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.People> _pplTyperepo;
        private readonly IMapper _mapper;
        public GetAllPeoplesbyFiltersQueryHandler(IGenericRepositoryAsync<Domain.Entities.People> PplTyperepo, IMapper mapper)
        {
            _pplTyperepo = PplTyperepo;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<Domain.Entities.People>>> Handle(GetAllPeoplesbyFiltersQuery request, CancellationToken cancellationToken)
        {
            var pplObj = await _pplTyperepo.GetPaginatedResultsByFiltersAsync(request.SearchFilter);
            var pplVM = _mapper.Map<IEnumerable<Domain.Entities.People>>(pplObj.PagedList);
            return new PagedResponse<IEnumerable<Domain.Entities.People>>(pplVM, request.SearchFilter.PageNumber, request.SearchFilter.PageSize, pplObj.TotalPages, pplObj.TotalRecordsCount);
        }

    }
}
