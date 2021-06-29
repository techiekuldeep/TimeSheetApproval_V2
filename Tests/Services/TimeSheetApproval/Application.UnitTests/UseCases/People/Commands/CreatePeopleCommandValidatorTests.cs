using Shouldly;
using TimeSheetApproval.Application.UseCases.People.Commands;
using Xunit;

namespace TimeSheetApproval.Application.UnitTests.UseCases.People.Commands
{
    public class CreatePeopleCommandValidatorTests
    {
        readonly CreatePeopleCommandValidator _validator;
        public CreatePeopleCommandValidatorTests()
        {
            _validator = new CreatePeopleCommandValidator();
        }
        [Fact]
        public void CreatePeople_Should_Save_when_req_data_is_Valid()
        {
            var savecmd = new CreatePeopleCommand()
            {
                PeopleId = 0,
                PeopleFirstName = "FirstName",
                PeopleLastName = "LastName",
                Gender = "Male",
                HourlyRate = 25.00,
                BankAccount = "999999"
            };
            _validator.Validate(savecmd).IsValid.ShouldBeTrue();
        }
        [Fact]
        public void CreatePeople_Should_Save_when_req_data_is_Invalid()
        {
            var savecmd = new CreatePeopleCommand()
            {
                PeopleId = 1,
                PeopleFirstName = "",
                PeopleLastName = "",
                Gender = "Male",
                HourlyRate = 25.00,
                BankAccount = "999999"
            };
            _validator.Validate(savecmd).IsValid.ShouldBeFalse();
        }
    }
}
