using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace MyServer
{
    public class Server
    {

        public IPAddress myIp { get; private set; }
        public int port { get; private set; }
        public bool serverStatus { get; set; }
        private TcpListener tcpListener { get; set; }
        public Socket sockeForclient { get; private set; }

        public NetworkStream networkStream { get; set;}
        public StreamReader streamReader { get; set;}
        public StreamWriter streamWriter { get; set;}



        public Server(IPAddress myIp, int port)
        {
            serverStatus = true;
            this.myIp = myIp;
            this.port = port;
        }

        public void startListening()
        {
            try
            {
                tcpListener = new TcpListener(myIp, port);
                tcpListener.Start();
            }
            catch
            {
                Console.Write("Could start listening");
            }
        }

        public void acceptClient()
        {
            try
            {
                sockeForclient = tcpListener.AcceptSocket();
            }
            catch
            {
                Console.WriteLine("couldnt accept client");
            }
        }


        //allows server to exchange data with the client
        public void clientData()
        {
            networkStream = new NetworkStream(sockeForclient);
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);
        }


        public void disconect()
        {
            networkStream.Close();
            streamReader.Close();
            streamWriter.Close();
            sockeForclient.Close();
        }

    }
}
