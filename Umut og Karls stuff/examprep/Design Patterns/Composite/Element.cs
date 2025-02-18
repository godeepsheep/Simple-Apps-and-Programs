namespace Composite;

public interface IComposite
{
    List<IComposite?> Nodes { get; set; }

    public void Operation();
    public void Add(Element element) {}    // default empty method
    public void Remove(Element element) {} // default empty method
}

public class Element : IComposite
{
    private int id = 0;

    public List<IComposite?> Nodes { get; set; } = new List<IComposite?>();

    public Element(int id)
    {
        this.id = id;
    }

    public void Operation()
    {
        Console.WriteLine("Element {0}", id);
        foreach (IComposite? node in Nodes)
        {
            node?.Operation();
        }
    }
}

/*
public void Main(string []args)
{
    Element root = new Element(0);
    root.Nodes.Add(new Element(1));
    root.Nodes.Add(new Element(2));
    root.Nodes.Add(new Element(3));

    root.Nodes.Get(0).Nodes.Get(0).Nodes.Add(new Element(4));
    root.Nodes.Get(1).Nodes.Add(new Element(5));
    root.Nodes.Get(1).Nodes.Add(new Element(6));
        
    root.Operation();
}
*/