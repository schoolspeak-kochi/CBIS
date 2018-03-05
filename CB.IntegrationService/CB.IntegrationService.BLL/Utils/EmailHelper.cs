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
    /// <summary>
    /// Send email using Amazon AWS SES service
    /// </summary>
    public class EmailHelper : IDisposable
    {
        private AmazonSimpleEmailServiceClient Client = null;

        /// <summary>
        /// Default sender email id
        /// Note: Applied sender should be verified before use, otherwise the client throw a verification exception .
        /// </summary>
        private string DefaultSender = "akhil3203@gmail.com";

        /// <summary>
        /// Initialise the simple email service client to send mail.
        /// </summary>
        public EmailHelper()
        {
            Client = new AmazonSimpleEmailServiceClient(AwsCredentialsMaster.SESCredentials.AccessKeyId, AwsCredentialsMaster.SESCredentials.SecretAccessKey, RegionEndpoint.USWest2);
        }

        /// <summary>
        /// Send email using the amazon aws simple email service
        /// Note: The SES will throw an exception if the sender is not verified, make sure the sender applied are verified before is used.
        /// </summary>
        /// <param name="email">Email payload information</param>
        public void SendMail(EmailPayload email)
        {
            Logger.LogTrace("EmailHelper.cs Method: SendEmail()");
            // Validate the event payload information before attempt for delivery.
            if (email == null)
            {
                throw new ApplicationException("Email payload cannot be empty, please try again.");
            }

            // If the sender not specified, send email from the default sender
            if (String.IsNullOrWhiteSpace(email.Sender))
            {
                email.Sender = DefaultSender;
            }

            // Create send email client request.
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
                // Send the request through AWS SES  client.
                var response = Client.SendEmail(sendRequest);
            }
            catch (Exception)
            {
                Logger.LogTrace("EmailHelper.cs Method: SendEmail() End");
                throw new ApplicationException("Email send failed");
            }

        }

        /// <summary>
        /// Dispose the AWS client connection
        /// </summary>
        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
