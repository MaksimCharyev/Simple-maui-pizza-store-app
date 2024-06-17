namespace PizzaStoreAndManagement.DomainLayer.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        public Guid FoodId { get; set; }
        public Food Food { get; set; }
        public int Quantity { get; set; }
        public decimal Summary { get; set; }
        public string? Extra { get; set; }
        public CartItem()
        {

        }
        public CartItem(Guid cartId, Guid foodId, int quantity, string? extra, int mod, decimal price)
        {
            Id = Guid.NewGuid();
            CartId = cartId;
            FoodId = foodId;
            Quantity = quantity;
            Extra = extra;
            Summary = mod*price;
        }
    }
}
