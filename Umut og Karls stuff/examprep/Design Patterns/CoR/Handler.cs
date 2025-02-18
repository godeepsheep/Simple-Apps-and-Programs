namespace COR;

// Handler interface
public interface IHandler
{
    void SetNextHandler(IHandler handler);
    void HandleRequest(int request);
}

public class HandlerFoo : IHandler
{
    private IHandler? _nextHandler;

    public void SetNextHandler(IHandler handler)
    {
        _nextHandler = handler;
    }

    public void HandleRequest(int request)
    {
        if (request <= 10)
        {
            Console.WriteLine("HandlerFoo is handling the request.");
            return;
        }

        _nextHandler?.HandleRequest(request);
    }
}

public class HandlerBar : IHandler
{
    private IHandler? _nextHandler;

    public void SetNextHandler(IHandler handler)
    {
        _nextHandler = handler;
    }

    public void HandleRequest(int request)
    {
        if (request > 10 && request <= 20)
        {
            Console.WriteLine("HandlerBar is handling the request.");
            return;
        }
        
        _nextHandler?.HandleRequest(request);
    }
}