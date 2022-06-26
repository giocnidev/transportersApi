using BusinessLogic;
using Entities.Interfaces.BusinessLogic;
using Entities.Interfaces.Repositories;
using Repositories;
using Repositories.SQLite;

namespace TransportersApi.Extensions
{
    /// <summary>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// </summary>
        public static IServiceCollection AddDependencyRepositories(this IServiceCollection services) =>
            services
                .AddDbContext<DbContextRepositorySQLite>()
                .AddTransient<IContainerRepository, ContainerRepositorySQLite>();


        /// <summary>
        /// </summary>
        public static IServiceCollection AddDependencyBusiness(this IServiceCollection services) =>
            services
                .AddTransient<IContainerBL, ContainerBL>();
    }
}
