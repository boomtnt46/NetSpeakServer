using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using NetSpeakServer;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace NetSpeakServer
{
    public partial class ServerScreen : Form
    {
        public ServerScreen()
        {
            InitializeComponent();
            this.FormClosing += OnExit;
            new Setup().ShowDialog();
            Server();
        }

        private void OnExit(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void ServerScreen_Load(object sender, EventArgs e)
        {
            if (Global.Control.isAnonymous)
            {
                if (!Utils.XmlHandler.UserExists("Anonymous"))
                    Utils.XmlHandler.AddAnonymousUser();
            }
            else
            {
                if (Utils.XmlHandler.UserExists("Anonymous"))
                    Utils.XmlHandler.DeleteUser("Anonymous");
            }

        }

        private void Server()
        {
            Global.Net.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Global.Net.listener.Bind(Global.Net.serverAddress);

            ThreadStart manager = new ThreadStart(ConnectionManager);
            Thread connectionManager = new Thread(manager);
            connectionManager.Start();

        }

        private void ConnectionManager()
        {
            Global.Net.listener.Listen(Global.Control.maxConnections);
            while (Global.Control.acceptNewConnections)
            {
                Socket newSocket = Global.Net.listener.Accept();
                Task.Factory.StartNew(() => { ConnectionHandler(newSocket); });
            }
        }
        private void ConnectionHandler(Socket client)
        {
            byte[] rawPackage = new byte[256];
            client.Receive(rawPackage);

            if (!Encoding.UTF8.GetString(rawPackage).Contains("registration"))
            {
                Global.Messages.Login loginPackage = new Global.Messages.Login();
                loginPackage = JsonConvert.DeserializeObject<Global.Messages.Login>(@Encoding.UTF8.GetString(rawPackage));

                if (Utils.XmlHandler.ValidateUser(loginPackage))
                {
                    Global.Messages.LoginResponse loginResponse = new Global.Messages.LoginResponse();
                    loginResponse.status = "OK";
                    loginResponse.loginMessage = "Welcome to the server! Read the rules before chatting!";

                    client.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(loginResponse)));

                    string info = loginPackage.username + " joined the server";
                    messages.Invoke(new MethodInvoker(delegate { messages.Items.Add(info); }));

                    users.Invoke(new MethodInvoker(delegate { users.Items.Add(loginPackage.username); }));
                    while (client.Connected)
                    {
                        try
                        {
                            rawPackage = new byte[4096];
                            client.Receive(rawPackage);

                            messages.Invoke(new MethodInvoker(delegate { messages.Items.Add(loginPackage.username + " : " + JsonConvert.DeserializeObject<Global.Messages.Message>(Encoding.UTF8.GetString(rawPackage)).message); }));
                        }
                        catch  (Exception e){ }
                    }
                }
                else
                {
                    Global.Messages.LoginResponse loginResponse = new Global.Messages.LoginResponse();
                    loginResponse.status = "USER NOT FOUND";
                    loginResponse.loginMessage = "User does not exists or bad login details";

                    client.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(loginResponse)));
                }
            }
            else if (Encoding.UTF8.GetString(rawPackage).Contains("registration"))
            {
                Global.Messages.Registration registrationPackage = new Global.Messages.Registration();
                registrationPackage = JsonConvert.DeserializeObject<Global.Messages.Registration>(Encoding.UTF8.GetString(rawPackage));

                Global.Messages.LoginResponse loginResponse = new Global.Messages.LoginResponse();
                //Check if the user alredy exists
                if (Utils.XmlHandler.UserExists(registrationPackage.username))
                {
                    loginResponse.status = "USER ALREDY EXISTS";
                    loginResponse.loginMessage = "This user alredy exists";
                }
                Utils.XmlHandler.RegisterUser(registrationPackage);
                loginResponse.status = "OK";
                loginResponse.loginMessage = "Welcome to the server! Read the rules before chatting!";

                client.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(loginResponse)));

                string info = registrationPackage.username + " + joined the server";
                messages.Invoke(new MethodInvoker(delegate { messages.Items.Add(info); }));
                users.Invoke(new MethodInvoker(delegate { users.Items.Add(registrationPackage.username); }));

            }


        }
    }
}
