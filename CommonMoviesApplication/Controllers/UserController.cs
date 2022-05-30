using Business.Interfaces;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Entity.Entity;
using System;
using System.Threading.Tasks;
using Tcdogrulama;

namespace CommonMoviesApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IUserService userService;

        public UserController(IMovieService movieService, IUserService userService)
        {
            this.movieService = movieService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<bool> Register(string firstName, string lastName, string nickName, int birthYear, string tck, string email, string password)
        {
            long longTCK = long.Parse(tck);

            var client = new Tcdogrulama.KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            var resp = await client.TCKimlikNoDogrulaAsync(longTCK, firstName, lastName, birthYear);
            bool result = resp.Body.TCKimlikNoDogrulaResult;

            User user = new();

            user.Name = firstName;
            user.Surname = lastName;
            user.Nickname = nickName;
            user.BirthYear = birthYear.ToString();
            user.Tc = tck;
            user.Email = email;
            user.Password = password;

            userService.CreateUser(user);

            return result;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public bool Login(string email, string password)
        {
            LoginResult result = userService.Login(email, password);
            if (result.Success == true)
            {
                HttpContext.Session.SetString("Session", JsonConvert.SerializeObject(result.Value));
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult SearchUser()
        {
            UserRequests userRequests = new();
            User userLoggedIn = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("Session"));

            userRequests = userService.GetRequests(userLoggedIn.Id);
            userRequests.MyFriends = userService.GetMyFriends(userLoggedIn);
            userRequests.ToUser = userLoggedIn;
            return View(model: userRequests);
        }

        [HttpPost]
        public bool SearchUser(string nickname)
        {
            User userLoggedIn = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("Session"));
            UserResult isUserValid = new();

            if (nickname is not null)
            {
                isUserValid = userService.GetUserByNickname(nickname);

                if (isUserValid.Success is true && nickname != userLoggedIn.Nickname)
                {
                    userService.CreateRequest(userLoggedIn, isUserValid.Value);
                    return true;
                }
                return false;
            }
            return false;
        }

        [HttpDelete]
        public bool DeleteRequest(string fromUser)
        {
            User userLoggedIn = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("Session"));
            userService.DeleteRequest(fromUser, userLoggedIn.Nickname);
            return true;
        }

        [HttpPost]
        public bool AcceptRequest(string fromUser)
        {
            User userLoggedIn = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("Session"));
            userService.AcceptRequest(fromUser, userLoggedIn.Nickname);
            return true;
        }
    }
}
