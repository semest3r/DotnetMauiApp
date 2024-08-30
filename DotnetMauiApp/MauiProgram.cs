using CommunityToolkit.Maui;
using DotnetMauiApp.Repositories;
using DotnetMauiApp.ViewModels;
using DotnetMauiApp.Views;
using Microsoft.Extensions.Logging;

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
            
            //Page
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<TransaksiPage>();

            //
            builder.Services.AddTransient<AddPemasukanPopUp>();
            builder.Services.AddTransient<AddPengeluaranPopUp>();

            //View Model
            builder.Services.AddSingleton<TransaksiViewModel>();

            //Database
            builder.Services.AddSingleton<WalletRepository>();
            builder.Services.AddSingleton<TransaksiRepository>();

#if WINDOWS
            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("NoUnderlineWindows", (h,v) => 
            {
                h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness()
                {
                    Bottom = 0,
                    Top = 0,
                    Left = 0,
                    Right = 0,
                };
            });
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderlineWindows", (h,v) => 
            {
                h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness()
                {
                    Bottom = 0,
                    Top = 0,
                    Left = 0,
                    Right = 0,
                };
            });
#endif

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
