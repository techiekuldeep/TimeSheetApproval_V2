using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Commands;
using TimeSheetApproval.Domain.Entities;
using Xunit;
namespace TimeSheetApproval.Application.UnitTests.UseCases.TimesheetsStatusType.Commands
{
    public class CreateTimesheetsStatusTypeCommandTests
    {
        [Fact]
        public async Task CreateTimesheetsStatusTypeCommand_ShouldSave_Successfully()
        {
            var mockMapper = new Mock<IMapper>();
            var mockRepostiory = new Mock<IGenericRepositoryAsync<TimesheetsStatusTypes>>();
            var mstcmd = new CreateTimesheetsStatusTypeCommand
            {
                TssTypeId = 1,
                TssTypeName = "Draft"
            };

            var mstObj = new TimesheetsStatusTypes
            {
                TssTypeId = 1,
                TssTypeName = "Draft",
                CreatedDateTime = DateTime.Now,
                CreatedBy = "TechUser",
                UpdatedDateTime = DateTime.Now,
                UpdatedBy = "TechUser"
            };

            var PersistObj = new TimesheetsStatusTypes
            {
                TssTypeId = 1,
                TssTypeName = "Draft",
                CreatedDateTime = DateTime.Now,
                CreatedBy = "TechUser",
                UpdatedDateTime = DateTime.Now,
                UpdatedBy = "TechUser"
            };

            mockMapper.Setup(_ => _.Map<TimesheetsStatusTypes>(mstcmd)).Returns(mstObj);
            mockRepostiory.Setup(_ => _.AddAsync(mstObj)).Callback(() => { mstObj.TssTypeId = 1; }).ReturnsAsync(PersistObj);

            var returnRequest = new CreateTimesheetsStatusTypeCommandHandler(mockRepostiory.Object, mockMapper.Object);
            var reqResponce = await returnRequest.Handle(mstcmd, default);
            reqResponce.Succeeded.ToString();
            Assert.Equal(PersistObj.TssTypeId.ToString(), reqResponce.Data.ToString());
        }
    }
}
