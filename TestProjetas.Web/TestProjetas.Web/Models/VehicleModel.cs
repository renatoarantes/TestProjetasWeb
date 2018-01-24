using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProjetas.Web.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public bool IsZero { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
