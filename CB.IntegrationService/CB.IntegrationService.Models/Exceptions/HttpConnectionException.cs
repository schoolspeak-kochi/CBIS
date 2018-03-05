using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.Models.Exceptions
{

    [Serializable]
    public class HttpConnectionException : Exception
    {
        public int HttpStatusCode { get; }

        public HttpConnectionException() { }

        public HttpConnectionException(int httpStatusCode, string message) : base(message) { HttpStatusCode = httpStatusCode; }

        public HttpConnectionException(int httpStatusCode, string message, Exception inner) : base(message, inner) { HttpStatusCode = httpStatusCode;}

        protected HttpConnectionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
