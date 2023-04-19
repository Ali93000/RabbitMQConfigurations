using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Entities.GenericModels
{
    public class GenericResponse
    {
        public GenericResponse()
        {
            ResponseMessages = new List<string>();
        }
        public bool IsSuccessfully { get; set; }
        public int ResponseCode { get; set; }
        public List<string> ResponseMessages { get; set; }
    }
}
