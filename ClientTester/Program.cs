using System;
using System.IO;
using System.Net.Sockets;

namespace ClientTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var tcp = new TcpClient("127.0.0.1", 8081);
            var stream = new StreamReader(tcp.GetStream());
            while (true)
            {
                Console.WriteLine(stream.ReadLine());
                /*
                var b = new byte[1];
                tcp.GetStream().Read(b, 0, 1);
                Console.Write(b[0] + " ");
                */
            }
        }
    }
}