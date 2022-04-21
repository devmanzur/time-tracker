using TimeTracker.Core.Auth.Rules;
using Microsoft.AspNetCore.Identity;

namespace TimeTracker.Core.Auth.Models.Entities
{
    public class ApplicationUser : IdentityAggregateRoot
    {
        protected ApplicationUser()
        {
        }

        public ApplicationUser(UserManager<ApplicationUser> userManager, string email, string firstName,
            string lastName)
        {
            CheckRules(
                new MustProvideValidEmailRule(email),
                new EmailMustBeUniqueRule(userManager, email),
                new MustProvideNameRule(firstName, lastName)
            );
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.UserName = CreateUsername(email);
        }

        private string CreateUsername(string email)
        {
            return "@" + email.Split("@")[0] + Guid.NewGuid();
        }

        public void AssignRole(ApplicationRole role)
        {
            this._roles.Add(new ApplicationUserRole()
            {
                RoleId = role.Id,
                Role = role,
                UserId = this.Id,
                User = this
            });
        }

        private readonly List<ApplicationUserRole> _roles = new List<ApplicationUserRole>();
        public IReadOnlyList<ApplicationUserRole> Roles => _roles.AsReadOnly();

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => $"{FirstName} {LastName}";
        public override DateTime CreatedTime { get; set; }
        public override string CreatedBy { get; set; }
        public override DateTime LastModifiedTime { get; set; }
        public override string LastModifiedBy { get; set; }
    }
}