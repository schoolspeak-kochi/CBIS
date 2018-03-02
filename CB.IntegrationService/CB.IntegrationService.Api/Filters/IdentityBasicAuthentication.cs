using CB.IntegrationService.Api.ActionResults;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace CB.IntegrationService.Api.Filters
{
    /// <summary>
    /// Defines the basic authentication filter
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <seealso cref="System.Web.Http.Filters.IAuthenticationFilter" />
    public abstract class IdentityBasicAuthentication : Attribute, IAuthenticationFilter
    {
        /// <summary>
        /// Gets a value indicating whether more than one instance of the indicated attribute can be specified for a single program element.
        /// </summary>
        public bool AllowMultiple { get { return false; } }

        /// <summary>
        /// Abstract method which authenticates and returns a principal.
        /// </summary>
        /// <param name="username">The username which will be the product identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected abstract Task<IPrincipal> AuthenticateAsync(string username, string password, CancellationToken cancellationToken);

        /// <summary>
        /// Authenticates the request.
        /// </summary>
        /// <param name="context">The authentication context.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A Task that will perform authentication.
        /// </returns>
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            // Look for credentials in the request.
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            // If there are no credentials, do nothing.
            if (authorization == null)
            {
                return;
            }

            // If there are credentials but the filter does not recognize the 
            // authentication scheme, do nothing.
            if (authorization.Scheme != "Basic")
            {
                return;
            }

            // If there are credentials that the filter understands, try to validate them.
            // If the credentials are bad, set the error result.
            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
                return;
            }

            // Extract the credentials
            Tuple<string, string> usernameAndPassword = ExtractCredentials(authorization.Parameter);
            if (usernameAndPassword == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);
            }

            string username = usernameAndPassword.Item1;
            string password = usernameAndPassword.Item2;

            IPrincipal principal = await AuthenticateAsync(username, password, cancellationToken);
            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid productId or productSecret", request);
            }

            // If the credentials are valid, set principal.
            else
            {
                context.Principal = principal;
            }

        }

        /// <summary>
        /// Add authentication challenges to the response, if needed.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);

            // Return the WWWAuthenticate header in response to unauthorized requests
            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic"));
            }

            context.Result = new ResponseMessageResult(result);
        }

        /// <summary>
        /// Extracts the credentials.
        /// </summary>
        /// <param name="authorizationParameter">The authorization parameter.</param>
        /// <returns></returns>
        private static Tuple<string, string> ExtractCredentials(string authorizationParameter)
        {
            byte[] credentialBytes;

            try
            {
                credentialBytes = Convert.FromBase64String(authorizationParameter);
            }
            catch (FormatException)
            {
                return null;
            }

            // The currently approved HTTP 1.1 specification says characters here are ISO-8859-1.
            Encoding encoding = Encoding.GetEncoding("ISO-8859-1");

            // Make a writable copy of the encoding to enable setting a decoder fallback.
            encoding = (Encoding)encoding.Clone();
            
            // Fail on invalid bytes rather than silently replacing and continuing.
            encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
            string decodedCredentials;

            try
            {
                decodedCredentials = encoding.GetString(credentialBytes);
            }
            catch (DecoderFallbackException)
            {
                return null;
            }

            if (String.IsNullOrEmpty(decodedCredentials))
            {
                return null;
            }

            int colonIndex = decodedCredentials.IndexOf(':');

            if (colonIndex == -1)
            {
                return null;
            }

            string userName = decodedCredentials.Substring(0, colonIndex);
            string password = decodedCredentials.Substring(colonIndex + 1);
            return new Tuple<string, string>(userName, password);
        }
    }
}