using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.WebAPI.Models.ClientServerDTO
{
    public class BasicRejection
    {
        public int PropertyRef { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
