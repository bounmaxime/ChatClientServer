using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface AuthenticationManager
    {
        bool addUser(String login, String password);
        void removeUser(String login);
        bool authentify(String login, String password);
        AuthenticationManager load(String path);
        void save(String path);
    }
}
