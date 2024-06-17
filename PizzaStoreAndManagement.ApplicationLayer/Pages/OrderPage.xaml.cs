using PizzaStoreAndManagement.ApplicationLayer.Services;
using PizzaStoreAndManagement.ApplicationLayer.ViewModels;
using PizzaStoreAndManagement.Persistance.Interfaces;

namespace PizzaStoreAndManagement.ApplicationLayer.Pages;
public partial class OrderPage : ContentPage
{
	public OrderPage()
	{
		InitializeComponent();
		BindingContext = new OrdersViewModel(Application.Current.Handler.MauiContext.Services.GetService<IOrderRepository>(),Application.Current.Handler.MauiContext.Services.GetService<IUserToken>(), Application.Current.Handler.MauiContext.Services.GetService<IOrderItemRepository>());
	}
}