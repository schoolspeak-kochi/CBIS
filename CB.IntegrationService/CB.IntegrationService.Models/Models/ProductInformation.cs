using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.Models.Models
{
    public class ProductInformation
    {
        public long EbProductId;
        public string ProductSecret { get; set; }
        public string ProductName { get; set; }
        public string EndpointURL { get; set; }
    }
}
