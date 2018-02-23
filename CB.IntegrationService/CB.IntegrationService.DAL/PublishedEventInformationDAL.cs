using CB.IntegrationService.DAL.Data;
using CB.IntegrationService.Models;
using CB.IntegrationService.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.DAL
{
    public  class PublishedEventInformationDAL
    {
        string connectionString { get; set; }

        public PublishedEventInformationDAL()
        {
            connectionString = DatabaseCredentials.ConnectionString;
        }
        /// <summary>
        /// Create a new published record for the event specified
        /// </summary>
        /// <param name="ebEventId">Id of the event to pubish</param>
        /// <param name="ebProductId">Id of the product which publishing the event</param>
        /// <param name="payload">Payload information of the publishing event</param>
        /// <param name="eventDeliveryInformation">Event delivery information</param>
        /// <returns></returns>
        public long CreatePublishedEventInformation(string ebEventId, string ebProductId, string payload, EventDeliveryInformation eventDeliveryInformation)
        {
            long tokenId = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("events.CreatePublishedEventInformation", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ebEventId", SqlDbType.BigInt).Value = ebEventId;
                    cmd.Parameters.AddWithValue("@ebProductId", SqlDbType.BigInt).Value = ebProductId;
                    cmd.Parameters.AddWithValue("@payload", SqlDbType.NVarChar).Value = payload;
                    cmd.Parameters.AddWithValue("@deliveryInformation", SqlDbType.NVarChar).Value = JsonHelper.Serialize(eventDeliveryInformation);
                    sqlConnection.Open();
                    object token = cmd.ExecuteScalar();
                    if (token != null)
                    {
                        tokenId = long.Parse(token.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create the published event information in the database", ex);
            }

            return tokenId;
        }
        /// <summary>
        /// Get published event information by event id
        /// </summary>
        /// <param name="tokenId">Published event id</param>
        /// <returns></returns>
        public PublishedEventInformation GetPublishedEventInformation(long tokenId)
        {
            return GetPublishedEventInformation(tokenId.ToString());
        }

        /// <summary>
        /// Get published event information by event if
        /// </summary>
        /// <param name="tokenId">Published event id</param>
        /// <returns></returns>
        public PublishedEventInformation GetPublishedEventInformation(string tokenId)
        {
            PublishedEventInformation eventInformation = null;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("events.GetPublishedEventInformation", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tokenId", SqlDbType.BigInt).Value = tokenId;
                    sqlConnection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            eventInformation = new PublishedEventInformation();
                            long.TryParse(reader["TokenId"].ToString(), out eventInformation.TokenId);
                            long.TryParse(reader["EbEventId"].ToString(), out eventInformation.EbEventId);
                            long.TryParse(reader["EbProductId"].ToString(), out eventInformation.EbProductId);
                            eventInformation.Payload = reader["Payload"].ToString();
                            eventInformation.EventDeliveryInformation = JsonHelper.DeSerialize<EventDeliveryInformation>(reader["DeliveryInformation"].ToString());
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get the published event information from the database", ex);
            }

            return eventInformation;
        }

        /// <summary>
        /// Update published event delivery information
        /// </summary>
        /// <param name="tokenId">Id of the event to update the delivery information</param>
        /// <param name="eventDeliveryInformation">Event delivery information to be update</param>
        /// <returns></returns>
        public bool UpdatePublishedEventDeliveryInformation(long tokenId, EventDeliveryInformation eventDeliveryInformation)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("events.UpdatePublishedEventDeliveryInformation", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tokenId", SqlDbType.BigInt).Value = tokenId;
                    cmd.Parameters.AddWithValue("@deliveryInformation", SqlDbType.NVarChar).Value = JsonHelper.Serialize(eventDeliveryInformation);
                    sqlConnection.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create the published event information in the database", ex);
            }

            return false;
        }

        /// <summary>
        /// Delete published event information from the database by id
        /// </summary>
        /// <param name="tokenId">Published event id</param>
        /// <returns></returns>
        public bool DeletePublishedEventInformation(long tokenId)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("events.DeletePublishedEventInformation", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tokenId", SqlDbType.BigInt).Value = tokenId;
                    sqlConnection.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create the published event information in the database", ex);
            }

            return false;
        }
    }
}
