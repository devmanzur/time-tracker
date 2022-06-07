using static TimeTracker.Core.Shared.Utils.ValidationUtils;

namespace TimeTracker.Core.Shared.Models.Dto
{
    public class Envelope<T>
    {
        // ReSharper disable once MemberCanBeProtected.Global
        protected internal Envelope(T body, string? errorMessage)
        {
            Body = body;
            Errors = GetProblemDetailFormattedErrors(errorMessage);
            TimeGenerated = DateTime.UtcNow;
            IsSuccess = errorMessage == null;
        }

        public T Body { get; }
        public Dictionary<string, List<string>>? Errors { get; }
        public DateTime TimeGenerated { get; }
        public bool IsSuccess { get; }
    }

    public class Envelope : Envelope<string>
    {
        private Envelope(string? errorMessage)
            : base((errorMessage == null ? null : "") ?? string.Empty, errorMessage)
        {
        }

        public static Envelope<T> Ok<T>(T result)
        {
            return new Envelope<T>(result, null);
        }

        public static Envelope Ok()
        {
            return new Envelope(null);
        }

        public static Envelope Error(string errorMessage)
        {
            return new Envelope(errorMessage);
        }
    }
}