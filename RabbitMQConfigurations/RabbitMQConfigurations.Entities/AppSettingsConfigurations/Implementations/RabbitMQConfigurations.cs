using RabbitMQConfigurations.Entities.AppSettingsConfigurations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Entities.AppSettingsConfigurations.Implementations
{
    public class RabbitMQConfigurations : IRabbitMQConfigurations
    {
        public string Uri { get; set; }
    }
}
