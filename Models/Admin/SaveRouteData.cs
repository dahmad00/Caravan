using System.Data.SqlClient;

namespace RailwaySystem.Models.Admin
{
    public class SaveRouteData
    {
        public void saveData()
        {
            using (SqlConnection conn = DatabaseUtils.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO Route(TrainID, Departure, Arrival, SourceSt, DestSt, Total_A, Total_B, Total_C_Both, Total_C_Seat, Fare_A, Fare_B, Fare_C_Both, Fare_C_Seat) " +
                               "VALUES (@TrainId, @Dep, @Arr, @SourceStationId, @DestStationId, @CountA, @CountB, @CountC, @CountC_Seats, @FareA, @FareB, @FareC, @FareC_Seats)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TrainId", TrainId);
                cmd.Parameters.AddWithValue("@Dep", Dep);
                cmd.Parameters.AddWithValue("@Arr", Arr);
                cmd.Parameters.AddWithValue("@SourceStationId", SourceStationId);
                cmd.Parameters.AddWithValue("@DestStationId", DestStationId);
                cmd.Parameters.AddWithValue("@CountA", CountA);
                cmd.Parameters.AddWithValue("@CountB", CountB);
                cmd.Parameters.AddWithValue("@CountC", CountC);
                cmd.Parameters.AddWithValue("@CountC_Seats", CountC_Seats);
                cmd.Parameters.AddWithValue("@FareA", FareA);
                cmd.Parameters.AddWithValue("@FareB", FareB);
                cmd.Parameters.AddWithValue("@FareC", FareC);
                cmd.Parameters.AddWithValue("@FareC_Seats", FareC_Seats);

                cmd.ExecuteNonQuery();
            }
        }

        public int Id { get; set; }
        public string Dep { get; set; }
        public string Arr { get; set; }
        public int TrainId { get; set; }
        public int SourceStationId { get; set; }
        public int DestStationId { get; set; }
        public int CountA { get; set; }
        public int CountB { get; set; }
        public int CountC { get; set; }
        public int CountC_Seats { get; set; }
        public int FareA { get; set; }
        public int FareB { get; set; }
        public int FareC { get; set; }
        public int FareC_Seats { get; set; }
    }
}
