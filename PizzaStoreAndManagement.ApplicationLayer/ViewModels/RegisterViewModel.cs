using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using PizzaStoreAndManagement.ApplicationLayer.Services;
using PizzaStoreAndManagement.Persistance.Interfaces;

namespace PizzaStoreAndManagement.ApplicationLayer.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly IUserRepository _userService;
        private bool _isBusy;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _name;
        public RegisterViewModel(IUserRepository userService)
        {
            _userService = userService;
            RegisterCommand = new Command(async () => await Register(), () => !IsBusy);
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }

        public ICommand RegisterCommand { get; }

        private async Task Register()
        {
            if (IsBusy) return;
            IsBusy = true;

            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Passwords do not match.", "OK");
                IsBusy = false;
                return;
            }

            var success = await _userService.CreateUser(Name, Email, Password);
            if (success != null)
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Registration failed.", "OK");
            }

            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
