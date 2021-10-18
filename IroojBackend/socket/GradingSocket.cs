using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using IroojBackend.Models;

namespace IroojBackend.socket
{
    public class GradingSocket
    {
        private static TcpClient tcp;
        private Stream stream;
        private StreamReader reader;
        private StreamWriter writer;
        public GradingSocket()
        {
            tcp = new TcpClient("127.0.0.1", 8080);
            stream = tcp.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        }

        private void Write(string data)
        {
            writer.WriteLine(data.Length);
            writer.Flush();
            writer.Write(data);
            writer.Flush();
        }

        public string Read()
        {
            try
            {
                var dataSize = int.Parse(reader.ReadLine());
                var buffer = new char[dataSize + 10];
                reader.Read(buffer, 0, dataSize);
                var xmldoc = new XmlDocument();
                var s = new string(buffer);
                xmldoc.LoadXml(s);
                var judgeNumber = long.Parse(xmldoc.GetElementsByTagName("root")[0]["judge_number"].InnerText);
                DBModel.ApplyData(judgeNumber, s);
                return s;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }

        public void WriteGradingInfo(long timeLimit, long memoryLimit, long testCaseCount, string language, string code)
        {
            var xml = new XElement("root", new XElement("time_limit", timeLimit),
                new XElement("memory_limit", memoryLimit), 
                new XElement("test_case_count", testCaseCount),
                new XElement("language", language), 
                new XElement("code", code),
                new XElement("judge_number", DBModel.GetJudgeNumber()));
            Write(xml.ToString());
        }
    }
}