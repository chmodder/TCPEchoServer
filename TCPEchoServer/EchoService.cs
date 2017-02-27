using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPEchoServer
{
    class EchoService
    {
        private TcpClient _connectionSocket;

        public TcpClient ConnectionSocket
        {
            get { return _connectionSocket; }
            set { _connectionSocket = value; }
        }

        public EchoService(TcpClient connectionSocket)
        {
            _connectionSocket = connectionSocket;
        }

        public void DoIt()
        {
            try
            {
                Stream ns = _connectionSocket.GetStream();
                // Stream ns = new NetworkStream(connectionSocket);

                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true; // enable automatic flushing

                sw.WriteLine("Connection established");

                string message = sr.ReadLine();
                string answer = "";


                while (message != null && message != "")
                {
                    Console.WriteLine("Client: " + message);
                    answer = message.ToUpper();
                    if (answer == "STOP")
                    {
                        answer = "SERVER STOPPED";
                        sw.WriteLine(answer);
                        message = sr.ReadLine();
                        break;
                    }
                    sw.WriteLine(answer);
                    message = sr.ReadLine();
                }

                _connectionSocket.Close();
                ns.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }
}
