using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.Parameters;
using TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Queries;
using TimeSheetApproval.Domain.Entities;
using Xunit;
namespace TimeSheetApproval.Application.UnitTests.UseCases.TimesheetsStatusType.Queries
{
    public class GetAllTimesheetsStatusTypesbyFiltersQueryTests
    {
        private readonly SearchFilters SearchFilterobj = new SearchFilters
        {
            PageNumber = 1,
            PageSize = 10,
            SortParams = new List<Sorter>()
                {
                    new Sorter
                    {
                        SortColumn = "TssTypeId",
                        SortOrder = SortOrders.Asc
                    }
                },
            FilterParams = new Filters()
            {
                FilterColumn = "TssTypeId",
                Operator = "eq",
                FilterValue = "1",
                LogicOperator = "and",
                FilterList = null
            }
        };

        private readonly IList<TimesheetsStatusTypes> MasterListObj = new List<TimesheetsStatusTypes>()
        {
            new TimesheetsStatusTypes
            {
                TssTypeId = 1,
                TssTypeName = "Draft",
                CreatedDateTime = DateTime.Now,
                CreatedBy = "TechUser",
                UpdatedDateTime = DateTime.Now,
                UpdatedBy = "TechUser"
            }
        };
        [Fact]
        public async Task GetAllTimesheetsStatusTypesbyFiltersQuery_ShouldPassSuccessfully()
        {
            var reqFiltersQuery = new GetAllTimesheetsStatusTypesbyFiltersQuery
            {
                SearchFilter = SearchFilterobj
            };

            var MasterList = MasterListObj.AsQueryable();

            var mockMapper = new Mock<IMapper>();
            var mockScRequestRepostiory = new Mock<IGenericRepositoryAsync<TimesheetsStatusTypes>>();

            var pagedresponse = new PaginatedResult<TimesheetsStatusTypes>(MasterList, MasterList.Count(), reqFiltersQuery.SearchFilter.PageSize);
            var resultData = Task.FromResult(pagedresponse);

            mockScRequestRepostiory.Setup(_ => _.GetPaginatedResultsByFiltersAsync(SearchFilterobj)).ReturnsAsync(pagedresponse);

            var returnRequest = new GetAllTimesheetsStatusTypesbyFiltersQueryHandler(mockScRequestRepostiory.Object, mockMapper.Object);
            var reqResponce = await returnRequest.Handle(reqFiltersQuery, default);
            reqResponce.Succeeded.ShouldBeTrue();
        }
        [Fact]
        public async Task GetAllTimesheetsStatusTypesbyFiltersQuery_ShouldPassSuccessfullyWithDefultSortSearchParams()
        {
            var reqFiltersQuery = new GetAllTimesheetsStatusTypesbyFiltersQuery
            {
                SearchFilter = SearchFilterobj
            };

            var MasterList = MasterListObj.AsQueryable();
            var mockMapper = new Mock<IMapper>();
            var mockScRequestRepostiory = new Mock<IGenericRepositoryAsync<TimesheetsStatusTypes>>();

            var pagedresponse = new PaginatedResult<TimesheetsStatusTypes>(MasterList, MasterList.Count(), 10);
            var resultData = Task.FromResult(pagedresponse);

            mockScRequestRepostiory.Setup(_ => _.GetPaginatedResultsByFiltersAsync(It.IsAny<SearchFilters>())).ReturnsAsync(pagedresponse);

            GetAllTimesheetsStatusTypesbyFiltersQueryHandler returnRequest = new GetAllTimesheetsStatusTypesbyFiltersQueryHandler(mockScRequestRepostiory.Object, mockMapper.Object);
            var reqResponce = await returnRequest.Handle(reqFiltersQuery, default);
            reqResponce.Succeeded.ShouldBeTrue();
        }

    }
}
