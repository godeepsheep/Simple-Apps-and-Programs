namespace Adapter;

public class TextBox
{
    private int w;
    private int h;

    public TextBox(int w, int h)
    {
        this.w = w;
        this.h = h;
    }

    public void ResizeW(int w)
    {
        this.w = w;
    }

    public void ResizeH(int h)
    {
        this.h = h;
    }
}