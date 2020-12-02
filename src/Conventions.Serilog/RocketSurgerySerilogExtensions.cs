using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.Logging;
using Rocket.Surgery.Conventions.Reflection;
using Rocket.Surgery.Conventions.Serilog;
using Serilog;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// Extension method to apply logging conventions
    /// </summary>
    public static class RocketSurgerySerilogExtensions
    {
        /// <summary>
        /// Apply serilog conventions
        /// </summary>
        /// <param name="loggerConfiguration"></param>
        /// <param name="conventionContext"></param>
        /// <returns></returns>
        public static LoggerConfiguration ApplyConventions(this LoggerConfiguration loggerConfiguration, IConventionContext conventionContext, IConfiguration? configuration = null)
        {
            configuration = configuration ?? conventionContext.Get<IConfiguration>() ?? throw new ArgumentException("Configuration was not found in context", nameof(conventionContext));
            var options = conventionContext.GetOrAdd(() => new RocketSerilogOptions());

            foreach (var item in conventionContext.Conventions.Get<ISerilogConvention, SerilogConvention>())
            {
                if (item is ISerilogConvention convention)
                {
                    convention.Register(conventionContext, configuration, loggerConfiguration);
                }
                else if (item is SerilogConvention @delegate)
                {
                    @delegate(conventionContext, configuration, loggerConfiguration);
                }
            }

            return loggerConfiguration;
        }
    }
}