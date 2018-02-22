using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.Models
{
    public class PublishedEventInformation
    {
        public long TokenId;
        public long EbEventId;
        public long EbProductId;
        public string Payload;
        public EventDeliveryInformation EventDeliveryInformation;
    }
}
