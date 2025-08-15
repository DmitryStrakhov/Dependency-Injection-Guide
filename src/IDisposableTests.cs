using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DependencyInjectionGuide;

[TestFixture]
public class DisposableTests {
    [Test]
    public void Test() {
        ServiceCollection services = new();
        services.AddSingleton<IDisposableObjectFactoryService, DefaultDisposableObjectFactoryService>();
        services.AddSingleton<Client2>();
        DefaultLogger logger = new();

        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        Client2 client = serviceProvider.GetRequiredService<Client2>();
        client.Do(logger);
        logger.GetString().Should().Be("Client2.Start;DisposableObject.Do();Client2.End;");
    }
}