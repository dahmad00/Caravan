using System.Data.SqlClient;

namespace RailwaySystem.Models.User
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public int RouteId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

        public void deleteBooking()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Ticket WHERE ID = @Id;", sqlCon);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void makeBooking()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();
                string query = "INSERT INTO Ticket([User], [Route], [Type], [Status]) " +
                               "VALUES(@UserEmail, @RouteId, @Type, @Status)";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@UserEmail", UserEmail);
                cmd.Parameters.AddWithValue("@RouteId", RouteId);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Status", "Booked");
                cmd.ExecuteNonQuery();
            }
        }

        public int getBookingId()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();
                string query = "INSERT INTO Ticket([User], [Route], [Type], [Status]) " +
                               "VALUES(@UserEmail, @RouteId, @Type, @Status); " +
                               "SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@UserEmail", UserEmail);
                cmd.Parameters.AddWithValue("@RouteId", RouteId);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Status", "Booked");

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
