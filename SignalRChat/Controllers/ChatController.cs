using Microsoft.AspNetCore.Mvc;
using SignalRChat.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages()
        {
            List<ChatMessage> messages = await _chatService.GetMessages();
            return Ok(messages);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
        {
            if (message == null || string.IsNullOrEmpty(message.UserName) || string.IsNullOrEmpty(message.MessageText))
            {
                return BadRequest("Invalid message data.");
            }

            await _chatService.SaveMessage(message.UserName, message.MessageText);
            return Ok();
        }
    }
}
