using Entities.Database;
using Entities.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.SQLite
{
    public class ContainerRepositorySQLite : IContainerRepository
    {
        private readonly IServiceScopeFactory _serviceScope;

        public ContainerRepositorySQLite(IServiceScopeFactory serviceScope) {
            _serviceScope = serviceScope;
        }

        public bool UpdateStats(Stats stats){
            using var scope = _serviceScope.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DbContextRepository>();
            context.Database.EnsureCreated();

            context.Stats.Update(stats);

            int affectedRecords = context.SaveChanges();
            return affectedRecords > 0;
        }

        public Stats? GetStats(){
            using var scope = _serviceScope.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DbContextRepository>();
            context.Database.EnsureCreated();

            return context.Stats.FirstOrDefault();
        }
    }
}
