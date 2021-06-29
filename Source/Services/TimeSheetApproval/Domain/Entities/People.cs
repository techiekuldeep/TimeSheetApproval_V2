using System.Collections.Generic;
using System.Text.Json.Serialization;
using TimeSheetApproval.Domain.Common;

namespace TimeSheetApproval.Domain.Entities
{
    public class People : AuditableEntity
    {
        public People()
        {
            Timesheets = new List<Timesheet>();
        }
        public long PeopleId { get; set; }
        public string PeopleFirstName { get; set; }
        public string PeopleLastName { get; set; }
        public string Gender { get; set; }
        public double HourlyRate { get; set; }
        public string BankAccount { get; set; }
        [JsonIgnore]
        public IList<Timesheet> Timesheets { get; set; }
    }
}
