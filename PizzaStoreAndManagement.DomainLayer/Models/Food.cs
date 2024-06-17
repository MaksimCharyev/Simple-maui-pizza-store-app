namespace PizzaStoreAndManagement.DomainLayer.Models
{
    public class Food
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Food(string name, string description) 
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }
        public Food()
        {
            
        }
    }
}
