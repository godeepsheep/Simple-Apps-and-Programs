namespace Adapter;

public class Element
{
    private int w;
    private int h;

    public Element(int w, int h)
    {
        this.w = w;
        this.h = h;
    }

    public virtual void Resize(int w, int h)
    {
        this.w = w;
        this.h = h;
    }
}