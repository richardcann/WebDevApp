using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Housing.WebAPI.Models
{
    public class Rejection
    {
        [Key]
        public int ID { get; set; }
        [Required, StringLength(200)]
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Timestamp { get; set; }

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
            return Equals((Rejection)obj);
        }

        public bool Equals(Rejection obj)
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
                Description == obj.Description &&
                Timestamp == obj.Timestamp &&
                PropertyRef == obj.PropertyRef);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Description, Timestamp, PropertyRef, Property);
        }
    }
}
