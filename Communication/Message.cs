using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    [Serializable]
    class Message
    {
       
        public String _msg;

        public Message(String message)
        {
            _msg = message;

        }



    }
}
