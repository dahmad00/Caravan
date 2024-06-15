using System;
using System.Data.SqlClient;

namespace RailwaySystem.Models.Utility
{
    public class Login
    {
        public Login(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public bool tryLogin()
        {
            using (SqlConnection conn = DatabaseUtils.GetConnection())
            {
                conn.Open();

                string query = "SELECT [Email] FROM [User] WHERE [Email] = @Email AND [Password] = @Password;";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        public string getName(string email)
        {
            using (SqlConnection conn = DatabaseUtils.GetConnection())
            {
                conn.Open();

                string query = "SELECT [Name] FROM [User] WHERE [Email] = @Email;";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["Name"] as string;
                    }
                    else
                    {
                        throw new Exception("User not found.");
                    }
                }
            }
        }
    }
}
