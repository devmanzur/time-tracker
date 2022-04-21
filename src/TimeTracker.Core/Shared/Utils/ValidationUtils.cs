using System.Net.Mail;

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
    }
}