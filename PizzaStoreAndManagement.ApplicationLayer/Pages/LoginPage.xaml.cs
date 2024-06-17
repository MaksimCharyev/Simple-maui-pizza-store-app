using PizzaStoreAndManagement.Persistance.Interfaces;
using PizzaStoreAndManagement.ApplicationLayer.ViewModels;
namespace PizzaStoreAndManagement.ApplicationLayer.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel(Application.Current!.Handler.MauiContext!.Services.GetService<IUserRepository>()!);
    }
    private async void OnRegClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegPage());
    }
}