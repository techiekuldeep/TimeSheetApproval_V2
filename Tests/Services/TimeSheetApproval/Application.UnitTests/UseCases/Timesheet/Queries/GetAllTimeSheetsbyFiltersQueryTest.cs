using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.Parameters;
using TimeSheetApproval.Application.UseCases.Timesheet.Queries;
using TimeSheetApproval.Domain.Entities;
using Xunit;

namespace TimeSheetApproval.Application.UnitTests.UseCases.Timesheet.Queries
{
    public class GetAllTimeSheetsbyFiltersQueryTest
    {
        Mock<IMapper> mockMapper;
        Mock<IGenericRepositoryAsync<Domain.Entities.Timesheet>> mockRepostiory;
        IQueryable<Domain.Entities.Timesheet> MasterList;
        public GetAllTimeSheetsbyFiltersQueryTest()
        {
            mockMapper = new Mock<IMapper>();
            mockRepostiory = new Mock<IGenericRepositoryAsync<Domain.Entities.Timesheet>>();
            MasterList = new List<Domain.Entities.Timesheet>()
                {
                    new Domain.Entities.Timesheet
                    {
                            TimesheetId = 1,
                            PeopleId = 1,
                            TssTypeId = 1,
                            TimesheetDate = Convert.ToDateTime("2021-06-27 7:19:17.1070000"),
                            WorkFromTime = Convert.ToDateTime("2021-06-27 7:19:17.1070000"),
                            WorkToTime = Convert.ToDateTime("2021-06-27 17:19:17.1070000"),
                            WorkTotalTime = 10M,
                            CreatedBy = "TechUser",
                            CreatedDateTime = DateTime.Now,
                            UpdatedBy = "TechUser",
                            UpdatedDateTime = DateTime.Now
                    },
                    new Domain.Entities.Timesheet
                    {
                            TimesheetId = 2,
                            PeopleId = 1,
                            TssTypeId = 1,
                            TimesheetDate = Convert.ToDateTime("2021-06-28 7:19:17.1070000"),
                            WorkFromTime = Convert.ToDateTime("2021-06-28 7:19:17.1070000"),
                            WorkToTime = Convert.ToDateTime("2021-06-28 17:19:17.1070000"),
                            WorkTotalTime = 10M,
                            CreatedBy = "TechUser",
                            CreatedDateTime = DateTime.Now,
                            UpdatedBy = "TechUser",
                            UpdatedDateTime = DateTime.Now
                    },
                    new Domain.Entities.Timesheet
                    {
                            TimesheetId = 3,
                            PeopleId = 2,
                            TssTypeId = 1,
                            TimesheetDate = Convert.ToDateTime("2021-06-28 7:19:17.1070000"),
                            WorkFromTime = Convert.ToDateTime("2021-06-28 7:19:17.1070000"),
                            WorkToTime = Convert.ToDateTime("2021-06-28 17:19:17.1070000"),
                            WorkTotalTime = 10M,
                            CreatedBy = "TechUser",
                            CreatedDateTime = DateTime.Now,
                            UpdatedBy = "TechUser",
                            UpdatedDateTime = DateTime.Now
                    }
                }.AsQueryable();
        }
        [Fact]
        public async Task GetAllTimeSheetsbyFiltersQuery_ShouldPassSuccessfully()
        {
            var reqFilltersParam = new SearchFilters
            {
                PageNumber = 1,
                PageSize = 10,
                TotalCount = 0,
                SortParams = new List<Sorter>()
                                {
                                   new Sorter{ SortColumn = "TimeSheetId",
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

            var reqFiltersQuery = new GetAllTimeSheetsbyFiltersQuery()
            {
                SearchFilter = reqFilltersParam
            };

            var pagedresponse = new PaginatedResult<Domain.Entities.Timesheet>(MasterList, MasterList.Count(), reqFiltersQuery.SearchFilter.PageSize);
            var resultData = Task.FromResult(pagedresponse);
            mockRepostiory.Setup(_ => _.GetPaginatedResultsByFiltersAsync(reqFiltersQuery.SearchFilter)).ReturnsAsync(pagedresponse);
            var returnRequest = new GetAllTimeSheetsbyFiltersQueryHandler(mockRepostiory.Object, mockMapper.Object);
            var reqResponce = await returnRequest.Handle(reqFiltersQuery, default);
            reqResponce.Succeeded.ToString();
        }
        [Fact]
        public async Task GetAllTimeSheetsbyFiltersQuery_ShouldPassSuccessfullyWithDefultSortSearchParams()
        {
            var reqFilltersParam = new SearchFilters
            {
                PageNumber = 1,
                PageSize = 10,
                TotalCount = 0,
                SortParams = new List<Sorter>()
                                {
                                   new Sorter{ SortColumn = "TimeSheetId",
                                    SortOrder = SortOrders.Asc
                                   }
                                },
                FilterParams = new Parameters.Filters()
                {
                    FilterColumn = "Peopleid",
                    Operator = "eq",
                    FilterValue = "1",
                    LogicOperator = "and",
                    FilterList = null
                }
            };

            var reqFiltersQuery = new GetAllTimeSheetsbyFiltersQuery()
            {
                SearchFilter = reqFilltersParam
            };

            var pagedresponse = new PaginatedResult<Domain.Entities.Timesheet>(MasterList, It.IsAny<int>(), It.IsAny<int>());
            var resultData = Task.FromResult(pagedresponse);
            mockRepostiory.Setup(_ => _.GetPaginatedResultsByFiltersAsync(reqFiltersQuery.SearchFilter)).ReturnsAsync(pagedresponse);
            var returnRequest = new GetAllTimeSheetsbyFiltersQueryHandler(mockRepostiory.Object, mockMapper.Object);
            var reqResponce = await returnRequest.Handle(reqFiltersQuery, default);
            reqResponce.Succeeded.ToString();
        }

        [Fact]
        public async Task GetAllTimeSheetsbyFiltersQuery_Between_WorkFrom_WorkToDates_ShouldPassSuccessfully()
        {
            var reqFilltersParam = new SearchFilters
            {
                PageNumber = 1,
                PageSize = 10,
                TotalCount = 0,
                SortParams = new List<Sorter>()
                                {
                                   new Sorter{ SortColumn = "TimeSheetId",
                                    SortOrder = SortOrders.Asc
                                   }
                                },
                FilterParams = new Parameters.Filters()
                {
                    FilterColumn = "",
                    Operator = "",
                    FilterValue = "",
                    LogicOperator = "and",
                    FilterList = new List<Filters>()
                    {
                        new Filters(){
                        FilterColumn = "WorkFromTime",
                        Operator = "gte",
                        FilterValue = "2021-06-27",
                        LogicOperator = "and",
                        FilterList=null
                        },
                        new Filters(){
                        FilterColumn = "WorkToTime",
                        Operator = "lte",
                        FilterValue = "2021-06-28",
                        LogicOperator = "and",
                        FilterList=null
                        }
                    }
                }
            };

            var reqFiltersQuery = new GetAllTimeSheetsbyFiltersQuery()
            {
                SearchFilter = reqFilltersParam
            };
            var FilterResponse= MasterList.ToFilterView(reqFilltersParam);
            var pagedresponse = new PaginatedResult<Domain.Entities.Timesheet>(FilterResponse, FilterResponse.Count(), reqFiltersQuery.SearchFilter.PageSize);
            var pplVM = mockMapper.Setup(_ => _.Map<IEnumerable<Domain.Entities.Timesheet>>(pagedresponse.PagedList)).Returns(FilterResponse.AsEnumerable());
            var resultData = Task.FromResult(pagedresponse);
            mockRepostiory.Setup(_ => _.GetPaginatedResultsByFiltersAsync(reqFiltersQuery.SearchFilter)).ReturnsAsync(pagedresponse);
            var returnRequest = new GetAllTimeSheetsbyFiltersQueryHandler(mockRepostiory.Object, mockMapper.Object);
            var reqResponce = await returnRequest.Handle(reqFiltersQuery, default);
            reqResponce.Succeeded.ToString();
            Assert.True(reqResponce.Data.ToList().Count == 1);
        }
    }
}
