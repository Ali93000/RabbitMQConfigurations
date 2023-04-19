using RabbitMQConfigurations.Entities.RabbitMQModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Infrastructure.Interfaces
{
    public interface IQueueSettingsHelper
    {
        QueueConfigurations LoadQueueSettings(int queueId);
    }
}
