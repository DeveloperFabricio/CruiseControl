using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruiseControl.Core.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public string PlateNumber { get; set; }
        public double Mileage { get; set; }
        public CarCategory Category { get; set; }
        public bool IsAvailable { get; set; }
    }
}
