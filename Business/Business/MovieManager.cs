using Business.Interfaces;
using DAL.Interfaces;
using Entity.Concrete;
using Entity.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Business.Business
{
    public class MovieManager : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        private HttpClient client;

        private string TMDB_API_KEY = "1cf50e6248dc270629e802686245c2c8";

        public MovieManager(IMovieRepository movieRepository, HttpClient client)
        {
            this.movieRepository = movieRepository;
            this.client = client;
        }

        public async Task<DashboardMovies> GetDashboardMovies()
        {
            string movieDBKEY = "1cf50e6248dc270629e802686245c2c8";
            var URL = $"https://api.themoviedb.org/3/movie/popular?api_key={movieDBKEY}&language=en-US";
            DashboardMovies res = new();

            var response = client.GetAsync(URL).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<DashboardMovies>(response.Content.ReadAsStringAsync().Result);

                foreach (var item in res.results)
                {
                    item.Genres = FindGenreNames(item.genre_ids);
                }
                return res;
            }

            return res;
        }

        public List<string> FindGenreNames(List<int> genreIds)
        {
            GenreList genres = GetGenres().Value;
            List<string> genreNames = new();

            foreach (var genre in genres.Genres)
            {
                foreach (var genreId in genreIds)
                {
                    if (genreId == genre.id) genreNames.Add(genre.name);
                }
            }
            return genreNames;
        }

        public async Task<MoviesResult> GetMovieBySearch(string searchQuery)
        {
            DashboardMovies currentMovies = new();
            Guid guid = System.Guid.NewGuid();
            var URL = $"https://api.themoviedb.org/3/search/movie?api_key={TMDB_API_KEY}&query={searchQuery}";
            var response = client.GetAsync(URL).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                currentMovies = JsonConvert.DeserializeObject<DashboardMovies>(response.Content.ReadAsStringAsync().Result);

                return new MoviesResult { Id = guid, Message = "The movie has been exist.", Success = true, Value = currentMovies };
            }
            return new MoviesResult { Id = guid, Message = "The movie has not been exist.", Success = false, Value = currentMovies };
        }

        public void CreateMovieSearch(DashboardMovies movies)
        {
            foreach (var movie in movies.results)
            {
                SearchedMovie addedMovie = new();
                addedMovie.FilmId = movie.id;
                movieRepository.CreateMovieSearch(addedMovie);
            }
        }

        public DashboardMovieModel GetMovieById(int movieId)
        {
            DashboardMovieModel movie = new();
            var URL = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={TMDB_API_KEY}&language=en-US";
            var response = client.GetAsync(URL).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                movie = JsonConvert.DeserializeObject<DashboardMovieModel>(response.Content.ReadAsStringAsync().Result);
                return movie;
            }
            return null;
        }

        public List<DashboardMovieModel> GetSearchedMovies()
        {
            List<DashboardMovieModel> currentSearchedMovies = new();
            List<SearchedMovie> searchedMovies = movieRepository.GetSearchedMovies();

            foreach (var searchedMovie in searchedMovies)
            {
                currentSearchedMovies.Add(GetMovieById(searchedMovie.FilmId));
            }

            return currentSearchedMovies;
        }

        public void AddFavourite(int movieId, int userId)
        {
            movieRepository.AddFavourite(movieId, userId);
        }

        public List<DashboardMovieModel> GetFavourites(int userId)
        {
            List<DashboardMovieModel> currentMovies = new();

            List<FavouriteMovie> favMovies = movieRepository.GetFavourites(userId);

            foreach (var movie in favMovies)
            {
                DashboardMovieModel newMovie = GetMovieById(movie.MovieId);

                currentMovies.Add(newMovie);
            }
            return currentMovies;
        }

        public void DeleteSearchedMovies()
        {
            movieRepository.DeleteSearchedMovies();
        }

        public void RemoveFavourite(int movieId, int userId)
        {
            movieRepository.RemoveFavourite(movieId, userId);
        }

        public GenreResult GetGenres()
        {
            string movieDBKEY = "1cf50e6248dc270629e802686245c2c8";
            GenreList genreList = new();

            Guid guid = System.Guid.NewGuid();

            var URL = $"https://api.themoviedb.org/3/genre/movie/list?api_key={movieDBKEY}&language=en-US";

            var response = client.GetAsync(URL).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                genreList = JsonConvert.DeserializeObject<GenreList>(response.Content.ReadAsStringAsync().Result);

                return new GenreResult { Id = guid, Message = "All genres are availeable.", Success = true, Value = genreList };
            }

            return new GenreResult { Id = guid, Message = "Genres has not been exist.", Success = false, Value = genreList };
        }

        public List<DashboardMovieModel> GetCommonMovies(int userLoggedInId, int userFriendId)
        {
            List<DashboardMovieModel> currentMovies = new();
            List<FavouriteMovie> userLoggedInMovies = movieRepository.GetFavourites(userLoggedInId);
            List<FavouriteMovie> userFriendMovies = movieRepository.GetFavourites(userFriendId);

            foreach (var userLoggedInMovie in userLoggedInMovies)
            {
                foreach (var userFriendMovie in userFriendMovies)
                {
                    if (userFriendMovie.MovieId == userLoggedInMovie.MovieId)
                    {
                        currentMovies.Add(GetMovieById(userFriendMovie.MovieId));
                    }
                }
            }

            return currentMovies;
        }
    }
}
