namespace XMLCarsPrøveEksamen;

public class Car
{
    public string Name { get; set; }
    public int Cylinders { get; set; }
    public string Country { get; set; }


    public override string ToString()
    {
        return $"Name: {Name}, Cylinders: {Cylinders}, Country: {Country}";
    }
}