using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Client
{
   
    interface ClientGestInterface
    {
        List<String> ListTopics(TcpClient comm);
        void CreateTopic(TcpClient comm, String topic);
        void JoinTopic(TcpClient comm, String topic);


    }
    public class ClientGestTopics : ClientGestInterface
    {
        public void CreateTopic(TcpClient comm, string topic)
        {
            Message m = new Message();
            m._head = Com.Message.Header.CREATE_TOPIC;
            m._data.Add(topic);
            Net.sendMsg(comm.GetStream(), m);
        }

        public void JoinTopic(TcpClient comm, string topic)
        {
            String username = "Bob";
            List<String> s = new List<string>();
            s.Add(topic);
            s.Add(username);
            Net.sendMsg(comm.GetStream(), new Message(Com.Message.Header.JOIN_TOPIC, s));

        }

        public List<string> ListTopics(TcpClient comm)
        {
            List<String> result = new List<String>();
            Net.sendMsg(comm.GetStream(), new Message(Com.Message.Header.LIST_TOPICS));
            return result;
        }

        public Client ConnectToTopic(String hostname , Message response)
        {
            Client c = new Client(hostname, int.Parse(response._data[0]));
  
            return c;

        } 
        public void Disconnect(TcpClient comm, Client c)
        {
            Net.sendMsg(comm.GetStream(), new Message(Com.Message.Header.LIST_TOPICS, c.username));
            comm.GetStream().Close();
            comm.Close();

        }


    }
}