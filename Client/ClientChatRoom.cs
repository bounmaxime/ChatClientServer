using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com;
using System.Net.Sockets;

namespace Client
{
    class ClientChatRoom    
    {
        private TcpClient _comm;
        
        public TcpClient comm
        {
            get { return _comm; }
            set { _comm = value; }
        }



    }
}
