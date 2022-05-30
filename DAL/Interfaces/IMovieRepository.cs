using Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IMovieRepository
    {
        void CreateMovieSearch(SearchedMovie searchedMovies);

        public List<SearchedMovie> GetSearchedMovies();

        public void DeleteSearchedMovies();

        public void AddFavourite(int movieId, int userId);

        public List<FavouriteMovie> GetFavourites(int userId);

        public void RemoveFavourite(int movieId, int userId);

        public List<FavouriteMovie> GetAllFavouriteMovies();
    }
}
