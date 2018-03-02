using System;

namespace CB.IntegrationService.Models.Exceptions
{
    [Serializable]
    public class ProductException : Exception
    {
        public string ProductId { get; }

        public ProductException() { }

        public ProductException(string productId, string message) : base(message) { ProductId = productId; }

        public ProductException(string productId, string message, Exception inner) : base(message, inner) { ProductId = productId; }

        protected ProductException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
