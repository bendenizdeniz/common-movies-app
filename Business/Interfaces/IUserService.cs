using Entity.Concrete;
using Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserService
    {
        public LoginResult Login(string email, string password);

        public UserResult GetUserByNickname(string nickname);

        public User GetUserById(int userId);

        public RequestResult CreateRequest(User fromUser, User toUser);

        public UserRequests GetRequests(int toUserId);

        public void AcceptRequest(string fromUser, string toUser);

        public void DeleteRequest(string fromUser, string toUser);

        public RequestResult CreateFriend(Request request);

        public List<User> GetMyFriends(User user);

        public User CreateUser(User user);
    }
}
