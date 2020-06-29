using System;
using JetBrains.Annotations;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.Reflection;
using Rocket.Surgery.Conventions.Scanners;
using Rocket.Surgery.Conventions.Serilog;
using Rocket.Surgery.Conventions.Serilog.Conventions;
using Rocket.Surgery.Hosting.Serilog.Conventions;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

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
        private readonly IConventionScanner _scanner;
        private readonly ILogger _diagnosticSource;
        private readonly RocketSerilogOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogHostingConvention" /> class.
        /// </summary>
        /// <param name="scanner">The scanner.</param>
        /// <param name="diagnosticSource">The diagnostic source.</param>
        /// <param name="options">The options.</param>
        public SerilogHostingConvention(
            IConventionScanner scanner,
            ILogger diagnosticSource,
            RocketSerilogOptions? options = null
        )
        {
            _scanner = scanner;
            _diagnosticSource = diagnosticSource;
            _options = options ?? new RocketSerilogOptions();
        }

        /// <inheritdoc />
        public void Register([NotNull] IHostingConventionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Get<IConventionScanner>().ExceptConvention(typeof(SerilogExtensionsConvention));
            context.Builder.ConfigureServices((context, services) =>
            {
                foreach (var item in services
                    .Where(x => x.ImplementationType?.FullName.StartsWith("Microsoft.Extensions.Logging") == true)
                    .ToArray()
                )
                {
                    services.Remove(item);
                }
            });
            context.Builder.UseSerilog(
                (ctx, loggerConfiguration) =>
                {
                    new SerilogBuilder(
                        _scanner,
                        context.Get<IAssemblyProvider>(),
                        context.Get<IAssemblyCandidateFinder>(),
                        ctx.HostingEnvironment,
                        ctx.Configuration,
                        loggerConfiguration,
                        _diagnosticSource,
                        context.Properties
                    ).Configure();
                },
                _options.PreserveStaticLogger,
                _options.WriteToProviders
            );
        }

        private class LoggingBuilder : ILoggingBuilder
        {
            public LoggingBuilder(IServiceCollection services) => Services = services;

            public IServiceCollection Services { get; }
        }
    }
}