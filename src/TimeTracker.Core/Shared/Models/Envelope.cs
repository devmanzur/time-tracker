namespace TimeTracker.Core.Shared.Models
{
    public class Envelope<T>
    {
        // ReSharper disable once MemberCanBeProtected.Global
        protected internal Envelope(T body, string? errorMessage, Dictionary<string, string>? errorDetails)
        {
            Body = body;
            ErrorMessage = errorMessage;
            ErrorDetails = errorDetails;
            TimeGenerated = DateTime.UtcNow;
            IsSuccess = errorMessage == null;
        }

        public T Body { get; }
        public string? ErrorMessage { get; }
        public DateTime TimeGenerated { get; }
        public bool IsSuccess { get; }
        public Dictionary<string, string>? ErrorDetails { get; }
    }

    public class Envelope : Envelope<string>
    {
        private Envelope(string? errorMessage, Dictionary<string, string>? details)
            : base((errorMessage == null ? null : "") ?? string.Empty, errorMessage, details)
        {
        }

        public static Envelope<T> Ok<T>(T result)
        {
            return new Envelope<T>(result, null, null);
        }

        public static Envelope Ok()
        {
            return new Envelope(null, null);
        }

        public static Envelope Error(string errorMessage, Dictionary<string, string>? details = null)
        {
            return new Envelope(errorMessage, details);
        }
    }
}