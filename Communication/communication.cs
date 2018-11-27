using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    class communication
    {

      
            public static void sendMsg(Stream s, Message msg)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, msg);
            }

            public static Message rcvMsg(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Message)bf.Deserialize(s);
            }
        


    }
}
