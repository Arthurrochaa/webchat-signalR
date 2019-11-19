using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_app_chat.Models;

namespace web_app_chat.Repositories
{
    public class ConnectionsRepository
    {
        private readonly Dictionary<string, User> connections = new Dictionary<string, User>();

        public void Add(string id, User user)
        {
            if (!connections.ContainsKey(id))
            {
                connections.Add(id, user);
            }
        }

        public string GetUserById(long id)
        {
            return (from con in connections
                    where con.Value.key == id
                    select con.Key).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return (from con in connections
                    select con.Value).ToList();
        }
    }
}
