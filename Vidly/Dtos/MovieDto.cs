using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "Release date is required")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Number In Stock")]
        [Required(ErrorMessage = "Number in stock is required")]
        [Range(1, 20, ErrorMessage = "Number in stock must be between 1 and 20")]
        public int NumberInStock { get; set; }

        public GenreDto Genre { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Genre is required")]  // using it here makes it work with the corresponding 
        public byte GenreId { get; set; }
    }
}