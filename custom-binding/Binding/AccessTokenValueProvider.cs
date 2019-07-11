namespace TokenAuthCustomBinding.Binding
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Azure.WebJobs.Host.Bindings;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Creates a <see cref="ClaimsPrincipal"/> instance for the supplied header and configuration values.
    /// </summary>
    /// <remarks>
    /// This is where the actual authentication happens - replace this code to implement a different authentication solution.
    /// </remarks>
    public class AccessTokenValueProvider : IValueProvider
    {
        private const string AUTH_HEADER_NAME = "Authorization";
        private const string BEARER_PREFIX = "Bearer ";
        private HttpRequest _request;
        private readonly string _issuerToken;
        private readonly string _audience;
        private readonly string _issuer;

        public AccessTokenValueProvider(HttpRequest request, string issuerToken, string audience, string issuer)
        {
            _request = request;
            _issuerToken = issuerToken;
            _audience = audience;
            _issuer = issuer;
        }

        public Task<object> GetValueAsync()
        {
            try
            {
                // Get the token from the header
                if (_request.Headers.ContainsKey(AUTH_HEADER_NAME) &&
                   _request.Headers[AUTH_HEADER_NAME].ToString().StartsWith(BEARER_PREFIX))
                {
                    var token = _request.Headers["Authorization"].ToString().Substring(BEARER_PREFIX.Length);

                    // Create the parameters
                    var tokenParams = new TokenValidationParameters()
                    {
                        RequireSignedTokens = true,
                        ValidAudience = _audience,
                        ValidateAudience = true,
                        ValidIssuer = _issuer,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_issuerToken))
                    };

                    // Validate the token
                    var handler = new JwtSecurityTokenHandler();
                    var result = handler.ValidateToken(token, tokenParams, out var securityToken);
                    return Task.FromResult<object>(AccessTokenResult.Success(result));
                }
                else
                {
                    return Task.FromResult<object>(AccessTokenResult.NoToken());
                }
            }
            catch (SecurityTokenExpiredException)
            {
                return Task.FromResult<object>(AccessTokenResult.Expired());
            }
            catch (Exception ex)
            {
                return Task.FromResult<object>(AccessTokenResult.Error(ex));
            }
        }

        public Type Type => typeof(ClaimsPrincipal);

        public string ToInvokeString() => string.Empty;
    }
}
