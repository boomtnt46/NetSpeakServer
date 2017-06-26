using System;
using System.Windows.Forms;
using System.Net;

namespace NetSpeakServer
{
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
            this.FormClosed += OnClose;
            useAdvancedLogin.Checked = true;
        }

        private void OnClose(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Setup_Load(object sender, EventArgs e)
        {

        }

        private void useAdvancedLogin_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void accept_Click(object sender, EventArgs e)
        {
            IPAddress serverIp;
            try { serverIp = IPAddress.Parse(ipTextBox.Text); }
            catch { MessageBox.Show("Invalid IP"); return; }
            try { Global.Net.serverAddress = new IPEndPoint(serverIp, int.Parse(portTextBox.Text)); }
            catch { MessageBox.Show("The maximun port is 65355"); return; }
            if (useAdvancedLogin.Checked) Global.Control.isAnonymous = true;
            this.Hide();
        }

    }
}
