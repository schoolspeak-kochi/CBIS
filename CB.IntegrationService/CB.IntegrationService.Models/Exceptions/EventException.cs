using System;

namespace CB.IntegrationService.Models.Exceptions
{
    [Serializable]
    public class EventException : Exception
    {
        public string EventName { get; }

        public EventException() { }

        public EventException(string eventName, string message) : base(message) { EventName = eventName; }

        public EventException(string eventName, string message, Exception inner) : base(message, inner) { EventName = eventName;}

        protected EventException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
