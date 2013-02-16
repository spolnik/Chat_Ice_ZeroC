using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Chat.Properties;
using Ice;
using Exception = System.Exception;

namespace Chat.Engine
{
    internal class ChatClient : IChatClient
    {
        private readonly ToolStripStatusLabel _messageStatus;
        private readonly TextBox _outputTxtBx;
        private Dictionary<string, string> _context;

        private LobbyPrx _lobby;
        private RoomAccess _roomAccess;
        private RoomVersion _roomVersion;

        private JobQueue _jobQueue;

        public ChatClient(string[] args, TextBox output, ToolStripStatusLabel messageToolStripSatusLbl)
        {
            this._outputTxtBx = output;
            this._messageStatus = messageToolStripSatusLbl;
            this._roomVersion = RoomVersion.Base;
            this.Initialize(args);
        }

        internal void ChangeRoomVersion(RoomVersion roomVersion)
        {
            this._roomVersion = roomVersion;
        }

        #region IChatClient Members

        public Communicator Communicator { get; private set; }

        public void Join(string name, string topic, RoomVersion roomVersion)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(Resources.Client_Join_Cannot_be_empty_or_null, "name");

            if (string.IsNullOrEmpty(topic))
                throw new ArgumentException(Resources.Client_Join_Cannot_be_empty_or_null, "topic");

            if (this._lobby == null)
                throw new ApplicationException("Lobby is null");

            ObjectAdapter adapter = this.Communicator.createObjectAdapter(string.Empty);
            this._jobQueue = new JobQueue();
            Listener listener = new ListenerI(this._outputTxtBx, this._jobQueue);
            ObjectPrx listenerPrx = adapter.addWithUUID(listener);
            this._jobQueue.Start();
            adapter.activate();
            this._lobby.ice_getConnection().setAdapter(adapter);

            this._roomAccess = this._lobby.Join(name, topic, listenerPrx.ice_getIdentity());
            this._context = new Dictionary<string, string> { { SESSIONCTXPROP.value, topic }, {AUTHORCTXPROP.value, name} };
        }

        public void Say(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException(Resources.Client_Join_Cannot_be_empty_or_null, "message");

            IMessageCallback messageCallback = new MessageCallback(this._messageStatus);

            switch (this._roomVersion)
            {
                case RoomVersion.Base:
                    RoomPrx room = RoomPrxHelper.checkedCast(this._roomAccess.RoomProxy.ice_context(this._context));
                    room.begin_Say(message).whenCompleted(messageCallback.SayCallback, messageCallback.FailureCallback);
                    break;
                case RoomVersion.V1:
                    RoomAdmPrx roomAdm = RoomAdmPrxHelper.checkedCast(this._roomAccess.RoomProxy.ice_context(this._context), "V1");
                    roomAdm.begin_Say(message).whenCompleted(messageCallback.SayCallback, messageCallback.FailureCallback);
                    break;
                case RoomVersion.V2:
                    RoomV2Prx roomV2 = RoomV2PrxHelper.checkedCast(this._roomAccess.RoomProxy.ice_context(this._context), "V2");
                    roomV2.begin_Say(message, DateTime.Now.Ticks).whenCompleted(messageCallback.SayCallback, messageCallback.FailureCallback);
                    break;
            }
        }

        public void Leave()
        {
            try
            {
                switch (this._roomVersion)
                {
                    case RoomVersion.Base:
                        RoomPrx room = RoomPrxHelper.checkedCast(this._roomAccess.RoomProxy.ice_context(this._context));
                        room.LeaveRoom();
                        break;
                    case RoomVersion.V1:
                        RoomAdmPrx roomAdm = RoomAdmPrxHelper.checkedCast(this._roomAccess.RoomProxy.ice_context(this._context), "V1");
                        roomAdm.LeaveRoom();
                        break;
                    case RoomVersion.V2:
                        RoomV2Prx roomV2 = RoomV2PrxHelper.checkedCast(this._roomAccess.RoomProxy.ice_context(this._context), "V2");
                        roomV2.LeaveRoom();
                        break;
                }
            }
            finally
            {
                this._jobQueue.Destroy();
                this._jobQueue.Join();
                this._jobQueue = null;
            }
        }

        public List<string> GetParticipants()
        {
            RoomAdmPrx roomAdm = RoomAdmPrxHelper.checkedCast(this._roomAccess.RoomProxy.ice_context(this._context), "V1");
            return roomAdm.GetParticipants();
        }

        public void Kick(string nick)
        {
            RoomAdmPrx roomAdm = RoomAdmPrxHelper.checkedCast(this._roomAccess.RoomProxy.ice_context(this._context), "V1");
            roomAdm.Kick(nick);
        }

        #endregion

        private void Initialize(string[] args)
        {
            try
            {
                InitializationData initializationData = new InitializationData
                                                            {properties = Util.createProperties()};
                initializationData.properties.setProperty("Ice.ThreadPool.Client.Size", "1");
                initializationData.properties.setProperty("Ice.ThreadPool.Client.SizeMax", "10");
                initializationData.properties.setProperty("Ice.ThreadPool.Server.Size", "1");
                initializationData.properties.setProperty("Ice.ThreadPool.Server.SizeMax", "10");
                initializationData.properties.setProperty("Ice.ACM.Client", "0");

                this.Communicator = Util.initialize(ref args, initializationData);
                ObjectPrx obj = this.Communicator.stringToProxy("ChatLobby:default -p 12321");
                this._lobby = LobbyPrxHelper.checkedCast(obj);
                if (this._lobby == null)
                    throw new ApplicationException("Invalid proxy");
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("ERROR: Cannot connect with a server"), Resources.ChatForm_Chat_Exception, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Environment.Exit(1);
            }
        }
    }
}