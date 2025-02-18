namespace COR;

public class Client
{
    public void Main()
    {
        // Creating handler instances
        IHandler handler1 = new HandlerFoo();
        IHandler handler2 = new HandlerBar();

        // Setting up the chain
        handler1.SetNextHandler(handler2);

        // Making requests
        handler1.HandleRequest(5); // HandlerFoo is handling the request.
        handler1.HandleRequest(15); // HandlerBar is handling the request.
        handler1.HandleRequest(25); // No handler found for the request.
    }
}
