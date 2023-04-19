using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Entities.AppSettingsConfigurations.Interfaces
{
    public interface IRabbitMQConfigurations
    {
        public string Uri { get; set; }
    }
}
