using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatGraphique
{
    public partial class    ChatWindow : Form
    {
        
        Client.Client _clientgui;
        Client.Client _mainclient;
        bool connectedToTopic;
        TopicsManager t1;
        String _room;
        public  delegate void printmessage(String text);
        public delegate void UpdateListUsers(String text);
        public delegate void clearListUsers();

        private printmessage textChat;
        private UpdateListUsers listuserupdater;
        private clearListUsers listuserclearer;

     


        public ChatWindow()
        {
            InitializeComponent();
        }

        public ChatWindow(Client.Client clientgui)
        {
            textChat += new printmessage(this.updatetext);
            listuserupdater += new UpdateListUsers(this.updatelistusers);
            listuserclearer += new clearListUsers(this.clearListUser);
            _clientgui = clientgui;
            _mainclient = clientgui;
            connectedToTopic = false;
            InitializeComponent();
            _room = "";
        }

        public String room
        {
            get { return _room; }
            set { _room = value; }
        }


        public Client.Client clientgui
        {
            get { return _clientgui; }
            set { _clientgui = value; }
        }

        public Client.Client mainclient
        {
            get { return _mainclient; }
            set { _mainclient = value; }
        }

        public TextBox Chattextbox
        {
            get { return textBox2; }
            set { textBox2 = value; }
        }
        public printmessage textchat
        {
            get { return textChat; }
            set { textChat = value; }
        }

        public clearListUsers clearlistusers
        {
            get { return listuserclearer; }
            set { listuserclearer = value; }
        }
        public UpdateListUsers listusers
        {
            get { return listuserupdater; }
            set { listuserupdater = value; }
        }

        public ToolStripMenuItem topicsbutton
        {
            get { return connectionToolStripMenuItem; }
            set { connectionToolStripMenuItem = value; }
        }
        public bool ConnectionToTopic
        {
            get { return connectedToTopic; }
            set { connectedToTopic = value;}
        }

        public ListBox userslistbox
        {
            get { return this.listBox1; }
            set { this.listBox1 = value; }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void connectToServerToolStripMenuItem_Click(object sender, EventArgs e)// add topic
        {
   
        }

        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
     
            t1 = new TopicsManager(_clientgui);
            t1.chatwindow = this;
            Com.Message response = new Com.Message();
      


            _clientgui.GestTopic.ListTopics(_clientgui.com);
            response = Com.Net.rcvMsg(_clientgui.com.GetStream());
            

            t1.listbox.BeginUpdate();
            foreach (String topic in response._data)
            {
                t1.listbox.Items.Add(topic);
            }


            t1.listbox.EndUpdate();
            t1.Show();

            
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                List<String> data = new List<string>();
                Com.Message msg = new Com.Message();
                 data.Add(textBox1.Text);
                data.Add(_clientgui.username);
                data.Add(this._room);

                Com.Net.sendMsg(_clientgui.com.GetStream(), new Com.Message(Com.Message.Header.POST, data));
                textBox1.Text = String.Empty;
                /*msg = Com.Net.rcvMsg(_clientgui.com.GetStream());

                if (msg._head == Com.Message.Header.ERROR)
                {

                    textBox1.Text = String.Empty;
                }*/


            }
            else
            {
                textBox1.Text = String.Empty;
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

       public void updatetext (String s)
        {
            textBox2.Text += s + "\r\n";
        }

        public void clearListUser()
        {
            listBox1.BeginUpdate();

            listBox1.Items.Clear();
            listBox1.EndUpdate();
        }


        public void updatelistusers(String s)
        {
            listBox1.BeginUpdate();
    
                listBox1.Items.Add(s);
            listBox1.EndUpdate();
        }


        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
