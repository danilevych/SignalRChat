using Microsoft.EntityFrameworkCore;
using System;

namespace SignalRChat.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? MessageText { get; set; }
        public DateTime SentTime { get; set; }
    }
}