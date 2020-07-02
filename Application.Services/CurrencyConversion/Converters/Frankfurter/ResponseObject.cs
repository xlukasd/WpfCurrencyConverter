using System.Collections.Generic;

namespace Application.Services.CurrencyConversion.Converters.Frankfurter
{
    internal class ResponseObject
    {
        public ResponseObject(IDictionary<string, double> rates)
        {
            Rates = rates;
        }

        public IDictionary<string, double> Rates { get; }
    }
}
