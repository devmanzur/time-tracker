using System.Net.Mail;
using FluentValidation.Results;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Core.Shared.Utils
{
    public static class ValidationUtils
    {
        public const string ValidationErrorSeparator = ";";

        public static bool IsValidEmailAddress(string? email)
        {
            return !email.IsNullEmptyOrWhiteSpace() && MailAddress.TryCreate(email!, out _);
        }

        public static bool IsValidEntityId(int id)
        {
            return id >= 1;
        }


        public static string GetSerializedErrors(this List<ValidationFailure> errors)
        {
            if (!errors.Any())
            {
                return "Validation failed";
            }

            var errorMessages = errors.Select(x => $"{x.PropertyName}:{x.ErrorMessage}").ToList();
            return string.Join(ValidationErrorSeparator, errorMessages);
        }

        public static Dictionary<string, List<string>>? GetProblemDetailFormattedErrors(string? errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                return null;
            }

            var detailedError = new Dictionary<string, List<string>>();
            var errors = errorMessage.Split(ValidationErrorSeparator);

            foreach (var error in errors)
            {
                var split = error.Split(":");
                var key = split[0];
                var errorDetail = split[1];
                if (detailedError.ContainsKey(key))
                {
                    var errorList = detailedError[key];
                    errorList.Add(errorDetail);
                    detailedError[key] = errorList;
                }
                else
                {
                    detailedError[key] = new List<string>()
                    {
                        errorDetail
                    };
                }
            }

            return detailedError;
        }
    }
}