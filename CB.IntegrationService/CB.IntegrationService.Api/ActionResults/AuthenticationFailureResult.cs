using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CB.IntegrationService.Api.ActionResults
{
    /// <summary>
    /// Defines an action result for authentication failure
    /// </summary>
    /// <seealso cref="System.Web.Http.IHttpActionResult" />
    public class AuthenticationFailureResult : IHttpActionResult
    {
        /// <summary>
        /// Gets the reason phrase.
        /// </summary>
        /// <value>
        /// The reason phrase.
        /// </value>
        public string ReasonPhrase { get; private set; }

        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        public HttpRequestMessage Request { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationFailureResult"/> class.
        /// </summary>
        /// <param name="reasonPhrase">The reason phrase.</param>
        /// <param name="request">The request.</param>
        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        /// <summary>
        /// Creates an <see cref="T:System.Net.Http.HttpResponseMessage" /> asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that, when completed, contains the <see cref="T:System.Net.Http.HttpResponseMessage" />.
        /// </returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = Request,
                ReasonPhrase = ReasonPhrase
            };

            return response;
        }
    }
}