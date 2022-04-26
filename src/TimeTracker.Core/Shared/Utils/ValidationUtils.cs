using System.Net.Mail;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Core.Shared.Utils
{
    public static class ValidationUtils
    {
        public static bool IsValidEmailAddress(string? email)
        {
            if (email.IsNullEmptyOrWhiteSpace())
            {
                return false;
            }

            MailAddress? mail;
            return MailAddress.TryCreate(email, out mail);
        }

        public static bool IsValidEntityId(int id)
        {
            return id >= 1;
        }
    }
}