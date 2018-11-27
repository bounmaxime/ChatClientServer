using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Com
{

    [Serializable]
    public class Message
    {
        public enum Header { JOIN, POST, QUIT, GET, LIST_TOPICS, CREATE_TOPIC, JOIN_TOPIC, JOINED, LEFT, UNDEFINED , LOGIN, REGISTER,DELETE_USER ,ERROR, SUCCESS, LIST_USERS} // Header of the message
        public Header _head;
        public List<string> _data;
   

        public Message()
        {
            _head = Header.UNDEFINED;
            _data = new List<string>();
        }
        public Message(Header head)
        {
            _head = head;
            _data = new List<string>();
        }
        public Message(Header head, string data)
        {
            _head = head;
            List <String> s = new List<string>();
            s.Add(data);
            _data = s;

        }

        public Message(Header head, List<string> data)
        {
            _head = head;
            _data =data;
        }





    }
}
