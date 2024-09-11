using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotnetMauiApp.Models;
using DotnetMauiApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetMauiApp.ViewModels
{
    public partial class EditTransaksiPopup : BaseViewModel
    {
        public EditTransaksiPopup(AuthService authService):base(authService)
        {
            
        }

        [ObservableProperty]
        Transaction transaction;

        [RelayCommand]
        async Task EditTransaksiAsync()
        {

        }
    }
}
