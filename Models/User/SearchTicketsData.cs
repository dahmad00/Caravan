using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using models = RailwaySystem.Models.Admin;

namespace RailwaySystem.Models.User
{
    public class SearchTicketsData
    {
        public SearchTicketsData(string sourceLoc, string destLoc, string onDate)
        {
            this.onDate = onDate;
            SourceLocation = sourceLoc;
            DestLocation = destLoc;
            routes = new LinkedList<models.Route>();
            Locations = new LinkedList<string>();

            string onDateTime = onDate + " 00:00:01";
            string toDateTime = DateOnly.Parse(this.onDate).AddDays(1).ToString("yyyy-MM-dd") + " 00:00:01";

            Message = $"The following routes are operational from {SourceLocation} to {DestLocation} on {onDate}.";

            LoadRoutes(onDateTime, toDateTime, sourceLoc, destLoc);
            LoadLocations();
        }

        private void LoadRoutes(string onDateTime, string toDateTime, string sourceLoc, string destLoc)
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();

                string query = "SELECT * FROM [Route] " +
                               "WHERE Departure > @OnDateTime AND Departure < @ToDateTime " +
                               "AND SourceSt IN (SELECT ID FROM Station WHERE [Location] = @SourceLoc) " +
                               "AND DestSt IN (SELECT ID FROM Station WHERE [Location] = @DestLoc)";

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@OnDateTime", onDateTime);
                cmd.Parameters.AddWithValue("@ToDateTime", toDateTime);
                cmd.Parameters.AddWithValue("@SourceLoc", sourceLoc);
                cmd.Parameters.AddWithValue("@DestLoc", destLoc);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            models.Route tempRoute = new models.Route
                            {
                                Id = (int)reader["ID"],
                                TrainID = (int)reader["TrainID"],
                                DepartureTime = reader["Departure"].ToString(),
                                ArrivalTime = reader["Arrival"].ToString(),
                                SourceStationID = (int)reader["SourceSt"],
                                DestinationStationID = (int)reader["DestSt"],
                                TotalA = (int)reader["Total_A"],
                                TotalB = (int)reader["Total_B"],
                                TotalC_Both = (int)reader["Total_C_Both"],
                                TotalC_Seat = (int)reader["Total_C_Seat"],
                                Purchased_A = (int)reader["Purchased_A"],
                                Purchased_B = (int)reader["Purchased_B"],
                                Purchased_C_Both = (int)reader["Purchased_C_Both"],
                                Purchased_C_Seat = (int)reader["Purchased_C_Seat"],
                                FareA = (int)reader["Fare_A"],
                                FareB = (int)reader["Fare_B"],
                                FareC_Both = (int)reader["Fare_C_Both"],
                                FareC_Seat = (int)reader["Fare_C_Seat"]
                            };

                            tempRoute.setValues();
                            routes.AddLast(tempRoute);
                        }
                    }
                }
            }
        }

        private void LoadLocations()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();

                string query = "SELECT DISTINCT Location FROM Station";

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string temp = reader["Location"] as string;
                            if (temp != null)
                            {
                                Locations.AddLast(temp);
                            }
                        }
                    }
                }
            }
        }

        public LinkedList<models.Route> routes { get; set; }
        public string? SourceLocation { get; set; }
        public string? DestLocation { get; set; }
        public string? onDate { get; set; }
        public string Message { get; set; }
        public LinkedList<string> Locations { get; set; }
        public DateTime? DateToday { get; set; }
    }
}
