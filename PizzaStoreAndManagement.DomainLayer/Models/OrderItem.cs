namespace PizzaStoreAndManagement.DomainLayer.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public Order Order { get; set; }
        public Guid FoodId { get; set; }
        public Food Food { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Extra { get; set; }
        public OrderItem(Guid orderId, Guid foodId, int quantity, decimal price, string? extra, string Name)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            FoodId = foodId;
            Quantity = quantity;
            Price = price;
            Extra = extra;
            this.Name = Name;

        }
        public OrderItem()
        {
            
        }
    }
}
