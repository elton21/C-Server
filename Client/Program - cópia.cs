using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading;


namespace MyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string myIp = "192.168.1.70";
            int port = 3000;

            Client client = new Client(myIp, port);

            client.ConnectToServer();
            Console.Write("Connected to server");
            Thread.Sleep(1000);
            Console.Clear();

         

            Console.Clear();

            client.serverData();
            try
            {
                string messageToServer = "";
                string messageFromServer = "";

                while (client.clientStatus)
                {
                    Console.Write("Client");
                    messageToServer = Console.ReadLine();

                    if (messageToServer == "exit")
                    {
                        client.clientStatus = false;
                        client.streamWriter.WriteLine("Bye");
                        client.streamWriter.Flush();
                    }
                    if (messageToServer != "bye")
                    {
                        client.streamWriter.WriteLine(messageToServer);
                        client.streamWriter.Flush();
                        messageFromServer = client.streamReader.ReadLine();
                        Console.WriteLine("Server : " + messageFromServer);
                    }
                }
            }
            catch
            {
                Console.WriteLine("problem reading from sercer");
            }

            client.disconect();

        }
    }
}
