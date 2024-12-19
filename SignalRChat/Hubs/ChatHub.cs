using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Models;
using SignalRChat.Services;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;

        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;
        }


        public async Task SendMessage(string userName, string messageText)
        {
            await Clients.All.SendAsync("ReceiveMessage", userName, messageText);

            await _chatService.SaveMessage(userName, messageText);
        }
    }
}