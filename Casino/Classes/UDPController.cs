using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Casino.Classes
{
    public class UDPController
    {
        public int Port { get => port; }

        public delegate void DelMessageReceived(string message, string senderIP);
        public event DelMessageReceived MessageReceived;

        private int port;
        private UdpClient udpClient;
        private bool receiv = false;
        private bool canreceiv = true;

        private Thread thread;

        public UDPController(int port)
        {
            this.port = port;
            this.udpClient = new UdpClient(port);
        }

        public void StartReceiver()
        {
            Thread thread = new Thread(() =>
            {
                receiv = true;

                while (receiv)
                {
                    if (canreceiv)
                    {
                        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, port);
                        byte[] data = udpClient.Receive(ref remoteEP);
                        MessageReceived?.Invoke(Encoding.UTF8.GetString(data, 0, data.Length), remoteEP.Address.ToString());

                        canreceiv = false;
                        StartTimer();
                    }
                }
            });

            this.thread = thread;

            thread.Start();
        }

        public void StopReveiver()
        {
            this.receiv = false;
            this.thread.Abort();
        }

        private void StartTimer()
        {
            try
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    Thread.Sleep(300);
                    this.canreceiv = true;
                }));
                thread.Start();
            }
            catch (Exception)
            {
                this.canreceiv = true;
            }
        }
    }
}
