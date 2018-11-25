using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.WebAPI.Models.InternalDTO
{
    public class BasicImage
    {
        //PropertyID
        public int PropertyRef { get; set; }
        public int Position { get; set; }
        public string Path { get; set; }
    }
}
