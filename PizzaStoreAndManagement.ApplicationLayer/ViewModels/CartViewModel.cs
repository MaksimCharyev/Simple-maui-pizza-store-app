using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PizzaStoreAndManagement.DomainLayer.Models;
using PizzaStoreAndManagement.Persistance.Interfaces;
using PizzaStoreAndManagement.ApplicationLayer.Services;

namespace PizzaStoreAndManagement.ApplicationLayer.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        private readonly ICartItemRepository _cartItemsService;
        private readonly ICartRepository _cartService;
        private readonly IUserToken _token;
        private readonly IOrderItemRepository _orderItemService;
        private readonly IOrderRepository _orderService;
        private ObservableCollection<CartItem> _cartItems;
        private decimal _totalPrice;
        private Guid _cartID;

        public CartViewModel()
        {

        }

        public CartViewModel(ICartRepository cart, ICartItemRepository cartItem, IUserToken token, IOrderItemRepository orderItemService, IOrderRepository orderService)
        {
            _cartItemsService = cartItem;
            _cartService = cart;
            _token = token;

            RemoveItemCommand = new Command<CartItem>(OnRemoveItem);
            PlaceOrderCommand = new Command(OnPlaceOrder);
            IncreaseQuantityCommand = new Command<CartItem>(OnIncreaseQuantity);
            DecreaseQuantityCommand = new Command<CartItem>(OnDecreaseQuantity);

            LoadCartItems();
            _orderItemService = orderItemService;
            _orderService = orderService;
        }

        public ObservableCollection<CartItem> CartItems
        {
            get => _cartItems;
            set
            {
                _cartItems = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalPrice
        {
            get => _totalPrice;
            set
            {
                _totalPrice = value;
                OnPropertyChanged();
            }
        }

        public ICommand RemoveItemCommand { get; }
        public ICommand PlaceOrderCommand { get; }
        public ICommand IncreaseQuantityCommand { get; }
        public ICommand DecreaseQuantityCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void LoadCartItems()
        {
            var userId = _token.GetUserId();
            var cart = await _cartService.GetCartByUserID(userId);
            _cartID = cart.Id;

            var cartItems = await _cartItemsService.GetAllCartItemsByCartID(cart.Id);
            CartItems = new ObservableCollection<CartItem>(cartItems);
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = 0;
            foreach (var item in CartItems)
            {
                TotalPrice += item.Summary * item.Quantity;
            }
            OnPropertyChanged(nameof(TotalPrice));
        }

        private async void OnRemoveItem(CartItem item)
        {
            await _cartItemsService.DeleteCartItem(_cartID, item.Id);
            CartItems.Remove(item);
            CalculateTotalPrice();
        }

        private async void OnIncreaseQuantity(CartItem item)
        {
            item.Quantity += 1;
            await _cartItemsService.UpdateCartItem(item.Id, item.FoodId, item.Quantity);
            RefreshCartItems();
            CalculateTotalPrice();
        }

        private async void OnDecreaseQuantity(CartItem item)
        {
            if (item.Quantity > 1)
            {
                item.Quantity -= 1;
                await _cartItemsService.UpdateCartItem(item.Id, item.FoodId, item.Quantity);
            }
            else
            {
                OnRemoveItem(item);
            }
            RefreshCartItems();
            CalculateTotalPrice();
        }

        private async void OnPlaceOrder()
        {
            var userId = _token.GetUserId();
            var order = await _orderService.CreateOrder(userId, DateTime.Now);
            foreach (var item in CartItems)
            {
                var createOrderItemTask = await _orderItemService.CreateOrderItem(
                    item.FoodId,
                    item.Quantity,
                    order.Id,
                    item.Summary,
                    item.Extra,
                    item.Food.Name
                );
                await Task.Delay(200);
                //await Task.WhenAll(createOrderItemTask);
            }
            await Task.Delay(300);
            await _orderService.UpdateOrder(order.Id);
            await _cartItemsService.DeleteCartItems(_cartID);
            CartItems.Clear();
            TotalPrice = 0;
        }

        private void RefreshCartItems()
        {
            
            CartItems = new ObservableCollection<CartItem>(CartItems);
        }
    }
}
