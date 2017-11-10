using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Compilation;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie {Name = "Shrek"};
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"},
                new Customer {Name = "Customer 3"},
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        // movies (optional parameters are nullable)
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            if (string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name";
            }

            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content($"{year}/{month}");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var movie = _context.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Movie = new Movie() // Need this to pass the intial validation (the ID field will be checked to see if is there)
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Movie = movie
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                // Generate instance of the view model for the validation.
                var viewModel = new MovieFormViewModel
                {
                    Genres = _context.Genres.ToList(),
                    Movie = movie
                };

                return View("MovieForm", viewModel);
            }

            // Are we creating a new one?
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now; // Set the requirement property here.
                _context.Movies.Add(movie);
            }
            else
            {
                var databaseMovie = _context.Movies.Single(m => m.Id == movie.Id);

                databaseMovie.ReleaseDate = movie.ReleaseDate;
                databaseMovie.GenreId = movie.GenreId;
                databaseMovie.Name = movie.Name;
                databaseMovie.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();

            // Go back for the list
            return RedirectToAction("Index");
        }
    }
}