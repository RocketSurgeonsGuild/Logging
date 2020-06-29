using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using Rocket.Surgery.AspNetCore.Serilog.Conventions;
using Rocket.Surgery.Conventions;

namespace Rocket.Surgery.AspNetCore.Serilog
{
    /// <summary>
    /// RequestLoggingSerilogExtensions.
    /// </summary>
    [PublicAPI]
    public static class RequestLoggingSerilogExtensions
    {
        /// <summary>
        /// Uses the serilog request logging.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>IHostBuilder.</returns>
        public static IHostBuilder UseSerilogRequestLogging(
            [NotNull] this IHostBuilder container
        )
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.GetConventions().UseSerilogRequestLogging();
            return container;
        }

        /// <summary>
        /// Uses the serilog request logging.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>IConventionHostBuilder.</returns>
        public static IConventionHostBuilder UseSerilogRequestLogging(
            [NotNull] this IConventionHostBuilder container
        )
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Scanner.PrependConvention<RequestLoggingConvention>();
            return container;
        }
    }
}