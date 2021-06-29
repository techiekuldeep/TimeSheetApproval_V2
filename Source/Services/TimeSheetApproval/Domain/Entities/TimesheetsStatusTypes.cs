using System.Text.Json.Serialization;
using TimeSheetApproval.Domain.Common;

namespace TimeSheetApproval.Domain.Entities
{
    public class TimesheetsStatusTypes : AuditableEntity
    {
        public long TssTypeId { get; set; }
        public string TssTypeName { get; set; }
        [JsonIgnore]
        public Timesheet Timesheets { get; set; }
    }
}
