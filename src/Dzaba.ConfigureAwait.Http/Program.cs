using Dzaba.ConfigureAwait.Lib;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterConfigureAwaitLib();

var logger = new LoggerConfiguration()
    .Enrich.WithThreadId()
    .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{ThreadId}] [{SourceContext}] {Level:u3} - {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
builder.Services.AddLogging(l => l.AddSerilog(logger, true));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
