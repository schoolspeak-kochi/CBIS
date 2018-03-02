
using CB.IntegrationService.BLL.Utils;
using CB.IntegrationService.DAL;
using CB.IntegrationService.Models;
using CB.IntegrationService.Models.Exceptions;
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
                string token = MessageBroker.PublishEvent(productInformation, eventInfo, productNotificationRequestDTO);
                return new PublishEventResponseDTO() { EbPublishedEventId = token };
            }
            catch (Exception ex)
            {
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
