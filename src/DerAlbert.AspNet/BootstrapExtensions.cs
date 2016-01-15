using DerAlbert.Bootstrap;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DerAlbert.AspNet
{
    public static class BootstrapExtensions
    {
        public static void UseBoostrapper(this IApplicationBuilder builder)
        {
            var bootstrapper = builder.ApplicationServices.GetService<Bootstrapper>();
            bootstrapper.Execute();
        }
    }
}