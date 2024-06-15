namespace RailwaySystem.Models.User
{
    public class BookTicketData
    {
        public BookTicketData(int routeID, string email, string ticketClass)
        {
            Route = new Admin.Route(routeID);
            User = new User(email);
            TicketClass = (ticketClass != "C2") ? ticketClass: "C Without Berth";
        }

        public Admin.Route Route { get; set; }
        public User User { get; set; }
        public String TicketClass { get; set; }

    }
}
