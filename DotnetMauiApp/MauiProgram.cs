using CommunityToolkit.Maui;
using DotnetMauiApp.Data;
using DotnetMauiApp.Repositories;
using DotnetMauiApp.Services;
using DotnetMauiApp.ViewModels;
using DotnetMauiApp.Views;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DotnetMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Database
            builder.Services.AddDbContext<DataContext>();

            //Services
            builder.Services.AddTransient<AuthService>();

            //Page
            builder.Services.AddTransient<OnBoardingPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<TransaksiPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<ResetDatabasePage>();

            //Poup
            builder.Services.AddTransientPopup<AddPemasukanPopUp, PemasukanPopUpViewModel>();
            builder.Services.AddTransientPopup<AddPengeluaranPopUp, PengeluaranPopUpViewModel>();
            builder.Services.AddTransientPopup<EditTransaksiPopup, EditTransaksiPopupViewModel>();

            //View Model
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<TransactionViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<EditTransaksiPopupViewModel>();

            //Database
            builder.Services.AddTransient<WalletRepository>();
            builder.Services.AddTransient<TransactionRepository>();

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            });

#if DEBUG
            builder.Logging.AddDebug(); 
#endif

            return builder.Build();
        }
    }
}
