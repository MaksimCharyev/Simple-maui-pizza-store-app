using PizzaStoreAndManagement.ApplicationLayer.Services;
using PizzaStoreAndManagement.DomainLayer.Models;
using PizzaStoreAndManagement.Persistance.Interfaces;
using System.ComponentModel;
using System.Windows.Input;
namespace PizzaStoreAndManagement.ApplicationLayer.ViewModels
{
    public class FoodDetailViewModel : INotifyPropertyChanged
    {
        private Food _foodItem;
        private string? _selectedSize;
        private string? _selectedCrust;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IUserToken _user;
        public string SelectedSize
        {
            get => _selectedSize;
            set
            {
                _selectedSize = value;
                OnPropertyChanged(nameof(_selectedSize));
            }
        }

        public string SelectedCrust
        {
            get => _selectedCrust;
            set
            {
                _selectedCrust = value;
                OnPropertyChanged(nameof(_selectedCrust));
            }
        }
        public Food FoodItem
        {
            get => _foodItem;
            set
            {
                _foodItem = value;
                OnPropertyChanged(nameof(FoodItem));
            }
        }
        public FoodDetailViewModel()
        {
            
        }
        public bool IsPizza => _foodItem.Category?.Equals("Пицца", StringComparison.OrdinalIgnoreCase) == true;
        public FoodDetailViewModel(Food foodItem, ICartRepository cartService, IUserToken userService, ICartItemRepository cartItem)
        {
            FoodItem = foodItem;
            _cartItemRepository = cartItem;
            _user = userService;
            _cartRepository = cartService;
            SelectSizeCommand = new Command<string?>(SelectSize);
            SelectCrustCommand = new Command<string?>(SelectCrust);
            AddToCartCommand = new Command(AddToCart);
            _selectedSize = "";
            _selectedCrust = "";
        }
        public ICommand SelectSizeCommand { get; }
        public ICommand SelectCrustCommand { get; }
        public ICommand AddToCartCommand { get; }
        private void SelectSize(string? s)
        {
            SelectedSize = s;
        }
        private void SelectCrust(string? s)
        {
            SelectedCrust = s;
        }
        private async void AddToCart()
        {
            int Pricemodifer = 0;
            string Extra;
            if (SelectedCrust != "" && SelectedSize != "") 
            {
                Extra = $"Размер: {SelectedSize}, Тесто: {SelectedCrust}";
            }
            Extra = "";
            switch (SelectedSize)
            {
                case "Маленькая":
                    {
                        Pricemodifer = 1;
                        break;
                    }
                case "Средняя":
                    {
                        Pricemodifer = 2;
                        break;
                    }
                case "Большая":
                    {
                        Pricemodifer = 3;
                        break;
                    }
                default:
                    {
                        Pricemodifer = 1;
                        break;
                    }
            }
            var guid = _user.GetUserId();
            var cart = await _cartRepository.GetCartByUserID(guid);
            if (cart == null)
            {
                var Newcart = await _cartRepository.CreateCart(guid);
                await _cartItemRepository.CreateCartItem(Newcart.Id, FoodItem.Id, 1, Extra, Pricemodifer,FoodItem.Price);
            }
            else
            {
                await _cartItemRepository.CreateCartItem(cart.Id,FoodItem.Id, 1, Extra,Pricemodifer,FoodItem.Price);
            }
            await Shell.Current.GoToAsync("//MenuPage");
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
