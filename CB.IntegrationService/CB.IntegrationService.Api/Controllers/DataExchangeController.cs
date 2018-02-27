/*
 * Education Brands Integration Service APIs
 *
 * The integration framework is an attempt to define a standard and simple socket each brand can plug in to interact with other EB products. It defines what each product needs to do to integrate with other products. It has a set of API to interact with other products and what each product should implement to receive communication from other products. This is an API specification detailing the APIs for Education Brands IntegrationService.  Most of these APIs will be implemented in EBIS.  The APIs in the 'Product Endpoints' section has to be implemented by each of the Products.  <b>NOTE - <i>This specification is still in early development stage and is subject to change without notice.</i></b> 
 *
 * OpenAPI spec version: 1.0.0
 * Contact: sobin@schoolspeak.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using CB.IntegrationService.ApiModels;
using System;
using System.Web.Http;
using CB.IntegrationService.BLL.Utils;
using CB.IntegrationService.DAL;
using CB.IntegrationService.BLL.Constants;
using CB.IntegrationService.StandardDataSet.Models;
using CB.IntegrationService.Utils;
using CB.IntegrationService.Models;
using System.Linq;
using CB.IntegrationService.StandardDataSet.Constants;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommunityBrands.IntegrationService.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class DataExchangeController : ApiController
    { 
        /// <summary>
        /// Query for data
        /// </summary>
        /// <remarks>The Query API will allow a user to query data from services(like admissions, enrollment etc) offered by EB products.  It also provides means to retrieve information of various types including member (student, parent, staff), household, admission, enrollment etc</remarks>
        /// <param name="getQueryRequest"></param>
        /// <response code="200">Successfully returned the data.</response>
        [HttpPost]
        [Route("CBIS/1.0.0/query")]
        public virtual IHttpActionResult GetQueryRequest([FromBody]GetQueryRequest getQueryRequest)
        {
            return BadRequest("This method has not been implemented");
        }

        /// <summary>
        /// Acknowledge an event notification.
        /// </summary>
        /// <remarks>Acknowledge a notification for an event. A product that has received a notification can send an acknowledgement to the EBIS and EBis willcacknowledge  the original publisher with a status and message. The publisher will use the event token to identify the acknowledgement was against which publish.</remarks>
        /// <param name="notificationAcknowledgeRequest"></param>
        /// <response code="200">Notification has been acknowledged successfully</response>
        [HttpPost]
        [Route("CBIS/1.0.0/notifications/acknowledge")]
        public virtual IHttpActionResult NotificationAcknowledge([FromBody]NotificationAcknowledgeRequest notificationAcknowledgeRequest)
        {
            if (!CBAuthorizationHandler.AuthorizeRequest(Request))
            {
                // If the request failed to authenticate the system responds with a 401 unauthorized access.
                return Unauthorized();
            }

            if (notificationAcknowledgeRequest == null)
            {
                // Validate the acknowledgement request 
                return BadRequest("Invalid acknowledgement information.");
            }

            if (String.IsNullOrWhiteSpace(notificationAcknowledgeRequest.EventToken))
            {
                // Validate the event tocken
                return BadRequest("Invalid acknowledgement request tocken information.");
            }
            PublishedEventInformation eventInfo = new PublishedEventInformationDAL().GetPublishedEventInformation(notificationAcknowledgeRequest.EventToken);
            if (eventInfo == null)
            {
                return BadRequest("Failed to acknowledge with the given event tocken");
            }

            try
            {
                ProductNotificationRequest ebEventData = JsonHelper.DeSerialize<ProductNotificationRequest>(eventInfo.Payload.ToString());
                if (ebEventData == null)
                {
                    ErrorNotifyHelper.InternalError("Failed to parse event payload information while acknowledge");
                    return BadRequest();
                }
                if (!ebEventData.AcknowledgementRequired.Value)
                {
                    BadRequest("Acknowledgement is not opted for the event");
                }
            }
            catch (Exception ex)
            {
                ErrorNotifyHelper.InternalError("Failed to publish event notification", ex);
                return InternalServerError();
            }


            ProductInformation productInformation = new ProductInformationDAL().GetProductInformationById(eventInfo.EbProductId.ToString());
            if (productInformation == null)
            {
                return NotFound();
            }



            // Send acknowledgement to the client.
            SendAcknowledgement(productInformation.EndpointURL, notificationAcknowledgeRequest).GetAwaiter().GetResult();
            return Ok("Successfully acknowledged");
        }

        /// <summary>
        /// Send event acknowledgement to the orginator of the message
        /// </summary>
        /// <param name="ebAcknowledgeData"></param>
        /// <returns></returns>
        [NonAction]
        public static async Task<HttpResponseMessage> SendAcknowledgement(string url, NotificationAcknowledgeRequest notificationAcknowledgeRequest)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await new HttpClient().PostAsJsonAsync<NotificationAcknowledgeRequest>(url + @"/acknowledgeNotification", notificationAcknowledgeRequest);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                ErrorNotifyHelper.ClientError("Failed to acknowledge", "Product base url is" + url, ex);
            }
            // return URI of the created resource.
            return response;
        }
        /// <summary>
        /// Publish an event.
        /// </summary>
        /// <remarks>Publish a notification against a event. All those who have subscribed to the event will receive notification. The payload that is provided in the Publish will be passed on to each of the subscribers.</remarks>
        /// <param name="publishEventRequest"></param>
        /// <response code="200">EBIS published Event token</response>
        [HttpPost]
        [Route("CBIS/1.0.0/notifications/publish")]
        public virtual IHttpActionResult PublishEvent([FromBody]PublishEventRequest publishEventRequest)
        {
            if (!CBAuthorizationHandler.AuthorizeRequest(Request))
            {
                // If the request failed to authenticate the system responds with a 401 unauthorized access.
                return Unauthorized();
            }

            if (publishEventRequest == null)
            {
                return BadRequest("Invalid event information.");
            }

            // Validate requested event from DB
            EventInformation eventInfo = new EventInformationDAL().GetEventInformationByName(publishEventRequest.EventName);

            // Notify the client if the requested event not supported
            if (eventInfo == null)
            {
                return BadRequest("Unsupported event. Please request for a valid event");
            }

            // IIntegrationModel integrationModel = null;
            CB.IntegrationService.StandardDataSet.Constants.StandardDataModels MessageType = eventInfo.ModelType;
            try
            {

                switch (eventInfo.ModelType)
                {
                    case StandardDataModels.Member:
                        MembersCollection member = JsonHelper.DeSerialize<MembersCollection>(publishEventRequest.Payload.ToString());
                        break;

                    case StandardDataModels.Default:
                    default:
                       // MessageType = StandardDataModels.Default;
                        //integrationModel = (IIntegrationModel)JsonHelper.DeSerialize<EBDefaultModel>(publishEventRequest.ToString());
                        break;
                }
            }
            catch
            {
                // Notify, If the request failed to map with the requested event standard model
                return BadRequest("Unexpected error occurred while parsing requested model. Please use the standard model assigned to the event.");
            }

            // Validate request model 
            //if (integrationModel == null)
            //{
            //    return BadRequest("Unsupported event model, please use a standard data model to map your model.");
            //}

            string productId = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Request.Headers.Authorization.Parameter)).Split(':')[0];
            ProductInformation productInformation = new ProductInformationDAL().GetProductInformationById(productId);
            if (productInformation == null)
            {
                Unauthorized();
            }

            // Generate the notification request
            ProductNotificationRequest productNotificationRequest = new ProductNotificationRequest()
            {
                PublishingProductName = productInformation.ProductName,
                InstitutionName = publishEventRequest.InstitutionName,
                EbInstitutionId = publishEventRequest.EbInstitutionId,
                EventName = publishEventRequest.EventName,
                MessageType = MessageType.ToString(),
                AcknowledgementRequired = publishEventRequest.AcknowledgementRequired,
                Payload = publishEventRequest.Payload
            };


            try
            {
                string token = MessageBroker.PublishEvent(productInformation, eventInfo, productNotificationRequest);
                return Json(new PublishEventResponse { EbPublishedEventId = token });
            }
            catch (ApplicationException ex)
            {
                ErrorNotifyHelper.InternalError("Failed to publish event notification", ex);
                return InternalServerError(ex);
            }
            catch (Exception ex)
            {
                ErrorNotifyHelper.InternalError("Failed to publish event notification", ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Subscribe to a published event
        /// </summary>
        /// <remarks>Inorder to get the notifications for an event, the product has to subscribe for that event.</remarks>
        /// <param name="subscribeEventRequest"></param>
        /// <response code="200">Eb Event Subscription Id</response>
        [HttpPost]
        [Route("CBIS/1.0.0/notifications/subscribe")]
        public virtual IHttpActionResult SubscribeNotificationEvent([FromBody]SubscribeEventRequest subscribeEventRequest)
        {
            return BadRequest("This method has not been implemented");
        }
    }
}
