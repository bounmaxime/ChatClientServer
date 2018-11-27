using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Server
{
    [Serializable]
    class Authentication : AuthenticationManager
    {
        //[XmlAttribute()]
        Dictionary<String, String> _users;

        public Authentication()
        {
            _users = new Dictionary<string, string>();
        }

        public bool addUser(String login, String password)
        {

     
            if (!_users.ContainsKey(login))
            {
                _users.Add(login, password);
                Console.WriteLine(login + " has been added. ");
                return true;

            }
            else
            {
                return false;

            }
        }


        public bool authentify(String login, String password)
        {

            try
            {
                if (_users[login] == password)
                {
                    Console.WriteLine(login + " connected.");
                    return true;

                }
                else
                {
                    return false;
                }


            }
            catch(Exception e)
            {
                return false;
            }

        }



        public void removeUser(String login)
        {
            if (_users.ContainsKey(login))
            {
                _users.Remove(login);
                Console.WriteLine(login + " has been removed ! ");

            }
            else
            {
                throw new UserUnknownException(login);

            }
        }


        public void save(string filename)
        {
            FileStream ffs = new FileStream(filename, FileMode.Create);
            BinaryFormatter binaryf = new BinaryFormatter();
            binaryf.Serialize(ffs, this);
            ffs.Close();


        }
        public AuthenticationManager load(string filename)
        {
            FileStream ffs = new FileStream(filename, FileMode.Open);
            BinaryFormatter binaryf = new BinaryFormatter();
            AuthenticationManager o = (AuthenticationManager)binaryf.Deserialize(ffs);
            ffs.Close();
            return o;

        }


    }
}
