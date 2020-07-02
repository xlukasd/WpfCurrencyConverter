using Application.Services.CurrencyConversion;
using Application.Services.CurrencyConversion.Converters;
using Application.Services.CurrencyConversion.Converters.Frankfurter;
using Ninject.Modules;

namespace Application.Services
{
    public class ServicesBindingModule : NinjectModule
    {
        public override void Load()
        {
            LoadCurrencyConversionApis();

            Bind<ICurrencyConverter>().To<CurrencyConverter>();
            Bind<ICurrencyProvider>().To<CurrencyProvider>();
        }

        private void LoadCurrencyConversionApis()
        {
            Bind<IOnlineCurrencyConverter>().To<FrankfurterCurrencyConverter>().WhenInjectedExactlyInto<CurrencyConverter>();
        }
    }
}
