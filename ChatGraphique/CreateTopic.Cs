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
    public partial class CreateTopic : Form
    {

        Client.Client _clientgui;
        ChatWindow chatwindow;
        TopicsManager _t;

        public CreateTopic()
        {
            InitializeComponent();
        }
        public CreateTopic(Client.Client clientgui, TopicsManager t)
        {
            _clientgui = clientgui;
            InitializeComponent();
            _t = t;
                        
          
        }

        private void CreateTopic_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             List<String> s = new List<string>();
            Com.Message msg = new Com.Message();
            bool topicadded = false;
            while (!topicadded)
            {
                s.Add(textBox1.Text);

                Com.Net.sendMsg(_clientgui.com.GetStream(), new Com.Message(Com.Message.Header.CREATE_TOPIC, s));
                msg = Com.Net.rcvMsg(_clientgui.com.GetStream());

                if (msg._head == Com.Message.Header.ERROR)
                {
                    textBox1.Text = String.Empty;
                    topicadded = false;



                }
                else
                {
                    topicadded = true;
                  
                    _t.listbox.BeginUpdate();
                    _t.listbox.Items.Add(textBox1.Text);
                    _t.listbox.EndUpdate();

                    this.Hide();

                }
            }

          
            



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
