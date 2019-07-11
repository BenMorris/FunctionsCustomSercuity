﻿namespace TokenAuthInjection.AccessTokens
{
    using System;
    using System.Security.Claims;

    /// <summary>
    /// Contains the result of an access token check.
    /// </summary>
    public sealed class AccessTokenResult
    {
        private AccessTokenResult() { }

        /// <summary>
        /// Gets the security principal associated with a valid token.
        /// </summary>
        public ClaimsPrincipal Principal
        { get; private set; }

        /// <summary>
        /// Gets the status of the token, i.e. whether it is valid.
        /// </summary>
        public AccessTokenStatus Status
        { get; private set; }

        /// <summary>
        /// Gets any exception encountered when validating a token.
        /// </summary>
        public Exception Exception
        { get; private set; }

        /// <summary>
        /// Returns a valid token.
        /// </summary>
        public static AccessTokenResult Success(ClaimsPrincipal principal)
        {
            return new AccessTokenResult
            {
                Principal = principal,
                Status = AccessTokenStatus.Valid
            };
        }

        /// <summary>
        /// Returns a result that indicates the submitted token has expired.
        /// </summary>
        public static AccessTokenResult Expired()
        {
            return new AccessTokenResult
            {
                Status = AccessTokenStatus.Expired
            };
        }

        /// <summary>
        /// Returns a result to indicate that there was an error when processing the token.
        /// </summary>
        public static AccessTokenResult Error(Exception ex)
        {
            return new AccessTokenResult
            {
                Status = AccessTokenStatus.Error,
                Exception = ex
            };
        }

        /// <summary>
        /// Returns a result in response to no token being in the request.
        /// </summary>
        /// <returns></returns>
        public static AccessTokenResult NoToken()
        {
            return new AccessTokenResult
            {
                Status = AccessTokenStatus.NoToken
            };
        }
    }
}
