using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.DAL.Data
{
    public static class AwsCredentialsMaster
    {
        public static AwsCredentials SQSCredentials = new AwsCredentials
        {
            AccessKeyId = "AKIAJLB7PCKKCMYX6NWA",
            SecretAccessKey = "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",
            ResourceURL = "https://sqs.us-east-2.amazonaws.com/818709630326/EBIS_Delivery_Queue.fifo"
        };

        public static AwsCredentials SESCredentials = new AwsCredentials
        {
            Username = "SESUSER",
            AccessKeyId = "AKIAIJXFYMWXGFGV33GA",
            SecretAccessKey = "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"
        };

        public static AwsCredentials RDSSQLServerCredentials = new AwsCredentials
        {
            ResourceName = "ebispocinstance",
            ResourceURL = "https://ebispocinstance.cxqva5dydjnp.us-east-2.rds.amazonaws.com",
            Username = "ebispoc",
            Password = "ebispoc_kochi"
        };
    }
}
