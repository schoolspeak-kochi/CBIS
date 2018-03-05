using Amazon.SQS;
using CB.IntegrationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CB.IntegrationService.DAL;
using CB.IntegrationService.Utils;
using CB.IntegrationService.DAL.Data;
using Amazon.SQS.Model;
using CB.IntegrationService.Models.Exceptions;

namespace CB.IntegrationService.BLL.Utils
{
    public class MessageBroker
    {
        #region Private fields
        /// <summary>
        /// The cached ARN Table (TODO: Replace this with RedisCache)
        /// </summary>
        private static Dictionary<string, string> ARN_Table;

        private static AmazonSQSConfig amazonSQSConfig;

        private static PublishedEventInformationDAL publishedEventInformationDAL;

        #endregion Private fields

        /// <summary>
        /// Initializes the <see cref="MessageBroker"/> class.
        /// </summary>
        static MessageBroker()
        {
            ARN_Table = new Dictionary<string, string>();
            publishedEventInformationDAL = new PublishedEventInformationDAL();
            amazonSQSConfig = new AmazonSQSConfig();
            amazonSQSConfig.RegionEndpoint = Amazon.RegionEndpoint.USEast2;
            amazonSQSConfig.ServiceURL = "https://sqs.us-east-2.amazonaws.com";
        }

        /// <summary>
        /// Publishes the event.
        /// </summary>
        /// <param name="productInformation">The product information.</param>
        /// <param name="eventInformation">The event information.</param>
        /// <param name="notificationRequest">The notification request.</param>
        /// <exception cref="System.ArgumentNullException">
        /// notificationRequest - Event data cannot null while publishing an event
        /// or
        /// Payload - Event Payload cannot be null while publishing an event
        /// </exception>
        /// <exception cref="System.ApplicationException"></exception>
        public static string PublishEvent(ProductInformation productInformation, CbisEvent eventInformation, ProductNotificationRequestDTO notificationRequest)
        {
            Logger.LogTrace("MessageBroker.cs Method: PublishEvent() publish an event");
            if (notificationRequest == null)
            {
                throw new ArgumentNullException(nameof(notificationRequest), "Event data cannot null while publishing an event");
            }

            if (notificationRequest.Payload == null)
            {
                throw new ArgumentNullException(nameof(notificationRequest.Payload), "Event Payload cannot be null while publishing an event");
            }

            try
            {

                // Generate the unique id and token
                QueuedEventInformation eventQueueInformation = new QueuedEventInformation();
                eventQueueInformation.EventToken = publishedEventInformationDAL
                                                    .CreatePublishedEventInformation(eventInformation.EbEventId.ToString(),
                                                    productInformation.EbProductId.ToString(),
                                                    JsonHelper.Serialize(notificationRequest),
                                                    new EventDeliveryInformation(eventInformation.Subscribers));

                // Insert into the delivery queue
                foreach (string productId in eventInformation.Subscribers)
                {
                    AmazonSQSClient amazonSQSClient = new AmazonSQSClient(AwsCredentialsMaster.SQSCredentials.AccessKeyId, AwsCredentialsMaster.SQSCredentials.SecretAccessKey, amazonSQSConfig);
                    SendMessageRequest sendMessageRequest = new SendMessageRequest(AwsCredentialsMaster.SQSCredentials.ResourceURL, JsonHelper.Serialize(eventQueueInformation));
                    sendMessageRequest.MessageGroupId = productId;
                    sendMessageRequest.MessageDeduplicationId = Guid.NewGuid().ToString();
                    SendMessageResponse sendMessageResponse = amazonSQSClient.SendMessage(sendMessageRequest);
                    if (sendMessageResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new HttpConnectionException((int)sendMessageResponse.HttpStatusCode, $"Publish event failed for EbProductId: {productId} and Event: {eventInformation.EventName}");
                    }
                }
                Logger.LogTrace("MessageBroker.cs Method: PublishEvent() End");
                return eventQueueInformation.EventToken.ToString();
            }
            catch (Exception ex)
            {
                Logger.LogTrace("MessageBroker.cs Method: PublishEvent() End");
                throw ex;
            }
        }
    }
}
