using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    class TextChatRoom : Chatroom
    {
        Dictionary<String,Chatter> _participants; 
        public TextChatRoom ()
        {
            _participants = new Dictionary<String, Chatter>();
        }

        public string getTopic()
        {
            throw new NotImplementedException();
        }

        public void join(Chatter c)
        {
            if (!_participants.ContainsKey(c.getAlias()))
            {
                Console.WriteLine(c.getAlias() + " has joined the room.");
                _participants.Add(c.getAlias(), c);
            }
            else
            {
                Console.WriteLine("You must join the chatroom first ! ");
            }
            
        }

        public void post(string msg, Chatter c)
        {
            if (_participants[c.getAlias()] == c)
            {
                Console.WriteLine(c.getAlias() + ": " + msg);
            }
            else
            {
                Console.WriteLine( "You must join the chatroom first ! ");
            }
        }

        public void quit(Chatter c)
        {
            throw new NotImplementedException();
        }
    }
}
