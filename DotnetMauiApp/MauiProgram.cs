using CommunityToolkit.Maui;
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
                .UseSentry(options => {
                    // The DSN is the only required setting.
                    options.Dsn = "https://257e831fe2a7f4c7203e62a9633fffc9@o4507887972581376.ingest.de.sentry.io/4507887974613072";

                    // Use debug mode if you want to see what the SDK is doing.
                    // Debug messages are written to stdout with Console.Writeline,
                    // and are viewable in your IDE's debug console or with 'adb logcat', etc.
                    // This option is not recommended when deploying your application.
                    options.Debug = true;

                    // Set TracesSampleRate to 1.0 to capture 100% of transactions for tracing.
                    // We recommend adjusting this value in production.
                    options.TracesSampleRate = 1.0;

                    // Other Sentry options can be set here.
                })
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Services
            builder.Services.AddTransient<AuthService>();

            //Page
            builder.Services.AddTransient<OnBoardingPage>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddTransient<TransaksiPage>();
            builder.Services.AddTransient<RegisterPage>();

            //Poup
            builder.Services.AddTransientPopup<AddPemasukanPopUp, PemasukanPopUpViewModel>();
            builder.Services.AddTransientPopup<AddPengeluaranPopUp, PengeluaranPopUpViewModel>();

            //View Model
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddTransient<TransaksiViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();

            //Database
            builder.Services.AddSingleton<WalletRepository>();
            builder.Services.AddSingleton<TransaksiRepository>();

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
