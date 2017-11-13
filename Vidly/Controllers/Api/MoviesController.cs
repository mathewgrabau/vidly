using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    /// <summary>
    /// The API controller used to implement the classes.
    /// </summary>
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/movies
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
        }

        // GET /api/movie/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            // Found it, return the object with 200 status.
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movie
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            // Update generated id to reflect that.
            movieDto.Id = movie.Id;

            return Created(new Uri($"{Request.RequestUri}/{movie.Id}"), movieDto);
        }

        // PUT /api/movies/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var databaseMovie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (databaseMovie == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Source gets mapped to the destination object automatically
            Mapper.Map(movieDto, databaseMovie);

            _context.SaveChanges();
        }

        // DELETE /api/movies/1
        public void DeleteMovie(int id)
        {
            var databaseMovie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (databaseMovie == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Movies.Remove(databaseMovie);
            _context.SaveChanges();
        }
    }
}
