/*
 * TCPEchoServer
 *
 * Author Michael Claudius, ZIBAT Computer Science
 * Version 1.0. 2014.02.10
 * Copyright 2014 by Michael Claudius
 * Revised 2014.09.01
 * All rights reserved
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TCPEchoServer
{
    class TCPEchoServer
    {

        public static void Main(string[] args)
        {

            TcpListener serverSocket = new TcpListener(6789);
            serverSocket.Start();
            Console.WriteLine($"Server Started");

            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                //Socket connectionSocket = serverSocket.AcceptSocket();
                Console.WriteLine("Server activated");

                EchoService theEchoService = new EchoService(connectionSocket);
                //Thread s1 = new Thread(theEchoService.DoIt);
                //s1.Start();
                Task.Factory.StartNew(() => theEchoService.DoIt());

            }

            serverSocket.Stop();
            Console.ReadKey();
        }
    }

}
