﻿using BusinessLogic;
using Entities.Interfaces.BusinessLogic;
using Entities.Interfaces.Repositories;
using Repositories;
using Repositories.SQLite;

namespace TransportersApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyRepositories(this IServiceCollection services) =>
            services
                .AddDbContext<DbContextRepository>()
                .AddTransient<IContainerRepository, ContainerRepositorySQLite>();


        public static IServiceCollection AddDependencyBusiness(this IServiceCollection services) =>
            services
                .AddTransient<IContainerBL, ContainerBL>();
    }
}
