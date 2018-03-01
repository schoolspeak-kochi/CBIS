using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace CB.IntegrationService.Api.Filters
{
    /// <summary>
    /// Defines the basic authentication filter attribute
    /// </summary>
    /// <seealso cref="CB.IntegrationService.Api.Filters.IdentityBasicAuthentication" />
    public class IdentityBasicAuthenticationAttribute : IdentityBasicAuthentication
    {
        /// <summary>
        /// Authenticates and returns a principal.
        /// </summary>
        /// <param name="username">The username which will be the product identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected override Task<IPrincipal> AuthenticateAsync(string username, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            // Verify the credentials
            Models.ProductInformation product = new DAL.ProductInformationDAL().GetProductInformationById(username);
            if (product == null || product.ProductSecret != password)
                return null;

            // Create a ClaimsIdentity with all the claims for the product.
            Claim nameClaim = new Claim(ClaimTypes.Name, username);
            List<Claim> claims = new List<Claim> { nameClaim };

            // Important to set the identity this way, otherwise IsAuthenticated will be false
            // See: http://leastprivilege.com/2012/09/24/claimsidentity-isauthenticated-and-authenticationtype-in-net-4-5/
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Basic");

            // Return the principal
            IPrincipal principal = new ClaimsPrincipal(identity);
            return Task.FromResult(principal);
        }
    }
}