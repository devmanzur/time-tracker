using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;

namespace TimeTracker.Core.Auth.Rules
{
    public class MustProvideValidEmailRule : IBusinessRule
    {
        private readonly string _email;

        public MustProvideValidEmailRule(string email)
        {
            this._email = email;
        }

        public string Message => "Must provide a valid email address";

        public bool IsBroken()
        {
            if (string.IsNullOrWhiteSpace(_email) || !ValidationUtils.IsValidEmailAddress(_email))
            {
                //rule is broken
                return true;
            }

            return false;
        }
    }
}