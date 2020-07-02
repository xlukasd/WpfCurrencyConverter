using Application.Services;
using Ninject.Modules;

namespace Application.ViewModels
{
    public class ViewModelBindingModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Load(new[] {new ServicesBindingModule()});

            Bind<MainWindowViewModel>().ToSelf();
        }
    }
}
