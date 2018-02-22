using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using CB.IntegrationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CB.IntegrationService.DAL.Data;
using Amazon;

namespace CB.IntegrationService.Utils
{
    public class EmailHelper : IDisposable
    {
        private AmazonSimpleEmailServiceClient Client = null;
        private string DefaultSender = "akhil3203@gmail.com";

        public EmailHelper()
        {
            Client = new AmazonSimpleEmailServiceClient(AwsCredentialsMaster.SESCredentials.AccessKeyId, AwsCredentialsMaster.SESCredentials.SecretAccessKey, RegionEndpoint.USWest2);
        }
        public void SendMail(EmailPayload email)
        {
            if (email == null)
            {
                throw new ApplicationException("Email payload cannot be empty, please try again.");
            }

            // If the sender not specified, take the default the sender
            if (String.IsNullOrWhiteSpace(email.Sender))
            {
                email.Sender = DefaultSender;
            }

            var sendRequest = new SendEmailRequest
            {
                Source = email.Sender,
                Destination = new Destination
                {
                    ToAddresses = email.Recipients
                },
                Message = new Message
                {
                    Subject = new Content(email.Subject),
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Charset = "UTF-8",
                            Data = email.Body
                        },
                        Text = new Content
                        {
                            Charset = "UTF-8",
                            Data = email.Text
                        }
                    }
                },
                // If you are not using a configuration set, comment
                // or remove the following line 
                // ConfigurationSetName = configSet
            };
            try
            {
                var response = Client.SendEmail(sendRequest);
            }
            catch (Exception)
            {

            }

        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
