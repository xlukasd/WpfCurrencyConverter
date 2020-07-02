using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.CurrencyConversion.Converters;
using CSharpFunctionalExtensions;
using Domain.Shared.CurrencyConversion;

namespace Application.Services.CurrencyConversion
{
    internal class CurrencyConverter : ICurrencyConverter
    {
        private readonly IList<IOnlineCurrencyConverter> _currencyConverters;

        public CurrencyConverter(IEnumerable<IOnlineCurrencyConverter> currencyConverters)
        {
            _currencyConverters = currencyConverters.ToList();
        }

        public async Task<Result<OutputObject>> Convert(InputObject inputObject)
        {
            Result<OutputObject> result = Result.Failure<OutputObject>("No available services for conversion");

            foreach (var currencyConverter in _currencyConverters)
            {
                result = await currencyConverter.Convert(inputObject);

                if (result.IsSuccess)
                {
                    break;
                }
            }

            return result;
        }
    }
}
