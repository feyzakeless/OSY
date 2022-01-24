using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSY.Model;
using OSY.Model.ModelMessage;
using OSY.Service;
using OSY.Service.MessageServiceLayer;

namespace OSY.API.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    [Authorize]

    public class MessageController : Controller
    {
        private readonly IMessageService messageService;
        private readonly JwtService jwtService;
        public MessageController(IMessageService _messageService, JwtService _jwtService)
        {
            messageService = _messageService;
            jwtService = _jwtService;
        }

        [HttpPost]
        public General<MessageViewModel> Insert([FromBody] SendMessageViewModel message)
        {
           
            return messageService.SendMessage(message);
        }

        
        [HttpGet]
        public General<MessageViewModel> GetFirst([FromQuery] int id)
        {
            return messageService.GetMessage(id);
        }

        [HttpGet("Detail")]
        public General<MessageViewModel> GetDetail([FromQuery] int receiverId, int senderId)
        {
            return messageService.GetListRelatedToAnyChat(receiverId, senderId);
        }
    }
}
