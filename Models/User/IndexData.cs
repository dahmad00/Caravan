using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using RailwaySystem.Models.Admin;

namespace RailwaySystem.Models.User
{
    public class IndexData
    {
        public IndexData()
        {
            Locations = new LinkedList<string>();
            DateToday = DateTime.Today.ToString("yyyy-MM-dd");

            LoadLocations();
        }

        private void LoadLocations()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();

                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Location FROM Station", sqlCon);

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

        public LinkedList<string> Locations { get; set; }
        public string DateToday { get; set; }
    }
}
