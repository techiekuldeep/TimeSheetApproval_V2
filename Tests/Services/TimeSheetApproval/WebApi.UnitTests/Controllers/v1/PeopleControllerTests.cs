using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Parameters;
using TimeSheetApproval.Application.UseCases.People.Commands;
using TimeSheetApproval.Application.UseCases.People.Queries;
using TimeSheetApproval.Application.Wrappers;
using TimeSheetApproval.Domain.Entities;
using TimeSheetApproval.WebApi.Controllers.v1;
using Xunit;
namespace TimeSheetApproval.WebApi.UnitTests.Controllers.v1
{
    public class PeopleControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly PeopleController _controller;
        public PeopleControllerTests()
        {
            _mediator = new Mock<IMediator>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(_ => _.GetService(typeof(IMediator))).Returns(_mediator.Object);
            _controller = new PeopleController()
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
        public async Task GetAllPeoples_ReturnsOkResult()
        {
            SearchFilters Param = new SearchFilters();
            _mediator.Setup(_ => _.Send(new GetAllPeoplesbyFiltersQuery() { SearchFilter = Param }, default)).ReturnsAsync(It.IsAny<PagedResponse<IEnumerable<People>>>);

            var actionResult = await _controller.Get(Param);

            await _mediator.Object.Send(It.IsAny<IRequest<GetAllPeoplesbyFiltersQuery>>(), default);
            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var branchObj = okObjectResult.Value as PagedResponse<IEnumerable<People>>;
            Assert.Null(branchObj);

            _mediator.Verify(x => x.Send(It.IsAny<GetAllPeoplesbyFiltersQuery>(), It.IsAny<CancellationToken>()));
            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<GetAllPeoplesbyFiltersQuery>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void GetAllPeoples_without_Controller_Get_should_Fail()
        {
            SearchFilters Param = new SearchFilters();
            _mediator.Setup(_ => _.Send(new GetAllPeoplesbyFiltersQuery() { SearchFilter = Param }, default)).ReturnsAsync(It.IsAny<PagedResponse<IEnumerable<People>>>);

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(_ => _.GetService(typeof(IMediator))).Returns(_mediator.Object);

            await _mediator.Object.Send(It.IsAny<IRequest<GetAllPeoplesbyFiltersQuery>>(), default);

            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<GetAllPeoplesbyFiltersQuery>>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetAllPeoplesbyFiltersQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }
        [Fact]
        public async void GetAllPeoples_without_Mediator_Send_should_Fail()
        {
            SearchFilters Param = new SearchFilters();
            _mediator.Setup(_ => _.Send(new GetAllPeoplesbyFiltersQuery() { SearchFilter = Param }, default)).ReturnsAsync(It.IsAny<PagedResponse<IEnumerable<People>>>);

            var result = await _controller.Get(Param);

            _mediator.Verify(x => x.Send(It.IsAny<GetAllPeoplesbyFiltersQuery>(), It.IsAny<CancellationToken>()));
            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<GetAllPeoplesbyFiltersQuery>>(), It.IsAny<CancellationToken>()), Times.Never);
        }
        [Fact]
        public async void CreatePeoples_ReturnsOkResult()
        {
            CreatePeopleCommand cmd = new CreatePeopleCommand();
            _mediator.Setup(_ => _.Send(cmd, default)).ReturnsAsync(It.IsAny<Response<long>>);

            var result = await _controller.Post(cmd);

            await _mediator.Object.Send(It.IsAny<IRequest<CreatePeopleCommand>>(), default);

            _mediator.Verify(x => x.Send(It.IsAny<CreatePeopleCommand>(), It.IsAny<CancellationToken>()));
            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<CreatePeopleCommand>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void UpdatePeoples_ReturnsOkResult()
        {
            int _peopleId = 1;
            var cmd = new UpdatePeopleCommand
            {
                PeopleId = 1,
                PeopleFirstName = "FirstName",
                PeopleLastName = "LastName",
                Gender = "Male",
                HourlyRate = 25.00,
                BankAccount = "999999"
            };
            _mediator.Setup(_ => _.Send(cmd, default)).ReturnsAsync(It.IsAny<Response<long>>);

            var result = await _controller.Put(_peopleId, cmd);

            await _mediator.Object.Send(It.IsAny<IRequest<UpdatePeopleCommand>>(), default);

            _mediator.Verify(x => x.Send(It.IsAny<UpdatePeopleCommand>(), It.IsAny<CancellationToken>()));
            _mediator.Verify(mock => mock.Send(It.IsAny<IRequest<UpdatePeopleCommand>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
