using MarCorp.DemoBack.Application.DTO.Enums;

namespace MarCorp.DemoBack.Application.DTO
{
    public sealed record DiscountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Percent { get; set; }
        public DiscountStatusDTO Status { get; set; }
    }
}
