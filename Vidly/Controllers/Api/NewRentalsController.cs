using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
                ;
            }

            base.Dispose(disposing);
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalsDto newRental)
        {
            // No movies requested?
            if (newRental.MovieIds.Count == 0)
            {
                return BadRequest("No movies specified");
            }

            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);

            if (customer == null)
            {
                return BadRequest("Invalid customer ID");
            }

            var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();

            if (movies.Count != newRental.MovieIds.Count)
            {
                return BadRequest("One or more MovieIds is invalid");
            }

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable > 0)
                {
                    // Update the movie now
                    movie.NumberAvailable--;

                    var rental = _context.Rentals.Add(new Rental
                    {
                        Customer = customer,
                        Movie = movie,
                        DateRented = DateTime.Now
                    });

                    _context.Rentals.Add(rental);
                }
                else
                {
                    return BadRequest("No copies of movie " + movie.Id + " are available");
                }
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}