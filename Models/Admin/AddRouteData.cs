using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace RailwaySystem.Models.Admin
{
    public class AddRouteData
    {
        public AddRouteData()
        {
            trains = new LinkedList<Train>();
            stations = new LinkedList<Station>();
            errors = new LinkedList<string>();

            LoadTrains();
            LoadStations();
        }

        private void LoadTrains()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Train", sqlCon);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Train tempTrain = new Train
                            {
                                Name = (string)reader["Name"],
                                Id = (int)reader["Id"]
                            };
                            trains.AddLast(tempTrain);
                        }
                    }
                }
            }
        }

        private void LoadStations()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Station", sqlCon);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Station tempStation = new Station
                            {
                                Name = (string)reader["Station_Name"],
                                Id = (int)reader["Id"]
                            };
                            stations.AddLast(tempStation);
                        }
                    }
                }
            }

            foreach (Station station in stations)
            {
                System.Diagnostics.Debug.WriteLine(station.Name);
            }
        }

        public void addError(string error)
        {
            errors.AddLast(error);
        }

        public bool hasErrors()
        {
            return errors.Count > 0;
        }

        public void addErrors(LinkedList<string> errors)
        {
            this.errors = errors;
        }

        public LinkedList<Train> trains;
        public LinkedList<Station> stations;
        public LinkedList<string> errors;
    }
}
