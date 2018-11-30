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
        [StringLength(70)]
        public string AddressLine2 { get; set; }
        [Required, StringLength(30)]
        public string City { get; set; }
        [Required, StringLength(30)]
        public string County { get; set; }
        [Required, StringLength(10)]
        public string Postcode { get; set; }
        [Required, StringLength(1000, MinimumLength = 50)]
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
            return Equals((Property)obj);
        }

        public bool Equals(Property obj)
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
                         AddressLine1 == obj.AddressLine1 &&
                         AddressLine2 == obj.AddressLine2 &&
                         City == obj.City &&
                         County == obj.County &&
                         Postcode == obj.Postcode &&
                         PropertyDescription == obj.PropertyDescription &&
                         Latitude == obj.Latitude &&
                         Longitude == obj.Longitude &&
                         PropertyStatus == obj.PropertyStatus &&
                         AppUserRef == obj.AppUserRef);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(ID);
            hash.Add(AddressLine1);
            hash.Add(AddressLine2);
            hash.Add(City);
            hash.Add(County);
            hash.Add(Postcode);
            hash.Add(PropertyDescription);
            hash.Add(Latitude);
            hash.Add(Longitude);
            hash.Add(PropertyStatus);
            hash.Add(Timestamp);
            hash.Add(AppUserRef);
            hash.Add(AppUser);
            hash.Add(Images);
            hash.Add(Rejections);
            return hash.ToHashCode();
        }
    }
}

