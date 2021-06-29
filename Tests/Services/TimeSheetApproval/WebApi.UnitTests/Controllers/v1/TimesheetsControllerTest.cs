using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Parameters;
using TimeSheetApproval.Application.UseCases.Timesheet.Commands;
using TimeSheetApproval.Application.UseCases.Timesheet.Queries;
using TimeSheetApproval.Application.Wrappers;
using TimeSheetApproval.Domain.Entities;
using TimeSheetApproval.WebApi.Controllers.v1;
using Xunit;

namespace TimeSheetApproval.WebApi.UnitTests.Controllers.v1
{
    public class TimesheetsControllerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly TimesheetsController _controller;
        public TimesheetsControllerTest()
        {
            _mediator = new Mock<IMediator>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(_ => _.GetService(typeof(IMediator))).Returns(_mediator.Object);
            _controller = new TimesheetsController()
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestServices = serviceProviderMock.Object
                    }
                }
            };
        }

        [Fact]
        public async Task GetAllTimeSheets_ReturnsOkResult()
        {
            SearchFilters Param = new SearchFilters();
            _mediator.Setup(_ => _.Send(new GetAllTimeSheetsbyFiltersQuery() { SearchFilter = Param }, default)).ReturnsAsync(It.IsAny<PagedResponse<IEnumerable<Timesheet>>>);

            var actionResult = await _controller.Get(Param);

            await _mediator.Object.Send(It.IsAny<IRequest<GetAllTimeSheetsbyFiltersQuery>>(), default);
            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var branchObj = okObjectResult.Value as PagedResponse<IEnumerable<Timesheet>>;
            Assert.Null(branchObj);

            _mediator.Verify(x => x.Send(It.IsAny<GetAllTimeSheetsbyFiltersQuery>(), It.IsAny<CancellationToken>()));
            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<GetAllTimeSheetsbyFiltersQuery>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void GetAllTimeSheets_without_Controller_Get_should_Fail()
        {
            SearchFilters Param = new SearchFilters();
            _mediator.Setup(_ => _.Send(new GetAllTimeSheetsbyFiltersQuery() { SearchFilter = Param }, default)).ReturnsAsync(It.IsAny<PagedResponse<IEnumerable<Timesheet>>>);

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(_ => _.GetService(typeof(IMediator))).Returns(_mediator.Object);

            await _mediator.Object.Send(It.IsAny<IRequest<GetAllTimeSheetsbyFiltersQuery>>(), default);

            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<GetAllTimeSheetsbyFiltersQuery>>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetAllTimeSheetsbyFiltersQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }
        [Fact]
        public async void GetAllTimeSheets_without_Mediator_Send_should_Fail()
        {
            SearchFilters Param = new SearchFilters();
            _mediator.Setup(_ => _.Send(new GetAllTimeSheetsbyFiltersQuery() { SearchFilter = Param }, default)).ReturnsAsync(It.IsAny<PagedResponse<IEnumerable<Timesheet>>>);

            var result = await _controller.Get(Param);

            _mediator.Verify(x => x.Send(It.IsAny<GetAllTimeSheetsbyFiltersQuery>(), It.IsAny<CancellationToken>()));
            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<GetAllTimeSheetsbyFiltersQuery>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async void CreateTimeSheets_ReturnsOkResult()
        {
            CreateTimesheetCommand cmd = new CreateTimesheetCommand();
            _mediator.Setup(_ => _.Send(cmd, default)).ReturnsAsync(It.IsAny<Response<long>>);

            var result = await _controller.Post(cmd);

            await _mediator.Object.Send(It.IsAny<IRequest<CreateTimesheetCommand>>(), default);

            _mediator.Verify(x => x.Send(It.IsAny<CreateTimesheetCommand>(), It.IsAny<CancellationToken>()));
            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<CreateTimesheetCommand>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void UpdateTimeSheets_ReturnsOkResult()
        {
            int _timesheetId = 1;
            var cmd = new UpdateTimesheetCommand
            {
                TimesheetId = 1,
                PeopleId = 1,
                TssTypeId = 2,
                TimesheetDate = DateTime.Now,
                WorkFromTime = DateTime.Now,
                WorkToTime = DateTime.Now,
                WorkTotalTime = 10M
            };
            _mediator.Setup(_ => _.Send(cmd, default)).ReturnsAsync(It.IsAny<Response<long>>);

            var result = await _controller.Put(_timesheetId, cmd);

            await _mediator.Object.Send(It.IsAny<IRequest<UpdateTimesheetCommand>>(), default);

            _mediator.Verify(x => x.Send(It.IsAny<UpdateTimesheetCommand>(), It.IsAny<CancellationToken>()));
            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<UpdateTimesheetCommand>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
