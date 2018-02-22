using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CB.IntegrationService.Models.Constants;
using CB.IntegrationService.Models.Models;
using CB.IntegrationService.DAL;

namespace CB.IntegrationService.BLL.Utils
{
    public class CBAuthorizationHandler
    {
        /// <summary>
        /// Authorize the product using product authenitcation information included in the header
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static bool AuthorizeProduct(HttpRequestMessage request)
        {
            if (request.Headers.Contains(AuthenticationHeaders.PRODUCT_ID)
                && request.Headers.Contains(AuthenticationHeaders.PRODUCT_SECRET)
                )
            {
                ProductInformation product = new ProductInformationDAL().GetProductInformationById(request.Headers.GetValues(AuthenticationHeaders.PRODUCT_ID).FirstOrDefault());
                if (product == null)
                    return false;

                if (product.ProductSecret == request.Headers.GetValues(AuthenticationHeaders.PRODUCT_SECRET).FirstOrDefault())
                    return true;

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
