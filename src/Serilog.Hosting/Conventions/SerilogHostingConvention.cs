using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.Serilog;
using Rocket.Surgery.Hosting.Serilog.Conventions;
using Serilog;

[assembly: Convention(typeof(SerilogHostingConvention))]

namespace Rocket.Surgery.Hosting.Serilog.Conventions
{
    /// <summary>
    /// SerilogHostingConvention.
    /// Implements the <see cref="IHostingConvention" />
    /// </summary>
    /// <seealso cref="IHostingConvention" />
    public class SerilogHostingConvention : IHostingConvention
    {
        private readonly RocketSerilogOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogHostingConvention" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public SerilogHostingConvention(RocketSerilogOptions? options = null)
        {
            _options = options ?? new RocketSerilogOptions();
        }

        public void Register(IConventionContext context, IHostBuilder builder)
        {
            builder.ConfigureServices(
                (_, services) =>
                {
                    foreach (var item in services
                       .Where(x => x.ImplementationType?.FullName.StartsWith("Microsoft.Extensions.Logging") == true)
                       .ToArray()
                    )
                    {
                        services.Remove(item);
                    }
                }
            );
            builder.UseSerilog(
                (c, loggerConfiguration) => loggerConfiguration.ApplyConventions(context, c.Configuration),
                _options.PreserveStaticLogger,
                _options.WriteToProviders
            );
        }
    }
}