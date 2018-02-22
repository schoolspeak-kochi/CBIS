using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CB.IntegrationService.Models;
using System.Data.SqlClient;
using System.Data;
using CB.IntegrationService.DAL.Data;

namespace CB.IntegrationService.DAL
{
    public class ProductInformationDAL
    {
        public ProductInformation GetProductInformationById(string ebProductId)
        {
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
                throw new ApplicationException("Failed to get the product information from the database", ex);
            }

            return productInformation;
        }

        public ProductInformation GetProductInformationByName(string productName)
        {
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
                throw new ApplicationException("Failed to get the product information from the database", ex);
            }

            return productInformation;
        }
    }
}
