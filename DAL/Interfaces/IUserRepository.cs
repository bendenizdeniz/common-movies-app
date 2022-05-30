using Entity.Concrete;
using Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        public User CreateUser(User user);

        public User Login(string email, string password);

        public User GetUserByNickname(string nickname);

        public User GetUserById(int userId);

        public void RequestFriend(Friend friends);

        public Request CreateRequest(Request request);

        public List<Request> GetRequests(int toUserId);

        public void DeleteRequest(Request request);

        public void AcceptRequest(Friend friend);

        public RequestResult CreateFriend(Request request);

        public List<Friend> GetFriends();

        public List<Friend> GetMyFriends(int toUserId);

    }
}
