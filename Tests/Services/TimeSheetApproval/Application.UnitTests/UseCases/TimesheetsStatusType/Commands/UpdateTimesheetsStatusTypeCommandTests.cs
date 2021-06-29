using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Exceptions;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Commands;
using TimeSheetApproval.Domain.Entities;
using Xunit;
namespace TimeSheetApproval.Application.UnitTests.UseCases.TimesheetsStatusType.Commands
{
    public class UpdateTimesheetsStatusTypeCommandTests
    {
        [Fact]
        public async Task UpdateCurrencyCommand_ShouldSave_Successfully()
        {
            var mockMapper = new Mock<IMapper>();
            var mockRepostiory = new Mock<IGenericRepositoryAsync<TimesheetsStatusTypes>>();
            var mstcmd = new UpdateTimesheetsStatusTypeCommand
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

            mockRepostiory.Setup(_ => _.GetByIdAsync(mstObj.TssTypeId)).Returns(Task.FromResult(mstObj));

            mockRepostiory.Setup(_ => _.UpdateAsync(mstObj)).Callback(() =>
            {
                PersistObj.TssTypeName = mstcmd.TssTypeName;
            }).Returns(Task.FromResult(PersistObj));

            var returnRequest = new UpdateTimesheetsStatusTypeCommandHandler(mockMapper.Object, mockRepostiory.Object);
            var reqResponce = await returnRequest.Handle(mstcmd, default);
            reqResponce.Succeeded.ToString();
            Assert.Equal(PersistObj.TssTypeName.ToString(), mstcmd.TssTypeName);
        }
        [Fact]
        public async Task UpdateCurrencyCommand_Should_throw_Exception()
        {
            var mockMapper = new Mock<IMapper>();
            var mockRepostiory = new Mock<IGenericRepositoryAsync<TimesheetsStatusTypes>>();
            var mstcmd = new UpdateTimesheetsStatusTypeCommand
            {
                TssTypeId = 2,
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

            mockRepostiory.Setup(_ => _.GetByIdAsync(mstObj.TssTypeId)).Returns(
              Task.FromResult(mstObj.TssTypeId == PersistObj.TssTypeId ? mstObj : null)
          );

            mockRepostiory.Setup(_ => _.UpdateAsync(mstObj)).Callback(() =>
            {
                PersistObj.TssTypeName = mstcmd.TssTypeName;
            }).Returns(Task.FromResult(PersistObj));

            var returnRequest = new UpdateTimesheetsStatusTypeCommandHandler(mockMapper.Object, mockRepostiory.Object);
            await Assert.ThrowsAsync<ApiException>(async () => await returnRequest.Handle(mstcmd, default));
        }
    }
}
