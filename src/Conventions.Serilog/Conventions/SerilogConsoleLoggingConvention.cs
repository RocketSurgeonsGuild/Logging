using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.Serilog.Conventions;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

[assembly: Convention(typeof(SerilogConsoleLoggingConvention))]

namespace Rocket.Surgery.Conventions.Serilog.Conventions
{
    /// <summary>
    /// SerilogConsoleLoggingConvention.
    /// Implements the <see cref="ISerilogConvention" />
    /// </summary>
    /// <seealso cref="ISerilogConvention" />
    [LiveConvention]
    public sealed class SerilogConsoleLoggingConvention : ISerilogConvention
    {
        private readonly RocketSerilogOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogConsoleLoggingConvention" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public SerilogConsoleLoggingConvention(RocketSerilogOptions? options = null) => _options = options ?? new RocketSerilogOptions();

        public void Register(IConventionContext context, IConfiguration configuration, LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration.WriteTo.Async(
                c => c.Console(
                    LogEventLevel.Verbose,
                    _options.ConsoleMessageTemplate,
                    theme: AnsiConsoleTheme.Literate
                )
            );
        }
    }
}