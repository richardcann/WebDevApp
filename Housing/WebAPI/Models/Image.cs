using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Housing.WebAPI.Models
{
    public class Image
    {
        [Key]
        public int ID { get; set; }
        //At what index does this image come in the view?
        [Required]
        public int Position { get; set; }
        //Location of image stored on server
        [Required, StringLength(45)]
        public string Path { get; set; }

        [ForeignKey("Property")]
        public int PropertyRef { get; set; }
        public virtual Property Property { get; set; }
    }
}
