using PizzaStoreAndManagement.Persistance.Interfaces;
using PizzaStoreAndManagement.ApplicationLayer.ViewModels;
using PizzaStoreAndManagement.ApplicationLayer.Services;
namespace PizzaStoreAndManagement.ApplicationLayer.Pages;

public partial class CartPage : ContentPage
{
	public CartPage()
	{
		InitializeComponent();
        BindingContext = new CartViewModel(Application.Current.Handler.MauiContext.Services.GetService<ICartRepository>(), Application.Current.Handler.MauiContext.Services.GetService<ICartItemRepository>(), Application.Current.Handler.MauiContext.Services.GetService<IUserToken>(), Application.Current.Handler.MauiContext.Services.GetService<IOrderItemRepository>(), Application.Current.Handler.MauiContext.Services.GetService<IOrderRepository>());
    }
}