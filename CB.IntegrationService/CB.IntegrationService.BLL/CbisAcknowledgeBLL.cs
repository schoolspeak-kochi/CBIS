using CB.IntegrationService.DAL;
using CB.IntegrationService.Models;
using CB.IntegrationService.Models.DTO;
using CB.IntegrationService.Models.Exceptions;
using CB.IntegrationService.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CB.IntegrationService.BLL
{
    public class CbisAcknowledgeBLL
    {
        public HttpResponseMessage Acknowledge(NotificationAcknowledgeRequestDTO notificationAcknowledgeRequestDTO)
        {
            Logger.LogTrace("Method: Acknowledge() Create the acknowledge notification request");
            if (notificationAcknowledgeRequestDTO == null)
            {
                throw new ArgumentNullException(nameof(notificationAcknowledgeRequestDTO));
            }

            if (String.IsNullOrWhiteSpace(notificationAcknowledgeRequestDTO.EventToken))
            {
                throw new MissingFieldException(nameof(NotificationAcknowledgeRequestDTO), nameof(notificationAcknowledgeRequestDTO.EventToken));
            }

            PublishedEventInformation publishedEventInfo = new PublishedEventInformationDAL().GetPublishedEventInformation(notificationAcknowledgeRequestDTO.EventToken);
            if (publishedEventInfo == null)
            {
                throw new KeyNotFoundException($"Failed to get published event information from DB using the event token {notificationAcknowledgeRequestDTO.EventToken}");
            }

            ProductNotificationRequestDTO productNotificationRequestDTO = null;
            try
            {
                productNotificationRequestDTO = JsonHelper.DeSerialize<ProductNotificationRequestDTO>(publishedEventInfo.Payload.ToString());
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to parse event payload information while acknowledge." + ex.Message);
            }

            if (productNotificationRequestDTO == null)
            {
                throw new NullReferenceException("Failed to load event payload.");
            }

            if (!productNotificationRequestDTO.AcknowledgementRequired.Value)
            {
                throw new Exception("Acknowledgement is not opted for the event.");
            }

            ProductInformation productInformation = new ProductInformationDAL().GetProductInformationById(publishedEventInfo.EbProductId.ToString());
            if (productInformation == null)
            {
                throw new ProductException(publishedEventInfo.EbProductId.ToString(), $"Failed to load information of product {publishedEventInfo.EbProductId.ToString()} from DB");
            }
            Logger.LogTrace("Method: Acknowledge() End");
            // Send acknowledgement to the client.
            return SendAcknowledgement(productInformation.EndpointURL, notificationAcknowledgeRequestDTO).GetAwaiter().GetResult();

        }

        /// <summary>
        /// Send event acknowledgement to the orginator of the message
        /// </summary>
        /// <param name="url"></param>
        /// <param name="notificationAcknowledgeRequestDTO"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAcknowledgement(string url, NotificationAcknowledgeRequestDTO notificationAcknowledgeRequestDTO)
        {
            Logger.LogTrace("Method: SendAcknowledgement() Send Acknowledgement");
            HttpResponseMessage response = null;
            try
            {
                response = await new HttpClient().PostAsJsonAsync<NotificationAcknowledgeRequestDTO>(url + @"/acknowledgeNotification", notificationAcknowledgeRequestDTO);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Logger.LogTrace("Method: SendAcknowledgement() End");
                if (response == null)
                {
                    throw new ApplicationException("Failed to acknowledge. Product base url is" + url, ex);
                }
                else
                {
                    throw new HttpConnectionException((int)response.StatusCode, "couldn't connect to the product acknowledgement end point" ,ex.InnerException);
                }
            }
            Logger.LogTrace("Method: SendAcknowledgement() End");
            return response;
        }
    }
}
