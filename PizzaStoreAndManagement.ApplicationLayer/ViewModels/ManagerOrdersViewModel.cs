using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PizzaStoreAndManagement.DomainLayer.Models;
using PizzaStoreAndManagement.Persistance.Interfaces;

namespace PizzaStoreAndManagement.ApplicationLayer.ViewModels
{
    public class OrderDTO
    {
        public OrderDTO() { }
        public List<OrderItem> orderItems { get; set; }
        public Order Order { get; set; }
    }
    public class ManagerOrdersViewModel : INotifyPropertyChanged
    {
        private readonly IOrderRepository _orderService;
        private readonly IOrderItemRepository _itemService;
        private ObservableCollection<OrderDTO> _orders;
        private ObservableCollection<string> statuses = new ObservableCollection<string> { "Принят", "Готовится", "В Пути", "Доставлен" };

        public ManagerOrdersViewModel(IOrderRepository order, IOrderItemRepository item)
        {
            // Замените на ваш сервис для работы с заказами
            _orderService = order;
            _itemService = item;
            Orders = [];
            LoadOrders();
            SaveChangesCommand = new Command(OnSaveChanges);
        }
        public ManagerOrdersViewModel()
        {
            _orders = new ObservableCollection<OrderDTO>();
        }
        public ObservableCollection<OrderDTO> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged();
            }
        }


        public ICommand SaveChangesCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void LoadOrders()
        {
            var orders = await _orderService.GetAllOrders();
            List<OrderDTO> dto = new List<OrderDTO>();
            foreach (var order in orders)
            {
                var items = await _itemService.GetAllOrdersItemOfOrderById(order.Id);
                var Newdto = new OrderDTO { Order = order, orderItems = items };
                dto.Add(Newdto);
            }
            Orders = new ObservableCollection<OrderDTO>(dto);
        }
        public ObservableCollection<string> Statuses
        {
            get => statuses;
            set
            {
                statuses = value;
                OnPropertyChanged();
            }
        }

        private string selectedStatus;
        public string SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
                OnPropertyChanged();
                // Здесь можно добавить логику для изменения статуса заказа
            }
        }
        private async void OnSaveChanges()
        {
            foreach (var order in Orders)
            {
                await _orderService.UpdateStatusOrder(order.Order.Id, SelectedStatus);
            }

            await Application.Current.MainPage.DisplayAlert("Успех", "Изменения успешно сохранены!", "ОК");
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
