using CSharpFunctionalExtensions;
using MediatR;

namespace TimeTracker.Core.Auth.UseCases.ResetPassword;

public class InitiatePasswordResetCommand : IRequest<Result<(string Token, string Name)>>
{
    public InitiatePasswordResetCommand(string email)
    {
        Email = email;
    }

    public string Email { get; private set; }
}