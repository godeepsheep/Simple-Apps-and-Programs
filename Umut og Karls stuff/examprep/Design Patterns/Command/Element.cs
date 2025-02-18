namespace Command;

// Element -> Invoker -> Callback
public class Element
{
    private Invoker invoker;

    public Element(Invoker invoker)
    {
        this.invoker = invoker;
    }

    public void Update()
    {
        Thread thread = new Thread(() => {
            for (int counter = 0; counter < 10; counter++)
            {
                // On every 3rd call, execute the invoker
                if (counter % 3 == 0)
                {
                    invoker.Execute();
                }
            }
        });
        thread.Start();
        thread.Join();
    }
}