using Microsoft.AspNetCore.Mvc;
using SignalRChat.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using SignalRChat.Models;
using Microsoft.Extensions.Logging;

namespace SignalRChat.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;
        private readonly ILogger<ChatController> _logger;

        public ChatController(ChatService chatService, ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages()
        {
            try
            {
                List<ChatMessage> messages = await _chatService.GetMessages();
                return Ok(messages);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"GetMessages: Error retrieving messages. {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving messages.");
            }
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
        {
            if (message == null || string.IsNullOrEmpty(message.UserName) || string.IsNullOrEmpty(message.MessageText))
            {
                _logger.LogWarning("SendMessage: Received invalid message data.");
                return BadRequest("Invalid message data.");
            }

            try
            {
                await _chatService.SaveMessage(message.UserName, message.MessageText);
                _logger.LogInformation($"SendMessage: Message from user '{message.UserName}' saved successfully.");
                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"SendMessage: Error saving message. {ex.Message}");
                return StatusCode(500, "An error occurred while saving the message.");
            }
        }
    }
}
