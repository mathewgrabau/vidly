using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class NewRentalsDto
    {
        /// <summary>
        /// Customer that is doing the rental.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The list of the movies that are to be rented.
        /// </summary>
        public List<int> MovieIds { get; set; }
    }
}