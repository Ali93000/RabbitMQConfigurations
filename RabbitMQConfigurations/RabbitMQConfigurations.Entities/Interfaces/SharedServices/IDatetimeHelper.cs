using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Entities.Interfaces.SharedServices
{
    public interface IDatetimeHelper
    {
        long GetDatetimeNowAsTimestamp();
    }
}
