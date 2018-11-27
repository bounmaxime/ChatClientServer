using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client;


namespace ChatGraphique
{
    public partial class Form1 : Form
    {
        Client.Client clientgui = new Client.Client("127.0.0.1", 81);
        ChatWindow chatwindow;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
                
        }

        private void textBox1_TextChanged(object text, EventArgs e)
        {
            //this.textBox1.Text += text;
        }

        private void textBox2_TextChanged(object text, EventArgs e)
        {
           // this.textBox2.Text += text;
        }

        private void button1_Click(object sender, EventArgs e)// connect button
        {
            chatwindow = new ChatWindow(clientgui);
            List<String> s = new List<string>();
            Com.Message msg = new Com.Message();
            /*if (textBox1.Text == "Maxime")
            {
                this.Hide();
            }
            else
            {
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
            }
         */
         if (textBox1.Text == String.Empty || textBox2.Text == String.Empty)
            {
                MessageBox.Show("Please fill all the fields");

                textBox1.Clear();
                textBox2.Clear();
            }
         else
            {
                s.Add(textBox1.Text);
                s.Add(textBox2.Text);

                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                Com.Net.sendMsg(clientgui.com.GetStream(), new Com.Message(Com.Message.Header.LOGIN, s));
                msg = Com.Net.rcvMsg(clientgui.com.GetStream());
                if (msg._data[0] == "True")
                {

                    this.Hide();
                    chatwindow.clientgui.username = s[0];
                    chatwindow.Show();

                }
                else
                {

                    MessageBox.Show("Failed to connect. Please check your username and password or register first");

                    textBox1.Clear();
                    textBox2.Clear();

                }
            }
  

        
        
          



        }

 

        private void button2_Click(object sender, EventArgs e)// registerbutton click
        {
            List<String> s = new List<string>();
            Com.Message msg = new Com.Message();

            if (textBox1.Text == String.Empty || textBox2.Text == String.Empty)
            {
                MessageBox.Show("Please fill all the fields");

                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {

                s.Add(textBox1.Text);
                s.Add(textBox2.Text);
                Com.Net.sendMsg(clientgui.com.GetStream(), new Com.Message(Com.Message.Header.REGISTER, s));
                msg = Com.Net.rcvMsg(clientgui.com.GetStream());

                if (msg._data[0] == "True")
                {


                    MessageBox.Show("User registered!");
                    textBox1.Clear();
                    textBox2.Clear();

                }
                else
                {

                    MessageBox.Show("User already exists!");
                    textBox1.Clear();
                    textBox2.Clear();

                }

            }


        }

    }
}

