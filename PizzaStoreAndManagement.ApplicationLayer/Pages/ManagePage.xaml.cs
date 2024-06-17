using PizzaStoreAndManagement.ApplicationLayer.ViewModels;
using PizzaStoreAndManagement.Persistance.Interfaces;
namespace PizzaStoreAndManagement.ApplicationLayer.Pages;
public partial class ManagePage : ContentPage
{
	public ManagePage()
	{
		InitializeComponent();
		BindingContext = new ManagerOrdersViewModel(Application.Current.Handler.MauiContext.Services.GetService<IOrderRepository>(), Application.Current.Handler.MauiContext.Services.GetService<IOrderItemRepository>());
		
	}
}