using ImageGallery.Api.Interfaces.Services;
using ImageGallery.Api.Services;

namespace ImageGallery.Api.Extensions
{
    public static class ServicesDI
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICacheService<>), typeof(CacheService<>));
            services.AddScoped<IFileService, FileService>();
            services.AddScoped(typeof(IQueryService<>), typeof(QueryService<>));

            return services;
        }
    }
}
