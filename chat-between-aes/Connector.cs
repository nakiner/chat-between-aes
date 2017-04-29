using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace chat_between_aes
{
    public class Connector
    {
        private UdpClient reciever;
        private UdpClient sender;
        private Thread thread;
        private Encryptor Encryptor = new Encryptor();

        private string broadcast = "255.255.255.255";
        private int port = 54545;
        private delegate void Message(string message);

        private void Catch()
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Any, 54545);
            Message message = Program.Instance.InsertMessage;
            while (true)
            {
                byte[] data = reciever.Receive(ref point);
                string decrypted = Encryptor.Decrypt(data, Program.Instance.key);
                Program.Instance.Invoke(message, decrypted);
            }
        }

        public void Connect()
        {
            ThreadStart start = new ThreadStart(Catch);
            sender = new UdpClient(broadcast, port);
            sender.EnableBroadcast = true;
            reciever = new UdpClient(port);
            thread = new Thread(start);
            thread.IsBackground = true;
            thread.Start();
        }

        public void Send(string message, string key)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            byte[] encrypted = Encryptor.Encrypt(data, key);
            sender.Send(encrypted, encrypted.Length);
        }
    }
}
