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
        RuleFor(x => x.Email).Must(ValidationUtils.IsValidEmailAddress).WithMessage("Email  is invalid");
        RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password  is invalid");
        RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is invalid");
        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is invalid");
    }
}