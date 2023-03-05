using LittleByte.Common;
using LittleByte.Common.AspNet.Middleware;
using LittleByte.Common.Logging.Configuration;
using LittleByte.Common.AspNet.Configuration;
using LittleByte.Common.Identity.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddLogs()
    .AddOpenApi("Wenab")
    .AddSingleton<IDateService, DateService>()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

app
    .UseHttpsRedirection()
    .UseHsts()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseHttpExceptions()
    .UseModelValidationExceptions()
    .UseEndpoints(endpoints => endpoints.MapControllers())
    .UseOpenApi();

app.Run();
