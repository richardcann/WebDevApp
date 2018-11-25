using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.WebAPI.Models.InternalDTO
{
    public class BPImages : BasicProperty
    {
        public List<string> Images { get; set; }
    }
}
