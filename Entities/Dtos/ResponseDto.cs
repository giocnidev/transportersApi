using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ResponseDto<T>
    {

        public ResponseDto(){
            ResponseTime = DateTime.Now;
            Message = "";
            TimeElapsed = "";
            StatusCode = (int)HttpStatusCode.OK;
            Code = 0;
        }

        public DateTime ResponseTime { get; set; }
        public string TimeElapsed { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        

        [JsonIgnore]
        public int StatusCode { get; set; }

    }
}
