using TimeTracker.Core.Auth.Models.Dto;
using TimeTracker.Core.Auth.Models.Entities;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace TimeTracker.Core.Auth.UseCases.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Result<UserDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidator<CreateAccountCommand> _validator;

    public CreateAccountCommandHandler(UserManager<ApplicationUser> userManager,IValidator<CreateAccountCommand> validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<Result<UserDto>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            return Result.Failure<UserDto>(validation.Errors.FirstOrDefault()?.ErrorMessage);
        }
        
        Maybe<ApplicationUser> existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser.HasValue)
        {
            return Result.Failure<UserDto>("User with same email address already exists");
        }

        var user = new ApplicationUser(_userManager, request.Email, request.FirstName, request.LastName);
        var registerUser = await _userManager.CreateAsync(user, request.Password);
        if (registerUser.Succeeded)
        {
            return Result.Success(new UserDto(user.FullName, user.Email, user.EmailConfirmed,
                user.PhoneNumber, user.PhoneNumberConfirmed));
        }

        return Result.Failure<UserDto>(registerUser.Errors.FirstOrDefault()?.Description ?? "Failed to register user");
    }
}