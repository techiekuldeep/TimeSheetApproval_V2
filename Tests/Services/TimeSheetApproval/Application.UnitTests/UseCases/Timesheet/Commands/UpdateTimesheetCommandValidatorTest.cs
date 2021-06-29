using AutoMapper;
using Moq;
using Shouldly;
using System;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.UseCases.Timesheet.Commands;
using TimeSheetApproval.Domain.Entities;
using Xunit;

namespace TimeSheetApproval.Application.UnitTests.UseCases.Timesheet.Commands
{
    public class UpdateTimesheetCommandValidatorTest
    {
        Mock<IMapper> mockMapper;
        Mock<IGenericRepositoryAsync<Domain.Entities.Timesheet>> mockRepostiory;
        public UpdateTimesheetCommandValidatorTest()
        {
            mockMapper = new Mock<IMapper>();
            mockRepostiory = new Mock<IGenericRepositoryAsync<Domain.Entities.Timesheet>>();
        }
        [Fact]
        public void CreateTimeSheetCommand_Should_Save_when_req_data_is_valid()
        {
            var validator = new UpdateTimesheetCommandValidator(mockRepostiory.Object,mockMapper.Object);

            var savecmd = new UpdateTimesheetCommand()
            {
                TimesheetId = 1,
                PeopleId = 1,
                TssTypeId = 1,
                TimesheetDate = DateTime.Now,
                WorkFromTime = DateTime.Now,
                WorkToTime = DateTime.Now,
                WorkTotalTime = 10M
            };
            validator.Validate(savecmd).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void CreateTimeSheetCommand_Should_Fail_when_req_data_is_Invalid()
        {
            var validator = new UpdateTimesheetCommandValidator(mockRepostiory.Object, mockMapper.Object);

            var savecmd = new UpdateTimesheetCommand()
            {
                TssTypeId = 1,
                TimesheetDate = DateTime.Now,
                WorkFromTime = DateTime.Now,
                WorkToTime = DateTime.Now,
                WorkTotalTime = 10M
            };
            validator.Validate(savecmd).IsValid.ShouldBeFalse();
        }
    }
}
