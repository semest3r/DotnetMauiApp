using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetMauiApp.Services
{
    public class AuthService
    {
        readonly WalletRepository _walletRepository;

        public AuthService(WalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<int> GetCurrentWalletId()
        {
            var walletId = Preferences.Get("wallet", 0);
            if(walletId == 0)
            {
                var wallet = await _walletRepository.GetFirstWallet();
                if(wallet != null)
                {
                    Preferences.Default.Set("wallet", wallet.Id);
                    return wallet.Id;
                }
            }
            return walletId;
        }
    }
}
