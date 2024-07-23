using FredDevelopmentKit.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly.Extensions.Http;
using Polly;

namespace FredDevelopmentKit.Services
{
    public static class FredServicesExtensions
    {
        /// <summary>
        /// Adds the Fred services to the host builder, typically used in Console applications.  Although 
        /// singletons could be used in the console case, using IHttpClientFactory in all cases will allow 
        /// resiliency features to be used in both cases.
        /// </summary>
        /// <param name="builder"></param>
        public static void AddFredServices(this IHostBuilder builder)
        {
            builder.ConfigureServices((hostContext, services) =>
            {
                AddFredServicesNoOptions(services);

                services.Configure<FredClientOptions>(
                    hostContext.Configuration.GetSection($"{FredClientOptions.FredClient}"));
            });
        }
        /// <summary>
        /// Adds the Fred services to the service collection, typically used in ASP.NET Core applications.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationManager"></param>
        public static void AddFredWebServices(this IServiceCollection services,
            ConfigurationManager configurationManager)
        {
            AddFredServicesNoOptions(services);
            services.Configure<FredClientOptions>(
                configurationManager.GetSection($"{FredClientOptions.FredClient}"));
        }
        /// <summary>
        /// shared method to add the services in all scenarios
        /// </summary>
        /// <param name="services"></param>
        public static void AddFredServicesNoOptions(IServiceCollection services)
        {
            services.AddHttpClient<IFredHttpClient, FredHttpClient>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy());

            services.AddTransient<IFredCategoryService, FredCategoryService>();
            services.AddTransient<IFredReleaseService, FredReleaseService>();
            services.AddTransient<IFredSeriesService, FredSeriesService>();
            services.AddTransient<IFredSourceService, FredSourceService>();
            services.AddTransient<IFredTagsService, FredTagsService>();
            services.AddTransient<IFredMapsService, FredMapsService>();
        }
        /// <summary>
        /// Gets a retry policy that will retry 6 times with exponential backoff
        /// </summary>
        /// <returns></returns>
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
