using System;
using System.Data.SqlClient;

namespace RailwaySystem.Models.Admin
{
    public class Route
    {
        public Route(int id)
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Route WHERE ID = @Id;", sqlCon);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                Id = (int)reader["ID"];
                TrainID = (int)reader["TrainID"];
                DepartureTime = reader["Departure"].ToString();
                ArrivalTime = reader["Arrival"].ToString();
                SourceStationID = (int)reader["SourceSt"];
                DestinationStationID = (int)reader["DestSt"];
                TotalA = (int)reader["Total_A"];
                TotalB = (int)reader["Total_B"];
                TotalC_Both = (int)reader["Total_C_Both"];
                TotalC_Seat = (int)reader["Total_C_Seat"];
                Purchased_A = (int)reader["Purchased_A"];
                Purchased_B = (int)reader["Purchased_B"];
                Purchased_C_Both = (int)reader["Purchased_C_Both"];
                Purchased_C_Seat = (int)reader["Purchased_C_Seat"];
                FareA = (int)reader["Fare_A"];
                FareB = (int)reader["Fare_B"];
                FareC_Both = (int)reader["Fare_C_Both"];
                FareC_Seat = (int)reader["Fare_C_Seat"];
            }
            setValues();
        }

        public Route() { }

        public int Id { get; set; }
        public int TrainID { get; set; }
        public string? Train { get; set; }
        public string? DepartureTime { get; set; }
        public string? ArrivalTime { get; set; }
        public string? SourceStation { get; set; }
        public int SourceStationID { get; set; }
        public string? DestinationStation { get; set; }
        public int DestinationStationID { get; set; }
        public int TotalA { get; set; }
        public int TotalB { get; set; }
        public int TotalC_Both { get; set; }
        public int TotalC_Seat { get; set; }
        public int Purchased_A { get; set; }
        public int Purchased_B { get; set; }
        public int Purchased_C_Both { get; set; }
        public int Purchased_C_Seat { get; set; }
        public int FareA { get; set; }
        public int FareB { get; set; }
        public int FareC_Both { get; set; }
        public int FareC_Seat { get; set; }
        public DateTime departureTime;
        public DateTime arrivalTime;

        public void setTrain()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT Name FROM Train WHERE ID = @TrainID;", sqlCon);
                cmd.Parameters.AddWithValue("@TrainID", TrainID);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                Train = (string)reader["Name"];
            }
        }

        public void setSource()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT Station_Name FROM Station WHERE ID = @SourceStationID;", sqlCon);
                cmd.Parameters.AddWithValue("@SourceStationID", SourceStationID);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                SourceStation = (string)reader["Station_Name"];
            }
        }

        public void setDestination()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT Station_Name FROM Station WHERE ID = @DestinationStationID;", sqlCon);
                cmd.Parameters.AddWithValue("@DestinationStationID", DestinationStationID);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                DestinationStation = (string)reader["Station_Name"];
            }
        }

        public void delete()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Route WHERE ID = @Id;", sqlCon);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void setValues()
        {
            setTrain();
            setSource();
            setDestination();
            departureTime = DateTime.Parse(DepartureTime);
            arrivalTime = DateTime.Parse(ArrivalTime);
        }
    }
}
