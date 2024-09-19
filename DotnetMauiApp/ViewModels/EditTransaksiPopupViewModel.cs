using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using DotnetMauiApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetMauiApp.ViewModels
{
    public partial class EditTransaksiPopupViewModel : BaseViewModel
    {
        private readonly TransactionService _transactionService;
        public EditTransaksiPopupViewModel(TransactionService transactionService, AuthService authService):base(authService)
        {
            _transactionService = transactionService;
        }

        [ObservableProperty]
        Transaction transaction;

        [RelayCommand]
        async Task EditTransaksiAsync()
        {
            if (string.IsNullOrEmpty(Transaction.Description))
            {
                return;
            }
            if (double.IsNaN(Transaction.TotalMoney))
            {
                return;
            }

            await _transactionService.UpdateTransactionAndWallet(Transaction);
        }
    }
}
