using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CB.IntegrationService.Models;
using System.Data.SqlClient;
using System.Data;
using CB.IntegrationService.DAL.Data;
using CB.IntegrationService.Utils;

namespace CB.IntegrationService.DAL
{
    public class ProductInformationDAL
    {
        /// <summary>
        /// Get product information by product id
        /// </summary>
        /// <param name="ebProductId">Product id</param>
        /// <returns></returns>
        public ProductInformation GetProductInformationById(string ebProductId)
        {
            Logger.LogTrace($" ProductInformationDAL.cs  Method:GetProductInformationById() ebProductId={ebProductId}");
            ProductInformation productInformation = null;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseCredentials.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("products.GetProductInformation", sqlConnection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ebProductId", SqlDbType.BigInt).Value = ebProductId;
                    sqlConnection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productInformation = new ProductInformation();
                            long.TryParse(reader["EbProductId"].ToString(), out productInformation.EbProductId);
                            productInformation.ProductName = reader["Name"].ToString();
                            productInformation.EndpointURL = reader["EndpointURL"].ToString();
                            productInformation.ProductSecret = reader["ProductSecret"].ToString();

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogTrace(" ProductInformationDAL.cs  Method:GetProductInformationById() END");
                throw new ApplicationException("Failed to get the product information from the database", ex);
            }
            Logger.LogTrace(" ProductInformationDAL.cs  Method:GetProductInformationById() END");
            return productInformation;
        }

        /// <summary>
        /// Get product information by product name
        /// </summary>
        /// <param name="productName">Product name</param>
        /// <returns></returns>
        public ProductInformation GetProductInformationByName(string productName)
        {
            Logger.LogTrace($" ProductInformationDAL.cs  Method:GetProductInformationByName() productName={productName}");
            ProductInformation productInformation = null;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseCredentials.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("products.GetProductInformationByName", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@productName", SqlDbType.NVarChar).Value = productName;
                    sqlConnection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productInformation = new ProductInformation();
                            long.TryParse(reader["EbProductId"].ToString(), out productInformation.EbProductId);
                            productInformation.ProductName = reader["Name"].ToString();
                            productInformation.EndpointURL = reader["EndpointURL"].ToString();
                            productInformation.ProductSecret = reader["ProductSecret"].ToString();

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogTrace(" ProductInformationDAL.cs  Method:GetProductInformationByName() END");
                throw new ApplicationException("Failed to get the product information from the database", ex);
            }
            Logger.LogTrace(" ProductInformationDAL.cs  Method:GetProductInformationByName() END");
            return productInformation;
        }
    }
}
