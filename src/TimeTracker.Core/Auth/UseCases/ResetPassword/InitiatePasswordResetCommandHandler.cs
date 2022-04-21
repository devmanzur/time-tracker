using TimeTracker.Core.Auth.Models.Entities;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace TimeTracker.Core.Auth.UseCases.ResetPassword;

public class InitiatePasswordResetCommandHandler : IRequestHandler<InitiatePasswordResetCommand,Result<(string Token, string Name)>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public InitiatePasswordResetCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<Result<(string Token, string Name)>> Handle(InitiatePasswordResetCommand request, CancellationToken cancellationToken)
    {
        Maybe<ApplicationUser> user = await _userManager.FindByEmailAsync(request.Email);
        if (user.HasNoValue)
        {
            return Result.Failure<(string Token, string Name)>("User not found");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user.Value);
        return Result.Success((token,user.Value.FullName));
    }
}