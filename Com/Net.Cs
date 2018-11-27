using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Com
{
    /*interface MessageConnection
    {
        void sendMsg(Stream s, Message msg);
        Message rcvMsg(Stream s);

    }*/


    public class Net/* : MessageConnection public to access it from client & Net*/
    {
        public static void sendMsg(NetworkStream s, Message msg)
        {
            IFormatter bf = new BinaryFormatter();
            bf.Serialize(s, msg);
        }

        public static Message rcvMsg(NetworkStream s)
        {
            IFormatter bf = new BinaryFormatter();
            return (Message)bf.Deserialize(s);
        }
    }
}
