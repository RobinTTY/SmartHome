using Autofac;

namespace SmartHome.Database.Utility
{
    /// <summary>
    /// Dependency container managing dependency resolution.
    /// </summary>
    public class ApplicationModule : Module
    {
        /// <summary>
        /// Adds registrations to the dependency container.
        /// </summary>
        /// <param name="builder">The passed down <see cref="ContainerBuilder"/>.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationManager>().As<IConfigurationManager>().SingleInstance();
            builder.RegisterType<DatabaseService>().As<IDatabaseService>().SingleInstance();
        }
    }
}
