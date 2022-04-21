using TimeTracker.Core.Auth.Models.Entities;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace TimeTracker.Core.Auth.UseCases.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        Maybe<ApplicationUser> user = await _userManager.FindByIdAsync(request.AuthorizedUser.Id);
        if (user.HasNoValue)
        {
            return Result.Failure("User not found!");
        }
        var changePassword =
            await _userManager.ChangePasswordAsync(user.Value, request.CurrentPassword, request.NewPassword);
        return changePassword.Succeeded
            ? Result.Success()
            : Result.Failure(changePassword.Errors.FirstOrDefault()?.Description ?? "Failed to change password");
    }
}