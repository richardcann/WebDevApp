using Housing.Models.ClientServerDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Housing.Models
{

    public enum UserRole
    {
        Officer, Landlord, Student
    }

    public class AppUser
    {
        public AppUser()
        {
            Properties= new HashSet<Property>();
        }

        public AppUser(RegisterUser ru, byte[] passHash, byte[] passSalt)
        {
            Username = ru.Username;
            FirstName = ru.FirstName;
            LastName = ru.LastName;
            Role = ru.Role;
            Email = ru.Email;
            PassHash = passHash;
            PassSalt = passSalt;
            Properties = new HashSet<Property>();
        }

        [Key]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string Email { get; set; }
        public byte[] PassHash { get; set; }
        public byte[] PassSalt { get; set; }
            
        public virtual ICollection<Property> Properties { get; set; }
    }

    public class AppUserDTO
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string Email { get; set; }
        public byte[] PassHash { get; set; }
        public byte[] PassSalt { get; set; }
    }
}
