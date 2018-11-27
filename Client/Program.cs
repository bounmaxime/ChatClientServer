using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Com;
using System.Threading;

namespace Client
{
    public class Client
    {
        private string hostname;

        private int port;
        private TcpClient comm;
        private ClientGestTopics gestTopics;
        private String _username;

        public Client(string h, int p)
        {
            hostname = h;
            port = p;
            comm = new TcpClient(hostname, port);
            gestTopics = new ClientGestTopics();
        }

        public TcpClient com
        {
            get { return comm; }
            set { comm = value; }
        }

        public ClientGestTopics GestTopic
        {
            get { return gestTopics; }
            set { gestTopics = value; }
        }


        public string HostName
        {
            get { return hostname; }
            set { hostname = value; }
        }

        public string username
        {
            get { return _username; }
            set { _username = value; }
        }





        public void start()
        {


            Console.WriteLine("Connection established");
            String username = "";


            while (true)
            {
                gestTopics = new ClientGestTopics();
                Console.WriteLine("What do you want to do ? ");
                Console.WriteLine("1. List the topics");
                Console.WriteLine("2. Create a new topic");
                Console.WriteLine("3. Join an existing topic");
                String choice = Console.ReadLine();
                Console.WriteLine("CHOICE = " + choice);
                Message response = new Message();


                if (choice == "1")
                {
                    gestTopics.ListTopics(comm);
                    response = Com.Net.rcvMsg(comm.GetStream());
                    Console.WriteLine("List of the topics : ");
                    foreach (String topic in response._data)
                    {
                        Console.WriteLine(topic);
                    }
                }
                else if (choice == "2")
                {
                    String topicname;
                    Console.WriteLine("Enter the name of the new topic: ");
                    topicname = Console.ReadLine();
                    gestTopics.CreateTopic(comm, topicname);
                    response = Com.Net.rcvMsg(comm.GetStream());
                    Console.WriteLine("List of the topics : ");
                    foreach (String topic in response._data)
                    {
                        Console.WriteLine(topic);
                    }



                }
                else if (choice == "3")
                {

                    String topictojoin;

                    gestTopics.ListTopics(comm);
                    response = Com.Net.rcvMsg(comm.GetStream());
                    Console.WriteLine("List of the topics : ");
                    foreach (String topic in response._data)
                    {
                        Console.WriteLine(topic);
                    }

                    Console.WriteLine("What topic do you want to join ?");
                    topictojoin = Console.ReadLine();
                    gestTopics.JoinTopic(comm, topictojoin);
                    response = Com.Net.rcvMsg(comm.GetStream());
                    Console.WriteLine("The port of the chatroom is " + response._data[0]);
                    Client c = gestTopics.ConnectToTopic(hostname, response);





                }
                else
                {

                    Console.WriteLine("Invalid choice");


                }




            }
        }


        /*public void Tochat(String username)
        {
                 
         
           
            String message="";
            Message m = new Message();
            Console.WriteLine(username+" has joined the room.");
            while (message != "/quit")
            {

                Console.WriteLine("Your message : ");
                message = Console.ReadLine();
               
                Com.Net.sendMsg(comm.GetStream(),new Message(Com.Message.Header.POST, message) );
                ReceiveMessage(comm);
                 m= Com.Net.rcvMsg(comm.GetStream());
               Console.WriteLine(m._data[0]);
          

            }


        }*/




        public void run()
        {
            try
            {
                Message msg;
                msg = Com.Net.rcvMsg(comm.GetStream());


                while (msg._head != Message.Header.QUIT)
                {

                    if (msg._head == Message.Header.JOINED)
                    {

                    }


                    msg = Com.Net.rcvMsg(comm.GetStream());

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public String authentication(TcpClient comm)
        {
            bool authenticated = false;
            String username = "";
            String password = "";
            List<String> s = new List<String>();
            Message msg = new Message();
            while (!authenticated)
            {
                /* Console.WriteLine("Enter your username:");
                 username= Console.ReadLine();
                 Console.WriteLine("Enter your password:");
                 password = Console.ReadLine();*/
                s.Add(username);
                s.Add(password);
                Com.Net.sendMsg(comm.GetStream(), new Message(Message.Header.LOGIN, s));
                msg = Com.Net.rcvMsg(comm.GetStream());
                if ((int)msg._head == 10 && msg._data[0] == "True")
                {
                    Console.WriteLine("Welcome " + username + "!");
                    authenticated = true;


                }


            }
            return username;


        }








        public class Program
        {
            public static void Main()
            {
                Client c1 = new Client("127.0.0.1", 81);

                c1.start();


            }
        }


    }
}