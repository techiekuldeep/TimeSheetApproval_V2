using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Parameters;
using TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Commands;
using TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Queries;
using TimeSheetApproval.Application.Wrappers;
using TimeSheetApproval.Domain.Entities;
using TimeSheetApproval.WebApi.Controllers.v1;
using Xunit;
namespace TimeSheetApproval.WebApi.UnitTests.Controllers.v1
{
    public class TimesheetsStatusTypesControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly TimesheetsStatusTypesController _controller;
        public TimesheetsStatusTypesControllerTests()
        {
            _mediator = new Mock<IMediator>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(_ => _.GetService(typeof(IMediator))).Returns(_mediator.Object);
            _controller = new TimesheetsStatusTypesController()
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
        public async Task GetAllTimesheetsStatusTypes_ReturnsOkResult()
        {
            SearchFilters Param = new SearchFilters();
            _mediator.Setup(_ => _.Send(new GetAllTimesheetsStatusTypesbyFiltersQuery() { SearchFilter = Param }, default)).ReturnsAsync(It.IsAny<PagedResponse<IEnumerable<TimesheetsStatusTypes>>>);

            var actionResult = await _controller.Get(Param);

            await _mediator.Object.Send(It.IsAny<IRequest<GetAllTimesheetsStatusTypesbyFiltersQuery>>(), default);
            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var branchObj = okObjectResult.Value as PagedResponse<IEnumerable<TimesheetsStatusTypes>>;
            Assert.Null(branchObj);

            _mediator.Verify(x => x.Send(It.IsAny<GetAllTimesheetsStatusTypesbyFiltersQuery>(), It.IsAny<CancellationToken>()));
            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<GetAllTimesheetsStatusTypesbyFiltersQuery>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void GetAllTimesheetsStatusTypes_without_Controller_Get_should_Fail()
        {
            SearchFilters Param = new SearchFilters();
            _mediator.Setup(_ => _.Send(new GetAllTimesheetsStatusTypesbyFiltersQuery() { SearchFilter = Param }, default)).ReturnsAsync(It.IsAny<PagedResponse<IEnumerable<TimesheetsStatusTypes>>>);

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(_ => _.GetService(typeof(IMediator))).Returns(_mediator.Object);

            await _mediator.Object.Send(It.IsAny<IRequest<GetAllTimesheetsStatusTypesbyFiltersQuery>>(), default);

            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<GetAllTimesheetsStatusTypesbyFiltersQuery>>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetAllTimesheetsStatusTypesbyFiltersQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }
        [Fact]
        public async void GetAllTimesheetsStatusTypes_without_Mediator_Send_should_Fail()
        {
            SearchFilters Param = new SearchFilters();
            _mediator.Setup(_ => _.Send(new GetAllTimesheetsStatusTypesbyFiltersQuery() { SearchFilter = Param }, default)).ReturnsAsync(It.IsAny<PagedResponse<IEnumerable<TimesheetsStatusTypes>>>);

            var result = await _controller.Get(Param);

            _mediator.Verify(x => x.Send(It.IsAny<GetAllTimesheetsStatusTypesbyFiltersQuery>(), It.IsAny<CancellationToken>()));
            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<GetAllTimesheetsStatusTypesbyFiltersQuery>>(), It.IsAny<CancellationToken>()), Times.Never);
        }
        [Fact]
        public async void CreateTimesheetsStatusTypes_ReturnsOkResult()
        {
            CreateTimesheetsStatusTypeCommand cmd = new CreateTimesheetsStatusTypeCommand();
            _mediator.Setup(_ => _.Send(cmd, default)).ReturnsAsync(It.IsAny<Response<long>>);

            var result = await _controller.Post(cmd);

            await _mediator.Object.Send(It.IsAny<IRequest<CreateTimesheetsStatusTypeCommand>>(), default);

            _mediator.Verify(x => x.Send(It.IsAny<CreateTimesheetsStatusTypeCommand>(), It.IsAny<CancellationToken>()));
            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<CreateTimesheetsStatusTypeCommand>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void UpdateTimesheetsStatusTypes_ReturnsOkResult()
        {
            int _tssTypeId = 1;
            var cmd = new UpdateTimesheetsStatusTypeCommand
            {
                TssTypeId = 1,
                TssTypeName = "Draft"
            };
            _mediator.Setup(_ => _.Send(cmd, default)).ReturnsAsync(It.IsAny<Response<long>>);

            var result = await _controller.Put(_tssTypeId, cmd);

            await _mediator.Object.Send(It.IsAny<IRequest<UpdateTimesheetsStatusTypeCommand>>(), default);

            _mediator.Verify(x => x.Send(It.IsAny<UpdateTimesheetsStatusTypeCommand>(), It.IsAny<CancellationToken>()));
            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<UpdateTimesheetsStatusTypeCommand>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
