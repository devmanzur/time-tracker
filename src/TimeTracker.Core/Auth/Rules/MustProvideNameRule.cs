using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;

namespace TimeTracker.Core.Auth.Rules
{
    public class MustProvideNameRule : IBusinessRule
    {
        private readonly string _firstName;
        private readonly string _lastName;

        public MustProvideNameRule(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public string Message => "User name must be provided";
        public bool IsBroken()
        {
            return _firstName.IsNullEmptyOrWhiteSpace() || _lastName.IsNullEmptyOrWhiteSpace();
        }
    }
}