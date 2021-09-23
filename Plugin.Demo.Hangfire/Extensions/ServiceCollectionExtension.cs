using Microsoft.Extensions.DependencyInjection;
using Plugin.Demo.Hangfire.Jobs.Implementations;
using Plugin.Demo.Hangfire.Jobs.Interfaces;

namespace Plugin.Demo.Hangfire.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void InitService(this IServiceCollection services)
        {
            services.AddTransient<ITestJob, TestJob>();
        }
    }
}
