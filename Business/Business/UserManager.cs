using Business.Interfaces;
using DAL.DataAccess;
using DAL.Interfaces;
using Entity.Concrete;
using Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Business
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserResult GetUserByNickname(string nickname)
        {
            User user = userRepository.GetUserByNickname(nickname);
            Guid guid = System.Guid.NewGuid();
            if (user is not null) return new UserResult { Id = guid, Success = true, Message = "User found successfully.", Value = user };
            return new UserResult { Id = guid, Success = false, Message = "User not found.", Value = user };
        }

        public LoginResult Login(string email, string password)
        {
            Guid guid = System.Guid.NewGuid();
            User userLoggedIn = userRepository.Login(email, password);
            if (userLoggedIn != null)
                return new LoginResult { Id = guid, Success = true, Message = "User logged in successfully.", Value = userLoggedIn };
            return new LoginResult { Id = guid, Success = false, Message = "User logged in failed.", Value = userLoggedIn };
        }

        public RequestResult CreateRequest(User fromUser, User toUser)
        {
            Guid guid = System.Guid.NewGuid();

            List<User> friendUsers = GetMyFriends(toUser);

            if (friendUsers.FirstOrDefault(x => x.Nickname == fromUser.Nickname) is null)
            {
                return new RequestResult { Id = guid, Message = "request create successfully.", Success = true, Value = userRepository.CreateRequest(new Request { Id = guid, FromUserId = fromUser.Id, ToUserId = toUser.Id, Status = 1 }) };
            }
            return new RequestResult { Id = guid, Message = "request create failed.", Success = false, Value = null };
        }

        public RequestResult CreateFriend(Request request)
        {
            return userRepository.CreateFriend(request);
        }

        public User GetUserById(int userId)
        {
            return userRepository.GetUserById(userId);
        }

        public UserRequests GetRequests(int toUserId)
        {
            List<Request> requests = userRepository.GetRequests(toUserId);

            UserRequests userRequests = new();

            List<User> userList = new();

            foreach (var item in requests)
            {
                var user = GetUserById(item.FromUserId);
                userList.Add(user);
            }

            userRequests.RequestsUser = userList;
            return userRequests;
        }

        public void DeleteRequest(string fromUser, string toUser)
        {
            User toUserObj = userRepository.GetUserByNickname(toUser);
            User fromUserObj = userRepository.GetUserByNickname(fromUser);
            List<Request> requests = userRepository.GetRequests(toUserObj.Id);
            Request deletedRequest = requests.FirstOrDefault(x => x.FromUserId == fromUserObj.Id && x.ToUserId == toUserObj.Id);
            userRepository.DeleteRequest(deletedRequest);
        }

        public void AcceptRequest(string fromUser, string toUser)
        {
            User toUserObj = userRepository.GetUserByNickname(toUser);
            User fromUserObj = userRepository.GetUserByNickname(fromUser);

            List<User> friendUsers = GetMyFriends(toUserObj);

            if (friendUsers.FirstOrDefault(x => x.Nickname == fromUser) is null)
            {
                List<Request> requests = userRepository.GetRequests(toUserObj.Id);
                Request acceptedRequest = requests.FirstOrDefault(x => x.FromUserId == fromUserObj.Id && x.ToUserId == toUserObj.Id);

                Friend newFriend = new();
                newFriend.FromUserId = acceptedRequest.FromUserId;
                newFriend.ToUserId = (int)acceptedRequest.ToUserId;

                DeleteRequest(fromUser, toUser);

                userRepository.AcceptRequest(newFriend);
            }
        }

        public List<User> GetMyFriends(User user)
        {
            List<User> friendUsers = new();

            List<Friend> friends = userRepository.GetFriends().Where(x => x.FromUserId == user.Id || x.ToUserId == user.Id).ToList();

            foreach (var item in friends)
            {
                if (item.FromUserId == user.Id)
                    friendUsers.Add(GetUserById(item.ToUserId));
                else if (item.ToUserId == user.Id)
                    friendUsers.Add(GetUserById(item.FromUserId));
            }
            return friendUsers;
        }

        public User CreateUser(User user)
        {
            return userRepository.CreateUser(user);
        }
    }
}
