using DAL.Interfaces;
using Entity.Concrete;
using Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class UserRepository : IUserRepository
    {
        CommonmoviesapplicationContext context = new();

        public UserRepository(CommonmoviesapplicationContext context)
        {
            this.context = context;
        }

        public User GetUserByNickname(string nickname)
        {
            return context.Users.FirstOrDefault(x => x.Nickname == nickname);
        }

        public User Login(string email, string password)
        {
            return context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public void RequestFriend(Friend friends)
        {

        }

        public Request CreateRequest(Request request)
        {
            context.Requests.Add(request);
            context.SaveChanges();
            return request;
        }

        public List<Request> GetRequests(int toUserId)
        {
            List<Request> requests = new();
            requests = context.Requests.Where(x => x.ToUserId == toUserId).ToList();
            return requests;
        }

        public RequestResult CreateFriend(Request request)
        {
            Friend friends = new();
            friends.FromUserId = request.FromUserId;
            friends.ToUserId = (int)request.ToUserId;

            context.Friends.Add(friends);
            context.SaveChanges();

            return new RequestResult { Id = request.Id, Message = "Friends record create successfully.", Success = true, Value = request };
        }

        public User GetUserById(int userId)
        {
            return context.Users.FirstOrDefault(x => x.Id == userId);
        }

        public void DeleteRequest(Request request)
        {
            context.Requests.Remove(request);
            context.SaveChanges();
        }

        public void AcceptRequest(Friend friend)
        {
            context.Friends.Add(friend);
            context.SaveChanges();
        }

        public List<Friend> GetMyFriends(int toUserId)
        {
            return context.Friends.Where(x => x.ToUserId == toUserId).ToList();
        }

        public List<Friend> GetFriends()
        {
            return context.Friends.ToList();
        }

        public User CreateUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
    }
}
