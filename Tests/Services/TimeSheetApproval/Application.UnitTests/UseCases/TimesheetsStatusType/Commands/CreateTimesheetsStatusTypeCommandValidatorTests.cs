using Moq;
using Shouldly;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Commands;
using TimeSheetApproval.Domain.Entities;
using Xunit;

namespace TimeSheetApproval.Application.UnitTests.UseCases.TimesheetsStatusType.Commands
{
    public class CreateTimesheetsStatusTypeCommandValidatorTests
    {
        readonly CreateTimesheetsStatusTypeCommandValidator _validator;
        readonly Mock<IGenericRepositoryAsync<TimesheetsStatusTypes>> _repo;
        public CreateTimesheetsStatusTypeCommandValidatorTests()
        {
            _repo = new Mock<IGenericRepositoryAsync<TimesheetsStatusTypes>>();
            _validator = new CreateTimesheetsStatusTypeCommandValidator(_repo.Object);
        }
        [Fact]
        public void CreateTsst_Should_Save_when_req_data_is_Valid()
        {
            var savecmd = new CreateTimesheetsStatusTypeCommand()
            {
                TssTypeId = 0,
                TssTypeName = "Draft"
            };
            _repo.Setup(_ => _.IsRecordExistForString("TssTypeName", savecmd.TssTypeName)).ReturnsAsync(false);
            _validator.Validate(savecmd).IsValid.ShouldBeTrue();
        }
        [Fact]
        public void CreateTsst_Should_Save_when_req_data_is_Invalid()
        {
            var savecmd = new CreateTimesheetsStatusTypeCommand()
            {
                TssTypeId = 1,
                TssTypeName = ""

            };
            _validator.Validate(savecmd).IsValid.ShouldBeFalse();
        }
    }
}
