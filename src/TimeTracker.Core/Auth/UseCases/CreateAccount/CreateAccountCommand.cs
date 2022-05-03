using TimeTracker.Core.Auth.Models.Dto;
using TimeTracker.Core.Shared.Utils;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;

namespace TimeTracker.Core.Auth.UseCases.CreateAccount;

public class CreateAccountCommand: IRequest<Result<UserDto>>
{
    public string Email { get;  set; }
    public string Password { get;  set; }
    public string FirstName { get;  set; }
    public string LastName { get;  set; }
}

public class CreateAccountCommandValidator : BaseFluentValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Email).Must(ValidationUtils.IsValidEmailAddress).WithMessage("Invalid email address");
        RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Invalid password");
        RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("Invalid first name");
        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Invalid last name");
    }
}