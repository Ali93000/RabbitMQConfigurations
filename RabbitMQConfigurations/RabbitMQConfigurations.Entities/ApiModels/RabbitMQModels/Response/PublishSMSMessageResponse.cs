using RabbitMQConfigurations.Entities.GenericModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Entities.ApiModels.RabbitMQModels.Response
{
    public class PublishSMSMessageResponse : GenericResponse
    {
        public string RequestReference { get; set; }
    }
}
