using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Entities.DBEntities
{
    public class DB_StringMessageConsumer
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
