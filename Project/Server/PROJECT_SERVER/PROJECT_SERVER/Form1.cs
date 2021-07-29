using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_SERVER
{
    public partial class Form1 : Form
    {
        struct User
        {
            public Socket client;
            public string name;
        }

        struct Request
        {
            public string inviter;
            public string invitee;
        }
 
        struct FriendListNode
        {
            public string clientName;
            public List<string> friendList;
        }

        struct NotificationNode
        {
            public string inviter;
            public string invitee;
            public string response;
        }
        struct PrivateMessageNode
        {
            public string sender;
            public string receiver;
            public string message;
        }


        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<User> clientUsers = new List<User>(); //clientSockets will hold the sockets of CONNECTED clients.
        List<Request> requestList = new List<Request>(); //this list will hold sent frinedship requests to the server. These will be sent to invitees
        List<Request> unrespondedRequestList = new List<Request>(); 
        List<NotificationNode> notificationList = new List<NotificationNode>();
        List<Request> removingFriendRequestList = new List<Request>();
        List<PrivateMessageNode> privateMessageList = new List<PrivateMessageNode>();

        public bool IsExistInClientUsers(string name)
        {
            bool result = false;
            foreach(User user in clientUsers)
            {
                if(user.name == name)
                {
                    result = true;
                }
            }
            return result;
        }

        public Socket GetClientSocket(string name)
        {
            foreach(User u in clientUsers)
            {
                if (u.name == name)
                {
                    return u.client;
                }
            }

            return null;
        }

        /*public bool IsExistInRequestList(string inviter, string invitee)
        {

            foreach (Request req in requestList)
            {
                if(req.inviter == inviter && req.invitee == invitee)
                {
                    return true;
                }
            }
            return false;
        }*/

        public bool IsExistInUnRespondedRequestList(string inviter, string invitee)
        {

            foreach (Request req in unrespondedRequestList)
            {
                if ((req.inviter == inviter && req.invitee == invitee) || (req.inviter == invitee && req.invitee == inviter))
                    //If A sends a request to B, B also should not send another request to A while pending
                {
                    return true;
                }
            }
            return false;
        }

        public int IndexOfUserInFriendList(string name)
        {
            for (int i = 0; i < UsersFriendList.Count(); i++)
            {
                if(name == UsersFriendList[i].clientName)
                {
                    return i;
                }
            }

            return -1;
        }

        public bool AreAlredyFriends(string client1, string client2)
        {
            int index = IndexOfUserInFriendList(client1);

            for (int i = 0; i < UsersFriendList[index].friendList.Count(); i++)
            {
                if(UsersFriendList[index].friendList[i] == client2) //then they are friends
                {
                    return true;
                }
            }
            return false;
        }

        public bool DidClientLeaveUnrespondedRequest(string clientName)
        {
            foreach(Request req in unrespondedRequestList)
            {
                if (req.invitee == clientName) //clientName has an unresponded request
                {
                    return true;
                }
            }

            return false;
        }

        bool terminating = false;
        bool listening = false;

        

        string[] userNames = System.IO.File.ReadAllLines("user_db.txt");
        List<FriendListNode> UsersFriendList = new List<FriendListNode>();

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_Closing);
            InitializeComponent();
        }

        private void btn_Listen_Click(object sender, EventArgs e)
        {
            int ServerPort;

            if (Int32.TryParse(txt_Port.Text, out ServerPort)) //If the first parameter(text) converted successfully,
                                                               //second parameter becomes the integer form of first parameter.
                                                               //If first parameter is not something convertable, returns false.
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, ServerPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3); //3 = the maximum length of the pending(bekleyen) connections queue.

                listening = true;
                btn_Listen.Enabled = false;

                Thread acceptedThread = new Thread(Accept);
                acceptedThread.Start();

                Thread continuousCheck = new Thread(CheckRequest);
                continuousCheck.Start();

                log.AppendText("Started Listening on port: " + ServerPort + "\n");
            }

            else
            {
                log.AppendText("Please check the port number \n");
            }
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept(); //Sockets in C# are in blocking mode by default. So, this
                                                              //method blocks until an incoming connection attempt is queued
                                                              //Once a connection is accepted, the original(our serverSocket in this case) Socket continues 
                                                              //queuing incoming connection requests until you close it.
                    
                    Byte[] buffer = new Byte[64];
                    newClient.Receive(buffer); //If no data is available for reading, the Receive 
                                                //method will block until data is available

                    string incomingName = Encoding.Default.GetString(buffer);
                    incomingName = incomingName.Substring(0, incomingName.IndexOf("\0"));

                    //CONDITIONS
                    if (userNames.Contains(incomingName) && !IsExistInClientUsers(incomingName))
                    {
                        User newUser;
                        newUser.name = incomingName;
                        newUser.client = newClient;
                        //clientUsers.Add(newUser);
                        //log.AppendText(incomingName + " is connected.\n");

                        //Server should send an acknowledgement
                        string check = "CONNECTED";
                        Byte[] buffer1 = new Byte[64];
                        buffer1 = Encoding.Default.GetBytes(check);
                        newClient.Send(buffer1);
                        
                        clientUsers.Add(newUser);
                        lock(log)
                        {
                            log.AppendText(incomingName + " is connected.\n");
                        }
                        

                        Thread receiveThread = new Thread(Receive);
                        receiveThread.Start();

                        //Thread continuousCheck = new Thread(CheckRequest);
                        //continuousCheck.Start();
                    }

                    else
                    {
                        //Server should send something in order to make client undertand that it is not connected
                        string check = "NOT_CONNECTED";
                        Byte[] buffer1 = new Byte[64];
                        buffer1 = Encoding.Default.GetBytes(check);
                        newClient.Send(buffer1);
                        newClient.Disconnect(false);
                    }
                    
                }

                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }

                    else
                    {
                        log.AppendText("The socket stopped working! \n");
                    }
                }
            }
        }

        private void Receive()
        {
            User thisUser = clientUsers[clientUsers.Count() - 1];
            Socket thisClient = thisUser.client;
            bool connected = true;

            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    thisClient.Receive(buffer); //If no data is available for reading, the Receive 
                                                //method will block until data is available

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0")); //"\0" represents null character. Marks the end of the string.

                    char type = incomingMessage[0];

                    if (type == '?') //Then it is a friend request
                    {
                        string inviter = incomingMessage.Substring(1, incomingMessage.IndexOf(">") - 2);
                        string invitee = incomingMessage.Substring(incomingMessage.IndexOf(">") + 2);
                        if (AreAlredyFriends(inviter, invitee) == true)
                        {
                            string MSG = "SERVER: You have already added " + invitee + " as a friend!";
                            Byte[] buffer2 = new Byte[64];
                            buffer2 = Encoding.Default.GetBytes(MSG);
                            GetClientSocket(inviter).Send(buffer2);
                        }
                        else
                        {
                            lock (log)
                            {
                                log.AppendText(inviter + " tries to send a friend request to " + invitee + "\n");
                            }
                            Request req;
                            req.inviter = inviter;
                            req.invitee = invitee;

                            if (IsExistInUnRespondedRequestList(inviter, invitee) == false)
                            {
                                lock(requestList)
                                {
                                    string check = "SERVER: Your friendship request has taken into consideration!";
                                    Byte[] buffer1 = new Byte[64];
                                    buffer1 = Encoding.Default.GetBytes(check);
                                    thisClient.Send(buffer1);

                                    requestList.Add(req);
                                    unrespondedRequestList.Add(req);
                                }    
                            }
                            else
                            {
                                log.AppendText("The request of " + inviter + " to " + invitee + " could not be sent since it is unresponded!\n");
                                string check = "SERVER: That request is pending!";
                                Byte[] buffer1 = new Byte[64];
                                buffer1 = Encoding.Default.GetBytes(check);
                                thisClient.Send(buffer1);
                            }
                        }
                    }

                    else if(type == '*') //someone sends a response to a friend request
                    {
                        string inviter = incomingMessage.Substring(1, incomingMessage.IndexOf(">") - 1);
                        int len = incomingMessage.IndexOf(":") - 1 - incomingMessage.IndexOf(">");
                        string invitee = incomingMessage.Substring(incomingMessage.IndexOf(">") + 1, len);
                        string response = incomingMessage.Substring(incomingMessage.IndexOf(":") + 1);

                        if(response == "ACCEPTED") //The friendship request has been accepted
                        {
                            int index = IndexOfUserInFriendList(inviter);
                            UsersFriendList[index].friendList.Add(invitee);

                            index = IndexOfUserInFriendList(invitee);
                            UsersFriendList[index].friendList.Add(inviter);

                            NotificationNode ntN;
                            ntN.inviter = inviter;
                            ntN.invitee = invitee;
                            ntN.response = response;

                            notificationList.Add(ntN);
                        }

                        else if (response == "REJECTED") //The friendsip request has been rejected
                        {
                            /*string check = invitee + " has rejected your friendship request!";
                            Byte[] buffer1 = new Byte[64];
                            buffer1 = Encoding.Default.GetBytes(check);
                            GetClientSocket(inviter).Send(buffer1);*/
                            NotificationNode ntN;
                            ntN.inviter = inviter;
                            ntN.invitee = invitee;
                            ntN.response = response;

                            notificationList.Add(ntN);
                        }
                    }

                    else if (type == '@') //somebody asks his/her friend list
                    {
                        string inviter = thisUser.name;
                        int index = IndexOfUserInFriendList(inviter);
                        string message = "";
                        if (UsersFriendList[index].friendList.Count() == 0)
                        {
                            message = "@-YOU HAVE NO FRIENDS!!";
                        }

                        else
                        {
                            message = "@+";

                            for (int i = 0; i < UsersFriendList[index].friendList.Count(); i++)
                            {
                                message = message + UsersFriendList[index].friendList[i] + ",";
                            }
                        }

                        Byte[] buffer1 = new Byte[64];
                        buffer1 = Encoding.Default.GetBytes(message);
                        thisClient.Send(buffer1);

                    }

                    else if(type == '_') //Then someone tries to remove his/her friend
                    {
                        string removerClient = thisUser.name;
                        string clientToBeRemoved = incomingMessage.Substring(1);

                        int index = IndexOfUserInFriendList(removerClient);
                        int index2 = IndexOfUserInFriendList(clientToBeRemoved);

                        lock (log)
                        {
                            log.AppendText(removerClient + " tries to remove " + clientToBeRemoved + " from his/her friend list.\n");
                        }

                        if (AreAlredyFriends(removerClient, clientToBeRemoved)) //deletion should be done, if they are already friends.
                        {
                            //They should remove EACH OTHER. So, we should delete from both lists.
                            UsersFriendList[index].friendList.Remove(clientToBeRemoved);
                            UsersFriendList[index2].friendList.Remove(removerClient);

                            //For sending the acknowledgement. We add it to a list.
                            Request req;
                            req.inviter = removerClient;
                            req.invitee = clientToBeRemoved;
                            removingFriendRequestList.Add(req);

                            string message = ""; //New friend list is being sent back to the user. In order to update the combo box
                            if (UsersFriendList[index].friendList.Count() == 0)
                            {
                                message = "@-YOU HAVE NO FRIENDS!!";
                            }

                            else
                            {
                                message = "@+";

                                for (int i = 0; i < UsersFriendList[index].friendList.Count(); i++)
                                {
                                    message = message + UsersFriendList[index].friendList[i] + ",";
                                }
                            }

                            Byte[] buffer1 = new Byte[64];
                            buffer1 = Encoding.Default.GetBytes(message);
                            thisClient.Send(buffer1);
                        }

                        else
                        {
                            string message = "YOU ARE NOT FRIENDS WITH THE PERSON YOU ARE DELETING!";
                            
                            Byte[] buffer1 = new Byte[64];
                            buffer1 = Encoding.Default.GetBytes(message);
                            thisClient.Send(buffer1);
                        }
                       
                    }

                    else if (type == '=') //Then a client tries to send a private message to his/her friends
                    {
                        string message = incomingMessage.Substring(1);
                        string sender = thisUser.name;
                        int index = IndexOfUserInFriendList(sender);

                        lock (log)
                        {
                            log.AppendText(sender + " tries to send \"" + message + "\" to his/her friends only.\n");
                        }

                        if (UsersFriendList[index].friendList.Count() == 0) //then client has no friends to send the message.
                        {
                            string ack = "YOU HAVE NO FRIENDS TO SEND THE MESSAGE!";
                            Byte[] buffer1 = new Byte[64];
                            buffer1 = Encoding.Default.GetBytes(ack);
                            thisClient.Send(buffer1);
                        }

                        else //Client has firends. Send the message to them only.
                        {
                            for (int i = 0; i < UsersFriendList[index].friendList.Count(); i++)
                            {
                                PrivateMessageNode pm;
                                pm.sender = sender;
                                pm.receiver = UsersFriendList[index].friendList[i];
                                pm.message = message;

                                privateMessageList.Add(pm);
                            }
                        }
                    }

                    else //Then it is a broadcast message
                    {
                        lock (log)
                        {
                            log.AppendText(thisUser.name + " sends \"" + incomingMessage + "\"\n");
                        }
                        
                        foreach (User u in clientUsers)
                        {
                            if (u.name != thisUser.name)
                            {
                                string MSG = thisUser.name + ": " + incomingMessage;
                                Byte[] buffer2 = new Byte[64];
                                buffer2 = Encoding.Default.GetBytes(MSG);
                                u.client.Send(buffer2);
                            }
                        }
                    }   

                }
                catch//Socket.Receive will send ObjectDisposedException if Socket has been closed.
                {
                    if (!terminating) //Server is not terminating. So, client socket has to be closed.
                    {
                        lock (log)
                        {
                            log.AppendText(thisUser.name + " has disconnected!\n");
                        }
                        //After a client disconnected, server should check if he/she left an unresponded request. If a client left an
                        //unresponded request, server should add it to requestList, so that it can be re sent when client becomes online again.

                        if (DidClientLeaveUnrespondedRequest(thisUser.name))
                        {
                            foreach(Request req in unrespondedRequestList)
                            {
                                if(req.invitee == thisUser.name)
                                {
                                    Request r;
                                    r.inviter = req.inviter;
                                    r.invitee = req.invitee;

                                    requestList.Add(r);
                                }
                            }
                        }

                        foreach (User u in clientUsers)
                        {
                            if (u.name != thisUser.name)
                            {
                                string MSG = thisUser.name + " has disconnected!";
                                Byte[] bfr = new Byte[64];
                                bfr = Encoding.Default.GetBytes(MSG);
                                u.client.Send(bfr);
                            }
                        }
                    }

                    thisClient.Close();
                    clientUsers.Remove(thisUser);
                    connected = false;

                }
            }
        }

        private void CheckRequest()
        {
            //bool connected = true;
            while (listening)
            {
                try
                {
                    if (requestList.Count() != 0)
                    {
                        int size = requestList.Count();
                        for(int i = 0; i < size; i++)
                        {
                            string inviteeName = requestList[i].invitee;

                            if (IsExistInClientUsers(inviteeName) == true) //invitee is online, we should send the request to it.
                            {
                                string MSG = "?" + requestList[i].inviter + ", has sent you a friend request!\n";
                                Byte[] bfr = new Byte[64];
                                bfr = Encoding.Default.GetBytes(MSG);
                                GetClientSocket(inviteeName).Send(bfr);
                                lock (log)
                                {
                                    log.AppendText("Request of " + requestList[i].inviter + " has been sent to " + requestList[i].invitee + "\n");
                                }
                                
                                requestList.Remove(requestList[i]); //remove the sent request from the list.
                            }
                        }
                    }
                    
                    if(notificationList.Count() != 0)
                    {
                        int size = notificationList.Count();
                        for(int i = 0; i < size; i++)
                        {
                            string inviterName = notificationList[i].inviter;

                            if(IsExistInClientUsers(inviterName) == true) //inviter is online we should send the notification
                            {
                                string MSG = "";

                                if(notificationList[i].response == "REJECTED")
                                {
                                    MSG = notificationList[i].invitee + " has rejected your friendship request!";
                                }

                                else if(notificationList[i].response == "ACCEPTED")
                                {
                                    MSG = notificationList[i].invitee + " has accepted your friendship request!";
                                }

                                Byte[] bfr = new Byte[64];
                                bfr = Encoding.Default.GetBytes(MSG);
                                GetClientSocket(inviterName).Send(bfr);

                                Request toBeRemoved;
                                toBeRemoved.invitee = notificationList[i].invitee;
                                toBeRemoved.inviter = inviterName;
                                unrespondedRequestList.Remove(toBeRemoved); //Mark that request as responded

                                notificationList.Remove(notificationList[i]); //remove the sent notification from the list!
                            }
                        }
                    }

                    if(removingFriendRequestList.Count() != 0)
                    {
                        int size = removingFriendRequestList.Count();

                        for (int i = 0; i < size; i++)
                        {
                            string removedFriend = removingFriendRequestList[i].invitee;
                            string removerFriend = removingFriendRequestList[i].inviter;
                            
                            if(IsExistInClientUsers(removedFriend)) //If removed friend is online, notify him/her.
                            {
                                string message = removerFriend + " has removed you from his/her friend list.";
                                Byte[] bfr = new Byte[64];
                                bfr = Encoding.Default.GetBytes(message);
                                GetClientSocket(removedFriend).Send(bfr);

                                Request toBeRemoved;
                                toBeRemoved.invitee = removedFriend;
                                toBeRemoved.inviter = removerFriend;
                                removingFriendRequestList.Remove(toBeRemoved);
                            }
                        }
                    }

                    if (privateMessageList.Count() != 0)
                    {
                        int size = privateMessageList.Count();

                        for(int i = 0; i < size; i++)
                        {
                            string sender = privateMessageList[i].sender;
                            string receiver = privateMessageList[i].receiver;
                            string message = sender + "(pm): " + privateMessageList[i].message;

                            if (IsExistInClientUsers(receiver)) //if the receiver firend is online we should send the message
                            {
                                Byte[] bfr = new Byte[64];
                                bfr = Encoding.Default.GetBytes(message);
                                GetClientSocket(receiver).Send(bfr);

                                PrivateMessageNode toBeRemoved;
                                toBeRemoved.message = privateMessageList[i].message;
                                toBeRemoved.sender = sender;
                                toBeRemoved.receiver = receiver;
                                privateMessageList.Remove(toBeRemoved);
                            }
                        }
                    }
                }

                catch
                { //IF NOT TERMINATING OLMASI DURUMUNA DA BAK!!
                    if (terminating)
                    {
                        listening = false;
                    }

                    /*else
                    {
                        log.AppendText("The socket stopped working!\n");
                    }*/
                }
            }            
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = false;
            Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach(string u in userNames)
            {
                FriendListNode fn;
                fn.clientName = u;
                fn.friendList = new List<string>();

                UsersFriendList.Add(fn);
            }
        }
    }
}
