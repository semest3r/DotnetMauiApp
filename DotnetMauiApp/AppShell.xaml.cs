using DotnetMauiApp.Repositories;

namespace DotnetMauiApp
{
    public partial class AppShell : Shell
    {
        private readonly WalletRepository _walletRepository;
        public AppShell(WalletRepository walletRepository)
        {
            InitializeComponent();
            _walletRepository = walletRepository;
            var activeWallet = _walletRepository.GetActiveWallet();
            if(activeWallet == null )
            {
                Shell.Current.GoToAsync("RegisterPage");
            }
        }
    }
}
