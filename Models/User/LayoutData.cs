
namespace RailwaySystem.Models.User
{
    public class LayoutData
    {
        public LayoutData()
        {
            Login = false;
        }

        public string? UserName { get; set; }
        public bool Login { get; set; }
    }
}
