using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.Models
{
    public class ProductNotificationRequestDTO
    {
        /// <summary>
        /// The name of the product that published the notification
        /// </summary>
        /// <value>The name of the product that published the notification</value>
        public string PublishingProductName { get; set; }

        /// <summary>
        /// The name of the institution
        /// </summary>
        /// <value>The name of the institution</value>
        public string InstitutionName { get; set; }

        /// <summary>
        /// The Education Brands institution id
        /// </summary>
        /// <value>The Education Brands institution id</value>
        public string EbInstitutionId { get; set; }

        /// <summary>
        /// The name of the event against which the notification was published
        /// </summary>
        /// <value>The name of the event against which the notification was published</value>
        public string EventName { get; set; }

        /// <summary>
        /// The unique token for the event. Use this token to acknowledge the event.
        /// </summary>
        /// <value>The unique token for the event. Use this token to acknowledge the event.</value>
        public string EventToken { get; set; }

        /// <summary>
        /// The name of the standard data type of the Payload
        /// </summary>
        /// <value>The name of the standard data type of the Payload</value>
        public string MessageType { get; set; }

        /// <summary>
        /// A value indicating whether the notifier has requested for acknowledgement or not
        /// </summary>
        /// <value>A value indicating whether the notifier has requested for acknowledgement or not</value>
        public bool? AcknowledgementRequired { get; set; }

        /// <summary>
        /// The serialized string of the actual data (standard data type) - MUST be same as the standard data type specified in the messageType field
        /// </summary>
        /// <value>The serialized string of the actual data (standard data type) - MUST be same as the standard data type specified in the messageType field</value>
        public string Payload { get; set; }
    }
}
