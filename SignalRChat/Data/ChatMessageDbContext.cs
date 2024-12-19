using Microsoft.EntityFrameworkCore;
using SignalRChat.Models;

public class ChatMessageDbContext : DbContext
{
    public ChatMessageDbContext(DbContextOptions<ChatMessageDbContext> options)
        : base(options)
    {
    }

    public DbSet<ChatMessage> ChatMessages { get; set; }
}

