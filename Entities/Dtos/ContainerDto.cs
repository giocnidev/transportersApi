using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ContainerDto
    {
        public string? Name { get; set; }
        public double TransportCost { get; set; }
        public double ContainerPrice { get; set; }
    }
}
