using TimeTracker.Core.Auth.Models.Entities;
using TimeTracker.Core.Auth.UseCases.AuthenticateUser;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TimeTracker.Core.Shared.Utils;

public static class CrossCuttingDependencyExtensions
{
    public static void AddCrossCuttingModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(ApplicationUser));
        services.AddValidatorsFromAssemblyContaining<AuthenticateUserByPasswordCommandValidator>();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    }

}