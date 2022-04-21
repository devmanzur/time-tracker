using TimeTracker.Core.Auth.Models.Entities;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace TimeTracker.Core.Auth.UseCases.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        Maybe<ApplicationUser> user = await _userManager.FindByEmailAsync(request.Email);
        if (user.HasNoValue)
        {
            return Result.Failure<(string Token, string Name)>("User not found");
        }

        var changePassword = await _userManager.ResetPasswordAsync(user.Value, request.Token, request.NewPassword);
        return changePassword.Succeeded
            ? Result.Success()
            : Result.Failure(changePassword.Errors.FirstOrDefault()?.Description);
    }
}