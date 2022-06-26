using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class DispatchDto
    {
        public double Budget { get; set; }
        public List<ContainerDto>? Containers { get; set; }
    }
}
