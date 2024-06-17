namespace PizzaStoreAndManagement.DomainLayer.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<CartItem> Items { get; set; }
        public Cart()
        {
            
        }
        public Cart(Guid userid)
        {
            Id = Guid.NewGuid();
            UserId = userid;
            Items = new List<CartItem>();
        }

    }
}
