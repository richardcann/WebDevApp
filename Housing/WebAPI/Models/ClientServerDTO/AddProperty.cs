using Housing.WebAPI.Models.InternalDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.WebAPI.Models.ClientServerDTO
{
    public class AddProperty
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string PropertyDescription { get; set; }
        public List<string> Images { get; set; }
    }
}
