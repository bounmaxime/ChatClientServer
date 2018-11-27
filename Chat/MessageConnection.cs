using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat
{

    interface MessageConnection 
    {
        Message getMessage();
        void sendMessage(Message m);

    }

    abstract class TCPServer 
    {
        Socket commSocket;
        private int port;

        public void start()
        {
            TcpListener l = new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), port);
            l.Start();

            while (true)
            {
                TcpClient comm = l.AcceptTcpClient();
                Console.WriteLine("Connection established @" + comm);
                //new Thread(new Receiver(comm).doOperation).Start();

            }



        }


    }


}
    class TCPClient
    {

    }

    class Message
    {

    }




