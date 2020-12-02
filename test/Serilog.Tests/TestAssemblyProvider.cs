using System.Collections.Generic;
using System.Reflection;
using Rocket.Surgery.Conventions.Reflection;
using Rocket.Surgery.Conventions.Serilog;

namespace Rocket.Surgery.Extensions.Serilog.Tests
{
    internal class TestAssemblyProvider : IAssemblyProvider
    {
        public IEnumerable<Assembly> GetAssemblies() => new[]
        {
            typeof(SerilogConvention).GetTypeInfo().Assembly,
            typeof(TestAssemblyProvider).GetTypeInfo().Assembly
        };
    }
}