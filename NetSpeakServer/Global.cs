using System;
using System.Collections.Generic;
using System.Xml;
using DevOne.Security.Cryptography.BCrypt;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace NetSpeakServer
{
    static class Global
    {
        public const string hashedAnonymousPassword = @"$2a$06$CYws6yYnnGN.Xx75I7MLWOqpF9rW0Q3HkI1W.yMPImmnsA4C5wS5y";

        public static class Control
        {
            public static bool isAnonymous { get; set; }
            public static bool acceptNewConnections { get; set; } = true;
            public static int maxConnections { get; set; } = 100;
        }

        public class User
        {
            public string username { get; set; }
            public string password { get; set; }
            public bool banned { get; set; } = false;
            public string banReason { get; set; }
            public string bannedUntil { get; set; }
        }

        public class Net
        {
            public static System.Net.Sockets.Socket listener { get; set; }
            public static IPEndPoint serverAddress { get; set; }
            public static int port { get; set; }
        }
        public class Messages
        {
            public class Login
            {
                public string username { get; set; }
                public string password { get; set; }
            }
            public class LoginResponse
            {
                public string status { get; set; }
                public string loginMessage { get; set; }
            }
            public class Message
            {
                public string message { get; set; }
            }
            public class Registration
            {
                public string type = "registration";
                public string username { get; set; }
                public string password { get; set; }
            }
        }
    }

    static class Utils
    {
        

        
        public static class XmlHandler
        {
            public static XmlDocument xmldoc = new XmlDocument();
            public static XmlElement xmlusers;
            static string xmlFilePath = "users.xml";

            static XmlHandler()
            {
                if (File.Exists(xmlFilePath))
                {
                    xmldoc.Load(xmlFilePath);
                    xmlusers = (XmlElement)xmldoc.FirstChild;
                }
                else
                {
                    xmlusers = (XmlElement)xmldoc.AppendChild(xmldoc.CreateElement("Users"));
                    xmldoc.Save(xmlFilePath);
                }

            }

            public static bool ValidateUser(Global.Messages.Login login)
            {
                List<Global.User> users = LoadUsersFromXML();
                foreach (Global.User user in users)
                {
                    if (user.username == login.username && BCryptHelper.CheckPassword(login.password, user.password))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }

            public static bool UserExists(string username)
            {
                List<Global.User> users = LoadUsersFromXML();
                foreach (Global.User user in users)
                {
                    if (user.username == username)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }

            public static void AddAnonymousUser()
            {
                Global.User anonuser = new Global.User();
                anonuser.username = "Anonymous";
                anonuser.password = Global.hashedAnonymousPassword;
                anonuser.banned = false;
                anonuser.banReason = "";
                anonuser.bannedUntil = "";
                WriteUserToXML(anonuser);
            }

            public static void RegisterUser(Global.Messages.Registration package)
            {
                Global.User user = new Global.User();
                user.username = package.username;
                user.password = BCryptHelper.HashPassword(package.password, BCryptHelper.GenerateSalt());
                user.banned = false;
                user.banReason = "";
                user.bannedUntil = "";

                WriteUserToXML(user);
            }

            public static void WriteUserToXML(Global.User userObj)
            {
                XmlElement user = (XmlElement)xmlusers.AppendChild(xmldoc.CreateElement("User"));
                user.SetAttribute("username", userObj.username);
                user.SetAttribute("password", userObj.password);
                user.SetAttribute("banned", userObj.banned.ToString());
                user.SetAttribute("banReason", userObj.banReason);
                user.SetAttribute("bannedUntil", userObj.bannedUntil);
                xmldoc.Save(xmlFilePath);
            }

            public static List<Global.User> LoadUsersFromXML()
            {
                List<Global.User> list = new List<Global.User>();
                XmlNodeList userNodes = xmldoc.GetElementsByTagName("User");

                foreach (XmlNode xmlnode in userNodes)
                {

                    XmlAttributeCollection xmlAttrs = xmlnode.Attributes;
                    list.Add(new Global.User
                    {
                        username = xmlAttrs.GetNamedItem("username").InnerText,
                        password = xmlAttrs.GetNamedItem("password").InnerText,
                        banned = Boolean.Parse(xmlAttrs.GetNamedItem("banned").InnerText),
                        banReason = xmlAttrs.GetNamedItem("banReason").InnerText,
                        bannedUntil = xmlAttrs.GetNamedItem("bannedUntil").InnerText
                    });
                }
                return list;

            }

            public static void DeleteUser(string UserToRemove)
            {
                if (MessageBox.Show("Are you sure you want to delete " + UserToRemove + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

                XmlNode xmlnode = xmldoc.GetElementsByTagName("Users")[0];
                xmlnode.RemoveChild(xmlnode.SelectSingleNode("User[@username = '" + UserToRemove + "']"));
                xmldoc.Save(xmlFilePath);

                MessageBox.Show("User deleted", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                xmldoc.Save(xmlFilePath);
            }

            public static void EditUsernameOfUser(string originalUserName, XmlElement editedNode)
            {
                XmlNode xmlnode = xmldoc.GetElementsByTagName("Users")[0];
                if (xmlnode.SelectSingleNode("User[@username = '" + originalUserName + "']") == editedNode)
                {
                    MessageBox.Show("The user has not been edited because the new name is the same as the original", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                xmlusers.ReplaceChild(editedNode, xmlnode.SelectSingleNode("User[@username = '" + originalUserName + "']"));
                xmldoc.Save(xmlFilePath);
                MessageBox.Show("Username edited succesfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            public static void BanUser(string banReason, XmlElement editedNode, DateTime bannedUntil)
            {
                XmlNode xmlnode = xmldoc.GetElementsByTagName("Users")[0];

                xmlusers.ReplaceChild(editedNode, xmlnode.SelectSingleNode("User[@banReason = '" + banReason + "']"));
                xmldoc.Save(xmlFilePath);
                MessageBox.Show("User banned until " + bannedUntil.ToString() + "." + Environment.NewLine + "Ban reason: " + banReason, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }


}
