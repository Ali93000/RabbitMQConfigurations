using MediatR;
using RabbitMQConfigurations.Entities.ApiModels.RabbitMQModels.Request;
using RabbitMQConfigurations.Entities.ApiModels.RabbitMQModels.Response;
using RabbitMQConfigurations.Entities.GenericModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.BLL.RabbitMQServices.Command
{
    public record PublishSMSMessageCommand(Publish_SMSMessageRequest publish_SMSMessageRequest) : IRequest<PublishSMSMessageResponse>;
}

