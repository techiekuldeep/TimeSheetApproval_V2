using AutoMapper;
using TimeSheetApproval.Application.UseCases.People.Commands;
using TimeSheetApproval.Application.UseCases.Timesheet.Commands;
using TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Commands;
using TimeSheetApproval.Domain.Entities;

namespace TimeSheetApproval.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region "Timesheets Status Types"
            //Timesheets Status Types Create and Update Mappings"
            CreateMap<CreateTimesheetsStatusTypeCommand, TimesheetsStatusTypes>();
            CreateMap<UpdateTimesheetsStatusTypeCommand, TimesheetsStatusTypes>();
            CreateMap<DeleteTimesheetsStatusTypesCommand, TimesheetsStatusTypes>();
            #endregion "Timesheets Status Types"

            #region "People"
            //People Create and Update Mappings"
            CreateMap<CreatePeopleCommand, People>();
            CreateMap<UpdatePeopleCommand, People>();
            CreateMap<DeletePeopleCommand, People>();
            #endregion "People"

            #region "Timesheets"
            //People Create and Update Mappings"
            CreateMap<CreateTimesheetCommand, Timesheet>();
            CreateMap<UpdateTimesheetCommand, Timesheet>();
            CreateMap<DeleteTimesheetCommand, Timesheet>();
            #endregion "Timesheets"

        }
    }
}