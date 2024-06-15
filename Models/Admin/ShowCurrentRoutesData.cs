using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace RailwaySystem.Models.Admin
{
    public class ShowCurrentRoutesData
    {
        public ShowCurrentRoutesData()
        {
            routes = new LinkedList<Route>();
            LoadRoutes();
        }

        private void LoadRoutes()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Route", sqlCon);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Route tempRoute = new Route();
                            tempRoute.Id = (int)reader["ID"];
                            tempRoute.TrainID = (int)reader["TrainID"];
                            DateTime departureTime = (DateTime)reader["Departure"];
                            tempRoute.DepartureTime = reader["Departure"].ToString();
                            tempRoute.ArrivalTime = reader["Arrival"].ToString();
                            tempRoute.SourceStationID = (int)reader["SourceSt"];
                            tempRoute.DestinationStationID = (int)reader["DestSt"];
                            tempRoute.TotalA = (int)reader["Total_A"];
                            tempRoute.TotalB = (int)reader["Total_B"];
                            tempRoute.TotalC_Both = (int)reader["Total_C_Both"];
                            tempRoute.TotalC_Seat = (int)reader["Total_C_Seat"];
                            tempRoute.Purchased_A = (int)reader["Purchased_A"];
                            tempRoute.Purchased_B = (int)reader["Purchased_B"];
                            tempRoute.Purchased_C_Both = (int)reader["Purchased_C_Both"];
                            tempRoute.Purchased_C_Seat = (int)reader["Purchased_C_Seat"];
                            tempRoute.FareA = (int)reader["Fare_A"];
                            tempRoute.FareB = (int)reader["Fare_B"];
                            tempRoute.FareC_Both = (int)reader["Fare_C_Both"];
                            tempRoute.FareC_Seat = (int)reader["Fare_C_Seat"];

                            tempRoute.setTrain();
                            tempRoute.setSource();
                            tempRoute.setDestination();

                            DateTime now = DateTime.Now;

                            if (DateTime.Compare(now, departureTime) < 0)
                            {
                                routes.AddLast(tempRoute);
                            }
                        }
                    }
                }
            }
        }

        public LinkedList<Route> routes;
    }
}
