using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.Models
{
    public class PublishEventResponseDTO
    {
        /// <summary>
        /// EBID of the event published
        /// </summary>
        /// <value>EBID of the event published</value>
        public string EbPublishedEventId { get; set; }
    }
}
