using ABiS.Sop.Domain.Common;

namespace ABiS.Sop.Domain.Entities
{
    public class Quotation : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RouteInfo { get; set; }
        public string Description { get; set; }
        public decimal RateCharges { get; set; }
    }
}
