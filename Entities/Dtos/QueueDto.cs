using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class QueueDto
    {
        public QueueDto(List<ContainerDto> data){
            Data = data;
        }

        public int Index { get; set; }
        public List<ContainerDto> Data { get; set; }

    }
}
