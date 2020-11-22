using Acr.UserDialogs;
using MvvmCross.IoC;
using SI.Core.Abstract;
using SI.Core.Services;

namespace SI.Core
{
    public static class SetupIoC
    {
        public static IMvxIoCProvider RegisterDependencies(IMvxIoCProvider provider)
        {
            provider.LazyConstructAndRegisterSingleton<IDSAService, DSAService>();
            provider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            return provider;
        }
    }
}
