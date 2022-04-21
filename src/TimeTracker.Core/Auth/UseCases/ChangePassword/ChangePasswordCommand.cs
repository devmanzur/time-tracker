using System.Data;
using TimeTracker.Core.Auth.Models.Dto;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;

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

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty();
        RuleFor(x => x.NewPassword).NotEmpty();
    }
}