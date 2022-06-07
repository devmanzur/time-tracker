using System.Security.Claims;
using TimeTracker.Core.Auth.Models.Entities;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TimeTracker.Core.Shared.Utils;

namespace TimeTracker.Core.Auth.UseCases.AuthenticateUser
{
    public class AuthenticateUserByPasswordCommandHandler : IRequestHandler<AuthenticateUserByPasswordCommand,
        Result<(ApplicationUser, ClaimsPrincipal)>>
    {
        private readonly SignInManager<ApplicationUser> _authenticationManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<AuthenticateUserByPasswordCommand> _validator;

        public AuthenticateUserByPasswordCommandHandler(SignInManager<ApplicationUser> authenticationManager,
            UserManager<ApplicationUser> userManager, IValidator<AuthenticateUserByPasswordCommand> validator)
        {
            _authenticationManager = authenticationManager;
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<Result<(ApplicationUser, ClaimsPrincipal)>> Handle(
            AuthenticateUserByPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                return Result.Failure<(ApplicationUser, ClaimsPrincipal)>(validation.Errors.GetSerializedErrors());
            }
            
            Maybe<ApplicationUser> user = await _userManager.FindByEmailAsync(request.Email);
            if (user.HasNoValue)
            {
                return Result.Failure<(ApplicationUser, ClaimsPrincipal)>("User not found!");
            }

            var signIn = await _authenticationManager.PasswordSignInAsync(user.Value!, request.Password, true, true);

            if (signIn.Succeeded)
            {
                var principal = await _authenticationManager.CreateUserPrincipalAsync(user.Value);

                return Result.Success((user.Value, principal));
            }

            if (signIn.IsLockedOut)
            {
                return Result.Failure<(ApplicationUser, ClaimsPrincipal)>("Account is locked!");
            }

            return Result.Failure<(ApplicationUser, ClaimsPrincipal)>(signIn.IsNotAllowed
                ? "Account is suspended!"
                : "Invalid username/ password combination");
        }
    }
}