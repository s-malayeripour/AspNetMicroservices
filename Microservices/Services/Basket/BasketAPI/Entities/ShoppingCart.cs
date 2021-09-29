namespace BasketAPI.Entities
{
    public class ShoppingCart
    {
        //I think it is in-memory data and it dont have to have ID
        public string UserName { get; set; } = string.Empty;
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public ShoppingCart() { }
        public ShoppingCart(string userName)
        {
            UserName = userName ?? string.Empty;
        }
        public decimal GetTotalPrice() => Items.Sum(i => i.Price * i.Count);
    }
}