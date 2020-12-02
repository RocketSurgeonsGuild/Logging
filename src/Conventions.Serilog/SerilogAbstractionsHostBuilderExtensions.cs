// ReSharper disable once CheckNamespace

using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Rocket.Surgery.Conventions.Serilog;
using Serilog;

namespace Rocket.Surgery.Conventions
{
    /// <summary>
    /// Helper method for working with <see cref="IConventionHostBuilder" />
    /// </summary>
    [PublicAPI]
    public static class SerilogAbstractionsHostBuilderExtensions
    {
        /// <summary>
        /// Configure the serilog delegate to the convention scanner
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="delegate">The delegate.</param>
        /// <returns>IHostBuilder.</returns>
        public static ConventionContextBuilder ConfigureSerilog(
            [NotNull] this ConventionContextBuilder builder,
            [NotNull] SerilogConvention @delegate
        )
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (@delegate == null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return builder.AppendDelegate(@delegate);
        }

        /// <summary>
        /// Configure the serilog delegate to the convention scanner
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="delegate">The delegate.</param>
        /// <returns>IHostBuilder.</returns>
        public static ConventionContextBuilder ConfigureSerilog(
            [NotNull] this ConventionContextBuilder builder,
            [NotNull] Action<IConfiguration, LoggerConfiguration> @delegate
        )
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (@delegate == null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return builder.AppendDelegate(new SerilogConvention((_, configuration, loggerConfiguration) => @delegate(configuration, loggerConfiguration)));
        }

        /// <summary>
        /// Configure the serilog delegate to the convention scanner
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="delegate">The delegate.</param>
        /// <returns>IHostBuilder.</returns>
        public static ConventionContextBuilder ConfigureSerilog(
            [NotNull] this ConventionContextBuilder builder,
            [NotNull] Action<LoggerConfiguration> @delegate
        )
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (@delegate == null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return builder.AppendDelegate(new SerilogConvention((_, _, loggerConfiguration) => @delegate(loggerConfiguration)));
        }
    }
}