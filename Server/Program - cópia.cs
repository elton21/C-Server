using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;

namespace MyServer
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Title = "SERVER";

            IPAddress myIp = IPAddress.Parse("192.168.1.70");
            Int32 port = 3000;
            Server server = new Server(myIp, port);


            server.startListening();
            Console.Write("Server Started ");

            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Waiting for connection");

            server.acceptClient();
            //Console.Clear();
            Console.WriteLine("Client Connected");


            string messageFromClient = "";
            string messageToClient = "";


            try
            {
                server.clientData();

                //while the server is on
                while (server.serverStatus)
                {
                    //As as the client connects
                    if (server.sockeForclient.Connected)
                    {
                        //expecting a message from the client
                        messageFromClient = server.streamReader.ReadLine();
                        Console.WriteLine("Client : " + messageFromClient);

                        if (messageFromClient == "exit")
                        {
                            // client can´t close socket connection, only the server
                            server.serverStatus = false;
                            server.streamReader.Close();
                            server.streamWriter.Close();
                            server.networkStream.Close();
                            return;
                        }

                        // if the client didn´t say no, now its my turn to talk
                        Console.Write("Server : ");
                        messageToClient = Console.ReadLine();
                        server.streamWriter.WriteLine(messageToClient);
                        server.streamWriter.Flush();
                    }
                }
                server.disconect();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
    }
}
