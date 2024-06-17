using PizzaStoreAndManagement.ApplicationLayer.Pages;
namespace PizzaStoreAndManagement.ApplicationLayer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegPage), typeof(RegPage));
            Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
            Routing.RegisterRoute(nameof(FoodOrderPage), typeof(FoodOrderPage));
        }
    }
}
