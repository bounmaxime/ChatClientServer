using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    class TextGestTopics : TopicsManager
    {
        Dictionary<string, Chatroom> _chatrooms;

        public TextGestTopics()
        {
            _chatrooms = new Dictionary<string, Chatroom>();

        }

        public void createTopic(string topic)
        {
            if (!_chatrooms.ContainsKey(topic))
            {
                _chatrooms.Add(topic, new TextChatRoom());

            }
            else
            {
                Console.WriteLine("The chatroom already exists!");

            }
            
        }

        public Chatroom joinTopic(string topic)
        {

            if (_chatrooms.ContainsKey(topic))
            {
                return _chatrooms[topic];

            }
            else
            {
                Console.WriteLine("The chatroom doesn't exist! Creating the chatroom " + topic);
                _chatrooms.Add(topic, new TextChatRoom());
                return _chatrooms[topic];

            }
        }

        public List<string> listTopics()
        {
            List<string> output = new List<string>();
            foreach (KeyValuePair<string, Chatroom> entry in _chatrooms)
            {
                output.Add(entry.Key);
            }
            return output;
        }
    }
}
