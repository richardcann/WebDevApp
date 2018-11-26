using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Housing.WebAPI.Models
{
    public class Property
    {

        public enum VerificationStatus
        {
            Approved, Pending, Rejected
        }

        public Property()
        {
            Rejections = new HashSet<Rejection>();
            Images = new HashSet<Image>();
        }
        
        [Key]
        public int ID { get; set; }
        [Required, StringLength(70)]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required, StringLength(30)]
        public string City { get; set; }
        [Required, StringLength(30)]
        public string County { get; set; }
        [Required, StringLength(10)]
        public string Postcode { get; set; }
        [Required, StringLength(1000)]
        public string PropertyDescription { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required, EnumDataType(typeof(VerificationStatus))]
        public VerificationStatus PropertyStatus { get; set; }
        //When the property was added, enables sort by reverse of when properties added
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [ForeignKey("AppUser")]
        public string AppUserRef { get; set; }
        public virtual AppUser AppUser { get; set; }

        //Enable a property to have many pictures of it
        public virtual ICollection<Image> Images { get; set; }
        //Defining like this to maintain a history of rejections for a property
        public virtual ICollection<Rejection> Rejections { get; set; }
    }
}

