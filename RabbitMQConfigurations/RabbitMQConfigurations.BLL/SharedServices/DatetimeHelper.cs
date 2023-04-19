using RabbitMQConfigurations.Entities.Interfaces.SharedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.BLL.SharedServices
{
    public class DatetimeHelper : IDatetimeHelper
    {
        public long GetDatetimeNowAsTimestamp()
        {
            DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
            var timeStamp = now.ToUnixTimeSeconds();
            return timeStamp;
        }
    }
}
