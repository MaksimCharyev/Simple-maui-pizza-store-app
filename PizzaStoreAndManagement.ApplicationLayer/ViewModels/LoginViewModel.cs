using PizzaStoreAndManagement.ApplicationLayer.Pages;
using PizzaStoreAndManagement.Persistance.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
namespace PizzaStoreAndManagement.ApplicationLayer.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IUserRepository _userService;
        private bool _isBusy;
        private string _email;
        private string _password;
        public LoginViewModel()
        {
            
        }
        public LoginViewModel(IUserRepository userService)
        {
            _userService = userService;
            LoginCommand = new Command(async () => await Login(), () => !IsBusy);
            RegisterCommand = new Command(async () => await Register(), () => !IsBusy);
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
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                ((Command)LoginCommand).ChangeCanExecute();
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private async Task Login()
        {
            if (IsBusy) return;
            IsBusy = true;
            var success = await _userService.VerifyUser(Email, Password);
            var isAdm = await _userService.VerifyAdmin(Email, Password);
            if(isAdm)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new ManagePage());
            }
            else
            {
                if (success)
                {
                    await Shell.Current.GoToAsync("//MenuPage");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid email or password.", "OK");
                }
            }
 

            IsBusy = false;
        }

        private async Task Register()
        {
            if (IsBusy) return;
            IsBusy = true;

            await Shell.Current.GoToAsync("//RegPage");

            IsBusy = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

