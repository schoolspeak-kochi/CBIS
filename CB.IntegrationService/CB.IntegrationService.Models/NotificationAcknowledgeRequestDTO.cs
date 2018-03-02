using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.Models
{
    public class NotificationAcknowledgeRequestDTO
    {
        /// <summary>
        /// Acknowledgment time stamp in UTC
        /// </summary>
        /// <value>Acknowledgment time stamp in UTC</value>
        public string AcknowledgeTimestamp { get; set; }

        /// <summary>
        /// Request processed time stamp in UTC
        /// </summary>
        /// <value>Request processed time stamp in UTC</value>
        public string RequestTimestamp { get; set; }

        /// <summary>
        /// Event token
        /// </summary>
        /// <value>Event token</value>
        public string EventToken { get; set; }

        /// <summary>
        /// Event token
        /// </summary>
        /// <value>Event token</value>
        public string StatusCode { get; set; }

        /// <summary>
        /// successfully processed
        /// </summary>
        /// <value>successfully processed</value>
        public string StatusMessage { get; set; }

    }
}
