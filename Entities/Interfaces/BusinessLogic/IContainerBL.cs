using Entities.Database;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces.BusinessLogic
{
    public interface IContainerBL
    {
        public ResponseDto<string?[]> SelectContainers(double budget, List<ContainerDto>? data);
        public ResponseDto<StatsDto> GetStats();
    }
}
