
using CB.IntegrationService.BLL.Utils;
using CB.IntegrationService.DAL;
using CB.IntegrationService.Models;
using CB.IntegrationService.Models.DTO;
using CB.IntegrationService.Models.Exceptions;
using CB.IntegrationService.Utils;
using System;

namespace CB.IntegrationService.BLL
{
    public class CbisEventBLL
    {
        public CbisEvent eventInfo { get; set; }

        /// <summary>
        /// Processing the publish request
        /// </summary>
        /// <param name="publishEventRequestDTO"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public PublishEventResponseDTO Publish(string productId, PublishEventRequestDTO publishEventRequestDTO)
        {
            Logger.LogTrace($"Publish event: product Id{productId} PublishEvent Name: {publishEventRequestDTO.EventName}");
            if (publishEventRequestDTO == null)
            {
                throw new ArgumentNullException(nameof(publishEventRequestDTO));
            }

            //Get requested event from DB
            eventInfo = new CbisEventDAL().GetEventInformationByName(publishEventRequestDTO.EventName);

            if (eventInfo == null)
            {
                throw new EventException(publishEventRequestDTO.EventName, $"The event {publishEventRequestDTO.EventName} is not supported by CBIS");
            }

            if (!this.AuthenticateEvent(publishEventRequestDTO.EbProductID))
            {
                throw new EventException(publishEventRequestDTO.EventName, $"The product {publishEventRequestDTO.EbProductID} cannot publish the event {publishEventRequestDTO.EventName}");
            }

            ProductInformation productInformation = new ProductInformationDAL().GetProductInformationById(productId);
            if (productInformation == null)
            {
                throw new ProductException(productId, $"Failed to load information of product {productId} from DB");
            }

            // Generate the notification request
            ProductNotificationRequestDTO productNotificationRequestDTO = GetProductNotificationRequest(publishEventRequestDTO);

            try
            {
                string token = CbisQueue.PublishEvent(productInformation, eventInfo, productNotificationRequestDTO);
                Logger.LogTrace("Publish event End");
                return new PublishEventResponseDTO() { EbPublishedEventId = token };
            }
            catch (Exception ex)
            {
                Logger.LogTrace("Publish event End");
                if (ex is HttpConnectionException)
                {
                    throw ex;
                }
                throw new ApplicationException("Failed to publish event notification. " + ex.Message);
            }
        }

        /// <summary>
        /// Checking whether the event is published by event owner(event creater)
        /// </summary>
        /// <param name="ebPublisher"></param>
        /// <returns></returns>
        private bool AuthenticateEvent(string ebPublisher)
        {
            Logger.LogTrace($"Method: AuthenticateEvent() Authenticate the published event ebPublisher:{ebPublisher}");
            if (eventInfo.EventOwner != ebPublisher)
                return false;

            return true;
        }

        /// <summary>
        /// Get Product notification request data model
        /// </summary>
        /// <param name="publishEventRequestDTO"></param>
        /// <returns></returns>
        private ProductNotificationRequestDTO GetProductNotificationRequest(PublishEventRequestDTO publishEventRequestDTO)
        {
            Logger.LogTrace("Method: GetProductNotificationRequest() Create the product notification request");
            return new ProductNotificationRequestDTO()
            {
                PublishingProductName = publishEventRequestDTO.ProductName,
                InstitutionName = publishEventRequestDTO.InstitutionName,
                EbInstitutionId = publishEventRequestDTO.EbInstitutionId,
                EventName = publishEventRequestDTO.EventName,
                MessageType = eventInfo.ModelType.ToString(),
                AcknowledgementRequired = publishEventRequestDTO.AcknowledgementRequired,
                Payload = publishEventRequestDTO.Payload
            };
        }
    }
}
