using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using RailwaySystem.Models.Admin;

namespace RailwaySystem.Models.User
{
    public class MyTicketsData
    {
        public LinkedList<Ticket> Tickets { get; set; }
        public string? Email { get; set; }

        public MyTicketsData(string email)
        {
            Tickets = new LinkedList<Ticket>();
            Email = email;
            getTickets();
        }

        public void getTickets()
        {
            using (SqlConnection sqlCon = DatabaseUtils.GetConnection())
            {
                sqlCon.Open();

                string query = "SELECT T.ID, T.[User], T.[Type] AS TicketType, Departure, Arrival, SourceSt, DestSt, Tr.[Name] AS TrainName, T.Status AS TicketStatus " +
                               "FROM [Ticket] T " +
                               "INNER JOIN [Route] R " +
                               "ON T.Route = R.ID " +
                               "INNER JOIN Train Tr " +
                               "ON R.TrainID = Tr.ID " +
                               "WHERE [User] = @Email;";

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@Email", Email);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Ticket ticket = new Ticket
                            {
                                Id = (int)reader["ID"],
                                Tain = reader["TrainName"].ToString(),
                                Departure = (DateTime)reader["Departure"],
                                Arrival = (DateTime)reader["Arrival"],
                                Type = reader["TicketType"].ToString(),
                                Status = reader["TicketStatus"].ToString()
                            };

                            Station sourceStation = new Station { Id = (int)reader["SourceSt"] };
                            Station destStation = new Station { Id = (int)reader["DestSt"] };
                            sourceStation.setData();
                            destStation.setData();

                            ticket.SourceStation = sourceStation.Name;
                            ticket.DestinationStation = destStation.Name;

                            Tickets.AddLast(ticket);
                        }
                    }
                }
            }
        }
    }
}
