namespace FunctionsCustomSercuity.Binding
{
    using System;
    using Microsoft.Azure.WebJobs;

    /// <summary>
    /// Called from Startup to load the custom binding when the Azure Functions host starts up.
    /// </summary>
    public static class AccessTokenExtensions
    {
        public static IWebJobsBuilder AddAccessTokenBinding(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddExtension<AccessTokenExtensionProvider>();
            return builder;
        }
    }
}
