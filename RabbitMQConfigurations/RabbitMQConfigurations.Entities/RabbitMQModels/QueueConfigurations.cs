using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Entities.RabbitMQModels
{
    public class QueueConfigurations
    {
        public int QueueId { get; set; }
        public string QueueName { get; set; }
        public string ExhangeName { get; set; }
        public string RoutingKey { get; set; }
    }


    
}
