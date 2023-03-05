using LittleByte.Common;
using LittleByte.Common.AspNet.Middleware;
using LittleByte.Common.AspNet.Configuration;
using LittleByte.Common.Configuration;
using Wenab.Ynab;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddOpenApi("Wenab")
    .AddSingleton<IDateService, DateService>()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder.Services.BindOptions<YnabOptions>(builder.Configuration);

var app = builder.Build();

app
    .UseHttpsRedirection()
    .UseHsts()
    .UseRouting()
    .UseHttpExceptions()
    .UseModelValidationExceptions()
    .UseEndpoints(endpoints => endpoints.MapControllers())
    .UseOpenApi();

app.Run();
