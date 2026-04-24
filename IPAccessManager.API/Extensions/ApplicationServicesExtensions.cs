using IPAccessManager.Core.Repository.contrent;
using IPAccessManager.Core.Services.Content;
using IPAccessManager.Infrastructure.Generic_Repsitory;
using IPAccessManager.Services.GeoLocationService;
using IPAccessManager.Services.IpValidationService;
using IPAccessManager.Services.TemporalBlockCleanupService;

namespace IPAccessManager.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IGenaricrepository<>), typeof(GenericRepository<>));

            services.AddHttpClient<IGeoLocationService, GeoLocationService>();

            services.AddScoped(typeof(IIpValidationService), typeof(IpValidationService));
         
            









            return services;
        }
    }
}
