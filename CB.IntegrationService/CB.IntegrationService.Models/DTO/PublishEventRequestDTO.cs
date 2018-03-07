using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.Models.DTO
{
    public class PublishEventRequestDTO
    {
        /// <summary>
        /// EBID of the product
        /// </summary>
        /// <value>EBID of the product</value>
        public string EbProductID { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        /// <value>Name of the product</value>
        public string ProductName { get; set; }

        /// <summary>
        /// EB Institution Id
        /// </summary>
        /// <value>EB Institution Id</value>
        public string EbInstitutionId { get; set; }

        /// <summary>
        /// Institution Name
        /// </summary>
        /// <value>Institution Name</value>
        public string InstitutionName { get; set; }

        /// <summary>
        /// Name of the published event
        /// </summary>
        /// <value>Name of the published event</value>
        public string EventName { get; set; }

        /// <summary>
        /// Is acknowledgement required
        /// </summary>
        /// <value>Is acknowledgement required</value>
        public bool? AcknowledgementRequired { get; set; }

        /// <summary>
        /// Message to be publish
        /// </summary>
        /// <value>Message to be publish</value>
        public string Payload { get; set; }
    }
}
