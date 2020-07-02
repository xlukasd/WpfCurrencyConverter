using Domain.Common;

namespace Domain.Shared.CurrencyConversion
{
    public class OutputObject
    {
        public OutputObject(double amount)
        {
            Amount = amount;
        }

        public double Amount { get; }
    }
}
