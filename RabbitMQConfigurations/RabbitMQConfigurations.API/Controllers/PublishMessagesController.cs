using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQConfigurations.BLL.RabbitMQServices.Command;
using RabbitMQConfigurations.Entities.ApiModels.RabbitMQModels.Request;

namespace RabbitMQConfigurations.API.Controllers
{
    [Route("api/v1/publish")]
    [ApiController]
    public class PublishMessagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PublishMessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("string-message")]
        public async Task<IActionResult> PublishStringMessage(Publish_StringMessageRequest request)
        {
            var result =  await _mediator.Send(new PublishStringMessageCommand(request));
            if (!result.IsSuccessfully)
                return BadRequest(result);
            return Ok(result);  
        }
    }
}
