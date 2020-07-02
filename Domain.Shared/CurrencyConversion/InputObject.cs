using Domain.Common;

namespace Domain.Shared.CurrencyConversion
{
    public class InputObject
    {
        public InputObject(CurrencyCodes fromCurrency, CurrencyCodes toCurrency, double amount)
        {
            FromCurrency = fromCurrency;
            ToCurrency = toCurrency;
            Amount = amount;
        }

        public CurrencyCodes FromCurrency { get; }
        public CurrencyCodes ToCurrency { get; }
        public double Amount { get; }
    }
}
