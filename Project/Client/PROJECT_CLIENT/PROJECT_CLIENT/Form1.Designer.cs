namespace PROJECT_CLIENT
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_IP = new System.Windows.Forms.TextBox();
            this.txt_Port = new System.Windows.Forms.TextBox();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.txt_Message = new System.Windows.Forms.TextBox();
            this.log = new System.Windows.Forms.RichTextBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.btn_Send = new System.Windows.Forms.Button();
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.groupbox1 = new System.Windows.Forms.GroupBox();
            this.combobox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_SendRequest = new System.Windows.Forms.Button();
            this.rBtn_Accept = new System.Windows.Forms.RadioButton();
            this.rBtn_Reject = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_SendResponse = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_RequestLine = new System.Windows.Forms.ComboBox();
            this.btn_ShowFriends = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_CurrentFriends = new System.Windows.Forms.ComboBox();
            this.btn_RemoveFriend = new System.Windows.Forms.Button();
            this.btn_SendToFriends = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupbox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Message";
            // 
            // txt_IP
            // 
            this.txt_IP.Location = new System.Drawing.Point(103, 40);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(190, 22);
            this.txt_IP.TabIndex = 4;
            // 
            // txt_Port
            // 
            this.txt_Port.Location = new System.Drawing.Point(103, 73);
            this.txt_Port.Name = "txt_Port";
            this.txt_Port.Size = new System.Drawing.Size(190, 22);
            this.txt_Port.TabIndex = 5;
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(103, 110);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(190, 22);
            this.txt_Name.TabIndex = 6;
            // 
            // txt_Message
            // 
            this.txt_Message.Enabled = false;
            this.txt_Message.Location = new System.Drawing.Point(138, 279);
            this.txt_Message.Name = "txt_Message";
            this.txt_Message.Size = new System.Drawing.Size(205, 22);
            this.txt_Message.TabIndex = 7;
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(357, 50);
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.Size = new System.Drawing.Size(333, 310);
            this.log.TabIndex = 8;
            this.log.Text = "";
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(103, 152);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 23);
            this.btn_Connect.TabIndex = 9;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_Send
            // 
            this.btn_Send.Enabled = false;
            this.btn_Send.Location = new System.Drawing.Point(138, 322);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 23);
            this.btn_Send.TabIndex = 10;
            this.btn_Send.Text = "Send";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // btn_Disconnect
            // 
            this.btn_Disconnect.Enabled = false;
            this.btn_Disconnect.Location = new System.Drawing.Point(198, 151);
            this.btn_Disconnect.Name = "btn_Disconnect";
            this.btn_Disconnect.Size = new System.Drawing.Size(94, 23);
            this.btn_Disconnect.TabIndex = 11;
            this.btn_Disconnect.Text = "Disconnect";
            this.btn_Disconnect.UseVisualStyleBackColor = true;
            this.btn_Disconnect.Click += new System.EventHandler(this.btn_Disconnect_Click);
            // 
            // groupbox1
            // 
            this.groupbox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupbox1.Controls.Add(this.btn_Disconnect);
            this.groupbox1.Controls.Add(this.btn_Connect);
            this.groupbox1.Controls.Add(this.txt_Name);
            this.groupbox1.Controls.Add(this.txt_Port);
            this.groupbox1.Controls.Add(this.txt_IP);
            this.groupbox1.Controls.Add(this.label3);
            this.groupbox1.Controls.Add(this.label2);
            this.groupbox1.Controls.Add(this.label1);
            this.groupbox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupbox1.Location = new System.Drawing.Point(36, 40);
            this.groupbox1.Name = "groupbox1";
            this.groupbox1.Size = new System.Drawing.Size(307, 191);
            this.groupbox1.TabIndex = 12;
            this.groupbox1.TabStop = false;
            this.groupbox1.Text = "LOGIN:";
            // 
            // combobox1
            // 
            this.combobox1.FormattingEnabled = true;
            this.combobox1.Location = new System.Drawing.Point(170, 47);
            this.combobox1.Name = "combobox1";
            this.combobox1.Size = new System.Drawing.Size(199, 24);
            this.combobox1.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Invite Friend:";
            // 
            // btn_SendRequest
            // 
            this.btn_SendRequest.Enabled = false;
            this.btn_SendRequest.Location = new System.Drawing.Point(378, 43);
            this.btn_SendRequest.Name = "btn_SendRequest";
            this.btn_SendRequest.Size = new System.Drawing.Size(125, 31);
            this.btn_SendRequest.TabIndex = 15;
            this.btn_SendRequest.Text = "Send Request";
            this.btn_SendRequest.UseVisualStyleBackColor = true;
            this.btn_SendRequest.Click += new System.EventHandler(this.btn_SendRequest_Click);
            // 
            // rBtn_Accept
            // 
            this.rBtn_Accept.AutoSize = true;
            this.rBtn_Accept.Location = new System.Drawing.Point(3, 3);
            this.rBtn_Accept.Name = "rBtn_Accept";
            this.rBtn_Accept.Size = new System.Drawing.Size(72, 21);
            this.rBtn_Accept.TabIndex = 17;
            this.rBtn_Accept.TabStop = true;
            this.rBtn_Accept.Text = "Accept";
            this.rBtn_Accept.UseVisualStyleBackColor = true;
            // 
            // rBtn_Reject
            // 
            this.rBtn_Reject.AutoSize = true;
            this.rBtn_Reject.Location = new System.Drawing.Point(3, 42);
            this.rBtn_Reject.Name = "rBtn_Reject";
            this.rBtn_Reject.Size = new System.Drawing.Size(69, 21);
            this.rBtn_Reject.TabIndex = 18;
            this.rBtn_Reject.TabStop = true;
            this.rBtn_Reject.Text = "Reject";
            this.rBtn_Reject.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rBtn_Accept);
            this.panel1.Controls.Add(this.rBtn_Reject);
            this.panel1.Location = new System.Drawing.Point(375, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(93, 66);
            this.panel1.TabIndex = 19;
            // 
            // btn_SendResponse
            // 
            this.btn_SendResponse.Enabled = false;
            this.btn_SendResponse.Location = new System.Drawing.Point(483, 106);
            this.btn_SendResponse.Name = "btn_SendResponse";
            this.btn_SendResponse.Size = new System.Drawing.Size(135, 33);
            this.btn_SendResponse.TabIndex = 20;
            this.btn_SendResponse.Text = "Send Response";
            this.btn_SendResponse.UseVisualStyleBackColor = true;
            this.btn_SendResponse.Click += new System.EventHandler(this.btn_SendResponse_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cb_RequestLine);
            this.groupBox2.Controls.Add(this.btn_SendResponse);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.btn_SendRequest);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.combobox1);
            this.groupBox2.Location = new System.Drawing.Point(28, 431);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(631, 175);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Friend Request";
            // 
            // cb_RequestLine
            // 
            this.cb_RequestLine.FormattingEnabled = true;
            this.cb_RequestLine.Location = new System.Drawing.Point(8, 113);
            this.cb_RequestLine.Name = "cb_RequestLine";
            this.cb_RequestLine.Size = new System.Drawing.Size(361, 24);
            this.cb_RequestLine.TabIndex = 21;
            // 
            // btn_ShowFriends
            // 
            this.btn_ShowFriends.Enabled = false;
            this.btn_ShowFriends.Location = new System.Drawing.Point(28, 641);
            this.btn_ShowFriends.Name = "btn_ShowFriends";
            this.btn_ShowFriends.Size = new System.Drawing.Size(133, 35);
            this.btn_ShowFriends.TabIndex = 22;
            this.btn_ShowFriends.Text = "Show My Friends";
            this.btn_ShowFriends.UseVisualStyleBackColor = true;
            this.btn_ShowFriends.Click += new System.EventHandler(this.btn_ShowFriends_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(179, 622);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 17);
            this.label6.TabIndex = 23;
            this.label6.Text = "Current Friends:";
            // 
            // cb_CurrentFriends
            // 
            this.cb_CurrentFriends.FormattingEnabled = true;
            this.cb_CurrentFriends.Location = new System.Drawing.Point(181, 647);
            this.cb_CurrentFriends.Name = "cb_CurrentFriends";
            this.cb_CurrentFriends.Size = new System.Drawing.Size(216, 24);
            this.cb_CurrentFriends.TabIndex = 24;
            // 
            // btn_RemoveFriend
            // 
            this.btn_RemoveFriend.Enabled = false;
            this.btn_RemoveFriend.Location = new System.Drawing.Point(421, 639);
            this.btn_RemoveFriend.Name = "btn_RemoveFriend";
            this.btn_RemoveFriend.Size = new System.Drawing.Size(136, 31);
            this.btn_RemoveFriend.TabIndex = 25;
            this.btn_RemoveFriend.Text = "Remove Friend";
            this.btn_RemoveFriend.UseVisualStyleBackColor = true;
            this.btn_RemoveFriend.Click += new System.EventHandler(this.btn_RemoveFriend_Click);
            // 
            // btn_SendToFriends
            // 
            this.btn_SendToFriends.Enabled = false;
            this.btn_SendToFriends.Location = new System.Drawing.Point(219, 322);
            this.btn_SendToFriends.Name = "btn_SendToFriends";
            this.btn_SendToFriends.Size = new System.Drawing.Size(124, 23);
            this.btn_SendToFriends.TabIndex = 26;
            this.btn_SendToFriends.Text = "Send to Friends";
            this.btn_SendToFriends.UseVisualStyleBackColor = true;
            this.btn_SendToFriends.Click += new System.EventHandler(this.btn_SendToFriends_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 697);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(467, 17);
            this.label7.TabIndex = 27;
            this.label7.Text = "To update your Current Friends, please click on Show My Friends button.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 727);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_SendToFriends);
            this.Controls.Add(this.btn_RemoveFriend);
            this.Controls.Add(this.cb_CurrentFriends);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_ShowFriends);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupbox1);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.log);
            this.Controls.Add(this.txt_Message);
            this.Controls.Add(this.label4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupbox1.ResumeLayout(false);
            this.groupbox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.TextBox txt_Port;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.TextBox txt_Message;
        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.GroupBox groupbox1;
        private System.Windows.Forms.ComboBox combobox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_SendRequest;
        private System.Windows.Forms.RadioButton rBtn_Accept;
        private System.Windows.Forms.RadioButton rBtn_Reject;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_SendResponse;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_ShowFriends;
        private System.Windows.Forms.ComboBox cb_RequestLine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_CurrentFriends;
        private System.Windows.Forms.Button btn_RemoveFriend;
        private System.Windows.Forms.Button btn_SendToFriends;
        private System.Windows.Forms.Label label7;
    }
}

