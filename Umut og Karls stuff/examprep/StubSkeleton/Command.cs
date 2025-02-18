using System.Dynamic;

public class CommandSkeleton
{
    public string Method { get; set; }
    public string Description { get; set; }
    public List<string> Parameters { get; set; } = new();

    public CommandSkeleton() {}

    public CommandSkeleton(string method, string description, params string[] parameters)
    {
        Method = method;
        Description = description;
        
        foreach (string param in parameters)
        {
            Parameters.Add(param);
        }
    }
}

public class CommandStub
{
    public string Method { get; set; }
    public List<string> Arguments { get; set; } = new();

    public CommandStub() {}

    public CommandStub(string method, List<string> arguments)
    {
        Method = method;
        Arguments = arguments;
    }
}