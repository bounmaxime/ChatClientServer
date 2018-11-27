using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class TextChatter : Chatter
    {
        String _username;
        String _password;


        public TextChatter(String username)
        {
            _username = username;
           

        }

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
            }
        }
        public String Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
            }
        }
 


        public string getAlias()
        {
            return _username;
        }

        public void receiveAMessage(string msg, Chatter c)
        {
            throw new NotImplementedException();
        }
    }
}
