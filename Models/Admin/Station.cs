using System.Data.SqlClient;

namespace RailwaySystem.Models.Admin
{
    public class Station
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }

        public void setData()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT Station_Name, Location FROM Station WHERE ID = @Id;", sqlCon);
                cmd.Parameters.AddWithValue("@Id", Id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Name = reader["Station_Name"] as string;
                        Location = reader["Location"] as string;
                    }
                }
            }
        }
    }
}
