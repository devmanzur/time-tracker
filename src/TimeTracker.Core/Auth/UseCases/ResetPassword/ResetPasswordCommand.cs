using CSharpFunctionalExtensions;
using MediatR;

namespace TimeTracker.Core.Auth.UseCases.ResetPassword;

public class ResetPasswordCommand : IRequest<Result>
{
    public ResetPasswordCommand(string email, string token, string newPassword)
    {
        Email = email;
        Token = token;
        NewPassword = newPassword;
    }

    public string Email { get; private set; }
    public string Token { get; private set; }
    public string NewPassword { get; private set; }

}