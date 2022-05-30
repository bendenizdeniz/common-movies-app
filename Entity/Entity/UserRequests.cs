using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    public class UserRequests
    {
        public User ToUser { get; set; }

        public List<User> RequestsUser { get; set; }

        public List<User> MyFriends { get; set; }
    }
}
