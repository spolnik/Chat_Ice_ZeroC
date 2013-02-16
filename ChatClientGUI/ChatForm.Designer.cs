namespace Chat
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.joinLeaveBtn = new System.Windows.Forms.Button();
            this.nickTxtBx = new System.Windows.Forms.TextBox();
            this.topicTxtBx = new System.Windows.Forms.TextBox();
            this.nickLbl = new System.Windows.Forms.Label();
            this.topicLbl = new System.Windows.Forms.Label();
            this.messageTxtBx = new System.Windows.Forms.TextBox();
            this.messageLbl = new System.Windows.Forms.Label();
            this.outputTxtBx = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.messageToolStripSatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.sendBtn = new System.Windows.Forms.Button();
            this.versionCmbBx = new System.Windows.Forms.ComboBox();
            this.kickUserLbl = new System.Windows.Forms.Label();
            this.kickUserTxtBx = new System.Windows.Forms.TextBox();
            this.showParticipantsBtn = new System.Windows.Forms.Button();
            this.kickUserBtn = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // joinLeaveBtn
            // 
            this.joinLeaveBtn.Location = new System.Drawing.Point(399, 6);
            this.joinLeaveBtn.Name = "joinLeaveBtn";
            this.joinLeaveBtn.Size = new System.Drawing.Size(75, 23);
            this.joinLeaveBtn.TabIndex = 3;
            this.joinLeaveBtn.Text = "Join";
            this.joinLeaveBtn.UseVisualStyleBackColor = true;
            this.joinLeaveBtn.Click += new System.EventHandler(this.joinLeaveBtn_Click);
            // 
            // nickTxtBx
            // 
            this.nickTxtBx.Location = new System.Drawing.Point(49, 8);
            this.nickTxtBx.Name = "nickTxtBx";
            this.nickTxtBx.Size = new System.Drawing.Size(100, 20);
            this.nickTxtBx.TabIndex = 0;
            // 
            // topicTxtBx
            // 
            this.topicTxtBx.Location = new System.Drawing.Point(195, 8);
            this.topicTxtBx.Name = "topicTxtBx";
            this.topicTxtBx.Size = new System.Drawing.Size(100, 20);
            this.topicTxtBx.TabIndex = 1;
            // 
            // nickLbl
            // 
            this.nickLbl.AutoSize = true;
            this.nickLbl.Location = new System.Drawing.Point(14, 11);
            this.nickLbl.Name = "nickLbl";
            this.nickLbl.Size = new System.Drawing.Size(29, 13);
            this.nickLbl.TabIndex = 10;
            this.nickLbl.Text = "Nick";
            // 
            // topicLbl
            // 
            this.topicLbl.AutoSize = true;
            this.topicLbl.Location = new System.Drawing.Point(155, 11);
            this.topicLbl.Name = "topicLbl";
            this.topicLbl.Size = new System.Drawing.Size(34, 13);
            this.topicLbl.TabIndex = 11;
            this.topicLbl.Text = "Topic";
            // 
            // messageTxtBx
            // 
            this.messageTxtBx.Enabled = false;
            this.messageTxtBx.Location = new System.Drawing.Point(70, 191);
            this.messageTxtBx.Name = "messageTxtBx";
            this.messageTxtBx.Size = new System.Drawing.Size(306, 20);
            this.messageTxtBx.TabIndex = 5;
            // 
            // messageLbl
            // 
            this.messageLbl.AutoSize = true;
            this.messageLbl.Location = new System.Drawing.Point(14, 194);
            this.messageLbl.Name = "messageLbl";
            this.messageLbl.Size = new System.Drawing.Size(50, 13);
            this.messageLbl.TabIndex = 12;
            this.messageLbl.Text = "Message";
            // 
            // outputTxtBx
            // 
            this.outputTxtBx.Location = new System.Drawing.Point(17, 35);
            this.outputTxtBx.MaxLength = 512000;
            this.outputTxtBx.Multiline = true;
            this.outputTxtBx.Name = "outputTxtBx";
            this.outputTxtBx.ReadOnly = true;
            this.outputTxtBx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTxtBx.Size = new System.Drawing.Size(457, 150);
            this.outputTxtBx.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLbl,
            this.messageToolStripSatusLbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 244);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(486, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLbl
            // 
            this.toolStripStatusLbl.Name = "toolStripStatusLbl";
            this.toolStripStatusLbl.Size = new System.Drawing.Size(79, 17);
            this.toolStripStatusLbl.Text = "Disconnected";
            // 
            // messageToolStripSatusLbl
            // 
            this.messageToolStripSatusLbl.Name = "messageToolStripSatusLbl";
            this.messageToolStripSatusLbl.Size = new System.Drawing.Size(0, 17);
            // 
            // sendBtn
            // 
            this.sendBtn.Enabled = false;
            this.sendBtn.Location = new System.Drawing.Point(399, 189);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(75, 23);
            this.sendBtn.TabIndex = 6;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // versionCmbBx
            // 
            this.versionCmbBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.versionCmbBx.FormattingEnabled = true;
            this.versionCmbBx.Location = new System.Drawing.Point(302, 8);
            this.versionCmbBx.Name = "versionCmbBx";
            this.versionCmbBx.Size = new System.Drawing.Size(91, 21);
            this.versionCmbBx.TabIndex = 2;
            this.versionCmbBx.SelectedIndexChanged += new System.EventHandler(this.versionCmbBx_SelectedIndexChanged);
            // 
            // kickUserLbl
            // 
            this.kickUserLbl.AutoSize = true;
            this.kickUserLbl.Enabled = false;
            this.kickUserLbl.Location = new System.Drawing.Point(14, 221);
            this.kickUserLbl.Name = "kickUserLbl";
            this.kickUserLbl.Size = new System.Drawing.Size(51, 13);
            this.kickUserLbl.TabIndex = 13;
            this.kickUserLbl.Text = "Kick user";
            // 
            // kickUserTxtBx
            // 
            this.kickUserTxtBx.Enabled = false;
            this.kickUserTxtBx.Location = new System.Drawing.Point(70, 218);
            this.kickUserTxtBx.Name = "kickUserTxtBx";
            this.kickUserTxtBx.Size = new System.Drawing.Size(119, 20);
            this.kickUserTxtBx.TabIndex = 7;
            // 
            // showParticipantsBtn
            // 
            this.showParticipantsBtn.Enabled = false;
            this.showParticipantsBtn.Location = new System.Drawing.Point(302, 216);
            this.showParticipantsBtn.Name = "showParticipantsBtn";
            this.showParticipantsBtn.Size = new System.Drawing.Size(172, 23);
            this.showParticipantsBtn.TabIndex = 9;
            this.showParticipantsBtn.Text = "Show room participants";
            this.showParticipantsBtn.UseVisualStyleBackColor = true;
            this.showParticipantsBtn.Click += new System.EventHandler(this.showParticipantsBtn_Click);
            // 
            // kickUserBtn
            // 
            this.kickUserBtn.Enabled = false;
            this.kickUserBtn.Location = new System.Drawing.Point(195, 216);
            this.kickUserBtn.Name = "kickUserBtn";
            this.kickUserBtn.Size = new System.Drawing.Size(100, 23);
            this.kickUserBtn.TabIndex = 8;
            this.kickUserBtn.Text = "Kick";
            this.kickUserBtn.UseVisualStyleBackColor = true;
            this.kickUserBtn.Click += new System.EventHandler(this.kickUserBtn_Click);
            // 
            // ChatForm
            // 
            this.AcceptButton = this.sendBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 266);
            this.Controls.Add(this.kickUserBtn);
            this.Controls.Add(this.showParticipantsBtn);
            this.Controls.Add(this.kickUserTxtBx);
            this.Controls.Add(this.kickUserLbl);
            this.Controls.Add(this.versionCmbBx);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.outputTxtBx);
            this.Controls.Add(this.messageLbl);
            this.Controls.Add(this.messageTxtBx);
            this.Controls.Add(this.topicLbl);
            this.Controls.Add(this.nickLbl);
            this.Controls.Add(this.topicTxtBx);
            this.Controls.Add(this.nickTxtBx);
            this.Controls.Add(this.joinLeaveBtn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ChatForm";
            this.Text = "Chatroom";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button joinLeaveBtn;
        private System.Windows.Forms.TextBox nickTxtBx;
        private System.Windows.Forms.TextBox topicTxtBx;
        private System.Windows.Forms.Label nickLbl;
        private System.Windows.Forms.Label topicLbl;
        private System.Windows.Forms.TextBox messageTxtBx;
        private System.Windows.Forms.Label messageLbl;
        private System.Windows.Forms.TextBox outputTxtBx;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLbl;
        private System.Windows.Forms.ToolStripStatusLabel messageToolStripSatusLbl;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.ComboBox versionCmbBx;
        private System.Windows.Forms.Label kickUserLbl;
        private System.Windows.Forms.TextBox kickUserTxtBx;
        private System.Windows.Forms.Button showParticipantsBtn;
        private System.Windows.Forms.Button kickUserBtn;
    }
}

