using PizzaStoreAndManagement.DomainLayer.Models;
using PizzaStoreAndManagement.Persistance.Interfaces;
using PizzaStoreAndManagement.ApplicationLayer.Pages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PizzaStoreAndManagement.ApplicationLayer.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        private readonly IFoodRepository _foodRepository;
        public ObservableCollection<Food> MenuItems { get; set; }
        public ICommand NavigateToCartCommand { get; }
        public ICommand NavigateToOrderCommand { get; }
        public ICommand NavigateToLoginCommand { get; }
        public ICommand ScrollToCategoryCommand { get; }

        public MenuViewModel(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
            MenuItems = new ObservableCollection<Food>();
            NavigateToCartCommand = new Command(OnNavigateToCart);
            NavigateToOrderCommand = new Command(OnNavigateToOrder);
            NavigateToLoginCommand = new Command(OnNavigateToLogin);
            ScrollToCategoryCommand = new Command<string>(ScrollToCategory);

            LoadMenuItems();
        }

        private async void OnNavigateToCart()
        {
            // Переход на страницу корзины
            await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
        }

        private async void OnNavigateToOrder()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new OrderPage());
        }
        public async void OnNavigateToLogin()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
        private async void LoadMenuItems()
        {
            var items = await _foodRepository.GetAllFoods();
            items = items.OrderByDescending(x => x.Category).ToList();
            foreach (var item in items)
            {
                MenuItems.Add(item);
            }
        }

        private void ScrollToCategory(string category)
        {
            var index = MenuItems.ToList().FindIndex(item => item.Category == category);
            ScrollToItem?.Invoke(this, index);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler<int> ScrollToItem;
    }
}
