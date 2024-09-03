using CommunityToolkit.Mvvm.ComponentModel;
using DotnetMauiApp.Services;

namespace DotnetMauiApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        public readonly AuthService _authService;
        public BaseViewModel(AuthService authService)
        {
            _authService = authService; 
        }
    }
}
