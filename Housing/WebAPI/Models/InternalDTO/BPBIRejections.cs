using Housing.WebAPI.Models.ClientServerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.WebAPI.Models.InternalDTO
{
    public class BPBIRejections : BPBasicImages
    {
        public Property.VerificationStatus PropertyStatus { get; set; }
        public List<BasicRejection> Rejections { get; set; }
    }
}
