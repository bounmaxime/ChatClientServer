using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    interface Chatter
    {
        void receiveAMessage(String msg, Chatter c);
        String getAlias();

    }

    class ClientChatter : Chatter
    {
        public string getAlias()
        {
            throw new NotImplementedException();
        }

        public void receiveAMessage(string msg, Chatter c)
        {
            throw new NotImplementedException();
        }
    }


}
