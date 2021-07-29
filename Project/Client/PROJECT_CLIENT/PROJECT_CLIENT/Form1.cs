using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_CLIENT
{
    /*public List<string> GenenrateFriendList(string serverMessage) //will generate a list of friend names where the incoming message from server is lik ",friend 1,friend 2,friend 3,
    {

    }*/

    public partial class Form1 : Form
    {
        bool terminating = false;
        bool connected = false;
        Socket clientSocket;
        string[] userNames = System.IO.File.ReadAllLines("user_db.txt");
        

        public Form1()
        {       
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            terminating = false;
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = txt_IP.Text;
            string clientName = txt_Name.Text;

            if (IP != "") // If IP is not empty
            {
                int portNum;
                if (Int32.TryParse(txt_Port.Text, out portNum))
                {
                    try
                    {
                        clientSocket.Connect(IP, portNum); //The first parameter(string) is the name of the remote host.
                                                           //The second parameter(int) is the port number of the remote host.
                                                           //If you are using a connection-oriented protocol such as TCP, 
                                                           //the Connect method synchronously establishes a network 
                                                           //connection between LocalEndPoint and the specified remote host.

                        Byte[] buffer = new Byte[64];
                        buffer = Encoding.Default.GetBytes(clientName);
                        clientSocket.Send(buffer); //Sending the name of the client for servert to check before establishing the connection


                        Byte[] buffer1 = new Byte[64];
                        clientSocket.Receive(buffer1); //If no data is available for reading, the Receive 
                                                       //method will block until data is available

                        string incomingResponse = Encoding.Default.GetString(buffer1);
                        incomingResponse = incomingResponse.Substring(0, incomingResponse.IndexOf("\0"));

                        if (incomingResponse == "CONNECTED")
                        {
                            btn_Connect.Enabled = false;
                            txt_Name.Enabled = false;
                            txt_IP.Enabled = false;
                            txt_Port.Enabled = false;

                            btn_Disconnect.Enabled = true;
                            txt_Message.Enabled = true;
                            btn_Send.Enabled = true;
                            btn_SendRequest.Enabled = true;
                            btn_SendResponse.Enabled = true;
                            btn_ShowFriends.Enabled = true;
                            btn_RemoveFriend.Enabled = true;
                            btn_SendToFriends.Enabled = true;


                            connected = true;
                            log.AppendText("Connected to the server!\n");
                            Thread receiveThread = new Thread(Receive);
                            receiveThread.Start();
                        }

                        else if (incomingResponse == "NOT_CONNECTED")
                        {
                            log.AppendText("You are either already connected or not in the database.\n");
                            clientSocket.Close();
                        }
                    }

                    catch
                    {
                        log.AppendText("Could not connect to the server!\n");
                    }
                }

                else
                {
                    log.AppendText("Your port number must be an integer!\n");
                }
            }

            else
            {
                log.AppendText("IP should not be empty!\n");
            }

            
        }

        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new byte[64];
                    clientSocket.Receive(buffer); //If no data is available for reading, the Receive 
                                                  //method will block until data is available

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    char type = incomingMessage[0];

                    if (type == '?') //Then it is a friend request
                    {
                        string requestLine = incomingMessage.Substring(1);
                        log.AppendText("You have friendship request(s)!\n");
                        cb_RequestLine.Items.Add(requestLine);
                        //cb_RequestLine.SelectedIndex = 0;
                    }

                    else if (type == '@') //then server send the friend list
                    {
                        incomingMessage = incomingMessage.Substring(1);
                        type = incomingMessage[0];

                        if (type == '-') //then client has no friends
                        {
                            incomingMessage = incomingMessage.Substring(1);
                            log.AppendText(incomingMessage + "\n");

                            cb_CurrentFriends.DataSource = null;
                            cb_CurrentFriends.SelectedItem = null;
                        }

                        else if (type == '+')
                        {
                            string friends = incomingMessage.Substring(1, incomingMessage.Length - 2);
                            string[] friendList = friends.Split(',');

                            log.AppendText("\nMY FRINENDS:\n");

                            for (int i = 0; i < friendList.Length; i++)
                            {
                                log.AppendText("-" + friendList[i] + "\n");
                            }
                            log.AppendText("\n");

                            cb_CurrentFriends.DataSource = friendList;
                            cb_CurrentFriends.SelectedItem = null;
                        }
                    }

                    else //It is a broadcast message
                    {
                        log.AppendText(incomingMessage + "\n");
                    }                   
                }

                catch
                {
                    if (!terminating)
                    {
                        log.AppendText("The server has disconnected!\n");
                        btn_Connect.Enabled = true;
                        txt_Port.Enabled = true;
                        txt_IP.Enabled = true;
                        txt_Name.Enabled = true;                     
                        
                        btn_Disconnect.Enabled = false;
                        txt_Message.Enabled = false;
                        btn_Send.Enabled = false;
                        btn_SendRequest.Enabled = false;
                        btn_SendResponse.Enabled = false;
                        btn_ShowFriends.Enabled = false;
                        btn_RemoveFriend.Enabled = false;
                        btn_SendToFriends.Enabled = false;
                    }

                    clientSocket.Close();
                    connected = false;
                }
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            string message = txt_Message.Text;

            if (message != "" && message.Length <= 64)
            {
                Byte[] buffer = new Byte[64];
                buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);

                log.AppendText("You: " + message + "\n");

                txt_Message.Text = "";
            }

            else
            {
                log.AppendText("You cannot send empty messages or messages longer than 64 bytes!\n");
            }
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            connected = false;
            terminating = true;

            clientSocket.Close();
            log.AppendText("Disconnected from the server!\n");

            txt_IP.Enabled = true;
            txt_Port.Enabled = true;
            txt_Name.Enabled = true;
            btn_Connect.Enabled = true;

            btn_Disconnect.Enabled = false;
            txt_Message.Enabled = false;
            btn_Send.Enabled = false;
            btn_SendRequest.Enabled = false;
            btn_SendResponse.Enabled = false;
            btn_ShowFriends.Enabled = false;
            btn_RemoveFriend.Enabled = false;
            btn_SendToFriends.Enabled = false;

            cb_RequestLine.DataSource = null;
            cb_RequestLine.SelectedItem = null;
            cb_RequestLine.Items.Clear();
            cb_CurrentFriends.DataSource = null;
            cb_CurrentFriends.SelectedItem = null;
            cb_CurrentFriends.Items.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            combobox1.DataSource = userNames;
            combobox1.SelectedItem = null;
            combobox1.SelectedText = "-- Select User --";
        }

        private void btn_SendRequest_Click(object sender, EventArgs e)
        {
            if (combobox1.SelectedItem != null)
            {
                string selectedName = combobox1.SelectedItem.ToString();
                string clientName = txt_Name.Text;
                string message = "?" + clientName + " > " + selectedName;
                if(selectedName != clientName)
                {
                    Byte[] buffer = new Byte[64];
                    buffer = Encoding.Default.GetBytes(message);
                    clientSocket.Send(buffer);
                    log.AppendText("Trying to send a friend request to " + selectedName + "\n");
                }
                else
                {
                    log.AppendText("You can't send a request to yourself!\n");
                }               
            }

            else
            {
                log.AppendText("Please select a user to send request!\n");
            }
            
        }

        private void btn_SendResponse_Click(object sender, EventArgs e)
        {
            if(cb_RequestLine.SelectedItem != null)
            {
                string reqLine = cb_RequestLine.SelectedItem.ToString();
                string invitee = txt_Name.Text;
                string inviter = reqLine.Substring(0, reqLine.IndexOf(","));

                bool accept = rBtn_Accept.Checked;
                bool reject = rBtn_Reject.Checked;

                if (accept) //sending server ack
                {
                    string notification = "*" + inviter + ">" + invitee + ":ACCEPTED";
                    Byte[] buffer = new Byte[64];
                    buffer = Encoding.Default.GetBytes(notification);
                    clientSocket.Send(buffer);
                    cb_RequestLine.Items.Remove(reqLine);
                    cb_RequestLine.SelectedItem = null;
                    cb_RequestLine.Text = "";
                }
                if (reject)
                {
                    string notification = "*" + inviter + ">" + invitee + ":REJECTED";
                    Byte[] buffer = new Byte[64];
                    buffer = Encoding.Default.GetBytes(notification);
                    clientSocket.Send(buffer);
                    cb_RequestLine.Items.Remove(reqLine);
                    cb_RequestLine.SelectedItem = null;
                    cb_RequestLine.Text = "";
                }
                if(accept == false && reject == false)
                {
                    log.AppendText("You should select a response in order to send it.\n");
                }
            }
            else
            {
                log.AppendText("You should select someone to send response!\n");
            }
        }

        private void btn_ShowFriends_Click(object sender, EventArgs e)
        {
            string frinedlistRequest = "@";
            Byte[] buffer = new Byte[64];
            buffer = Encoding.Default.GetBytes(frinedlistRequest);
            clientSocket.Send(buffer);

        }

        private void btn_RemoveFriend_Click(object sender, EventArgs e)
        {
            if (cb_CurrentFriends.SelectedItem != null)
            {
                string friendToBeRemoved = cb_CurrentFriends.SelectedItem.ToString();
                string messageToServer = "_" + friendToBeRemoved;
                Byte[] buffer = new Byte[64];
                buffer = Encoding.Default.GetBytes(messageToServer);
                clientSocket.Send(buffer);
                cb_RequestLine.Items.Remove(friendToBeRemoved);
                cb_RequestLine.SelectedItem = null;
                cb_RequestLine.Text = "";
            }

            else
            {
                log.AppendText("You should select a friend in order to remove. If your current friend list is empty or not up to date click on \"Show My Friends\" button\n");
            }
        }

        private void btn_SendToFriends_Click(object sender, EventArgs e)
        {
            string message = txt_Message.Text;

            if (message != "" && message.Length < 64)
            {
                log.AppendText("You try to send \"" + message + "\" to your friends only\n");
                txt_Message.Text = "";

                message = "=" + message;
                Byte[] buffer = new Byte[64];
                buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);
            }

            else
            {
                log.AppendText("You cannot send empty messages or messages longer than 64 bytes!\n");
            }

        }
    }
}
