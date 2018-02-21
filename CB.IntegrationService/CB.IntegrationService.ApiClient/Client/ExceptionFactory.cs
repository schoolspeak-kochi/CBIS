/* 
 * Education Brands Integration Service APIs
 *
 * The integration framework is an attempt to define a standard and simple socket each brand can plug in to interact with other EB products. It defines what each product needs to do to integrate with other products. It has a set of API to interact with other products and what each product should implement to receive communication from other products. This is an API specification detailing the APIs for Education Brands IntegrationService.  Most of these APIs will be implemented in EBIS.  The APIs in the 'Product Endpoints' section has to be implemented by each of the Products.  <b>NOTE - <i>This specification is still in early development stage and is subject to change without notice.</i></b> 
 *
 * OpenAPI spec version: 1.0.0
 * Contact: sobin@schoolspeak.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */


using System;
using RestSharp;

namespace CB.IntegrationService.ApiClient.Client
{
    /// <summary>
    /// A delegate to ExceptionFactory method
    /// </summary>
    /// <param name="methodName">Method name</param>
    /// <param name="response">Response</param>
    /// <returns>Exceptions</returns>
    public delegate Exception ExceptionFactory(string methodName, IRestResponse response);
}
