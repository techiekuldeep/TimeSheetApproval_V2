using System;
using System.Text.Json.Serialization;
using TimeSheetApproval.Domain.Common;

namespace TimeSheetApproval.Domain.Entities
{
    public class Timesheet : AuditableEntity
    {
        public long TimesheetId { get; set; }
        public long PeopleId { get; set; }
        public long TssTypeId { get; set; }
        public DateTime TimesheetDate { get; set; }
        public DateTime WorkFromTime { get; set; }
        public DateTime WorkToTime { get; set; }
        public decimal WorkTotalTime { get; set; }
        [JsonIgnore]
        public People People { get; set; }
        [JsonIgnore]
        public TimesheetsStatusTypes TimesheetsStatusTypes { get; set; }
    }
}
