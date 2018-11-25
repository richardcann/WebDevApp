using Housing.WebAPI.Models.InternalDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.WebAPI.Models.InternalDTO
{
    public class BPBasicImages : BasicProperty
    {
        public List<BasicImage> Images { get; set; }
    }
}
