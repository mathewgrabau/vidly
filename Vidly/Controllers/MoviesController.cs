using System.Web.Compilation;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie { Name = "Shrek" };

            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            return Content("Id = " + id);
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

            return Content($"pageIndex={pageIndex}&sortBy={sortBy}");
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content($"{year}/{month}");
        }
    }
}