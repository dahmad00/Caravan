namespace RailwaySystem.Models.User
{
    public class Ticket
    {
        public int? Id { get; set; } 
        public string? Tain { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public string? SourceStation { get; set; }
        public string? DestinationStation { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }

    }
}
