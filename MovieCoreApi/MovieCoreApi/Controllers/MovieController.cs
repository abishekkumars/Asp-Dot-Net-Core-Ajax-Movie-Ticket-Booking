using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCoreApi.Models;

namespace MovieCoreApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieDbContext moviedbcontext;
        public MovieController(MovieDbContext movieDbContext)
        {
            moviedbcontext = movieDbContext;
        }

        [HttpGet]
        public IEnumerable<Movie> GetMovies()
        {
            return moviedbcontext.movie.ToList();
        }
        [HttpGet("GetMovieById")]
        public Movie GetMovieById(int Id)
        {
            return moviedbcontext.movie.Find(Id);
        }
        [HttpPost("InsertMovie")]
        public IActionResult InsertMovie([FromBody] Movie movie)
        {
            if (movie.Id.ToString() != "")
            {               
                moviedbcontext.movie.Add(movie);
                moviedbcontext.SaveChanges();
                return Ok("Movie details saved successfully");
            }
            else
                return BadRequest(); 
        }

        [HttpPut("UpdateMovie")]
        public IActionResult UpdateMovie([FromBody] Movie movie)
        {
            if (movie.Id.ToString() != "")
            {
                moviedbcontext.Entry(movie).State = EntityState.Modified;
                moviedbcontext.SaveChanges();
                return Ok("Movie Details Updated successfully");
            }
            else
                return BadRequest();
        }      
        [HttpDelete("DeleteMovie")]
        public IActionResult DeleteMovie(int Id)
        {
            var result = moviedbcontext.movie.Find(Id);
            moviedbcontext.movie.Remove(result);
            moviedbcontext.SaveChanges();
            return Ok("Movie details deleted successfully");
        }
    }
}
