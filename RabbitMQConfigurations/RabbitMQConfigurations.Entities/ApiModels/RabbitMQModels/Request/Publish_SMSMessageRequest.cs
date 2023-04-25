using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Entities.ApiModels.RabbitMQModels.Request
{
    public class Publish_SMSMessageRequest
    {
        public string SenderName { get; set; }
        public string MessageType { get; set; }
        public string Recipient { get; set; }
        public string MessageContent { get; set; }
    }
}
