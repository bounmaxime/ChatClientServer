using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatGraphique
{
    public partial class TopicsManager : Form
    {
        Client.Client _client;
        Client.ClientGestTopics gestTopics;
        ChatWindow _chatwindow; 

        

        public TopicsManager()
        {
            InitializeComponent();
        }

        public TopicsManager(Client.Client clientgui)
        {
            _client = clientgui;
            InitializeComponent();
            
        }
        public ListBox listbox
        {
            get { return listBox1; }
            set { listBox1 = value; }
        }
        public ChatWindow chatwindow
        {
            get { return _chatwindow; }
            set { _chatwindow = value; }
        }

        private void button1_Click(object sender, EventArgs e)// join button
        {
            if(_chatwindow.ConnectionToTopic == false)
            {
                try
                {


                    String Selectedtopic = listBox1.Items[listBox1.SelectedIndex].ToString();// get the selected topic

                    if (Selectedtopic != "")
                    {
                        Com.Message response = new Com.Message();
                        List<string> s = new List<string>();


                        _chatwindow.room = Selectedtopic;

                        s.Add(Selectedtopic);
                        s.Add(_client.username);
                        Com.Net.sendMsg(_client.com.GetStream(), new Com.Message(Com.Message.Header.JOIN_TOPIC, s));// serverside: send a join request, add the chatter to the listchatters and receive the port of the room to create a new connection
                        response = Com.Net.rcvMsg(_client.com.GetStream());
                        Client.Client c = _client.GestTopic.ConnectToTopic(_client.HostName, response);
                        c.username = _client.username;
                        _chatwindow.ConnectionToTopic = true;
                        _chatwindow.mainclient = _chatwindow.clientgui;
                        _chatwindow.clientgui = c;

                        Thread thread = new Thread(receiveMessage);
                        thread.Start();

                        if (response._head == Com.Message.Header.ERROR)
                        {
                            MessageBox.Show("Error joining the topic");
                        }
                        else
                        {
                            response = Com.Net.rcvMsg(_client.com.GetStream());
                            _chatwindow.Chattextbox.Invoke(_chatwindow.textchat, response._data[0]);

                   

                            Hide();
                        }


                    }
                }
                catch (Exception ex)
                {
                
                    MessageBox.Show(ex.ToString());
                }

            }

            else
            {
                MessageBox.Show("You must disconnect from the topic!");
            }




        }





        private void button2_Click(object sender, EventArgs e)// Create button, shows the CreateTopic form
        {
            CreateTopic c = new CreateTopic(_client, this);
            c.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        private void TopicsManager_Load(object sender, EventArgs e)
        {

        }

        public void receiveMessage()
        {
            bool stop = false;
             try
            {
                Com.Message msg = new Com.Message(Com.Message.Header.UNDEFINED, new List<String>());
            
                while (!stop)
                {
                    msg = Com.Net.rcvMsg(_client.com.GetStream());

                    if (msg._head == Com.Message.Header.QUIT)
                    {
                        stop = true;
                        MessageBox.Show("lol");
                    }

                    else if (msg._head == Com.Message.Header.JOINED)
                    {
                        _chatwindow.Chattextbox.Invoke(_chatwindow.textchat, msg._data[0]);
                    }
                    else if (msg._head == Com.Message.Header.LEFT)
                    {
                        _chatwindow.Chattextbox.Invoke(_chatwindow.textchat, msg._data[0]);


                    }
                    else if (msg._head == Com.Message.Header.POST)
                    {
                        _chatwindow.Chattextbox.Invoke(_chatwindow.textchat, msg._data[1] + ": " + msg._data[0]);
                    }
                    else if (msg._head == Com.Message.Header.LIST_USERS)
                    {
                     
                        if (InvokeRequired)
                        {
                            _chatwindow.userslistbox.Invoke(_chatwindow.clearlistusers);
                            foreach (string user in msg._data)
                            {
                                if (!chatwindow.userslistbox.Items.Contains(user))
                                {
                                   
                                    _chatwindow.userslistbox.Invoke(_chatwindow.listusers, user);

                                }
                            }

                        }
                        else
                        {

                               _chatwindow.userslistbox.BeginUpdate();// update the userlistbox
                            foreach (string user in msg._data)
                            {
                                if (!chatwindow.userslistbox.Items.Contains(user))
                                {
                                    _chatwindow.userslistbox.Items.Add(user);
                                }
                              


                            }
                            _chatwindow.userslistbox.EndUpdate();
                        }

                       


                    }




                }
                


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            

        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if(_chatwindow.ConnectionToTopic == true)
            {
                List<String> s = new List<string>();
                Com.Message response = new Com.Message();
                s.Add(_client.username);
                s.Add(_chatwindow.room);
                Com.Net.sendMsg(_client.com.GetStream(), new Com.Message(Com.Message.Header.LEFT, s));// send a disconnect request to the server w/ the name of the user
                response = Com.Net.rcvMsg(_client.com.GetStream());

                if (response._head == Com.Message.Header.ERROR)
                {
                    MessageBox.Show(response._data[0]);
                }
                else
                {
                    _chatwindow.Chattextbox.Clear();
                    _chatwindow.userslistbox.Items.Clear();
                    MessageBox.Show("You have been disconnected from " + _chatwindow.room);

                    _chatwindow.room = "";
                    _chatwindow.ConnectionToTopic = false;
                    

                }
                Hide();


            }

            else
            {
                MessageBox.Show("You must connect to a topic!");
            }
    

        }
    }
}
