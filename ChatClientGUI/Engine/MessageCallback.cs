using System;
using System.Windows.Forms;
using Chat.Properties;
using Exception = System.Exception;

namespace Chat.Engine
{
    public class MessageCallback : IMessageCallback
    {
        private readonly ToolStripStatusLabel _output;

        public MessageCallback(ToolStripStatusLabel output)
        {
            this._output = output;
        }

        #region IMessageCallback Members

        public void SayCallback()
        {
            this._output.Text = string.Concat(Resources.MessageCallback_SayCallback_Message_sended, " - ", DateTime.Now.ToLongTimeString());
        }

        public void FailureCallback(Exception ex)
        {
            if (ex is IllegalChatSessionException)
                MessageBox.Show(string.Format("Message not sended.\r\nReason: {0}, sessionId: {1}",
                                              ((IllegalChatSessionException) ex).Reason,
                                              ((IllegalChatSessionException) ex).SessionId));
            else if (ex is IChatException)
                MessageBox.Show(string.Format("Message not sended.\r\nReason: {0}",
                                              ((IChatException) ex).Reason));
            else
                MessageBox.Show(string.Format("Message not sended.\r\nReason: {0}",
                                              ex.Message));
        }

        #endregion
    }
}