using RabbitMQ.Client;
using RabbitMQConfigurations.Entities.RabbitMQModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Infrastructure.Interfaces
{
    public interface IRabbitMQProducer
    {
        PublishMessageResponse PublishMessage<T>(int queueId, T message);
    }
}
