namespace TimeTracker.Core.Auth.Utils
{
    public static class LocalIdentityConfig
    {
        /// <summary>
        /// The resource/ Apis that can be accessed with token issued from this server
        /// </summary>
        public static class Resources
        {
            public const string? ResourceApi = "resource-api";
        }

        /// <summary>
        /// The scopes that can be requested when generating token
        /// Resources may deny the token if the token does not have specific scopes
        /// </summary>
        public static class Scopes
        {
            public const string SpaClient = "spa-client";
        }
        
        /// <summary>
        /// Returns the resources allowed for the 
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static string?[]? GetResources(string scope)
        {
            return scope switch
            {
                Scopes.SpaClient => new []{ Resources.ResourceApi},
                _ => null,
            };
        }
    }
}