using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace All_Server
{
    public partial class UDPSocket : UserControl
    {
        public UDPSocket()
        {
            InitializeComponent();
        }
        private IPEndPoint ServerEndPoint = null;
        private UdpClient UDP_Server = new UdpClient();
        private System.Threading.Thread thdUpd;

        public delegate void DataArrivalEventHandler(byte[] Data, IPAddress Ip, int Port);
        public event DataArrivalEventHandler DataArrival;
        private string localHost = "127.0.0.1";
        [Browsable(true), Category("Local"), Description("本地IP地址")]
        private string LocalHost
        {
            get { return localHost; }
            set { localHost = value; }
        }
        private int localPort = 11000;
        [Browsable(true), Category("Local"), Description("本地端口号")]
        public int LocalPort
        {
            get { return LocalPort; }
            set { localPort = value; }
        }
        private bool active = false;
        [Browsable(true), Category("Local"), Description("激活监听")]
        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
                if (active)
                {
                    OpenSocket();
                }
                else
                {
                    CloseSocket();
                }
            }
        }
        private void OpenSocket()
        {
        }
        private void CloseSocket()
        {
            if (UDP_Server != null)
            {
                UDP_Server.Close();
            }
            if (thdUpd != null)
            {
                Thread.Sleep(30);
                thdUpd.Abort();
            }
        }
        protected void Listener()
        {
            ServerEndPoint = new IPEndPoint(IPAddress.Any, localPort);
            if (UDP_Server != null)
            {
                UDP_Server.Close();
            }
            UDP_Server = new UdpClient(localPort);
            try
            {
                thdUpd = new Thread(new ThreadStart(GetUDPData));
                thdUpd.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void GetUDPData()
        {
            while (active)
            {
                try
                {
                    byte[] Data = UDP_Server.Receive(ref ServerEndPoint);
                    if (DataArrival != null)
                    {
                        DataArrival(Data, ServerEndPoint.Address, ServerEndPoint.Port);
                    }
                    Thread.Sleep(0);
                }
                catch { }
            }
        }
        public void Send(System.Net.IPAddress Host, int Port, byte[] Data)
        {
            try
            {
                IPEndPoint server = new IPEndPoint(Host, Port);
                UDP_Server.Send(Data, Data.Length, server);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
