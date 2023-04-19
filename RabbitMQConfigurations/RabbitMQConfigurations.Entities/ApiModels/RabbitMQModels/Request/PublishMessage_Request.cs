using RabbitMQConfigurations.Entities.RabbitMQModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Entities.ApiModels.RabbitMQModels.Request
{
    public class PublishMessage_Request<T>
    {
        public QueueConfigurations QueueConfigurations { get; set; }

        public T Message { get; set; }
    }
}
