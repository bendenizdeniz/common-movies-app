using Entity.Concrete;
using Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IMovieService
    {
        Task<MoviesResult> GetMovieBySearch(string searchQuery);

        Task<DashboardMovies> GetDashboardMovies();

        void CreateMovieSearch(DashboardMovies movies);

        public List<DashboardMovieModel> GetSearchedMovies();

        public void DeleteSearchedMovies();

        public void AddFavourite(int movieId, int userId);

        public List<DashboardMovieModel> GetFavourites(int userId);

        public void RemoveFavourite(int movieId, int userId);

        public GenreResult GetGenres();

        public List<DashboardMovieModel> GetCommonMovies(int userLoggedInId, int userFriendId);
    }
}
