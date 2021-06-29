using Shouldly;
using System;
using TimeSheetApproval.Application.UseCases.Timesheet.Commands;
using Xunit;

namespace TimeSheetApproval.Application.UnitTests.UseCases.Timesheet.Commands
{
    public class CreateTimeSheetCommandValidatorTest
    {
        [Fact]
        public void CreateTimeSheetCommand_Should_Save_when_req_data_is_valid()
        {
            var validator = new CreateTimeSheetCommandValidator();

            var savecmd = new CreateTimesheetCommand()
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
            var validator = new CreateTimeSheetCommandValidator();

            var savecmd = new CreateTimesheetCommand()
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
