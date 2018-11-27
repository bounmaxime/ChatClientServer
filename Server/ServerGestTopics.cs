using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com;
using System.Net.Sockets;
using System;

namespace Server
{

    interface ServerTopicsManager
    {
        List<String> listTopics();
        // Chatroom joinTopic(String topic);
        void createTopic(String topic);
        Message listTopics(Dictionary<string, ServerChatRoom> _chatrooms);
        ServerChatRoom joinTopic(String topic);
     
        void gereClient();


    }

    class ServerGestTopics
    {
        Dictionary<string, ServerChatRoom> _chatrooms;
        TcpClient comm;

        public ServerGestTopics(TcpClient comarg)
        {
            _chatrooms = new Dictionary<string, ServerChatRoom>();
       
            comm = comarg;


        }
        public ServerGestTopics(TcpClient comarg, Dictionary<string, ServerChatRoom> c)
        {
            _chatrooms = c;

            comm = comarg;


        }



        Message listTopics()
        {
            Message topics = new Message(Message.Header.LIST_TOPICS);
      

            foreach (KeyValuePair<string, ServerChatRoom> cr in _chatrooms)
            {
                 topics._data.Add(cr.Key);

            }
            
            return topics;
        }



        public void gereClient(TcpClient comm) // analyses the header of the received message
        {
            Message m = new Message();
          
            m = Com.Net.rcvMsg(comm.GetStream());           
        
            Console.WriteLine(" User wants choice " + m._head);
            List<String> msg = new List<String>();
            TCPGestTopics topicCreator = new TCPGestTopics();


            switch ((m._head))
            {
                case (Com.Message.Header.POST)://POST Message
                    msg.Add(m._data[0]);// message
                    msg.Add(m._data[1]);// client username
 
                    ServerChatRoom r1 = _chatrooms[m._data[2]];

                  
                    Console.WriteLine("Sent message : " + m._data[2] );
                 

                    foreach (ServerChatter chatter in r1.listchatters)
                    {
                        Com.Net.sendMsg(chatter.client.GetStream(), new Message(Com.Message.Header.POST, msg));
                    }




                    break;
                case (Com.Message.Header.LIST_TOPICS):// List topic

                        
                    Com.Net.sendMsg(comm.GetStream(), listTopics());// replies to the client
                  
                    break;

                case (Com.Message.Header.CREATE_TOPIC):// Create topic

                    try
                    {
                        _chatrooms.Add(m._data[0], new ServerChatRoom(new List<Chat.Chatter>(), topicCreator.NextPort));

                        topicCreator.createTopic(m._data[0], _chatrooms);
                        Com.Net.sendMsg(comm.GetStream(), listTopics());
                    }
                    catch (Exception e)
                    {
                        Com.Net.sendMsg(comm.GetStream(), new Message(Com.Message.Header.ERROR, "False"));
                    }


                    break;

                case (Com.Message.Header.JOIN_TOPIC):// Join topic

                    if (_chatrooms.ContainsKey(m._data[0]))
                    {

                        ServerChatRoom r = _chatrooms[m._data[0]];

                        Com.Net.sendMsg(comm.GetStream(), new Message(Com.Message.Header.JOIN, r.Port.ToString()));// sends back the port of the room
                        r.join(new ServerChatter(m._data[1],comm));

        


                    }
                    else
                    {
                        throw new Exception("Error Joining the topic: the chatroom doesnt contain the key.");
                    }

           
                    break;

                case (Com.Message.Header.LIST_USERS):
                    ServerChatRoom scr = _chatrooms[m._data[0]];// get the chatroom the user asked for
                    List<string> data = new List<string>();
                
                  

                    foreach(Chat.Chatter s in scr.listchatters)
                    {
                        data.Add(s.getAlias());
                    }

                    Message list_users = new Message(Message.Header.LIST_USERS, data);

                
                    Com.Net.sendMsg(comm.GetStream(), list_users);
                    break;

                case (Com.Message.Header.LEFT):
                   
                    try
                    {
                        ServerChatRoom scrtodisconnect = _chatrooms[m._data[1]];
                        ServerChatter chattertoremove = new Server.ServerChatter("undefined", new TcpClient());
                        foreach (ServerChatter c in scrtodisconnect.listchatters)
                        {
                            if (c.Username.Equals(m._data[0]))
                            {
                                Console.WriteLine(m._data[0] + " disconnected");
                                 chattertoremove = c;
                            }
                        }
                        scrtodisconnect.listchatters.Remove(chattertoremove);
                        scrtodisconnect.quit(chattertoremove);
                        Com.Net.sendMsg(comm.GetStream(), new Message(Com.Message.Header.SUCCESS));

                    }
                    catch (Exception e)
                    {
                        Com.Net.sendMsg(comm.GetStream(), new Message(Com.Message.Header.ERROR,e.ToString()));
                    }



                    break;













            }


        }//end GereClient








    }
}