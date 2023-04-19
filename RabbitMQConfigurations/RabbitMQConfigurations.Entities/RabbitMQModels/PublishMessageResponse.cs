using RabbitMQConfigurations.Entities.GenericModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Entities.RabbitMQModels
{
    public class PublishMessageResponse : GenericResponse
    {
        public string RequestReferance { get; set; }
    }
}
