using Housing.WebAPI.Models.ClientServerDTO;
using Housing.WebAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Housing.WebAPI.Models
{

    public enum UserRole
    {
        Officer, Landlord, Student
    }

    public class AppUser
    {
        public AppUser()
        {
            Properties = new HashSet<Property>();
        }

        public AppUser(RegisterUser ru)
        {
            Username = ru.Username;
            FirstName = ru.FirstName;
            LastName = ru.LastName;
            Role = ru.Role;
            Email = ru.Email;
            PassSalt = Crypto.GenerateSalt();
            PassHash = Crypto.GenerateHash(ru.Password, PassSalt);
            Properties = new HashSet<Property>();
        }

        [Key, StringLength(30, MinimumLength = 3)]
        public string Username { get; set; }
        [Required, StringLength(30)]
        public string FirstName { get; set; }
        [Required, StringLength(30)]
        public string LastName { get; set; }
        [Required, EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(80)]
        public string Email { get; set; }
        [Required]
        public byte[] PassHash { get; set; }
        [Required]
        public byte[] PassSalt { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

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
            return Equals((AppUser)obj);
        }

        public bool Equals(AppUser obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return (Username == obj.Username &&
                        FirstName == obj.FirstName &&
                        LastName == obj.LastName &&
                        Role == obj.Role &&
                        Email == obj.Email &&
                        PassHash.SequenceEqual(obj.PassHash) &&
                        PassSalt.SequenceEqual(obj.PassSalt));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Username, FirstName, LastName, Role, Email, PassHash, PassSalt, Properties);
        }
    }
}
