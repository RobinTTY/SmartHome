namespace SmartHome.Database.Models
{
    /// <summary>
    /// The application secrets.
    /// </summary>
    public class AppSecrets
    {
        public DatabaseSecrets? DatabaseSecrets { get; set; }
    }

    /// <summary>
    /// The database secrets.
    /// </summary>
    public class DatabaseSecrets
    {
        /// <summary>
        /// The connection string used to establish a connection to the database.
        /// </summary>
        public string? ConnectionString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? DatabaseName { get; set; }
    }
}
