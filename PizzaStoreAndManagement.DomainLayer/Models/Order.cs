namespace PizzaStoreAndManagement.DomainLayer.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedTimeOrder { get; set; }
        public string Status {  get; set; }
        public List<OrderItem> OrderItems { get; set;}
        public Order()
        {
            
        }
        public Order(Guid userId, DateTime created)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            OrderItems = new List<OrderItem>();
            CreatedTimeOrder = created;
            Status = "Принят";
        }
        public Order(Guid userId, DateTime created, List<OrderItem> orderItems)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CreatedTimeOrder = created;
            OrderItems = orderItems;
        }
        public void AddItems(List<OrderItem> orderItems)
        {
            OrderItems = orderItems;
        }
    }
}
