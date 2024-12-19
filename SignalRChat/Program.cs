using Microsoft.EntityFrameworkCore;
using SignalRChat.Hubs;
using SignalRChat.Models;
using SignalRChat.Controllers;
using System;
using SignalRChat.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSignalR();


builder.Services.AddDbContext<ChatMessageDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ChatService>();

builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Debug);
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");

app.MapGet("/api/messages", async (ChatMessageDbContext context) =>
{
    return await context.ChatMessages.ToListAsync();
});


app.Run();