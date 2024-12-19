using Microsoft.EntityFrameworkCore;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Services
{
    public class ChatService
    {
        private readonly ChatMessageDbContext _context;

        public ChatService(ChatMessageDbContext context)
        {
            _context = context;
        }

        public async Task SaveMessage(string userName, string messageText)
        {
            var chatMessage = new ChatMessage
            {
                UserName = userName,
                MessageText = messageText,
                SentTime = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChatMessage>> GetMessages()
        {
            return await _context.ChatMessages
                .OrderBy(m => m.SentTime)
                .ToListAsync();
        }
    }
}
