namespace Mediator;

public abstract class Colleague
{
    protected IMediator? _mediator;

    public void SetMediator(IMediator mediator)
    {
        _mediator = mediator;
    }

    public abstract void SendMessage(string message);
    public abstract void ReceiveMessage(string message);
}

public class ConcreteColleague : Colleague
{
    public override void SendMessage(string message)
    {
        _mediator?.SendMessage(message, this);
    }

    public override void ReceiveMessage(string message)
    {
        Console.WriteLine("Received: " + message);
    }
}