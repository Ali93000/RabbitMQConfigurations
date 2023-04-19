using RabbitMQConfigurations.Entities.RabbitMQModels;
using RabbitMQConfigurations.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Infrastructure.Implementations
{
    public class QueueSettingsHelper : IQueueSettingsHelper
    {
        private List<QueueConfigurations> queues = new List<QueueConfigurations>();
        public QueueSettingsHelper()
        {
            // this settings will add into database in v2
            queues.Add(new QueueConfigurations() { QueueId = 1, ExhangeName = "ex_string.message.direct", QueueName = "q_string.message.v1", RoutingKey = "rk_string.msgs" });
            queues.Add(new QueueConfigurations() { QueueId = 2, ExhangeName = "ex_sms.message.direct", QueueName = "q_sms.message.v1", RoutingKey = "rk_sms.msgs" });
            queues.Add(new QueueConfigurations() { QueueId = 3, ExhangeName = "ex_string.message.direct", QueueName = "q_stringmessage.v1", RoutingKey = "rk_string.msgs" });
        }
        public QueueConfigurations LoadQueueSettings(int queueId)
        {
            return queues.FirstOrDefault(c => c.QueueId == queueId);
        }
    }
}
