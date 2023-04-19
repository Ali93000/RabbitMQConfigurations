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
    public class PublishStringMessageHandler : IRequestHandler<PublishStringMessageCommand, PulishStringMessageResponse>
    {
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public PublishStringMessageHandler(IRabbitMQProducer rabbitMQProducer)
        {
            _rabbitMQProducer = rabbitMQProducer;
        }

        public Task<PulishStringMessageResponse> Handle(PublishStringMessageCommand request, CancellationToken cancellationToken)
        {
            var push = _rabbitMQProducer.PublishMessage((int)QueueTypeEnum.StringQueue, request.publishMessage_Request.Message);
            var result = new PulishStringMessageResponse()
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
