using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetflexProject.DTO;
using NetflexProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace NetflexProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly NetflexDB _context;


        public MoviesController(NetflexDB context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            List<Movie> mov = _context.Movies.ToList();
            List<Category> cat = _context.Categories.Include(n => n.Movies).ToList();
            List<Subscription> sub = _context.Subscriptions.ToList();
            List<MovieCategorySubscriptionDTO> movDTO = new List<MovieCategorySubscriptionDTO>();
            foreach (var item in mov)
            {
                MovieCategorySubscriptionDTO dto = new MovieCategorySubscriptionDTO()
                {
                    MovieVideo = item.MovieVideo,
                    MovieBackDrop = item.MovieBackDrop,
                    MovieDate = item.MovieDate,
                    MovieDescription = item.MovieDescription,
                    MovieID = item.MovieID,
                    MovieImage = item.MovieImage,
                    MovieName = item.MovieName,
                    MovieVedio = item.MovieVideo,

                };
                foreach (var i in sub)
                {
                    if (item.SubscriptionID == i.SubID)
                    {
                        dto.SubscriptionType = i.Type;
                    }
                }
                foreach (var y in cat)
                {
                    foreach (var x in y.Movies)
                    {
                        if (x.MovieID == item.MovieID)
                            dto.CategoryName = y.Type;
                    }
                }
                movDTO.Add(dto);
            }
            return Ok(movDTO);
        }

        // GET movie by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            Movie mov = _context.Movies.FirstOrDefault(n => n.MovieID == id);
            List<Category> cat = _context.Categories.Include(n => n.Movies).ToList();
            List<Subscription> sub = _context.Subscriptions.ToList();
            List<MovieCategorySubscriptionDTO> movDTO = new List<MovieCategorySubscriptionDTO>();


            MovieCategorySubscriptionDTO dto = new MovieCategorySubscriptionDTO()
            {
                MovieID = mov.MovieID,
                MovieName = mov.MovieName,
                MovieDescription = mov.MovieDescription,
                Rate = mov.Rate,
                MovieBackDrop = mov.MovieBackDrop,
                MovieDate = mov.MovieDate,
                MovieImage = mov.MovieImage,
                MovieVedio = mov.MovieVideo
            };
            foreach (var i in sub)
            {
                if (mov.SubscriptionID == i.SubID)
                {
                    dto.SubscriptionType = i.Type;
                }
            }
            foreach (var y in cat)
            {
                foreach (var x in y.Movies)
                {
                    if (x.MovieID == mov.MovieID)
                        dto.CategoryName = y.Type;
                }
            }
            movDTO.Add(dto);

            return Ok(movDTO);

        }

        //Get movies by Name

        [HttpGet("byName")]
        public async Task<ActionResult> searchMovieAndNovel([Required, MinLength(2)] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("not valid input");
            //Novel List 
            var novel = _context.Novels.Where(s => s.NovelName!.Contains(name)).Select(a => new NovelCategorySubscriptionDTO
            {
                NovelID = a.NovelID,
                NovelName = a.NovelName,
                NovelImage = a.NovelImage,
            }).ToList();
            //Movie List 
            var movie = _context.Movies.Where(s => s.MovieName!.Contains(name)).Select(a => new MovieCategorySubscriptionDTO
            {
                MovieID = a.MovieID,
                MovieName = a.MovieName,
                MovieImage = a.MovieImage,
            }).ToList();
            //Merge Two List
            List<object> objectList = novel.Cast<object>()
            .Concat(movie)
            .ToList();

            return Ok(objectList);
        }




        // Get Movies by GenreID
        [HttpGet("genre")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByGenre(int genreId)
        {
            var movies = _context.Movies.Where(movie => movie.Categories.Any(category => category.CategoryID == genreId)).Take(15);
            return Ok(movies);
        }



        //Add Movie
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Movie>>> AddMovie(MovieCategorySubscriptionAddDTO newMovie)
        {

            var Movie = new Movie()
            {
                MovieName = newMovie.MovieName,
                MovieDescription = newMovie.MovieDescription,
                MovieDate = newMovie.MovieDate,
                MovieImage = newMovie.MovieImage,
                Rate = newMovie.Rate,
                MovieBackDrop = newMovie.MovieBackdrop,
                MovieVideo = newMovie.MovieVideo,
                SubscriptionID = newMovie.SubscriptionId,
                Categories = new List<Category>()
            };
            var categoryIds = newMovie.CategoriesId;

            foreach (var item in categoryIds)
            {
                var category = await _context.Categories.FindAsync(item);
                if (category == null)
                {
                    return BadRequest($"Category with id {item} not found.");
                }

                Movie.Categories.Add(category);
            }

            _context.Movies.Add(Movie);
            _context.SaveChanges();
            return Ok(newMovie);

        }

        //Get most rated
        [HttpGet("highRated")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMostRated()
        {
            List<Movie> movies = _context.Movies.OrderByDescending(e => e.Rate).Take(10).ToList();
            return Ok(movies);
        }

        [HttpGet("ModernMovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetModernMovies()
        {
            List<Movie> movies = _context.Movies.OrderByDescending(e => e.MovieDate).Take(15).ToList();
            return Ok(movies);
        }

    }
}
