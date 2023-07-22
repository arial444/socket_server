using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SocketServer.MessageHandler;

namespace SocketServer
{
    // Socket Listener acts as a server and listens to the incoming
    // messages on the specified port and protocol.
    public class SocketListener
    {
        public static int Main(String[] args)
        {
            StartServer();
            return 0;
        }

        public static void StartServer()
        {
            // Get Host IP Address that is used to establish a connection
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1
            // If a host has multiple addresses, you will get a list of addresses
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = host.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 1080);

            try {
                while (true) {
                    // Create a Socket that will use Tcp protocol
                    Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    // A Socket must be associated with an endpoint using the Bind method
                    listener.Bind(localEndPoint);
                    // Specify how many requests a Socket can listen before it gives Server busy response.
                    // We will listen 10 requests at a time
                    listener.Listen(10);

                    Console.WriteLine("Waiting for a connection on: " + ipAddress);
                    Socket handler = listener.Accept();

                    // Incoming data from the client.
                    string? data = null;
                    byte[]? bytes = null;
                    int bytesRec = 0;

                    bytes = new byte[1024];
                    bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        IPEndPoint? remoteIpEndPoint = handler.RemoteEndPoint as IPEndPoint;
                        Console.WriteLine(remoteIpEndPoint + " is connected");           
                        byte[] msg = Encoding.ASCII.GetBytes("Please chose a number: ");
                        handler.Send(msg);
                        data = null;
                    }

                    while (handler.Connected)
                    {
                        bytes = new byte[1024];
                        bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        Console.WriteLine("Client Sent : {0}", data);
                        HandleMessage handleMessage = new HandleMessage(data);
                        byte[] msg = Encoding.ASCII.GetBytes(handleMessage.response);
                        handler.Send(msg);
                        data = null;
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }
    }
}