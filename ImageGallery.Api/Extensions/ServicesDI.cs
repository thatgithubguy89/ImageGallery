using ImageGallery.Api.Interfaces.Services;
using ImageGallery.Api.Services;

namespace ImageGallery.Api.Extensions
{
    public static class ServicesDI
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
