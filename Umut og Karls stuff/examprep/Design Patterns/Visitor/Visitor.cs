namespace Visitor;

public interface IVisitor
{
    void Visit(ElementA element);
    void Visit(ElementB element);
}

public class Visitor : IVisitor
{
    public void Visit(ElementA element)
    {
        element.OperationA();
    }

    public void Visit(ElementB element)
    {
        element.OperationB();
    }
}