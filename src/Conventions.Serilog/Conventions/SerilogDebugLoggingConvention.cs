using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.Serilog.Conventions;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

[assembly: Convention(typeof(SerilogDebugLoggingConvention))]

namespace Rocket.Surgery.Conventions.Serilog.Conventions
{
    /// <summary>
    /// SerilogDebugLoggingConvention.
    /// Implements the <see cref="ISerilogConvention" />
    /// </summary>
    /// <seealso cref="ISerilogConvention" />
    [LiveConvention]
    public sealed class SerilogDebugLoggingConvention : ISerilogConvention
    {
        private readonly RocketSerilogOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogDebugLoggingConvention" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public SerilogDebugLoggingConvention(RocketSerilogOptions? options = null) => _options = options ?? new RocketSerilogOptions();

        /// <inheritdoc />
        public void Register(IConventionContext context, IConfiguration configuration, LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration.WriteTo.Async(
                c => c.Debug(
                    LogEventLevel.Verbose,
                    _options.DebugMessageTemplate
                )
            );
        }
    }
}