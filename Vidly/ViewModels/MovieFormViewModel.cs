using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie Movie { get; set; }


        /// <summary>
        /// The genres that can be selected/used in the form.
        /// </summary>
        public IEnumerable<Genre> Genres { get; set; }
    }
}