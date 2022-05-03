using System.Security.Claims;
using TimeTracker.Core.Auth.Models.Entities;
using TimeTracker.Core.Shared.Utils;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;

namespace TimeTracker.Core.Auth.UseCases.AuthenticateUser
{
    public class AuthenticateUserByPasswordCommand : IRequest<Result<(ApplicationUser User, ClaimsPrincipal Principal)>>
    {
        public AuthenticateUserByPasswordCommand(string? email, string? password)
        {
            Email = email;
            Password = password;
        }

        public string? Email { get; private set; }
        public string? Password { get; private set; }
    }

    public class AuthenticateUserByPasswordCommandValidator : BaseFluentValidator<AuthenticateUserByPasswordCommand>
    {
        public AuthenticateUserByPasswordCommandValidator()
        {
            RuleFor(x => x.Email).Must(ValidationUtils.IsValidEmailAddress).WithMessage("Invalid email address");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Invalid password");
        }
    }
}