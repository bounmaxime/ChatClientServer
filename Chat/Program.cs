using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Testing the Chat package
            Chatter bob = new TextChatter("Bob");
            Chatter joe = new TextChatter("Joe");
            TopicsManager gt = new TextGestTopics();
            gt.createTopic("java");
            gt.createTopic("UML");
            List<String> topics = gt.listTopics();
            Console.WriteLine("The opened topics are : ");
            foreach(String topic in topics)
            {
                Console.WriteLine(topic);
            }
            gt.createTopic("jeux");
           topics =  gt.listTopics();

            Console.WriteLine("The opened topics are : ");
            foreach (String topic in topics)
            {
                Console.WriteLine(topic);
            }
         


              Chatroom cr = gt.joinTopic("jeux");
              cr.join(bob);
              cr.post("Je suis seul ou quoi ?", bob);
              cr.join(joe);
              cr.post("Tiens, salut Joe !", bob);
              cr.post("Toi aussi tu chat sur les forums de jeux pendant les TP, Bob ? ",joe);
              */

            /*usermanagement
            AuthenticationManager am = new Authentication();


            try
            {
                am.addUser("bob", "123");
               
                am.removeUser("bob");
                am.removeUser("bob");
            }

            catch (UserUnknownException e )
            {
                Console.WriteLine(e.Login + ": User unknown (unable to remove) ");

            }
            catch(UserExistsException e)
            {
                Console.WriteLine(e.Login + " has already been added !");

            }*/

          

            /* 
             Testing the authentication
            AuthenticationManager am = new Authentication();

            try
            {
                am.addUser("bob", "123");

                am.authentify("bob","123");
                am.authentify("bob", "456");

            }

            catch (WrongPasswordException e)
            {
                Console.WriteLine(e.Login + ": Wrong password !  ");

            }
            catch (UserUnknownException e)
            {
                Console.WriteLine(e.Login + ": User unknown (unable to remove) ");

            }
            catch (UserExistsException e)
            {
                Console.WriteLine(e.Login + " has already been added !");

            }*/
             

            try
            {
                AuthenticationManager am = new Authentication();
                am.addUser("bob", "123");
                am.save("users.txt");
                AuthenticationManager amloaded = new Authentication()   ;
               amloaded= amloaded.load("users.txt");
                amloaded.authentify("bob","123");
                Console.WriteLine("Loading complete");



            }

            catch (UserUnknownException e)
            {
                Console.WriteLine(e.Login + " is unknown ! Error during the saving / loading.");
            }

            catch (WrongPasswordException e)
            {
                Console.WriteLine(e.Login + " has provided an invalid password ! Error during the saving / loading.");

            }

            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadLine();
        }
    }
}
