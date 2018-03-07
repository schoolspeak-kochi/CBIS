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
using CB.IntegrationService.Models;
using System.Net.Http;
using CB.IntegrationService.BLL;
using CB.IntegrationService.BLL.Utils;
using AutoMapper;
using CB.IntegrationService.Utils;
using CB.IntegrationService.Models.Exceptions;
using CB.IntegrationService.Models.DTO;

namespace CommunityBrands.IntegrationService.Api.Controllers
{
    /// <summary>
    /// Defines the data exchange Api controller
    /// </summary>
    [CB.IntegrationService.Api.Filters.IdentityBasicAuthentication]
    [Authorize]
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
            Logger.LogTrace("Called the API 'CBIS/1.0.0/query' Method: POST");
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
            Logger.LogTrace("Called the API 'CBIS/1.0.0/notifications/acknowledge' Method: POST");

            //Auto map publishEventRequest object to its currsesponding DTO object
            NotificationAcknowledgeRequestDTO notificationAcknowledgeRequestDTO = AutoMapperConfig.MapperConfiguration.CreateMapper().Map<NotificationAcknowledgeRequest, NotificationAcknowledgeRequestDTO>(notificationAcknowledgeRequest);

            try
            {
                HttpResponseMessage response = null;
                response = new CbisAcknowledgeBLL().Acknowledge(notificationAcknowledgeRequestDTO);
                Logger.LogTrace("API 'CBIS/1.0.0/notifications/acknowledge' Method: POST  End");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return Ok("Successfully acknowledged");

                return BadRequest($"Status Code: {response.StatusCode.ToString()}. Failed to acknowledge");

            }
            catch (Exception ex)
            {
                Logger.LogTrace("API 'CBIS/1.0.0/notifications/acknowledge' Method: POST  End");
                if (ex is HttpConnectionException)
                {
                    ex.LogErrorAndNotify();
                    return BadRequest(ex.Message);
                }              
                ex.LogException();
                if (ex is ApplicationException)
                    return InternalServerError(ex);
                return BadRequest(ex.Message);
            }
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
            Logger.LogTrace("API 'CBIS/1.0.0/notifications/publish' Method: POST  Publish an event");
            //Auto map publishEventRequest object to its currsesponding DTO object
            PublishEventRequestDTO publishEventRequestDTO = AutoMapperConfig.MapperConfiguration.CreateMapper().Map<PublishEventRequest, PublishEventRequestDTO>(publishEventRequest);

            try
            {
                return Json(new CbisEventBLL().Publish(RequestContext.Principal.Identity.Name, publishEventRequestDTO));
            }
            catch (Exception ex)
            {
                if (ex is HttpConnectionException)
                {
                    ex.LogErrorAndNotify();
                    return BadRequest(ex.Message);
                }
                ex.LogException();
                if (ex is ApplicationException)
                    return InternalServerError(ex);

                return BadRequest(ex.Message);
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
            Logger.LogTrace("API CBIS/1.0.0/notifications/subscribe");
            return BadRequest("This method has not been implemented");
        }
    }
}
