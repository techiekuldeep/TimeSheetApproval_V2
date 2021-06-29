using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.UseCases.Timesheet.Commands;
using TimeSheetApproval.Domain.Entities;
using Xunit;

namespace TimeSheetApproval.Application.UnitTests.UseCases.Timesheet.Commands
{
    public class CreateTimesheetCommandTest
    {
        Mock<IMapper> mockMapper;
        Mock<IGenericRepositoryAsync<Domain.Entities.Timesheet>> mockRepostiory;
        public CreateTimesheetCommandTest()
        {
            mockMapper = new Mock<IMapper>();
            mockRepostiory = new Mock<IGenericRepositoryAsync<Domain.Entities.Timesheet>>();
        }
        [Fact]
        public async Task CreateTimesheetCommand_ShouldSave_Successfully()
        {
            var cmd = new CreateTimesheetCommand
            {
                TimesheetId = 1,
                PeopleId = 1,
                TssTypeId = 1,
                TimesheetDate = DateTime.Now,
                WorkFromTime = DateTime.Now,
                WorkToTime = DateTime.Now,
                WorkTotalTime = 10M
            };

            var obj = new Domain.Entities.Timesheet
            {
                TimesheetId = 1,
                PeopleId = 1,
                TssTypeId = 1,
                TimesheetDate = DateTime.Now,
                WorkFromTime = DateTime.Now,
                WorkToTime = DateTime.Now,
                WorkTotalTime = 10M,
                CreatedBy = "TechUser",
                CreatedDateTime = DateTime.Now,
                UpdatedBy = "TechUser",
                UpdatedDateTime = DateTime.Now
            };

            var PersistObj = new Domain.Entities.Timesheet
            {
                TimesheetId = 1,
                PeopleId = 1,
                TssTypeId = 1,
                TimesheetDate = DateTime.Now,
                WorkFromTime = DateTime.Now,
                WorkToTime = DateTime.Now,
                WorkTotalTime = 10M,
                CreatedBy = "TechUser",
                CreatedDateTime = DateTime.Now,
                UpdatedBy = "TechUser",
                UpdatedDateTime = DateTime.Now
            };

            mockMapper.Setup(_ => _.Map<Domain.Entities.Timesheet>(cmd)).Returns(obj);
            mockRepostiory.Setup(_ => _.AddAsync(obj)).Callback(() =>
            {
                obj.TimesheetId = 1;
            }).ReturnsAsync(PersistObj);

            var returnRequest = new CreateTimesheetCommandHandler(mockRepostiory.Object, mockMapper.Object);
            var reqResponce = await returnRequest.Handle(cmd, default);
            reqResponce.Succeeded.ToString();
            Assert.Equal(PersistObj.TimesheetId.ToString(), reqResponce.Data.ToString());
        }
    }
}
