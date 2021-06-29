using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.Parameters;
using TimeSheetApproval.Application.Wrappers;
using TimeSheetApproval.Domain.Entities;

namespace TimeSheetApproval.Application.UseCases.Timesheet.Queries
{
    public class GetAllTimeSheetsbyFiltersQuery : IRequest<PagedResponse<IEnumerable<Domain.Entities.Timesheet>>>
    {
        public SearchFilters SearchFilter { get; set; }
    }
    public class GetAllTimeSheetsbyFiltersQueryHandler : IRequestHandler<GetAllTimeSheetsbyFiltersQuery, PagedResponse<IEnumerable<Domain.Entities.Timesheet>>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Timesheet> _genRepo;
        private readonly IMapper _mapper;
        public GetAllTimeSheetsbyFiltersQueryHandler(IGenericRepositoryAsync<Domain.Entities.Timesheet> genRepo, IMapper mapper)
        {
            _genRepo = genRepo;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<Domain.Entities.Timesheet>>> Handle(GetAllTimeSheetsbyFiltersQuery request, CancellationToken cancellationToken)
        {
            var timesheetObj = await _genRepo.GetPaginatedResultsByFiltersAsync(request.SearchFilter);
            var timesheetVM = _mapper.Map<IEnumerable<Domain.Entities.Timesheet>>(timesheetObj.PagedList);
            return new PagedResponse<IEnumerable<Domain.Entities.Timesheet>>(timesheetVM, request.SearchFilter.PageNumber, request.SearchFilter.PageSize, timesheetObj.TotalPages, timesheetObj.TotalRecordsCount);
        }
    }
}
