using System.Collections.Generic;
using System.Linq;
using ReadingLog.Core;

namespace ReadingLog.Data
{
    public class UserRepository : IUserRepository
    {
        private List<User> users = new List<User>
        {
            new User { Id=3522, Name="roland", Password="K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=", Role="Admin"}
        };

        public User GetByUsernameAndPassword(string username, string password)
        {
            var user = users.SingleOrDefault(u => u.Name == username && u.Password == password);

            return user;
        }
    }
}