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


        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((Image)obj);
        }

        public bool Equals(Image obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return (ID == obj.ID &&
                        Position == obj.Position &&
                        Path == obj.Path &&
                        PropertyRef == obj.PropertyRef);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Position, Path, PropertyRef, Property);
        }
    }
}
