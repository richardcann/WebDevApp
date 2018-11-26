using Housing.WebAPI.Models.ClientServerDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Email { get; set; }
        [Required]
        public byte[] PassHash { get; set; }
        [Required]
        public byte[] PassSalt { get; set; }
            
        public virtual ICollection<Property> Properties { get; set; }
        
    }
}
