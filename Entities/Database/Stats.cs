using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Database
{
    public class Stats
    {
        public Guid Id { get; set; }
        public double ContainersDispatched { get; set; }
        public double ContainersNotDispatched { get; set; }
        public double BudgetUsed { get; set; }
    }
}
