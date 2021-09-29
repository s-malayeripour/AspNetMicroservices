namespace BasketAPI.Entities
{
    public class ShoppingCartItem
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Color { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}