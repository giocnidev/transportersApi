using Entities.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces.Repositories
{
    public interface IContainerRepository
    {
        public bool UpdateStats(Stats stats);

        public Stats? GetStats();
    }
}
