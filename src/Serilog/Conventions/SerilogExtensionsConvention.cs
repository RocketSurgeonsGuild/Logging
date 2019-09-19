using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rocket.Surgery.Conventions.Scanners;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System.Diagnostics;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Extensions.Logging;
using Rocket.Surgery.Extensions.Serilog.Conventions;
using Serilog.Core;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Rocket.Surgery.Extensions.DependencyInjection;

[assembly: Convention(typeof(SerilogExtensionsConvention))]

namespace Rocket.Surgery.Extensions.Serilog.Conventions
{
    /// <summary>
    ///  SerilogExtensionsConvention.
    /// Implements the <see cref="ILoggingConvention" />
    /// </summary>
    /// <seealso cref="ILoggingConvention" />
    public class SerilogExtensionsConvention : ILoggingConvention, IServiceConvention
    {
        private readonly IConventionScanner _scanner;
        private readonly ILogger _diagnosticSource;
        private readonly RocketSerilogOptions _serilogOptions;
        private readonly RocketLoggingOptions _loggingOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogExtensionsConvention"/> class.
        /// </summary>
        /// <param name="scanner">The scanner.</param>
        /// <param name="diagnosticSource">The diagnostic source.</param>
        /// <param name="serilogOptions">The serilog options.</param>
        /// <param name="loggingOptions">The logging options.</param>
        public SerilogExtensionsConvention(
            IConventionScanner scanner,
            ILogger diagnosticSource,
            RocketSerilogOptions? serilogOptions = null,
            RocketLoggingOptions? loggingOptions = null)
        {
            _scanner = scanner;
            _diagnosticSource = diagnosticSource;
            _serilogOptions = serilogOptions ?? new RocketSerilogOptions();
            _loggingOptions = loggingOptions ?? new RocketLoggingOptions();
        }

        /// <summary>
        /// Registers the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Register(IServiceConventionContext context)
        {
            context.Services.AddSingleton<ILoggerFactory>(_ =>
            {
                var providerCollection = _.GetRequiredService<LoggerProviderCollection>();
                var factory = new SerilogLoggerFactory(_.GetRequiredService<global::Serilog.ILogger>(), true, providerCollection);

                foreach (var provider in _.GetServices<ILoggerProvider>())
                {
                    factory.AddProvider(provider);
                }

                return factory;
            });
            context.Services.AddHostedService<SerilogFinalizerHostedService>();
        }

        /// <summary>
        /// Registers the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Register(ILoggingConventionContext context)
        {
            var loggerProviderCollection = context.Get<LoggerProviderCollection>() ?? new LoggerProviderCollection();
            context.Services.AddSingleton(loggerProviderCollection);

            var loggingLevelSwitch = context.Get<LoggingLevelSwitch>() ?? new LoggingLevelSwitch();

            var loggerConfiguration = context.Get<LoggerConfiguration>() ?? new LoggerConfiguration();
            loggerConfiguration.WriteTo.Providers(loggerProviderCollection);

            var serilogBuilder = new SerilogBuilder(
                _scanner,
                context.AssemblyProvider,
                context.AssemblyCandidateFinder,
                context.Environment,
                context.Configuration,
                context,
                loggingLevelSwitch,
                loggerConfiguration,
                _diagnosticSource,
                context.Properties
            );

            var logger = serilogBuilder.Build();
            Log.Logger = logger;

            context.Services.AddSingleton(logger);
        }
    }
}
