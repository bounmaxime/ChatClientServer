using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Com;
using System.IO;

namespace Server
{
    class TCPServer : ICloneable
    {

        private TcpClient comm;
        private int port;
        TcpListener l;
        private Thread listener;
        private String mode = "Connection";
        ServerGestTopics _ServerTopicManager;
        bool authenticated = false;
        Authentication userdata;


        public TCPServer(int port)
        {
            this.port = port;
            _ServerTopicManager = new ServerGestTopics(comm);
            l = new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), port);
            Console.WriteLine("port = " + port);
            comm = new TcpClient();

        }

        public String Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        public ServerGestTopics ServerTopicManager
        {
            get { return _ServerTopicManager; }
            set { _ServerTopicManager = value; }
        }
        public TcpClient Comm
        {
            get { return comm; }
            set { comm = value; }
        }

        public TcpListener tcplistener
        {
            get { return l; }
            set { l = value; }
        }

        public bool Authenticated
        {
            get { return authenticated; }
            set { authenticated = value; }
        }


        public void start()
        {
                    

                
                         
                l.Start();


                listener = new Thread(this.run);
                listener.Start();

            
        

            





        }

        public void run()
        {
           userdata = new Authentication();
            userdata.addUser("a", "123");
            userdata.addUser("b", "123");
            userdata.save("users.txt");
            Message msg;
            int counter = 0;
            bool ctrl = authenticated;
                counter++;
           
            //Console.WriteLine("Connection established @" + comm);






            
            while (true)
            {
                if (mode == "Connection")
                {
                    
                    comm = l.AcceptTcpClient();
                    Console.WriteLine("Connection established to {0}", comm.Client.RemoteEndPoint);
                    TCPServer serverClone = (TCPServer)this.Clone();
                    serverClone.Mode = "TreatClient";
                    new Thread(new ThreadStart(serverClone.run)).Start();
                   
                }
                else
                {
                 
                    while (!ctrl)
                    {
                        msg = (Message)Com.Net.rcvMsg(comm.GetStream());
                        int choice = (int)msg._head;




                        switch (choice)
                        {
                            case (10):
                                ctrl = connectUser(msg,userdata);

                                Com.Net.sendMsg(comm.GetStream(), new Message(Message.Header.LOGIN, ctrl.ToString()));
                                break;

                            case (11):
                                 addUser(msg,userdata);

                                Com.Net.sendMsg(comm.GetStream(), new Message(Message.Header.REGISTER,"True"));
                                break;
                            case (12):
                                DeleteUser(msg,userdata);
                                ctrl = true;
                                Com.Net.sendMsg(comm.GetStream(), new Message(Message.Header.DELETE_USER, ctrl.ToString()));
                                break;


                        }
                    }


                    ServerTopicManager.gereClient(comm);
                }

              


            }




        }





        public bool connectUser(Message msg,AuthenticationManager a)
        {
            bool ctrl;
      

            a =a.load("users.txt");
            ctrl = a.authentify(msg._data[0],msg._data[1]);
            return ctrl;

        }

        public bool addUser(Message msg, Authentication a)
        {
            bool ctrl;
           
            ctrl = a.addUser(msg._data[0], msg._data[1]);
            a.save("users.txt");
            return ctrl;

        }
        public void DeleteUser(Message msg, Authentication a)
        {
     
            a.removeUser(msg._data[0]);
         

        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }






    }

    class Receiver
    {
        private TcpClient comm;

        public Receiver(TcpClient s)
        {
            comm = s;
        }







        #region ICloneable Members



        #endregion

    }

}

 