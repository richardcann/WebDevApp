using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Housing.Models
{
    public class Rejection
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }

        [ForeignKey("Property")]
        public int PropertyRef { get; set; }
        public virtual Property Property { get; set; }
        
    }
}
