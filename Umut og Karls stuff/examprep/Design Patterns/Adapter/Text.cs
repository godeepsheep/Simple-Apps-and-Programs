namespace Adapter;

public class Text : Element
{
    private TextBox box;

    public Text(int w, int h) : base(w, h)
    { 
        box = new TextBox(w, h);
    }

    override public void Resize(int w, int h)
    {
        base.Resize(w, h);

        // TextBox does not implement resize(int, int), so we "adapt" to this,
        // by calling the correct functions in the Text.Resize() method.
        box.ResizeW(w);
        box.ResizeH(h);
    }
}