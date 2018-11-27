using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat;
using System.Net.Sockets;

namespace Server
{


    class ServerChatter : TextChatter
    {
        TcpClient _cli;
        public ServerChatter(string username, TcpClient client) : base(username)
        {
            _cli = client;
        }

        public TcpClient client
        {
            get { return _cli; }
            set { _cli = value; }
        }


    }
}