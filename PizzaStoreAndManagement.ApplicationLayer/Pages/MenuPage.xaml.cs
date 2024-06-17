using PizzaStoreAndManagement.ApplicationLayer.ViewModels;
using PizzaStoreAndManagement.Persistance.Interfaces;
using PizzaStoreAndManagement.ApplicationLayer.Services;
using PizzaStoreAndManagement.DomainLayer.Models;
namespace PizzaStoreAndManagement.ApplicationLayer.Pages;

public partial class MenuPage : ContentPage
{

    public MenuPage()
    {
        InitializeComponent();
        BindingContext = new MenuViewModel(Application.Current.Handler.MauiContext.Services.GetService<IFoodRepository>()!);
        if (BindingContext is MenuViewModel viewModel)
        {
            viewModel.ScrollToItem += (sender, index) =>
            {
                if (index >= 0)
                {
                    MenuItemsCollectionView.ScrollTo(index, position: ScrollToPosition.Start, animate: true);
                }
            };
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // ����� ����� �������� ����� ��������, ������� ������ ����������� ��� ��������� ��������
    }
    private async void OnPriceButtonClicked(object sender, EventArgs e)
    {
        // �������� ������
        var button = sender as Button;
        if (button?.BindingContext is Food selectedFoodItem)
        {
            // ��������� �� FoodDetailPage � �������� ������ � ��������� ��������
            var detailPage = new FoodOrderPage
            {
                BindingContext = new FoodDetailViewModel(selectedFoodItem, Application.Current.Handler.MauiContext.Services.GetService<ICartRepository>(), Application.Current.Handler.MauiContext.Services.GetService<IUserToken>(), Application.Current.Handler.MauiContext.Services.GetService<ICartItemRepository>())
            };
            await Navigation.PushAsync(detailPage);
        }
    }
}
