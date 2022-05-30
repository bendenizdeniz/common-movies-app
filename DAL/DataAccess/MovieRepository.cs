using DAL.Interfaces;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class MovieRepository : IMovieRepository
    {
        CommonmoviesapplicationContext context = new();

        public MovieRepository(CommonmoviesapplicationContext context)
        {
            this.context = context;
        }

        public void AddFavourite(int movieId, int userId)
        {
            FavouriteMovie favouriteMovie = new();
            favouriteMovie.MovieId = movieId;
            favouriteMovie.UserId = userId;
            context.FavouriteMovies.Add(favouriteMovie);
            context.SaveChanges();
        }

        public void CreateMovieSearch(SearchedMovie searchedMovie)
        {
            context.SearchedMovies.Add(searchedMovie);
            context.SaveChanges();
        }

        public List<SearchedMovie> GetSearchedMovies()
        {
            return context.SearchedMovies.ToList();
        }

        public List<FavouriteMovie> GetFavourites(int userId)
        {
            return context.FavouriteMovies.Where(x => x.UserId == userId).OrderByDescending(x => x.Id).ToList();
        }

        public void DeleteSearchedMovies()
        {
            context.SearchedMovies.RemoveRange(context.SearchedMovies);
            context.SaveChanges();
        }

        public void RemoveFavourite(int movieId, int userId)
        {
            FavouriteMovie deletedMovie = context.FavouriteMovies.FirstOrDefault(x => x.MovieId == movieId && x.UserId == userId); ;
            context.FavouriteMovies.Remove(deletedMovie);
            context.SaveChanges();
        }

        public List<FavouriteMovie> GetAllFavouriteMovies()
        {
            return context.FavouriteMovies.ToList();
        }
    }
}
