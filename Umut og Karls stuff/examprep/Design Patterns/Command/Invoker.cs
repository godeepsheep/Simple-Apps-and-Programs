namespace Command;

public delegate void Callback();

public class Invoker
{
    Callback? callback;

    public void Execute()
    {
        callback?.Invoke();
    }
}