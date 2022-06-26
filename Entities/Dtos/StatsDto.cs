using Entities.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class StatsDto
    {
        public double ContainersDispatched { get; set; }
        public double ContainersNotDispatched { get; set; }
        public double BudgetUsed { get; set; }

        public static StatsDto FromEntity(Stats stats)
        {
            return new StatsDto() { 
                BudgetUsed = Math.Round(stats.BudgetUsed, 2),
                ContainersDispatched = Math.Round(stats.ContainersNotDispatched, 2),
                ContainersNotDispatched = Math.Round(stats.ContainersNotDispatched, 2)
            };
        }
    }
}
