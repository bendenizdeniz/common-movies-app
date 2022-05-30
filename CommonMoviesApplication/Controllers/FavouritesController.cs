using Business.Interfaces;
using Entity.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonMoviesApplication.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly IMovieService movieService;

        public FavouritesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public ActionResult GetFavouritesMovies()
        {
            User userLoggedIn = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("Session"));
            List<DashboardMovieModel> favMovie = movieService.GetFavourites(userLoggedIn.Id);
            return View(model: favMovie);
        }

        [HttpDelete]
        public bool RemoveFavourite(int movieId)
        {
            User userLoggedIn = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("Session"));
            movieService.RemoveFavourite(movieId, userLoggedIn.Id);
            return true;
        }

        //("{friendId}")
        [HttpGet]
        public IActionResult GetCommonFavouritesMovies(int friendId)
        {
            User userLoggedIn = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("Session"));
            List<DashboardMovieModel> movies = movieService.GetCommonMovies(userLoggedIn.Id, friendId);
            return View(model: movies);
        }
    }
}
