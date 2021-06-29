using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Exceptions;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.UseCases.Timesheet.Commands;
using TimeSheetApproval.Domain.Entities;
using Xunit;

namespace TimeSheetApproval.Application.UnitTests.UseCases.Timesheet.Commands
{
    public class UpdateTimesheetCommandTest
    {
        Mock<IMapper> mockMapper;
        Mock<IGenericRepositoryAsync<Domain.Entities.Timesheet>> mockRepostiory;
        public UpdateTimesheetCommandTest()
        {
            mockMapper = new Mock<IMapper>();
            mockRepostiory = new Mock<IGenericRepositoryAsync<Domain.Entities.Timesheet>>();
        }
        [Fact]
        public async Task UpdateTimesheetCommand_ShouldSave_Successfully()
        {
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

            var Obj = new Domain.Entities.Timesheet
            {
                TimesheetId = 1,
                PeopleId = 1,
                TssTypeId = 2,
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

            mockMapper.Setup(_ => _.Map<Domain.Entities.Timesheet>(cmd)).Returns(Obj);

            mockRepostiory.Setup(_ => _.GetByIdAsync(Obj.TimesheetId)).Returns(Task.FromResult(Obj));

            mockRepostiory.Setup(_ => _.UpdateAsync(Obj)).Callback(() =>
            {
                PersistObj.TssTypeId = cmd.TssTypeId;
            }).Returns(Task.FromResult(PersistObj));

            var returnRequest = new UpdateTimesheetCommandHandler(mockRepostiory.Object, mockMapper.Object);
            var reqResponce = await returnRequest.Handle(cmd, default);
            reqResponce.Succeeded.ToString();
            Assert.Equal(PersistObj.TssTypeId, cmd.TssTypeId);
        }

        [Fact]
        public async Task UpdateTimesheetCommand_Should_throw_Exception()
        {
            var cmd = new UpdateTimesheetCommand
            {
                TimesheetId = 2,
                PeopleId = 1,
                TssTypeId = 2,
                TimesheetDate = DateTime.Now,
                WorkFromTime = DateTime.Now,
                WorkToTime = DateTime.Now,
                WorkTotalTime = 10M
            };

            var Obj = new Domain.Entities.Timesheet
            {
                TimesheetId = 1,
                PeopleId = 1,
                TssTypeId = 2,
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

            mockMapper.Setup(_ => _.Map<Domain.Entities.Timesheet>(cmd)).Returns(Obj);

            mockRepostiory.Setup(_ => _.GetByIdAsync(cmd.TimesheetId)).Returns(
                Task.FromResult(Obj.TimesheetId == cmd.TimesheetId ? Obj : null)
            );

            mockRepostiory.Setup(_ => _.UpdateAsync(Obj)).Callback(() =>
            {
                PersistObj.TssTypeId = cmd.TssTypeId;
            }).Returns(Task.FromResult(PersistObj));

            var returnRequest = new UpdateTimesheetCommandHandler(mockRepostiory.Object, mockMapper.Object);
            await Assert.ThrowsAsync<ApiException>(async () => await returnRequest.Handle(cmd, default));
        }
    }
}
