using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Razor;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name="Release Date")]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Display(Name="Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name="Number In Stock")]
        [Required]
        public int NumberInStock { get; set; }

        public Genre Genre { get; set; }

        [Display(Name="Genre")]
        [Required]  // using it here makes it work with the corresponding 
        public byte GenreId { get; set; }
    }
}