using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatIsAWebsite.Models
{
    public partial class Rejection
    {
        [Key]
        public int RejectionId { get; set; }
        public int PropertyId { get; set; }
        public string RejectionDescription { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Property Property { get; set; }
    }
}
