using ChessGameWithFogOfWar.Controllers;
using ChessGameWithFogOfWar.Hubs;
using ChessGameWithFogOfWar.Services;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<GameProcessCotroller>();
builder.Services.AddSingleton<QueueProvider>();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors",
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:4200")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapHub<GameProcessHub>("/GameProcessHub" , options =>
{
    options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets;
});
app.Use(async (context, next) =>
{
    var hubContext = context.RequestServices
                            .GetRequiredService<IHubContext<GameProcessHub>>();
    if (next != null)
    {
        await next.Invoke();
    }
});

app.Run();
