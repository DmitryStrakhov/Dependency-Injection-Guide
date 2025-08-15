using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DependencyInjectionGuide;

[TestFixture]
public class TransientTests {
    [Test]
    public void Test() {
        ServiceCollection services = new();
        services.AddTransient<IService1, DefaultService1>();
        services.AddTransient<IService2, DefaultService2>();
        services.AddTransient<Client>();
        DefaultLogger logger = new();

        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        Client client = serviceProvider.GetRequiredService<Client>();
        client.Do(logger);
        logger.GetString().Should().Be("DefaultService1.Do();DefaultService2.Do();");
    }
}