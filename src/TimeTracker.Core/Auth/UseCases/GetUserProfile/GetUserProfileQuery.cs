using TimeTracker.Core.Auth.Models.Dto;
using CSharpFunctionalExtensions;
using MediatR;

namespace TimeTracker.Core.Auth.UseCases.GetUserProfile
{
    public class GetUserProfileQuery : IRequest<Result<UserDto>>
    {
        public GetUserProfileQuery(AuthorizedUser user)
        {
            User = user;
        }

        public AuthorizedUser User { get; private set; }
    }
}