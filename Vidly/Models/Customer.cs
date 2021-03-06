﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Please enter customer name")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [DisplayName("Date of Birth")]
        //[Min18YearsOldToBeMember]
        public DateTime? BirthDate { get; set; }

        // Navigation property (can add the reference)
        public MembershipType MembershipType { get; set; }

        [Display(Name="Membership Type")]
        public byte MembershipTypeId { get; set; } // Just the foreign key reference (by convention)
    }
}