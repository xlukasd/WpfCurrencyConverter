using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;

namespace Application.Services.CurrencyConversion
{
    internal class CurrencyProvider : ICurrencyProvider
    {
        public IReadOnlyCollection<CurrencyCodes> GetAvailableCurrencies()
        {
            return Enum.GetValues(typeof(CurrencyCodes)).Cast<CurrencyCodes>().ToList();
        }
    }
}
