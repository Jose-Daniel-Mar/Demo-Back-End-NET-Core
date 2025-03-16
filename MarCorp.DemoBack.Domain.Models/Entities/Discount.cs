using MarCorp.DemoBack.Domain.Models.Common;
using MarCorp.DemoBack.Domain.Models.Enums;

namespace MarCorp.DemoBack.Domain.Models.Entities
{
    public class Discount : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Percent { get; set; }
        public DiscountStatus Status { get; set; }
    }
}
