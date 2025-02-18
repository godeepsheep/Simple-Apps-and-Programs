using System.Xml;
using XMLCarsPrøveEksamen;

class Program
{
    static void Main(string[] args)
    {
        List<Car> cars = [];

        var doc = new XmlDocument();
        try
        {
            doc.Load("http://www.fkj.dk/cars.xml");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl ved indlæsning af XML: " + ex.Message);
            return;
        }

        var carNodes = doc.GetElementsByTagName("car");
        
        if (carNodes.Count == 0)
        {
            Console.WriteLine("Ingen bilnoder fundet i XML.");
        }

        foreach (XmlNode carNode in carNodes)
        {
            var car = new Car()
            {
                Name = carNode.Attributes["name"].Value,
                Cylinders = int.Parse(carNode["cylinders"].InnerText),
                Country = carNode["country"].InnerText
            };
        }

        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }

        WriteCarsToXml(cars, "output_cars.xml");
    }

    static void WriteCarsToXml(List<Car> cars, string filePath)
    {
        using (var writer = XmlWriter.Create(filePath))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("cars");

            foreach (var car in cars)
            {
                writer.WriteStartElement("car");
                writer.WriteAttributeString("name", car.Name); // Skriv name som en attribut
                writer.WriteElementString("cylinders", car.Cylinders.ToString());
                writer.WriteElementString("country", car.Country);
                writer.WriteEndElement(); // car
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }
}