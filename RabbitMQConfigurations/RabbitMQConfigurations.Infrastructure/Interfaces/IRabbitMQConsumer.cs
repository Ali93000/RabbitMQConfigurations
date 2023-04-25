using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Infrastructure.Interfaces
{
    public interface IRabbitMQConsumer
    {
        void ConsumeMessages(int queueId);
    }
}
