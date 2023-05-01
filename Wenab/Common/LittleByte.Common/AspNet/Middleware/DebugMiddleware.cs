using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LittleByte.Common.AspNet.Middleware;

public static class DebugMiddlewareExtension
{
    public static IApplicationBuilder UseDebugMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<DebugMiddleware>();
}

public sealed class DebugMiddleware
{
    private readonly RequestDelegate next;

    public DebugMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    [UsedImplicitly]
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);
    }
}