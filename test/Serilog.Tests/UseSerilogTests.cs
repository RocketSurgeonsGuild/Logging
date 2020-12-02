using System;
using System.Collections.Generic;
using System.Diagnostics;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.DependencyInjection;
using Rocket.Surgery.Conventions.Logging;
using Rocket.Surgery.Conventions.Reflection;
using Rocket.Surgery.Extensions.Testing;
using Serilog;
using Serilog.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Rocket.Surgery.Extensions.Serilog.Tests
{
    public class UseSerilogTests : AutoFakeTest
    {
        public UseSerilogTests(ITestOutputHelper outputHelper) : base(outputHelper, LogLevel.Trace) { }

        // private class HostBuilder : ConventionHostBuilder<HostBuilder>
        // {
        //     public HostBuilder(
        //         IConventionScanner scanner,
        //         IAssemblyCandidateFinder assemblyCandidateFinder,
        //         IAssemblyProvider assemblyProvider,
        //         DiagnosticSource diagnosticSource,
        //         IServiceProviderDictionary serviceProperties
        //     ) : base(scanner, assemblyCandidateFinder, assemblyProvider, diagnosticSource, serviceProperties) { }
        // }
        //
        //
        // [Fact]
        // public void AddsAsAConvention()
        // {
        //     var builder = new ConventionContextBuilder(new Dictionary<object, object?>())
        //        .UseAssemblies(new [] { typeof(ServiceConvention).Assembly, typeof(SerilogHostBuilderExtensions).Assembly });
        //     var context = ConventionContext.From(builder);
        //
        //     var scanner = AutoFake.Resolve<SimpleConventionScanner>();
        //     AutoFake.Provide<IConventionScanner>(scanner);
        //     var services = new ServiceCollection();
        //     AutoFake.Provide<IServiceCollection>(services);
        //
        //     var builder = AutoFake.Resolve<HostBuilder>();
        //     var sb = AutoFake.Resolve<ServicesBuilder>();
        //     sb.Services.AddLogging(x => x.AddConsole().AddDebug());
        //
        //     var sp = sb.Build();
        //
        //     sp.GetService<ILoggerFactory>().Should().NotBeNull().And.BeOfType<SerilogLoggerFactory>();
        // }
        //
        // [Fact]
        // public void AddsLogging()
        // {
        //     var properties = new ServiceProviderDictionary();
        //     AutoFake.Provide<IServiceProviderDictionary>(properties);
        //     AutoFake.Provide<IDictionary<object, object?>>(properties);
        //     AutoFake.Provide<IServiceProvider>(properties);
        //     var configuration = new ConfigurationBuilder().Build();
        //     AutoFake.Provide<IConfiguration>(configuration);
        //
        //     properties[typeof(ILogger)] = Logger;
        //     var scanner = AutoFake.Resolve<SimpleConventionScanner>();
        //     AutoFake.Provide<IConventionScanner>(scanner);
        //     var services = new ServiceCollection();
        //     AutoFake.Provide<IServiceCollection>(services);
        //
        //     var builder = AutoFake.Resolve<HostBuilder>();
        //     var sb = AutoFake.Resolve<ServicesBuilder>();
        //     sb.Services.AddLogging(x => x.AddConsole().AddDebug());
        //
        //     static LogLevel? func(ILoggingConventionContext x) => LogLevel.Error;
        //
        //     builder.UseLogging(new RocketLoggingOptions { GetLogLevel = func }).UseSerilog();
        //
        //     var sp = sb.Build();
        //
        //     sp.GetService<ILoggerFactory>().Should().NotBeNull().And.BeOfType<SerilogLoggerFactory>();
        // }
    }
}