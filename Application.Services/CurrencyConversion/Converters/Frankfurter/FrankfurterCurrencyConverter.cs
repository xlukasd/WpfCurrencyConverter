using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.Shared.CurrencyConversion;

namespace Application.Services.CurrencyConversion.Converters.Frankfurter
{
    internal class FrankfurterCurrencyConverter : AbstractOnlineConverter<ResponseObject>, IOnlineCurrencyConverter
    {
        protected override string ApiBaseUri => @"https://api.frankfurter.app/latest";

        protected override IEnumerable<KeyValuePair<string, string>> GetQueryParameters(InputObject inputObject)
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("from", inputObject.FromCurrency.ToString()),
                new KeyValuePair<string, string>("to", inputObject.ToCurrency.ToString()),
                new KeyValuePair<string, string>("amount", inputObject.Amount.ToString(CultureInfo.InvariantCulture))
            };
        }

        protected override OutputObject ConvertResponseToCommonOutput(ResponseObject responseObject, InputObject inputObject)
        {
            double amount = responseObject.Rates.Single(x =>
                x.Key.Equals(inputObject.ToCurrency.ToString(), StringComparison.InvariantCultureIgnoreCase)).Value;

            return new OutputObject(amount);
        }
    }
}
