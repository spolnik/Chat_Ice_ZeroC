using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Chat.Engine;
using Chat.Properties;

namespace Chat
{
    enum ChatStatus { Connected, Disconnected }

    public partial class ChatForm : Form
    {
        private readonly IChatClient _chatClient;
        private ChatStatus _chatStatus;

        public ChatForm(string[] args)
        {
            InitializeComponent();
            this.versionCmbBx.DataSource = Enum.GetValues(typeof (RoomVersion));
            this._chatClient = new ChatClient(args, this.outputTxtBx, this.messageToolStripSatusLbl);
            this._chatStatus = ChatStatus.Disconnected;
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                if (this._chatClient.Communicator != null)
                {
                    if (this._chatStatus == ChatStatus.Connected)
                        this.LeaveRoom();
                    this._chatClient.Communicator.destroy();
                }
            }
            catch (Ice.Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void joinLeaveBtn_Click(object sender, EventArgs e)
        {
            switch (this._chatStatus)
            {
                case ChatStatus.Disconnected:
                    this.JoinToRoom();
                    break;
                case ChatStatus.Connected:
                    this.LeaveRoom();
                    break;
            }
        }

        private void LeaveRoom()
        {
            try
            {
                this._chatClient.Leave();
            }
            catch (IllegalChatSessionException illegalChatSessionException)
            {
                MessageBox.Show(
                    string.Concat("Reason: ", illegalChatSessionException.Reason, Environment.NewLine, "SessionId: ",
                                  illegalChatSessionException.SessionId), Resources.ChatForm_Chat_Exception);
            }
            catch (Ice.Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.ChatForm_Chat_Exception);
            }

            this.joinLeaveBtn.Text = Resources.ChatForm_LeaveRoom_Join;
            this._chatStatus = ChatStatus.Disconnected;
            this.toolStripStatusLbl.Text = ChatStatus.Disconnected.ToString();
            this.nickTxtBx.Enabled = true;
            this.topicTxtBx.Enabled = true;
            this.messageTxtBx.Text = string.Empty;
            this.messageTxtBx.Enabled = false;
            this.sendBtn.Enabled = false;

            this.DisableRoomAdmToolbar();
        }

        private void DisableRoomAdmToolbar()
        {
            this.kickUserLbl.Enabled = false;
            this.kickUserTxtBx.Text = string.Empty;
            this.kickUserTxtBx.Enabled = false;
            this.kickUserBtn.Enabled = false;
            this.showParticipantsBtn.Enabled = false;
        }

        private void JoinToRoom()
        {
            if (string.IsNullOrEmpty(this.nickTxtBx.Text))
            {
                MessageBox.Show(Resources.ChatForm_joinLeaveBtn_Click_Nick_cannot_be_empty,
                                Resources.ChatForm_Chatroom);
                return;
            }

            if (string.IsNullOrEmpty(this.topicTxtBx.Text))
            {
                MessageBox.Show(Resources.ChatForm_joinLeaveBtn_Click_Topic_cannot_be_empty,
                                Resources.ChatForm_Chatroom);
                return;
            }

            try
            {
                this._chatClient.Join(this.nickTxtBx.Text, this.topicTxtBx.Text, this.versionCmbBx.Text.ConvertTo<RoomVersion>());
            }
            catch (IChatException chatException)
            {
                MessageBox.Show(chatException.Reason, Resources.ChatForm_Chat_Exception);
            }
            catch (Ice.Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.ChatForm_Chat_Exception);
                return;
            }

            this.joinLeaveBtn.Text = Resources.ChatForm_joinLeaveBtn_Click_Leave;
            this._chatStatus = ChatStatus.Connected;
            this.toolStripStatusLbl.Text = ChatStatus.Connected.ToString();
            this.nickTxtBx.Enabled = false;
            this.topicTxtBx.Enabled = false;
            this.messageTxtBx.Enabled = true;
            this.sendBtn.Enabled = true;

            switch (this.versionCmbBx.Text.ConvertTo<RoomVersion>())
            {
                case RoomVersion.V1:
                    this.EnableRoomAdmToolbar();
                    break;
                default:
                    this.DisableRoomAdmToolbar();
                    break;
            }
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this._chatClient.Say(this.messageTxtBx.Text);
                this.messageTxtBx.Text = string.Empty;
            }
            catch (IllegalChatSessionException illegalChatSessionException)
            {
                MessageBox.Show(
                    string.Format("Illegal chat session\nSession id: {0}, reason: {1}",
                                  illegalChatSessionException.SessionId, illegalChatSessionException.Reason),
                    Resources.ChatForm_Chat_Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), Resources.ChatForm_Chat_Exception,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void versionCmbBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            RoomVersion roomVersion = this.versionCmbBx.Text.ConvertTo<RoomVersion>();
            ((ChatClient)this._chatClient).ChangeRoomVersion(roomVersion);

            if (this._chatStatus == ChatStatus.Disconnected)
                return;

            switch (roomVersion)
            {
                case RoomVersion.V1:
                    this.EnableRoomAdmToolbar();
                    break;
                default:
                    this.DisableRoomAdmToolbar();
                    break;
            }
        }

        private void EnableRoomAdmToolbar()
        {
            this.kickUserLbl.Enabled = true;
            this.kickUserTxtBx.Enabled = true;
            this.kickUserBtn.Enabled = true;
            this.showParticipantsBtn.Enabled = true;
        }

        private void showParticipantsBtn_Click(object sender, EventArgs e)
        {
            List<string> participants = this._chatClient.GetParticipants();

            StringBuilder output = new StringBuilder();

            foreach (string participant in participants)
                output.AppendLine(participant);

            MessageBox.Show(output.ToString(),
                            Resources.ChatForm_showParticipantsBtn_Click_Chatroom_participants,
                            MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void kickUserBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string nick = this.kickUserTxtBx.Text;
                this._chatClient.Kick(nick);
                MessageBox.Show(string.Concat("User is kicked off: ", nick), Resources.ChatForm_Chatroom, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (IChatException chatException)
            {
                MessageBox.Show(chatException.Reason, Resources.ChatForm_Chat_Exception, MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ChatForm_Chat_Exception, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            this.kickUserTxtBx.Text = string.Empty;
        }
    }
}
