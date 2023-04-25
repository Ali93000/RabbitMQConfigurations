using MediatR;
using RabbitMQConfigurations.BLL.RabbitMQServices.Command;
using RabbitMQConfigurations.Entities.ApiModels.RabbitMQModels.Response;
using RabbitMQConfigurations.Entities.Enums;
using RabbitMQConfigurations.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.BLL.RabbitMQServices.Handlers
{
    public class PublishSMSMessageHandler : IRequestHandler<PublishSMSMessageCommand, PublishSMSMessageResponse>
    {
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public PublishSMSMessageHandler(IRabbitMQProducer rabbitMQProducer)
        {
            _rabbitMQProducer = rabbitMQProducer;
        }

        public Task<PublishSMSMessageResponse> Handle(PublishSMSMessageCommand request, CancellationToken cancellationToken)
        {
            var push = _rabbitMQProducer.PublishMessage((int)QueueTypeEnum.SMSQueue, request.publish_SMSMessageRequest);
            var result = new PublishSMSMessageResponse()
            {
                RequestReference = push.RequestReferance,
                IsSuccessfully = push.IsSuccessfully,
                ResponseCode = push.ResponseCode,
                ResponseMessages = push.ResponseMessages,
            };
            return Task.FromResult(result);
        }
    }
}

