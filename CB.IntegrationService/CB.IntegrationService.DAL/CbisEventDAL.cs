using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CB.IntegrationService.Models;
using System.Data;
using CB.IntegrationService.DAL.Data;
using CB.IntegrationService.StandardDataSet.Constants;

namespace CB.IntegrationService.DAL
{
    public class CbisEventDAL
    {
        /// <summary>
        /// Create a new event information  entry.
        /// </summary>
        /// <param name="eventName">Event name should be a unique identifier</param>
        /// <param name="eventDescription">Event description</param>
        /// <param name="modelType">Standard data model used for the event</param>
        /// <param name="Subscribers">Event subscribers lists</param>
        /// <returns></returns>
        public long CreateEventInformation(string eventName, string eventDescription, StandardDataModels modelType, List<string> Subscribers)
        {
            long ebEventId = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseCredentials.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("events.CreateEventInformation", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@eventName", SqlDbType.NVarChar).Value = eventName;
                    cmd.Parameters.AddWithValue("@eventDescription", SqlDbType.NVarChar).Value = eventDescription;
                    cmd.Parameters.AddWithValue("@subscribers", SqlDbType.NVarChar).Value = string.Join(",", Subscribers);
                    cmd.Parameters.AddWithValue("@ModelType", SqlDbType.NVarChar).Value = modelType;
                    sqlConnection.Open();
                    object token = cmd.ExecuteScalar();
                    if (token != null)
                    {
                        ebEventId = long.Parse(token.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create the event information in the database", ex);
            }

            return ebEventId;
        }

        /// <summary>
        /// Get event information by event name
        /// </summary>
        /// <param name="ebEventName">Event name</param>
        /// <returns></returns>
        public CbisEvent GetEventInformationByName(string ebEventName)
        {
            CbisEvent eventInformation = null;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseCredentials.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("events.GetEventInformation", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ebEventName", SqlDbType.VarChar).Value = ebEventName;
                    sqlConnection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            eventInformation = new CbisEvent();
                            long.TryParse(reader["EbEventId"].ToString(), out eventInformation.EbEventId);
                            eventInformation.EventName = reader["EventName"].ToString();
                            eventInformation.EventDescription = reader["EventDescription"].ToString();
                            eventInformation.Subscribers = reader["Subscribers"].ToString().Split(',').ToList();
                            StandardDataModels modelType = StandardDataModels.None;
                            Enum.TryParse(reader["ModelType"].ToString(), out modelType);
                            eventInformation.ModelType = modelType;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get the event information from the database", ex);
            }

            return eventInformation;
        }

        /// <summary>
        /// Get event information by event id
        /// </summary>
        /// <param name="ebEventId">Event id</param>
        /// <returns></returns>
        public CbisEvent GetEventInformation(long ebEventId)
        {
            CbisEvent eventInformation = null;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DatabaseCredentials.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("events.GetEventInformation", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ebEventId", SqlDbType.BigInt).Value = ebEventId;
                    sqlConnection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            eventInformation = new CbisEvent();
                            long.TryParse(reader["EbEventId"].ToString(), out eventInformation.EbEventId);
                            eventInformation.EventName = reader["EventName"].ToString();
                            eventInformation.EventDescription = reader["EventDescription"].ToString();
                            eventInformation.Subscribers = reader["Subscribers"].ToString().Split(',').ToList();
                            StandardDataModels modelType = StandardDataModels.None;
                            Enum.TryParse(reader["ModelType"].ToString(), out modelType);
                            eventInformation.ModelType = modelType;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get the event information from the database", ex);
            }

            return eventInformation;
        }
    }
}
