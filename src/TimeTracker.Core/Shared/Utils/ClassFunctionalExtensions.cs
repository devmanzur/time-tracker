using CSharpFunctionalExtensions;

namespace TimeTracker.Core.Shared.Utils
{
    public static class ClassFunctionalExtensions
    {
        public static bool HasNoValue(this object? obj)
        {
            if (obj != null)
            {
                Maybe<object> maybeObj = obj;
                return maybeObj.HasNoValue;
            }

            return true;
        }
        
        public static bool HasValue(this object? obj)
        {
            if (obj != null)
            {
                Maybe<object> maybeObj = obj;
                return maybeObj.HasValue;
            }

            return false;
        }
    }
}