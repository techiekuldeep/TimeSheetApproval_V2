using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.Parameters;
using TimeSheetApproval.Application.Wrappers;
using TimeSheetApproval.Domain.Entities;

namespace TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Queries
{
    public class GetAllTimesheetsStatusTypesbyFiltersQuery : IRequest<PagedResponse<IEnumerable<TimesheetsStatusTypes>>>
    {
        public SearchFilters SearchFilter { get; set; }
    }
    public class GetAllTimesheetsStatusTypesbyFiltersQueryHandler : IRequestHandler<GetAllTimesheetsStatusTypesbyFiltersQuery, PagedResponse<IEnumerable<TimesheetsStatusTypes>>>
    {
        private readonly IGenericRepositoryAsync<TimesheetsStatusTypes> _tssTyperepo;
        private readonly IMapper _mapper;
        public GetAllTimesheetsStatusTypesbyFiltersQueryHandler(IGenericRepositoryAsync<TimesheetsStatusTypes> TssTyperepo, IMapper mapper)
        {
            _tssTyperepo = TssTyperepo;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<TimesheetsStatusTypes>>> Handle(GetAllTimesheetsStatusTypesbyFiltersQuery request, CancellationToken cancellationToken)
        {
            var tssTypeObj = await _tssTyperepo.GetPaginatedResultsByFiltersAsync(request.SearchFilter);
            var tssTypeVM = _mapper.Map<IEnumerable<TimesheetsStatusTypes>>(tssTypeObj.PagedList);
            return new PagedResponse<IEnumerable<TimesheetsStatusTypes>>(tssTypeVM, request.SearchFilter.PageNumber, request.SearchFilter.PageSize, tssTypeObj.TotalPages, tssTypeObj.TotalRecordsCount);
        }

    }
}
