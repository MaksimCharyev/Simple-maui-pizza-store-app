namespace PizzaStoreAndManagement.DomainLayer.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }
        public Order Order { get; set; }
        public Cart Cart { get; set; }
        public User(string name, string email, string password, bool isAdmin)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            this.isAdmin = isAdmin;
        }
        public User()
        {
            
        }
    }
}
