using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace TimeTracker.Web.Utils;

public static class SerilogExtensions
{
    public static IHostBuilder AddSerilog(this IHostBuilder builder)
    {
        return builder.UseSerilog(((context, configuration) =>
        {
            configuration
                .MinimumLevel.Error()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithThreadId()
                .WriteTo.Console()
                // .WriteTo.MSSqlServer(context.Configuration.GetConnectionString("ApplicationDatabase"),
                //     new MSSqlServerSinkOptions()
                //     {
                //         SchemaName = context.Configuration.GetConnectionString("Schema"),
                //         AutoCreateSqlTable = true,
                //         TableName = "EventLogs",
                //     })
                .ReadFrom.Configuration(context.Configuration);
        }));
    }

        
}