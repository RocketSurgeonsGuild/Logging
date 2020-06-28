using JetBrains.Annotations;

namespace Rocket.Surgery.Conventions.Serilog
{
    /// <summary>
    /// Implements the <see cref="IConvention{TContext}" />
    /// </summary>
    /// <seealso cref="IConvention{ISerilogConventionContext}" />
    [PublicAPI] 
    public interface ISerilogConvention : IConvention<ISerilogConventionContext> { }
}