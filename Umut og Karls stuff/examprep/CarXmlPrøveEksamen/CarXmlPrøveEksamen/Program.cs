using System;
using System.Collections.Generic;
using System.Xml;

class Car
{
    public string Name { get; set; }
    public int Cylinders { get; set; }
    public string Country { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Cylinders: {Cylinders}, Country: {Country}";
    }
}

class Program
{
    static void Main()
    {
        List<Car> cars = new List<Car>();

        XmlReaderSettings settings = new XmlReaderSettings
        {
            IgnoreWhitespace = true
        };

        using (XmlReader reader = XmlReader.Create("http://www.fkj.dk/cars.xml", settings))
        {
            while (reader.Read())
            {
                if (reader.IsStartElement() && reader.Name == "car")
                {
                    string name = reader["name"];
                    int cylinders = 0;
                    string country = string.Empty;

                    if (reader.IsEmptyElement)
                    {
                        // Håndter tomt <car>-element, hvis nødvendigt
                        continue;
                    }

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "car")
                        {
                            break;
                        }

                        if (reader.IsStartElement())
                        {
                            if (reader.Name == "cylinders")
                            {
                                cylinders = reader.ReadElementContentAsInt();
                            }
                            else if (reader.Name == "country")
                            {
                                country = reader.ReadElementContentAsString();
                            }
                        }
                    }

                    cars.Add(new Car
                    {
                        Name = name,
                        Cylinders = cylinders,
                        Country = country
                    });
                }
            }
        }

        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }

        WriteAllCarsXml(cars);
    }

    static void WriteAllCarsXml(List<Car> cars)
    {
        XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true
        };

        using (XmlWriter writer = XmlWriter.Create("cars_output.xml", settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("cars");

            foreach (var car in cars)
            {
                writer.WriteStartElement("car");
                writer.WriteAttributeString("name", car.Name);
                writer.WriteElementString("cylinders", car.Cylinders.ToString());
                writer.WriteElementString("country", car.Country);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        Console.WriteLine("XML-fil skrevet til cars_output.xml");
    }
}
