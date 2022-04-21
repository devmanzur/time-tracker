using TimeTracker.Core.Auth.Models.Dto;
using TimeTracker.Core.Auth.Models.Entities;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace TimeTracker.Core.Auth.UseCases.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, Result<UserDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserProfileQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<UserDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            Maybe<ApplicationUser> user = await _userManager.FindByIdAsync(request.User.Id);
            if (user.HasValue)
            {
                return Result.Success(new UserDto(user.Value!.FullName, user.Value.Email, user.Value.EmailConfirmed,
                    user.Value.PhoneNumber, user.Value.PhoneNumberConfirmed));
            }

            return Result.Failure<UserDto>("Profile not found!");
        }
    }
}