using System.Text;

namespace DependencyInjectionGuide;

class Client(IService1 service) {
    readonly IService1 service = service;

    public void Do(ILogger logger) {
        service.Do(logger);
    }
}

interface IService1 {
    void Do(ILogger logger);
}

interface IService2 {
    void Do(ILogger logger);
}

interface ILogger {
    void Write(string s);
}


class DefaultService1(IService2 service) : IService1 {
    readonly IService2 service = service;

    public void Do(ILogger logger) {
        logger.Write(nameof(DefaultService1) + ".Do()");
        service.Do(logger);
    }
}

class DefaultService2 : IService2 {
    public void Do(ILogger logger) {
        logger.Write(nameof(DefaultService2) + ".Do()");
    }
}

class DefaultLogger : ILogger {
    readonly StringBuilder stringBuilder = new();

    public void Write(string s) {
        stringBuilder.Append(s);
        stringBuilder.Append(';');
    }

    public string GetString() {
        return stringBuilder.ToString();
    }
}


class Client2(IDisposableObjectFactoryService service) {
    readonly IDisposableObjectFactoryService service = service;

    public void Do(ILogger logger) {
        logger.Write(nameof(Client2) + ".Start");

        using(DisposableObject obj = service.CreateObject()) {
            obj.Do(logger);
        }
        logger.Write(nameof(Client2) + ".End");
    }
}

interface IDisposableObjectFactoryService {
    DisposableObject CreateObject();
}

class DisposableObject : IDisposable {
    public void Do(ILogger logger) {
        logger.Write(nameof(DisposableObject) + ".Do()");
    }
    public void Dispose() {
        // dispose logic
    }
}

class DefaultDisposableObjectFactoryService : IDisposableObjectFactoryService {
    public DisposableObject CreateObject() => new();
}