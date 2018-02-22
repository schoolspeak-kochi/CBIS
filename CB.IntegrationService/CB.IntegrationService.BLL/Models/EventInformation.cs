using CB.IntegrationService.BLL.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.BLL.Models
{
    public class EventInformation
    {
        public long EbEventId;
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public StandardDataModels ModelType { get; set; } = StandardDataModels.Default;
        public List<string> Subscribers { get; set; }
    }
}
