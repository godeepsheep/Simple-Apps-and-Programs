namespace Mediator;

public interface IMediator
{
    void SendMessage(string message, Colleague colleague);
}

public class Mediator : IMediator
{
    private Dictionary<string, Colleague> _colleagues = new Dictionary<string, Colleague>();

    public void RegisterColleague(string name, Colleague colleague)
    {
        _colleagues[name] = colleague;
        colleague.SetMediator(this);
    }

    public void SendMessage(string message, Colleague colleague)
    {
        foreach (var col in _colleagues.Values)
        {
            if (col != colleague) // Avoid sending the message to itself
            {
                col.ReceiveMessage(message);
            }
        }
    }
}