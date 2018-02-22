using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.Models
{
    public class EventDeliveryInformation
    {
        public List<DeliveryInformation> DeliveryInformation;

        public EventDeliveryInformation()
        {

        }

        public EventDeliveryInformation(List<string> lstProductInfo)
        {
            DeliveryInformation = new List<Models.DeliveryInformation>();
            lstProductInfo?.ForEach(p => DeliveryInformation.Add(new Models.DeliveryInformation { EbProductId = p }));
        }
    }

    public class DeliveryInformation
    {
        public string EbProductId { get; set; }
        public bool DeliveryStatus { get; set; }
    }
}
