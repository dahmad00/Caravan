using System.Data.SqlClient;

public static class GlobalSettings
{
    public static string ConnectionString { get; } = "Data Source=DESKTOP-6BN7OVV;Initial Catalog=RailwaySystem;Integrated Security=True;";
}

public static class DatabaseUtils
{
    public static SqlConnection GetConnection()
    {
        return new SqlConnection(GlobalSettings.ConnectionString);
    }
}
