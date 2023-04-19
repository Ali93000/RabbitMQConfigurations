using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Infrastructure.Interfaces
{
    public interface IRabbitMQConnectionManager: IPooledObjectPolicy<IModel>
    {
        IConnection GetConnection();
        bool IsConnected { get; }
        IModel GetChannel { get; }
        bool IsChannelConnected { get; }
        void Dispose();


    }
}
