using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatIsAWebsite.Models
{
    public partial class Property
    {
        public Property()
        {
            Rejection = new HashSet<Rejection>();
        }

        [Key]
        public int PropertyId { get; set; }
        public string Username { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string PhotographPath { get; set; }
        public string PropertyDescription { get; set; }
        public string VerificationStatus { get; set; }
        public DateTime? Timestamp { get; set; }

        public virtual AppUser UsernameNavigation { get; set; }
        [ForeignKey("PropertyId")]
        public virtual ICollection<Rejection> Rejection { get; set; }
    }
}
