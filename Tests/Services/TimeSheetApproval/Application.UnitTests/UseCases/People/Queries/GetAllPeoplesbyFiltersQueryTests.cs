using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.Parameters;
using TimeSheetApproval.Application.UseCases.People.Queries;
using TimeSheetApproval.Domain.Entities;
using Xunit;

namespace TimeSheetApproval.Application.UnitTests.UseCases.People.Queries
{
    public class GetAllPeoplesbyFiltersQueryTests
    {
        private readonly SearchFilters SearchFilterobj = new SearchFilters
        {
            PageNumber = 1,
            PageSize = 10,
            SortParams = new List<Sorter>()
                {
                    new Sorter
                    {
                        SortColumn = "PeopleId",
                        SortOrder = SortOrders.Asc
                    }
                },
            FilterParams = new Parameters.Filters()
            {
                FilterColumn = "PeopleId",
                Operator = "eq",
                FilterValue = "1",
                LogicOperator = "and",
                FilterList = null
            }
        };

        private readonly IList<Domain.Entities.People> MasterListObj = new List<Domain.Entities.People>()
                {
                   new Domain.Entities.People
                   {
                       PeopleId = 1,
                        PeopleFirstName = "FirstName",
                        PeopleLastName = "LastName",
                        Gender="Male",
                        HourlyRate = 25.00,
                        BankAccount = "999999",
                        CreatedDateTime = DateTime.Now,
                        CreatedBy = "TechUser",
                        UpdatedDateTime = DateTime.Now,
                        UpdatedBy = "TechUser"
                   }
                };
        [Fact]
        public async Task GetAllPeoplesbyFiltersQuery_ShouldPassSuccessfully()
        {
            var reqFiltersQuery = new GetAllPeoplesbyFiltersQuery
            {
                SearchFilter = SearchFilterobj
            };

            var MasterList = MasterListObj.AsQueryable();

            var mockMapper = new Mock<IMapper>();
            var mockScRequestRepostiory = new Mock<IGenericRepositoryAsync<Domain.Entities.People>>();

            var pagedresponse = new PaginatedResult<Domain.Entities.People>(MasterList, MasterList.Count(), reqFiltersQuery.SearchFilter.PageSize);
            var resultData = Task.FromResult(pagedresponse);

            mockScRequestRepostiory.Setup(_ => _.GetPaginatedResultsByFiltersAsync(SearchFilterobj)).ReturnsAsync(pagedresponse);

            var returnRequest = new GetAllPeoplesbyFiltersQueryHandler(mockScRequestRepostiory.Object, mockMapper.Object);
            var reqResponce = await returnRequest.Handle(reqFiltersQuery, default);
            reqResponce.Succeeded.ShouldBeTrue();
        }
        [Fact]
        public async Task GetAllPeoplesbyFiltersQuery_ShouldPassSuccessfullyWithDefultSortSearchParams()
        {
            var reqFiltersQuery = new GetAllPeoplesbyFiltersQuery
            {
                SearchFilter = SearchFilterobj
            };

            var MasterList = MasterListObj.AsQueryable();
            var mockMapper = new Mock<IMapper>();
            var mockScRequestRepostiory = new Mock<IGenericRepositoryAsync<Domain.Entities.People>>();

            var pagedresponse = new PaginatedResult<Domain.Entities.People>(MasterList, MasterList.Count(), 10);
            var resultData = Task.FromResult(pagedresponse);

            mockScRequestRepostiory.Setup(_ => _.GetPaginatedResultsByFiltersAsync(It.IsAny<SearchFilters>())).ReturnsAsync(pagedresponse);

            GetAllPeoplesbyFiltersQueryHandler returnRequest = new GetAllPeoplesbyFiltersQueryHandler(mockScRequestRepostiory.Object, mockMapper.Object);
            var reqResponce = await returnRequest.Handle(reqFiltersQuery, default);
            reqResponce.Succeeded.ShouldBeTrue();
        }

    }
}
