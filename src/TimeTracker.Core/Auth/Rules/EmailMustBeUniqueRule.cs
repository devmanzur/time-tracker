using TimeTracker.Core.Auth.Models.Entities;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace TimeTracker.Core.Auth.Rules
{
    public class EmailMustBeUniqueRule : IBusinessRule
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string _email;

        public EmailMustBeUniqueRule(UserManager<ApplicationUser> userManager,string email)
        {
            _userManager = userManager;
            this._email = email;
        }

        public string Message => "Must provide a valid email address";

        public bool IsBroken()
        {
            if (_email.IsNullEmptyOrWhiteSpace())
            {
                //rule is broken
                return true;
            }

            Maybe<ApplicationUser> existingUser = _userManager.FindByEmailAsync(_email).Result;
            return existingUser.HasValue;
        }
    }
}