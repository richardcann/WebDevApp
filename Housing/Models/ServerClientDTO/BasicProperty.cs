using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.Models.ServerClientDTO
{
    public class BasicProperty
    {
        public int ID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string PropertyDescription { get; set; }
        public DateTime Timestamp { get; set; }
        public BasicAppUser Landlord { get; set; }
        public List<BasicImage> Images { get; set; }
    }
}
