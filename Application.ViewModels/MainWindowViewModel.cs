using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Application.Services.CurrencyConversion;
using CSharpFunctionalExtensions;
using Domain.Common;
using Domain.Shared.CurrencyConversion;
using GalaSoft.MvvmLight.CommandWpf;

namespace Application.ViewModels
{
    public class MainWindowViewModel : AbstractViewModel
    {
        private readonly ICurrencyConverter _currencyConverter;
        private string _result;

        private bool _isRunningConversion;
        private CurrencyCodes _selectedFromCurrency;
        private CurrencyCodes _selectedToCurrency;

        public MainWindowViewModel(ICurrencyConverter currencyConverter,
            ICurrencyProvider currencyProvider)
        {
            _currencyConverter = currencyConverter;
            ConvertCommand = new RelayCommand(Convert, CanConvert);

            Currencies = new ObservableCollection<CurrencyCodes>(currencyProvider.GetAvailableCurrencies());
            SelectedFromCurrency = Currencies.FirstOrDefault();
            SelectedToCurrency = Currencies.LastOrDefault();
        }

        public string FromAmount { get; set; }

        public ObservableCollection<CurrencyCodes> Currencies { get; }

        public CurrencyCodes SelectedFromCurrency
        {
            get => _selectedFromCurrency;
            set => SetProperty(ref _selectedFromCurrency, value);
        }

        public CurrencyCodes SelectedToCurrency
        {
            get => _selectedToCurrency;
            set => SetProperty(ref _selectedToCurrency, value);
        }

        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        public RelayCommand ConvertCommand { get; set; }

        private bool CanConvert()
        {
            return !_isRunningConversion 
                   && double.TryParse(FromAmount, NumberStyles.Currency, CultureInfo.CurrentCulture, out double _)
                   && SelectedToCurrency != SelectedFromCurrency;
        }

        private async void Convert()
        {
            _isRunningConversion = true;

            try
            {
                double fromAmount = double.Parse(FromAmount.Replace(',', '.'), NumberStyles.Currency,
                    CultureInfo.CurrentCulture);

                InputObject inputObject = new InputObject(SelectedFromCurrency, SelectedToCurrency, fromAmount);

                Result<OutputObject> conversionResult = await _currencyConverter.Convert(inputObject);

                if (conversionResult.IsSuccess)
                {
                    string finalAmount = conversionResult.Value.Amount.ToString(CultureInfo.InvariantCulture);

                    Result = $@"{fromAmount.ToString(CultureInfo.CurrentCulture)} {SelectedFromCurrency.ToString().ToUpperInvariant()} = {finalAmount.ToString(CultureInfo.CurrentCulture)} {SelectedToCurrency.ToString().ToUpperInvariant()}";
                }
                else
                {
                    Result = $@"Error: {conversionResult.Error}";
                }
            }
            finally
            {
                _isRunningConversion = false;
                ConvertCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
