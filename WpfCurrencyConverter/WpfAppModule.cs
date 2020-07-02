using Application.ViewModels;
using Ninject.Modules;

namespace WpfCurrencyConverter
{
    public class WpfAppModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Load(new[] { new ViewModelBindingModule() });
        }
    }
}
