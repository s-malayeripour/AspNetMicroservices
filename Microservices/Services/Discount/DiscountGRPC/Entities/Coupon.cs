namespace DiscountGRPC.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Amount { get; set; }
    }
}
