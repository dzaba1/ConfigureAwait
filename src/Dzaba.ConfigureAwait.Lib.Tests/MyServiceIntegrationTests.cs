using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using FluentAssertions;

namespace Dzaba.ConfigureAwait.Lib.Tests;

[TestFixture]
public class MyServiceIntegrationTests
{
    private ServiceProvider container;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var services = new ServiceCollection();
        services.RegisterConfigureAwaitLib();

        var logger = new LoggerConfiguration()
            .Enrich.WithThreadId()
            .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{ThreadId}] [{SourceContext}] {Level:u3} - {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        services.AddLogging(l => l.AddSerilog(logger, true));

        container = services.BuildServiceProvider();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        container?.Dispose();
    }

    [Test]
    public async Task SomeVeryImportantOperationAsync_WithoutConfigureAwait()
    {
        var myService = container.GetRequiredService<IMyService>();
        Console.WriteLine($"Thread ID before call: {Environment.CurrentManagedThreadId}");
        var result = await myService.SomeVeryImportantOperationAsync();
        Console.WriteLine($"Thread ID after call: {Environment.CurrentManagedThreadId}");
        result.Should().NotBeNull();
    }

    [Test]
    public async Task SomeVeryImportantOperationAsync_WithConfigureAwait()
    {
        var myService = container.GetRequiredService<IMyService>();
        Console.WriteLine($"Thread ID before call: {Environment.CurrentManagedThreadId}");
        var result = await myService.SomeVeryImportantOperationAsync().ConfigureAwait(false);
        Console.WriteLine($"Thread ID after call: {Environment.CurrentManagedThreadId}");
        result.Should().NotBeNull();
    }

    [Test]
    public async Task SomeVeryImportantOperationWithConfigureAwaitAsync_WithoutConfigureAwait()
    {
        var myService = container.GetRequiredService<IMyService>();
        Console.WriteLine($"Thread ID before call: {Environment.CurrentManagedThreadId}");
        var result = await myService.SomeVeryImportantOperationWithConfigureAwaitAsync();
        Console.WriteLine($"Thread ID after call: {Environment.CurrentManagedThreadId}");
        result.Should().NotBeNull();
    }

    [Test]
    public async Task SomeVeryImportantOperationWithConfigureAwaitAsync_WithConfigureAwait()
    {
        var myService = container.GetRequiredService<IMyService>();
        Console.WriteLine($"Thread ID before call: {Environment.CurrentManagedThreadId}");
        var result = await myService.SomeVeryImportantOperationWithConfigureAwaitAsync().ConfigureAwait(false);
        Console.WriteLine($"Thread ID after call: {Environment.CurrentManagedThreadId}");
        result.Should().NotBeNull();
    }

    [Test]
    public void SomeVeryImportantOperationWithConfigureAwaitAsync_WithResult()
    {
        var myService = container.GetRequiredService<IMyService>();
        Console.WriteLine($"Thread ID before call: {Environment.CurrentManagedThreadId}");
        var result = myService.SomeVeryImportantOperationWithConfigureAwaitAsync().Result;
        Console.WriteLine($"Thread ID after call: {Environment.CurrentManagedThreadId}");
        result.Should().NotBeNull();
    }

    [Test]
    public void SomeVeryImportantOperationAsync_WithResult()
    {
        var myService = container.GetRequiredService<IMyService>();
        Console.WriteLine($"Thread ID before call: {Environment.CurrentManagedThreadId}");
        var result = myService.SomeVeryImportantOperationAsync().Result;
        Console.WriteLine($"Thread ID after call: {Environment.CurrentManagedThreadId}");
        result.Should().NotBeNull();
    }
}