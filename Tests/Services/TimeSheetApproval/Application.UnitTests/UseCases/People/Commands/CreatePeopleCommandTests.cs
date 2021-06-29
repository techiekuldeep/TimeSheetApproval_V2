using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.UseCases.People.Commands;
using Xunit;

namespace TimeSheetApproval.Application.UnitTests.UseCases.People.Commands
{
    public class CreatePeopleCommandTests
    {
        [Fact]
        public async Task CreatePeopleCommand_ShouldSave_Successfully()
        {
            var mockMapper = new Mock<IMapper>();
            var mockRepostiory = new Mock<IGenericRepositoryAsync<Domain.Entities.People>>();
            var mstcmd = new CreatePeopleCommand
            {
                PeopleId = 1,
                PeopleFirstName = "FirstName",
                PeopleLastName = "LastName",
                Gender = "Male",
                HourlyRate = 25.00,
                BankAccount = "999999"
            };

            var mstObj = new Domain.Entities.People
            {
                PeopleId = 1,
                PeopleFirstName = "FirstName",
                PeopleLastName = "LastName",
                Gender = "Male",
                HourlyRate = 25.00,
                BankAccount = "999999",
                CreatedDateTime = DateTime.Now,
                CreatedBy = "TechUser",
                UpdatedDateTime = DateTime.Now,
                UpdatedBy = "TechUser"
            };

            var PersistObj = new Domain.Entities.People
            {
                PeopleId = 1,
                PeopleFirstName = "FirstName",
                PeopleLastName = "LastName",
                Gender = "Male",
                HourlyRate = 25.00,
                BankAccount = "999999",
                CreatedDateTime = DateTime.Now,
                CreatedBy = "TechUser",
                UpdatedDateTime = DateTime.Now,
                UpdatedBy = "TechUser"
            };

            mockMapper.Setup(_ => _.Map<Domain.Entities.People>(mstcmd)).Returns(mstObj);
            mockRepostiory.Setup(_ => _.AddAsync(mstObj)).Callback(() => { mstObj.PeopleId = 1; }).ReturnsAsync(PersistObj);

            var returnRequest = new CreatePeopleCommandHandler(mockRepostiory.Object, mockMapper.Object);
            var reqResponce = await returnRequest.Handle(mstcmd, default);
            reqResponce.Succeeded.ToString();
            Assert.Equal(PersistObj.PeopleId.ToString(), reqResponce.Data.ToString());
        }
    }
}
