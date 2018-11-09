using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatIsAWebsite.Models
{
    public partial class AppUser
    {
        public AppUser()
        {
            Property = new HashSet<Property>();
        }

        [Key]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
        public string Email { get; set; }
        public byte[] PassHash { get; set; }
        public byte[] PassSalt { get; set; }

        [ForeignKey("Username")]
        public virtual ICollection<Property> Property { get; set; }
    }
}
