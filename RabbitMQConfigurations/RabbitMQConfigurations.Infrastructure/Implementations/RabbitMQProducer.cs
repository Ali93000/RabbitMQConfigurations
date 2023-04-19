using RabbitMQ.Client;
using RabbitMQConfigurations.Entities;
using RabbitMQConfigurations.Entities.Enums;
using RabbitMQConfigurations.Entities.RabbitMQModels;
using RabbitMQConfigurations.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Infrastructure.Implementations
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IRabbitMQConnectionManager _rabbitMQConnection;
        private readonly IQueueSettingsHelper _queueSettingsHelper;
        public RabbitMQProducer(IRabbitMQConnectionManager rabbitMQConnection,
            IQueueSettingsHelper queueSettingsHelper)
        {
            _rabbitMQConnection = rabbitMQConnection;
            _queueSettingsHelper = queueSettingsHelper;
        }

        public PublishMessageResponse PublishMessage<T>(int queueId, T message)
        {
            var publishResponse = new PublishMessageResponse();
            var requestReferance = Guid.NewGuid().ToString();

            //Load Queue Configurations
            var queueConfigurations = _queueSettingsHelper.LoadQueueSettings(queueId);

            // get channel if connected, if not create a new channel
            IModel channel = _rabbitMQConnection.GetChannel;

            // Use this section to apply reliable confirm (publisher confirms)
            // Enable publish confirm
            channel.ConfirmSelect();

            // if mandatory = true
            // this section will fire when the message not arrived to a queue
            channel.BasicReturn += (sender, args) =>
            {
                // here we can add any logic or add it into database
                publishResponse.ResponseMessages.Add($"failed to send message {args.Body} {Environment.NewLine} , {args.ReplyText}");
            };

            channel.BasicAcks += (sender, args) =>
            {
                publishResponse.ResponseMessages.Add($"message delivery tag is {args.DeliveryTag}");
            };

            // Declare exchange
            channel.ExchangeDeclare(queueConfigurations.ExhangeName, ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);

            // Declare queue
            channel.QueueDeclare(queueConfigurations.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            // Queue Binding
            channel.QueueBind(queueConfigurations.QueueName, queueConfigurations.ExhangeName, queueConfigurations.RoutingKey, null);

            // Set Basic Properties for the message
            IBasicProperties properties = channel.CreateBasicProperties();
            properties.ContentType = "application/json";
            properties.DeliveryMode = 2; // Non-persistent (1) or persistent (2).
            properties.Expiration = "86400000"; // equal to 24 Hour  (expiration is in milleseconds)
            string messageId = Guid.NewGuid().ToString();
            properties.MessageId = messageId; // Application message Id.
            properties.Priority = 0; // Message priority, 0 to 9.

            // Add Custom Headers to message
            var headers = new Dictionary<string, object>();
            headers.Add("x-systemCode", 5000);
            headers.Add("x-createdAt", DateTime.Now);
            properties.Headers = headers;

            // prepare message to publish,
            // Convert the message to a byte array
            var messageString = message.ToString();

            var body = Encoding.UTF8.GetBytes(messageString);

            // publish message to rabbit MQ server
            channel.BasicPublish(exchange: queueConfigurations.ExhangeName,
                                    routingKey: queueConfigurations.RoutingKey,
                                    mandatory: true,
                                    body: body,
                                    basicProperties: properties);

            publishResponse.ResponseMessages.Add($"message published to queue successfully with message id {messageId}");
            return publishResponse;
        }
    }
}
