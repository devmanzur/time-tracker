namespace TimeTracker.Core.Shared.Utils
{
    public static class StringExtensions
    {
        public static bool IsNullEmptyOrWhiteSpace(this string? text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return text.All(char.IsWhiteSpace);
            }

            return true;
        }
    }
}