using System.Data;
using TimeTracker.Core.Auth.Models.Dto;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using TimeTracker.Core.Shared.Utils;

namespace TimeTracker.Core.Auth.UseCases.ChangePassword;

public class ChangePasswordCommand : IRequest<Result>
{
    public AuthorizedUser AuthorizedUser { get; }
    public string CurrentPassword { get; }
    public string NewPassword { get; }

    public ChangePasswordCommand(AuthorizedUser authorizedUser, string currentPassword, string newPassword)
    {
        AuthorizedUser = authorizedUser;
        CurrentPassword = currentPassword;
        NewPassword = newPassword;
    }
}

public class ChangePasswordCommandValidator : BaseFluentValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("CurrentPassword is invalid");
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("NewPassword is invalid");
    }
}