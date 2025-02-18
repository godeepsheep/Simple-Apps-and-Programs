using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarXmlPrøveEksamen
{
    public class Car
    {
        public string Name { get; set; }
        public int Cylinders { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return $"Car Name: {Name}, Cylinders: {Cylinders}, Country: {Country}";
        }
    }
}
