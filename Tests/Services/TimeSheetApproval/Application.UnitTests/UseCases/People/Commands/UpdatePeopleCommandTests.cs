using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Exceptions;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.UseCases.People.Commands;
using TimeSheetApproval.Domain.Entities;
using Xunit;
namespace TimeSheetApproval.Application.UnitTests.UseCases.People.Commands
{
    public class UpdatePeopleCommandTests
    {
        [Fact]
        public async Task UpdateCurrencyCommand_ShouldSave_Successfully()
        {
            var mockMapper = new Mock<IMapper>();
            var mockRepostiory = new Mock<IGenericRepositoryAsync<Domain.Entities.People>>();
            var mstcmd = new UpdatePeopleCommand
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

            mockRepostiory.Setup(_ => _.GetByIdAsync(mstObj.PeopleId)).Returns(Task.FromResult(mstObj));

            mockRepostiory.Setup(_ => _.UpdateAsync(mstObj)).Callback(() =>
            {
                PersistObj.PeopleFirstName = mstcmd.PeopleFirstName;
            }).Returns(Task.FromResult(PersistObj));

            var returnRequest = new UpdatePeopleCommandHandler(mockRepostiory.Object, mockMapper.Object);
            var reqResponce = await returnRequest.Handle(mstcmd, default);
            reqResponce.Succeeded.ToString();
            Assert.Equal(PersistObj.PeopleFirstName.ToString(), mstcmd.PeopleFirstName);
        }

        [Fact]
        public async Task UpdateCurrencyCommand_Should_throw_Exception()
        {
            var mockMapper = new Mock<IMapper>();
            var mockRepostiory = new Mock<IGenericRepositoryAsync<Domain.Entities.People>>();
            var mstcmd = new UpdatePeopleCommand
            {
                PeopleId = 2,
                PeopleFirstName = "FirstName",
                PeopleLastName = "LastName",
                Gender = "Male",
                HourlyRate = 25.00,
                BankAccount = "999999"
            };

            var mstObj = new Domain.Entities.People
            {
                PeopleId = 1,
                PeopleFirstName = "",
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

            mockRepostiory.Setup(_ => _.GetByIdAsync(mstObj.PeopleId)).Returns(
              Task.FromResult(mstObj.PeopleId == PersistObj.PeopleId ? mstObj : null)
          );

            mockRepostiory.Setup(_ => _.UpdateAsync(mstObj)).Callback(() =>
            {
                PersistObj.PeopleFirstName = mstcmd.PeopleFirstName;
            }).Returns(Task.FromResult(PersistObj));

            var returnRequest = new UpdatePeopleCommandHandler(mockRepostiory.Object, mockMapper.Object);
            await Assert.ThrowsAsync<ApiException>(async () => await returnRequest.Handle(mstcmd, default));
        }
    }
}
