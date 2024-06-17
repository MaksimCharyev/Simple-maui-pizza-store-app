using PizzaStoreAndManagement.ApplicationLayer.ViewModels;
using PizzaStoreAndManagement.Persistance.Interfaces;

namespace PizzaStoreAndManagement.ApplicationLayer.Pages;

public partial class RegPage : ContentPage
{
	public RegPage()
	{
		InitializeComponent();
		BindingContext = new RegisterViewModel(Application.Current!.Handler.MauiContext!.Services.GetService<IUserRepository>()!);
	}
}