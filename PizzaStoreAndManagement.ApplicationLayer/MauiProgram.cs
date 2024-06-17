using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaStoreAndManagement.Persistance.Context;
using PizzaStoreAndManagement.Persistance.Interfaces;
using PizzaStoreAndManagement.ApplicationLayer.Services;
using PizzaStoreAndManagement.ApplicationLayer.ViewModels;
namespace PizzaStoreAndManagement.ApplicationLayer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseMauiCommunityToolkit();
            builder.Services.AddDbContext<PizzaStoreDBContext>(options =>
            {
                options.UseMySql(
                    "server=192.168.0.10;user=Phone;password=MySQLW0rkB3nch!;database=pizzadb;",
                    ServerVersion.AutoDetect("server=192.168.0.10;user=Phone;password=MySQLW0rkB3nch!;database=pizzadb;"),
                    options => options.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: System.TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null)
                    );
            });
            builder.Services.AddTransient<IFoodRepository, FoodService>();
            builder.Services.AddTransient<IOrderItemRepository, OrderItemService>();
            builder.Services.AddTransient<IOrderRepository, OrderService>();
            builder.Services.AddTransient<IUserRepository, UserService>();
            builder.Services.AddTransient<ICartItemRepository, CartItemService>();
            builder.Services.AddTransient<ICartRepository, CartService>();
            builder.Services.AddSingleton<IUserToken, UserToken>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
