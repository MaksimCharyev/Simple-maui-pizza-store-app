using PizzaStoreAndManagement.ApplicationLayer.Services;
using PizzaStoreAndManagement.DomainLayer.Models;
using PizzaStoreAndManagement.Persistance.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PizzaStoreAndManagement.ApplicationLayer.ViewModels
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Order> _orders;
        private ObservableCollection<OrderItem> _items;
        private readonly IOrderRepository _orderService;
        private readonly IUserToken _token;
        private readonly IOrderItemRepository _itemService;
        public OrdersViewModel()
        {

        }
        public OrdersViewModel(IOrderRepository _orderService, IUserToken token, IOrderItemRepository item)
        {
            this._orderService = _orderService;
            this._token = token;
            _itemService = item;
            Orders = new ObservableCollection<Order>();
            OrderItems = new ObservableCollection<OrderItem>();
            LoadOrders();


        }
        public ObservableCollection<OrderItem> OrderItems
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(OrderItems));
            }
        }
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        private async void LoadOrders()
        {
            var UserId = _token.GetUserId();
            var orders = await _orderService.GetOrderByUserID(UserId);
            var orItems = await _itemService.GetAllOrdersItemOfOrderById(orders.Id);
            Orders.Add(orders);
            foreach(var item in orItems)
            {
                OrderItems.Add(item);
            }
            //.OrderByDescending(o => o.Date).ToList());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
