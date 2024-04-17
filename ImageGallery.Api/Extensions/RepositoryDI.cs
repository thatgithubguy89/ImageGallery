using ImageGallery.Api.Interfaces.Repositories;
using ImageGallery.Api.Repositories;

namespace ImageGallery.Api.Extensions
{
    public static class RepositoryDI
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IUserImageRepository, UserImageRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();

            return services;
        }
    }
}
