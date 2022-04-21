using System.ComponentModel.DataAnnotations;
using System.Net;
using TimeTracker.Core.Shared.Exceptions;
using TimeTracker.Core.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TimeTracker.Web.Middlewares;

public class ExceptionFormattingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ExceptionFormattingMiddleware> _logger;

    public ExceptionFormattingMiddleware(RequestDelegate next, IWebHostEnvironment env,
        ILoggerFactory loggerFactory)
    {
        _next = next;
        _env = env;
        _logger = loggerFactory
            .CreateLogger<ExceptionFormattingMiddleware>();
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleException(httpContext, ex, _env);
        }
    }

    private Task HandleException(HttpContext context, Exception exception, IWebHostEnvironment env)
    {
        var code = HttpStatusCode.InternalServerError;
        var error = env.IsDevelopment()? exception.Message : DummyMessage();

        switch (exception)
        {
            case BusinessRuleViolationException businessRuleViolationException:
                code = HttpStatusCode.Forbidden;
                error = businessRuleViolationException.Message;
                break;
            case ValidationException validationException:
                code = HttpStatusCode.UnprocessableEntity;
                error = validationException.Message;
                break;
        }

        if (code == HttpStatusCode.InternalServerError)
        {
            _logger.LogError(exception, message:
                "Http Request Exception Information: {Environment} Schema:{Schema} Host: {Host} Path: {Path} QueryString: {QueryString}  Error Message: {ErrorMessage} Error Trace: {StackTrace}",
                Environment.NewLine, context.Request.Scheme, context.Request.Host, context.Request.Path,
                context.Request.QueryString, exception.Message, exception.StackTrace);
        }

        var response = Envelope.Error(error);
        var result = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) code;
        return context.Response.WriteAsync(result);
    }

    private static string DummyMessage()
    {
        return "Sorry, Something wrong on our side, Please try again";
    }
}