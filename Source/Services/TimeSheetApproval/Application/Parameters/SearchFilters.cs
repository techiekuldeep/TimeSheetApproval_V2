using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TimeSheetApproval.Application.Parameters
{
    public enum SortOrders
    {
        Asc=1,
        Desc
    }
    public class Filters
    {
        public string FilterColumn { get; set; }
        public string Operator { get; set; }
        public object FilterValue { get; set; }
        public string LogicOperator { get; set; }
        public IEnumerable<Filters> FilterList { get; set; }
    }
    public class Sorter
    {
        public string SortColumn { get; set; }
        public SortOrders SortOrder { get; set; } = SortOrders.Asc;
    }
    public class SearchFilters : RequestParameter
    {
        [JsonIgnore]
        public int TotalCount { get; set; }
        public IEnumerable<Sorter> SortParams { get; set; }
        public Filters FilterParams { get; set; }
    }
}
