using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat;

namespace Server
{
    class ServerChatRoom 
    {
        List<Chatter> _chatters;
        int _port;
        public ServerChatRoom(List<Chatter> chatterlist,int port)
        {
            _chatters = chatterlist;
            _port = port;
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public List<Chatter> listchatters{
            get { return _chatters; }
            set { _chatters = value; }
            }

        public string getTopic()
        {
            throw new NotImplementedException();
        }

       public void join(Chatter c)
        {
           
                _chatters.Add(c);

                List<string> notificationmessage = new List<string>();
                List<string> listusers= new List<string>();
                notificationmessage.Add(c.getAlias() + " has joined the room.");
                Com.Message m = new Com.Message(Com.Message.Header.JOINED, notificationmessage);


                foreach (ServerChatter chatter in _chatters)// send the notification
                    {
                    Com.Net.sendMsg(chatter.client.GetStream(), m);
                    listusers.Add(chatter.getAlias());
                }

                
            foreach (ServerChatter chatter in _chatters)//Update list of users in the room
            {
               
                Com.Net.sendMsg(chatter.client.GetStream(), new Com.Message (Com.Message.Header.LIST_USERS, listusers));
                
            }




        }

        public void post(string msg, Chatter c)
        {
            if (_chatters.Contains(c))
            {
                List<String> s = new List<string>();
                s.Add(msg);
                s.Add(c.getAlias());
                Com.Message m = new Com.Message(Com.Message.Header.POST, s);


                foreach (ServerChatter chatter in _chatters)
                {
                    Com.Net.sendMsg(chatter.client.GetStream(), m);
                }
            }
            else
            {
                throw new Exception("The chatter is not in the room! ");
            }



        }

        public void quit(Chatter c)
        {
            _chatters.Remove(c);

            List<string> notificationmessage = new List<string>();
            List<string> listusers = new List<string>();
            notificationmessage.Add(c.getAlias() + " has left the room.");
            Com.Message m = new Com.Message(Com.Message.Header.LEFT, notificationmessage);


            foreach (ServerChatter chatter in _chatters)// send the notification
            {
                Com.Net.sendMsg(chatter.client.GetStream(), m);
                listusers.Add(chatter.getAlias());
            }


            foreach (ServerChatter chatter in _chatters)//Update list of users in the room
            {

                Com.Net.sendMsg(chatter.client.GetStream(), new Com.Message(Com.Message.Header.LIST_USERS, listusers));

            }

        }
    }
}