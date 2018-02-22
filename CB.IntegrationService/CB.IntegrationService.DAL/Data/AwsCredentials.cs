using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.DAL.Data
{
    public class AwsCredentials
    {
        public string SecretAccessKey { get; set; }
        public string AccessKeyId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceURL { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
