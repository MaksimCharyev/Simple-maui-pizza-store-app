namespace PizzaStoreAndManagement.ApplicationLayer
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }
        public App(IServiceProvider serv)
        {
            InitializeComponent();
            Services = serv;
            MainPage = new AppShell();
        }
    }
}
