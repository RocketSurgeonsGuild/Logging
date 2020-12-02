using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Rocket.Surgery.Conventions.Serilog
{
    /// <summary>
    /// ISerilogConvention
    /// Implements the <see cref="IConvention" />
    /// </summary>
    /// <seealso cref="IConvention" />
    public interface ISerilogConvention : IConvention
    {
        /// <summary>
        /// Register additional logging providers with the logging builder
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configuration"></param>
        /// <param name="loggerConfiguration"></param>
        void Register([NotNull] IConventionContext context, [NotNull] IConfiguration configuration, [NotNull] LoggerConfiguration loggerConfiguration);
    }
}