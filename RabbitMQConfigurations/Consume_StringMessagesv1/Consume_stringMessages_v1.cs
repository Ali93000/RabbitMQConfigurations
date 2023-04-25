using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using RabbitMQConfigurations.Domain.Domain;
using RabbitMQConfigurations.Entities.ApiModels.RabbitMQModels.Request;
using RabbitMQConfigurations.Entities.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQConfigurations.Infrastructure.Interfaces;
using RabbitMQConfigurations.Infrastructure.Implementations;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Extensions.Logging;
using RabbitMQConfigurations.Entities.AppSettingsConfigurations.Interfaces;
using RabbitMQConfigurations.Entities.AppSettingsConfigurations.Implementations;
using RabbitMQConfigurations.Entities.RabbitMQModels;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Consume_StringMessagesv1
{
    public class Consume_stringMessages_v1
    {
        private RabbitMQDBContext _rabbitMQDBContext;
        public Consume_stringMessages_v1()
        {
            var contextOptions = new DbContextOptionsBuilder<RabbitMQDBContext>()
                    .UseSqlServer("Server=.; Database=RabbitMQDB; Trusted_Connection=True; MultipleActiveResultSets=true;TrustServerCertificate=true")
                    .Options;
            _rabbitMQDBContext = new RabbitMQDBContext(contextOptions);
        }
        public async void ConsumeMessages(int queueId)
        {
            string exchangeName = "ex_string.message.direct";
            string queueName = "q_string.message.v1";
            string rkName = "rk_string.msgs";

            // get channel if connected, if not create a new channel
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            IConnection connection = factory.CreateConnection();

            IModel channel = connection.CreateModel();

            // Declare exchange
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);

            // Declare queue
            channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            // Queue Binding
            channel.QueueBind(queueName, exchangeName, rkName, arguments: null);

            // used to set a limit number of unacknowledged messages
            channel.BasicQos(0, 1, false);

            // start consuming
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) =>
            {
                // get the body ( byte[] ) from memory it's readonly memory
                var body = args.Body.ToArray();
                // get the body as string
                var message = Encoding.UTF8.GetString(body);

                // start process the message
                bool isProcessed = ProcessMessage(message).Result;
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


            string consumerTag = channel.BasicConsume(queueName,
                                                       autoAck: false,
                                                       consumer: consumer);

            // we put it here to keep the consumer is always running and continue listinging and proccessing messages
            //Console.ReadLine();

            // used to cancel or delete the content class consumer
            channel.BasicCancel(consumerTag);
        }

        private async Task<bool> ProcessMessage(string message)
        {

            //var model = JsonConvert.DeserializeObject<string>(message);
            //if (model == null)
            //    return false

            DB_StringMessageConsumer dB_StringMessage = new DB_StringMessageConsumer()
            {
                Message = message,
                CreatedAt = DateTime.Now,
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
