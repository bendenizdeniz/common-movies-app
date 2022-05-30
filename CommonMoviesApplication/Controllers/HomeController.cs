using Business.Interfaces;
using CommonMoviesApplication.Models;
using Entity.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;


namespace CommonMoviesApplication.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService movieService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            this.movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            DashboardMovies dashboardMoviesResp = new();

            dashboardMoviesResp.results = (movieService.GetDashboardMovies()).Result.results;

            GenreList genres = movieService.GetGenres().Value;

            movieService.DeleteSearchedMovies();

            return View(dashboardMoviesResp);
        }

        [HttpPost]
        public bool AddFavourite(int movieId)
        {
            User userLoggedIn = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("Session"));
            movieService.AddFavourite(movieId, userLoggedIn.Id);
            return true;
        }

        [HttpPost]
        public bool GetMovieBySearch(string searchQuery)
        {
            movieService.DeleteSearchedMovies();

            //SearchQueryMovies res = ((movieService.GetMovieBySearch(searchQuery)).Result).Value;

            DashboardMovies movies = ((movieService.GetMovieBySearch(searchQuery)).Result).Value;

            movieService.CreateMovieSearch(movies);

            return true;
        }

        [HttpGet]
        public IActionResult SearchedMovieDetail()
        {
            List<DashboardMovieModel> movies = movieService.GetSearchedMovies();
            return View(model: movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
