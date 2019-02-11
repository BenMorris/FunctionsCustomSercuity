namespace FunctionsCustomSercuity.Binding
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.Azure.WebJobs.Host.Bindings;
    using Microsoft.Azure.WebJobs.Host.Protocols;

    /// <summary>
    /// Runs on every request and passes the function context (e.g. Http request and host configuration) to a value provider.
    /// </summary>
    public class AccessTokenBinding : IBinding
    {
        public Task<IValueProvider> BindAsync(BindingContext context)
        {
            // Get the HTTP request
            var request = context.BindingData["req"] as DefaultHttpRequest;

            // Get the configuration files for the OAuth token issuer
            var issuerToken = Environment.GetEnvironmentVariable("IssuerToken");
            var audience = Environment.GetEnvironmentVariable("Audience");
            var issuer = Environment.GetEnvironmentVariable("Issuer");

            return Task.FromResult<IValueProvider>(new AccessTokenValueProvider(request, issuerToken, audience, issuer));
        }

        public bool FromAttribute => true;

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context) => null;

        public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor();
    }
}
