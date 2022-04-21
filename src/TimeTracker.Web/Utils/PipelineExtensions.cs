using TimeTracker.Web.Middlewares;

namespace TimeTracker.Web.Utils;

public static class PipelineExtensions
{
    public static IApplicationBuilder UseExceptionFormatter(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionFormattingMiddleware>();
        return builder;
    }
}