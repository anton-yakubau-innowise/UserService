using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration, builder.Environment);

startup.ConfigureServices(builder.Services);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });
});

var app = builder.Build();

startup.Configure(app);

app.Run();