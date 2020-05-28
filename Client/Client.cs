using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Net;
namespace MyClient
{
    public class Client
    {
        public string myIp { get; private set; }
        public int port { get; private set; }
        private TcpClient socketForServer;
        public bool clientStatus = true;

        public NetworkStream networkStream { get; set; }
        public StreamReader streamReader { get; set; }
        public StreamWriter streamWriter { get; set; }

        public Client(string myIp, int port)
        {
            this.myIp = myIp;
            this.port = port;
        }


        public void ConnectToServer()
        {
            try
            {

                socketForServer = new TcpClient(myIp.ToString(), port);
            }
            catch
            {
                Console.WriteLine("Failed to connect to server");
                return;
            }
        }

        public void serverData()
        {
            networkStream = socketForServer.GetStream();
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);
        }


        public void disconect()
        {
            streamWriter.Close();
            networkStream.Close();
            streamWriter.Close();
            socketForServer.Close();
        }



    }
}
