using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQConfigurations.Domain.Domain;
using RabbitMQConfigurations.Entities.ApiModels.RabbitMQModels.Request;
using RabbitMQConfigurations.Entities.DBEntities;
using RabbitMQConfigurations.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Infrastructure.Implementations
{
    public class RabbitMQConsumer : IRabbitMQConsumer
    {
        private readonly IRabbitMQConnectionManager _rabbitMQConnectionManager;
        private readonly IQueueSettingsHelper _queueSettingsHelper;
        private readonly RabbitMQDBContext _rabbitMQDBContext;

        public RabbitMQConsumer(IRabbitMQConnectionManager rabbitMQConnectionManager,
            IQueueSettingsHelper queueSettingsHelper,
            RabbitMQDBContext rabbitMQDBContext)
        {
            _rabbitMQConnectionManager = rabbitMQConnectionManager;
            _queueSettingsHelper = queueSettingsHelper;
            _rabbitMQDBContext = rabbitMQDBContext;
        }

        public async void ConsumeMessages(int queueId)
        {
            //Load Queue Configurations
            var queueConfigurations = _queueSettingsHelper.LoadQueueSettings(queueId);

            // get channel if connected, if not create a new channel
            IModel channel = _rabbitMQConnectionManager.GetChannel;

            // Declare exchange
            channel.ExchangeDeclare(queueConfigurations.ExhangeName, ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);

            // Declare queue
            channel.QueueDeclare(queueConfigurations.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            // Queue Binding
            channel.QueueBind(queueConfigurations.QueueName, queueConfigurations.ExhangeName, queueConfigurations.RoutingKey, arguments: null);

            // used to set a limit number of unacknowledged messages
            channel.BasicQos(0, 1, false);

            // start consuming
            var consumer =  new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (sender, args) =>
            {
                // get the body ( byte[] ) from memory it's readonly memory
                var body = args.Body.ToArray();
                // get the body as string
                var message = Encoding.UTF8.GetString(body);

                // start process the message
                bool isProcessed = await ProcessMessage(message);
                if (isProcessed)
                {
                    // note the message that it's acknowledged
                    channel.BasicAck(args.DeliveryTag, false);
                }
                else
                {
                    // requeue the message to the queue again
                    channel.BasicReject(args.DeliveryTag, requeue: true);
                }
            };


            string consumerTag = channel.BasicConsume(queueConfigurations.QueueName,
                                                       autoAck: false,
                                                       consumer: consumer);

            // we put it here to keep the consumer is always running and continue listinging and proccessing messages
            Console.ReadLine();

            // used to cancel or delete the content class consumer
            channel.BasicCancel(consumerTag);
        }
    
        private async Task<bool> ProcessMessage(string message)
        {
            var model = JsonConvert.DeserializeObject<Publish_StringMessageRequest>(message);
            if (model == null)
                return false;

            DB_StringMessageConsumer dB_StringMessage = new DB_StringMessageConsumer()
            {
                Message = model.Message,
                CreatedAt= DateTime.Now,
                CreatedBy = "RabbitMQ Consumer v1"
            };
            await _rabbitMQDBContext.AddAsync(dB_StringMessage);
            int result = await _rabbitMQDBContext.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }
    }
}
