using SmartHome.Database.Models;

namespace SmartHome.Database.Utility;

public interface IConfigurationManager
{
    /// <summary>
    /// The managed application secrets.
    /// </summary>
    AppSecrets AppSecrets { get; }
}