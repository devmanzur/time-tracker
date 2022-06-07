using TimeTracker.Core.Auth.Models.Entities;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TimeTracker.Core.Shared.Utils;

namespace TimeTracker.Core.Auth.UseCases.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidator<ChangePasswordCommand> _validator;

    public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager,
        IValidator<ChangePasswordCommand> validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            return Result.Failure(validation.Errors.GetSerializedErrors());
        }
        
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