var builder = WebApplication.CreateBuilder(args);

// 1. Add YARP services
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// 2. Use the Proxy middleware
app.MapReverseProxy();

app.Run();