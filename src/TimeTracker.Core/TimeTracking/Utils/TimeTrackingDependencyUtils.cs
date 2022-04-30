using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Persistence;
using TimeTracker.Core.TimeTracking.Interfaces;
using TimeTracker.Core.TimeTracking.Persistence;
using TimeTracker.Core.TimeTracking.Services;
using TimeTracker.Core.TimeTracking.Services.Activities;

namespace TimeTracker.Core.TimeTracking.Utils;

public static class TimeTrackingDependencyUtils
{
    public static void AddTimeTrackingModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(TimeTrackingContext));
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        services.AddScoped<ICrudService, CrudService>();
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<IQueryService, BasicQueryService>();
        services.AddDbContext<TimeTrackingContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ApplicationDatabase"));
        });
    }
}