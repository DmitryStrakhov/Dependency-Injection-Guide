using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DependencyInjectionGuide;

[TestFixture]
public class ScopedTests {
    [Test]
    public void Test() {
        ServiceCollection services = new();
        services.AddScoped<IService1, DefaultService1>();
        services.AddScoped<IService2, DefaultService2>();
        services.AddScoped<Client>();
        DefaultLogger logger = new();
        
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        using(IServiceScope scope = serviceProvider.CreateScope())
            Do(scope.ServiceProvider, logger);

        logger.GetString().Should().Be("DefaultService1.Do();DefaultService2.Do();");
        return;

        static void Do(IServiceProvider serviceProvider, ILogger logger) =>
            serviceProvider.GetRequiredService<Client>().Do(logger);
    }
}