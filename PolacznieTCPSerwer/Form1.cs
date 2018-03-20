using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolacznieTCPSerwer
{
    public partial class Form1 : Form
    {
        //pola prywatne
        private TcpListener serwer;
        private TcpClient klient;
        public Form1()
        {
            InitializeComponent();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            {
                IPAddress adresIP;
                try
                {
                    adresIP = IPAddress.Parse(tbHostAddress.Text);
                }
                catch
                {
                    MessageBox.Show("Błędny format adresu IP!", "Błąd");
                    tbHostAddress.Text = String.Empty;
                    return;
                }

                int port = System.Convert.ToInt16(numPort.Value);
                try
                {
                    serwer = new TcpListener(adresIP, port);
                    serwer.Start();
                    klient = serwer.AcceptTcpClient();
                    IPEndPoint IP = (IPEndPoint)klient.Client.RemoteEndPoint;
                    lbLogs.Items.Add("[" + IP.ToString() + "] :Nawiązano połączenie");
                    lbLogs.Items.Add("Nawiązano połączenie");
                    bStart.Enabled = false;
                    bStop.Enabled = true;
                    klient.Close();
                    serwer.Stop();
                }
                catch (Exception ex)
                {
                    lbLogs.Items.Add("Błąd inicjacji serwera!");
                    MessageBox.Show(ex.ToString(), "Błąd");
                }
            }
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            serwer.Stop();
            klient.Close();
            lbLogs.Items.Add("Zakończono pracę serwera ...");
            bStart.Enabled = true;
            bStop.Enabled = false;
        }
    }
}
