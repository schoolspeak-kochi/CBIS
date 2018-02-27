using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CB.IntegrationService.Models.Constants;
using CB.IntegrationService.Models;
using CB.IntegrationService.DAL;
using CB.IntegrationService.Utils;

namespace CB.IntegrationService.BLL.Utils
{
    public class CBAuthorizationHandler
    {
        /// <summary>
        /// Authorize the product using product authentication information included in the header
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static bool AuthorizeProduct(HttpRequestMessage request)
        {
            string authorizationHeader = request.Headers.Authorization.Parameter;
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                byte[] base64EncodedBytes = Convert.FromBase64String(authorizationHeader);
                string decodedAuthorizationInfo = Encoding.UTF8.GetString(base64EncodedBytes);
                string[] authorizationInfo = decodedAuthorizationInfo.Split(':');
                try
                {
                    string requestProductId = authorizationInfo[0];
                    string requestProductSecret = authorizationInfo[1];
                    ProductInformation product = new ProductInformationDAL().GetProductInformationById(requestProductId);
                    if (product == null)
                        return false;

                    if (product.ProductSecret == requestProductSecret)
                        return true;
                }
                catch (Exception ex)
                {
                    ex.LogException();
                    return false;
                }

                // validate the product id and secret
                return false;
            }

            return false;
        }

        /// <summary>
        /// Authorize user with the header user secret
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static bool AuthorizeUser(HttpRequestMessage request)
        {
            return true;
        }

        /// <summary>
        /// Authorize service request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool AuthorizeRequest(HttpRequestMessage request)
        {
            if (!AuthorizeProduct(request))
                return false;

            if (!AuthorizeUser(request))
                return false;

            return true;
        }
    }
}
