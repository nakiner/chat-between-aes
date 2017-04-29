using System;
using System.Windows.Forms;

namespace chat_between_aes
{
    public partial class Form1 : Form
    {
        public string key = "";
        private Connector Connection = new Connector();

        public Form1()
        {
            InitializeComponent();
        }

        public void InsertMessage(string message)
        {
            richTextBox1.Text += message;
            richTextBox1.Text += "\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length < 4 || textBox2.Text.Length < 4)
            {
                MessageBox.Show("Имя пользователя или пароль короткий.");
                return;
            }
            key = textBox2.Text;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox4.ReadOnly = false;
            button1.Enabled = false;
            Connection.Connect();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r' && textBox4.Text.Length > 0)
            {
                string message = textBox1.Text + ": " + textBox4.Text;
                string key = textBox2.Text;
                textBox4.Text = "";
                Connection.Send(message, key);
                e.Handled = true;
            }
        }
    }
}
