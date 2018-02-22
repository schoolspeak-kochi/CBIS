using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.DAL
{
    public class EventInformationDAL
    {
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

        public EventInformation GetEventInformationByName(string ebEventName)
        {
            EventInformation eventInformation = null;
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
                            eventInformation = new EventInformation();
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

        public EventInformation GetEventInformation(long ebEventId)
        {
            EventInformation eventInformation = null;
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
                            eventInformation = new EventInformation();
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
