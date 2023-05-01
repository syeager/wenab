using LittleByte.Common;
using LittleByte.Common.AspNet.Configuration;
using LittleByte.Common.AspNet.Middleware;
using LittleByte.Common.Configuration;
using Wenab.Api.Models;
using Wenab.Api.Ynab;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddSingleton<IDateService, DateService>()
    .AddOpenApi("v1")
    .BindOptions<BudgetConfig>(builder.Configuration);

builder.Services.BindOptions<YnabOptions>(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app
        .UseDebugMiddleware()
        .UseDeveloperExceptionPage()
        .UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
}

app
    .UseHttpsRedirection()
    .UseHsts()
    .UseRouting()
    .UseHttpExceptions()
    .UseModelValidationExceptions()
    .UseEndpoints(endpoints => endpoints.MapControllers())
    .UseOpenApi();

app.Run();