using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ContainerDto
    {
        /// <summary> 
        /// Nombrel de contenedor 
        /// </summary>
        public string? Name { get; set; }

        /// <summary> 
        /// Costo del transporte
        /// </summary>
        public double TransportCost { get; set; }

        /// <summary> 
        /// Precio del contenedor
        /// </summary>
        public double ContainerPrice { get; set; }
    }
}
