using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Rocket.Surgery.Conventions.Serilog
{
    /// <summary>
    /// Register additional logging providers with the logging builder
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="configuration"></param>
    /// <param name="loggerConfiguration"></param>
    public delegate void SerilogConvention([NotNull] IConventionContext context, [NotNull] IConfiguration configuration, [NotNull] LoggerConfiguration loggerConfiguration);
}