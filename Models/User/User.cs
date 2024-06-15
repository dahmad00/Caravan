using System;
using System.Data.SqlClient;

namespace RailwaySystem.Models.User
{
    public class User
    {
        public User(string email, string password, string name)
        {
            Email = email;
            Password = password;
            Name = name;
            Type = "Customer";
        }

        public User(string email, string password, string name, string type)
        {
            Email = email;
            Password = password;
            Name = name;
            Type = type;
        }

        public User(string email)
        {
            using (SqlConnection conn = DatabaseUtils.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM [User] WHERE [Email] = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Email = (string)reader["Email"];
                        Password = (string)reader["Password"];
                        Name = (string)reader["name"];
                        Type = (string)reader["Type"];
                    }
                }
            }
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public bool uniqueEmail()
        {
            using (SqlConnection conn = DatabaseUtils.GetConnection())
            {
                conn.Open();

                string query = "SELECT [Email] FROM [User] WHERE [Email] = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Email);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return !reader.HasRows;
                }
            }
        }

        public void updateDatabase()
        {
            using (SqlConnection conn = DatabaseUtils.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO [User](Email, [Password], [name], [Type]) " +
                               "VALUES(@Email, @Password, @Name, @Type)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Type", Type);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
