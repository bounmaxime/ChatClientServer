using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
   public  interface Chatter
    {
        void receiveAMessage(String msg, Chatter c);
        String getAlias();

    }

   public interface Chatroom
    {
        void post(String msg, Chatter c);
        void quit(Chatter c);
        void join(Chatter c);
        String getTopic();

    }

    public interface TopicsManager
    {
        List <String> listTopics();
        Chatroom joinTopic(String topic);
        void createTopic(String topic);

    }




}
