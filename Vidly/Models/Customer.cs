using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        // Navigation property (can add the reference)
        public MembershipType MembershipType { get; set; }

        public byte MembershipTypeId { get; set; } // Just the foreign key reference (by convention)
    }
}