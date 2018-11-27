using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Server
{
    class TCPGestTopics
    {
        static int _nextPort = 7999;
        public int NextPort // read only port
        {
            get { return _nextPort; }
        }


        public void createTopic(string name, Dictionary<string, ServerChatRoom> _chatrooms)
        {
            bool tryagain = true;//checks if the port is taken or not
            while (tryagain)
            {
                try
                {
                    TCPServer serv = new TCPServer(_nextPort);
                    serv.Authenticated = true; // because the user is already authenticated
                    serv.tcplistener.Start();
                    serv.Mode = "Connection";
                    tryagain = false;
                    serv.ServerTopicManager = new ServerGestTopics(serv.Comm, _chatrooms);
                    Thread t = new Thread(serv.start);
                    t.Start();


                }
                catch (Exception e)
                {
                    Console.WriteLine("Port " + _nextPort + " is already in use)");
                    _nextPort++;
                    tryagain = true;
                }

            }



        }

    }
}