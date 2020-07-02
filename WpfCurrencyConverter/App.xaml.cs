using System.Globalization;
using System.Windows;
using Application.ViewModels;
using Ninject;
using Presentation.Controls;
using WpfCurrencyConverter.Properties;

namespace WpfCurrencyConverter
{
    public partial class App
    {
        private MainWindowViewModel _mainWindowViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            StandardKernel standardKernel = new StandardKernel(new WpfAppModule());

            _mainWindowViewModel = standardKernel.Get<MainWindowViewModel>();

            RestoreMainWindowViewModelSettings();

            MainWindow = new MainWindow
            {
                DataContext = _mainWindowViewModel
            };

            ShutdownMode = ShutdownMode.OnMainWindowClose;

            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            StoreMainWindowViewModelSettings();

            base.OnExit(e);
        }

        private void StoreMainWindowViewModelSettings()
        {
            Settings settings = (Settings)Resources["Settings"];

            if (double.TryParse(_mainWindowViewModel.FromAmount, out double fromAmount))
            {
                settings.Amount = fromAmount;
            }
            else
            {
                settings.Amount = 0;
            }

            settings.FromCurrency = _mainWindowViewModel.SelectedFromCurrency;
            settings.ToCurrencty = _mainWindowViewModel.SelectedToCurrency;

            settings.Save();
        }

        private void RestoreMainWindowViewModelSettings()
        {
            Settings settings = (Settings)Resources["Settings"];

            _mainWindowViewModel.FromAmount = settings.Amount.ToString(CultureInfo.InvariantCulture);

            if (_mainWindowViewModel.Currencies.Contains(settings.FromCurrency))
            {
                _mainWindowViewModel.SelectedFromCurrency = settings.FromCurrency;
            }

            if (_mainWindowViewModel.Currencies.Contains(settings.ToCurrencty))
            {
                _mainWindowViewModel.SelectedToCurrency = settings.ToCurrencty;
            }
        }
    }
}
