using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DependencyInjectionGuide;

[TestFixture]
public class SingletonTests {
    [Test]
    public void Test() {
        ServiceCollection services = new();
        services.AddSingleton<IService1, DefaultService1>();
        services.AddSingleton<IService2, DefaultService2>();
        services.AddSingleton<Client>();
        DefaultLogger logger = new();

        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        Client client = serviceProvider.GetRequiredService<Client>();
        client.Do(logger);
        logger.GetString().Should().Be("DefaultService1.Do();DefaultService2.Do();");
    }
}
