using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace IroojBackend.socket
{
    public static class SocketMain
    {
        public static GradingSocket socket;
        private static List<StreamWriter> clients = new();
        public static void InitializeSocket()
        {
            socket = new GradingSocket();

            void Start()
            {
                while (true)
                {
                    var dat = socket.Read().Trim('\0');
                    if (dat == string.Empty) continue;
                    Console.WriteLine(dat);
                    var d = Encoding.UTF8.GetBytes(dat);
                    clients.ForEach(x =>
                    {
                        x.WriteLine(d.Length);
                        x.Flush();
                        x.BaseStream.Write(d);
                        x.Flush();
                    });
                }
            }

            void InitClientSocket()
            {
                bool IsSocketConnected(Socket s)
                {
                    return !((s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);
                }
                
                var client = new TcpListener(IPAddress.Any, 8081);
                client.Start();
                while (true)
                {
                    var clientSocket = client.AcceptSocket();
                    socket.WriteGradingInfo(1000, 512 * 1024, 2, "CPP", "#include <iostream>\nusing namespace std;\nsigned main(){int a, b;cin>>a>>b;cout<<a+b;return 0;}");
                    if (!clientSocket.Connected) continue;
                    var stream = new StreamWriter(new NetworkStream(clientSocket)); 
                    clients.Add(stream);
                    new Thread(() =>
                    {
                        while (true)
                        {
                            if (!IsSocketConnected(clientSocket)) break;
                        }

                        clients.Remove(stream);
                    }).Start();
                }
            }

            new Thread(Start).Start();
            new Thread(InitClientSocket).Start();
        }
    }
}