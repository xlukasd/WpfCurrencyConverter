using System.Collections.Generic;
using Domain.Common;

namespace Application.Services.CurrencyConversion
{
    public interface ICurrencyProvider
    {
        IReadOnlyCollection<CurrencyCodes> GetAvailableCurrencies();
    }
}
