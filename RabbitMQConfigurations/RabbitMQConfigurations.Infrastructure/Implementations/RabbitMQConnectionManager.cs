using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQConfigurations.Entities.AppSettingsConfigurations.Interfaces;
using RabbitMQConfigurations.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Infrastructure.Implementations
{
    public class RabbitMQConnectionManager : IRabbitMQConnectionManager
    {
        private readonly ILogger<RabbitMQConnectionManager> _logger;
        private IConnection _connection;
        private IModel _channel;
        private ConnectionFactory _factory;
        private readonly IRabbitMQConfigurations _rabbitMQConfigurations;


        public RabbitMQConnectionManager(ILogger<RabbitMQConnectionManager> logger,
            IRabbitMQConfigurations rabbitMQConfigurations)
        {
            _logger = logger;
            _rabbitMQConfigurations = rabbitMQConfigurations;

            // Create Factory for Connection
            var rabbitMQConnection = _rabbitMQConfigurations.Uri;
            _factory = new ConnectionFactory()
            {
                Uri = new Uri(rabbitMQConnection)
            };
        }


        public IConnection GetConnection()
        {
            if (IsConnected)
                return _connection;
            _connection = _factory.CreateConnection();
            Connect();
            return _connection;
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen;
            }
        }

        public void Connect()
        {
            if (IsConnected)
            {
                // register event 
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;
            }
        }

        public IModel GetChannel
        {
            get
            {
                if (!IsChannelConnected)
                    _channel = _connection.CreateModel();
                return _channel;
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
        }

        public bool IsChannelConnected
        {
            get
            {
                return _channel != null && _channel.IsOpen;
            }
        }


        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            _logger.LogError("ConnectionBlocked {CreationDate} {e}", DateTime.Now, Newtonsoft.Json.JsonConvert.SerializeObject(e));
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            _logger.LogError("ConnectionCallbackException {CreationDate} {e}", DateTime.Now, Newtonsoft.Json.JsonConvert.SerializeObject(e));

        }

        private void OnConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogError("ConnectionShutdown {CreationDate} {e}", DateTime.Now, Newtonsoft.Json.JsonConvert.SerializeObject(e));
        }



        // pooling methods manage channel in publisher   
        public IModel Create()
        {
            return _connection.CreateModel();
        }

        public bool Return(IModel obj)
        {
            if (obj.IsOpen)
            {
                obj.BasicCancel("");
                obj.Close();
            }
            return true;
        }

    }
}
