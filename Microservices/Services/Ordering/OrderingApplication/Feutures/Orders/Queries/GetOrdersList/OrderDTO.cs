namespace OrderingApplication.Feutures.Orders.Queries.GetOrdersList
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
        public string Country { get; set; } = string.Empty;
        public string? State { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        //Credit
        public string CardName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string Expiration { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public int PaymentMethod { get; set; }
    }
}
